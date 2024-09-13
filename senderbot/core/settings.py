from environs import Env
from dataclasses import dataclass


@dataclass
class Bots:
    bot_token: str
    admin_id: str
    channel: int
    bookmarks_chat: str


@dataclass
class Db:
    db_host: str
    db_database: str
    db_user: str
    db_password: str


@dataclass
class Settings:
    bots: Bots
    db: Db


def get_settings(path: str):
    env = Env()
    env.read_env(path)

    return Settings(
        bots=Bots(
            bot_token=env.str("TOKEN"),
            admin_id=env.int("ADMIN_ID"),
            channel=env.int("MY_CHANNEL"),
            bookmarks_chat=env.int("MY_CHAT_BOOKMARKS")
        ),
        db=Db(
            db_host=env.str("DB_HOST"),
            db_database=env.str("DB_DATABASE"),
            db_user=env.str("DB_USER"),
            db_password=env.str("DB_PASSWORD")
        )
    )


settings = get_settings('input')
