from aiogram.types import Message, CallbackQuery, InlineKeyboardMarkup, InlineKeyboardButton
from aiogram import Bot
from aiogram.filters import CommandObject
from aiogram.fsm.context import FSMContext

from core.utils.dbconnect import Request
from core.utils.sender_list import SenderList
from core.utils.sender_state import Steps
from core.keyboards.inline import get_confirm_button_keyboard


async def get_sender(message: Message, command: CommandObject, state: FSMContext):
    if not command.args:
        await message.answer(f'Для создания кампании для рассылки введите команду /sender и имя рассылки')
        return

    await message.answer(f'Приступаем создавать кампанию для рассылки. Имя кампании - {command.args}\r\n\r\n'
                         f'Отправь мне сообщение которое будет использовано как рекламное')

    await state.update_data(name_camp=command.args)
    await state.set_state(Steps.get_message)


async def get_message(message: Message, bot: Bot, state: FSMContext):
    await message.answer(f'Ок, я запомнил сообщение, которое ты хочешь разослать.\r\n'
                         f'Будем добавлять рекламную кнопку?', reply_markup=get_confirm_button_keyboard())
    await state.update_data(message_id=message.message_id, chat_id=message.from_user.id)
    await state.set_state(Steps.q_button)


async def q_button(call: CallbackQuery, bot: Bot, state: FSMContext):
    if call.data == 'add_button':
        await call.message.answer(f'Отправь текст для кнопки.', reply_markup=None)
        await state.set_state(Steps.get_text_button)
    elif call.data == 'no_button':
        await call.message.edit_reply_markup(reply_markup=None)
        data = await state.get_data()
        message_id = int(data.get('message_id'))
        chat_id = int(data.get('chat_id'))
        await confirm(call.message, bot, message_id, chat_id)
        await state.set_state(Steps.get_url_button)

    await call.answer()


async def get_text_button(message: Message, bot: Bot, state: FSMContext):
    await state.update_data(text_button=message.text)
    await message.answer(f'Теперь отправь ссылку')
    await state.set_state(Steps.get_url_button)


async def get_url_button(message: Message, bot: Bot, state: FSMContext):
    await state.update_data(url_button=message.text)
    added_keyboards = InlineKeyboardMarkup(inline_keyboard=[
        [
            InlineKeyboardButton(
                text=(await state.get_data()).get('text_button'),
                url=f'{message.text}'
            )
        ]
    ])
    data = await state.get_data()
    message_id = int(data.get('message_id'))
    chat_id = int(data.get('chat_id'))
    await confirm(message, bot, message_id, chat_id, added_keyboards)


async def confirm(message: Message, bot: Bot, message_id: int, chat_id: int, reply_markup: InlineKeyboardMarkup = None):
    await bot.copy_message(chat_id, chat_id, message_id, reply_markup=reply_markup)
    await message.answer(f'Вот сообщение, которое будет отправлено. Подтвердите.',
                         reply_markup=InlineKeyboardMarkup(inline_keyboard=[
                             [
                                 InlineKeyboardButton(
                                     text='Подтвердить',
                                     callback_data='confirm_sender'
                                 )
                             ],
                             [
                                 InlineKeyboardButton(
                                     text='Отменить',
                                     callback_data='cancel_sender'
                                 )
                             ]
                         ]))


async def sender_decide(call: CallbackQuery, bot: Bot, state: FSMContext, request: Request, senderlist: SenderList):
    data = await state.get_data()
    message_id = data.get('message_id')
    chat_id = data.get('chat_id')
    text_button = data.get('text_button')
    url_button = data.get('url_button')
    name_camp = data.get('name_camp')

    if call.data == 'confirm_sender':
        await call.message.edit_text(f'Начинаю рассылку', reply_markup=None)

        if not await request.check_table(name_camp):
            await request.create_table(name_camp)
        count = await senderlist.broadcaster(name_camp, chat_id, message_id, text_button, url_button)

        await call.message.answer(f'Успешно разослали рекламное сообщение [{count}] пользователям')
        await request.delete_table(name_camp)



    elif call.data == 'cancel_sender':
        await call.message.edit_text(f'Отменил рассылку', reply_markup=None)

    await state.clear()
