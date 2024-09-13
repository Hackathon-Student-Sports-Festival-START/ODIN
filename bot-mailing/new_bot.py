import telebot
import threading
import signal
import sys
from datetime import datetime
import gspread
from oauth2client.service_account import ServiceAccountCredentials
import time

# Подключение к Google Sheets
scope = ['https://spreadsheets.google.com/feeds',
         'https://www.googleapis.com/auth/drive']
creds = ServiceAccountCredentials.from_json_keyfile_name('creds.json', scope) # как создать creds можно тут посмотреть https://habr.com/ru/articles/483302/
client = gspread.authorize(creds)
sheet = client.open('Messages').get_worksheet(0)  # Название вашего Google Sheets

thread_running = True
usersheet = client.open('Messages').get_worksheet(1)

# Ваш токен для доступа к API бота Telegram
token = ''
bot = telebot.TeleBot(token)

# Обработчик выхода
def signal_handler(sig, frame):
    global thread_running
    print('You pressed Ctrl+C! Hold on')
    thread_running = False
    sys.exit(0)
signal.signal(signal.SIGINT, signal_handler)

def get_userlist():
    return client.open('Messages').get_worksheet(1).col_values(1)[1:]

def add_new_user(user_id):
    userlist = get_userlist()
    if not str(user_id) in userlist:
        usersheet.append_row([user_id])

def update_scheduled_time():
    global thread_running

    while thread_running:
        userlist = get_userlist()
        scheduled_time = sheet.col_values(2) # Получение времени из Google Sheets
        scheduled_messages = sheet.col_values(1)
        scheduled_photos = sheet.col_values(4)
        # print(scheduled_time, datetime.now().timestamp())
        for el in scheduled_time[1:]:
            # print(datetime.strptime(el, "%d.%m.%Y %H:%M:%S").timestamp())
            delta = datetime.now().timestamp() - datetime.strptime(el, "%d.%m.%Y %H:%M:%S").timestamp()
            print(delta)
            if 60 >= delta >= 0:
                for user in userlist:
                    bot.send_message(int(user), scheduled_messages[scheduled_time.index(el)])
                    photo_url = scheduled_photos[scheduled_time.index(el)]
                    bot.send_photo(int(user), photo_url)
        time.sleep(60)  # Обновление таблицы каждую минуту

@bot.message_handler(commands=['start'])
def handle_start(message):
    bot.send_message(message.chat.id, "Привет! Я бот для отправки отложенных сообщений.")
    add_new_user(message.chat.id)

@bot.message_handler(commands=['help'])
def handle_start(message):
    bot.send_message(message.chat.id, "Я работаю нормально. Вы скоро получите уведомление о событии")

# Обработчик всех входящих сообщений
@bot.message_handler(func=lambda message: True)
def handle_all_messages(message):
    bot.send_message(message.chat.id, "Я получил ваше сообщение: " + message.text)

# Запуск бота и обновление времени отложенного сообщения
update_thread = threading.Thread(target=update_scheduled_time)
update_thread.start()
bot.polling(none_stop=True)
update_thread.join()

