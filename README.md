# Решение кейса хакатона старт

- Основной проект: /srs/StartOdin
- Бот рассылки новостей: /bot-mailing

К открытию предпочтительна pptx версия презентации

## Сборка Backend сервера

ВНИМАНИЕ: Сервер не запущен на конкретном домене

ВНИМАНИЕ: Сервер использует базу данных MySQL с пользовательскими настройсками:

- UserName => api
- Password => EjbFmbFebcKH
- Database => start

База данных не создается локально, доступ временно открыт для упрощения разработки.

Для сборки проекта необходимо выполнить следующие шаги:

1. Клонировать репозиторий с проектом с помощью команды:
   ```
   git clone git@github.com:Hackathon-Student-Sports-Festival-START/ODIN.git
   ```

Для запуска необходимо установить Dotnet SDK 8-ой версии. Вы этом можете сделать по данноый ссылке:
https://dotnet.microsoft.com/en-us/download/dotnet/8.0


2. Установить DotNet SDK , выполнив команду:
   ```
   sudo apt-get update && \
   sudo apt-get install -y dotnet-sdk-8.0
   ```


3. Запустить сервер, введя команду, находясь в папке проекта:
   ```
   cd ./src/StartOdin/StartOdin
   dotnet run
   ```

4. После этого проект будет доступен по адресу `http://localhost:5051`.
