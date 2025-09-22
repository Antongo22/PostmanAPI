# PostmanAPI - REST API для тестирования

REST API на .NET 8 Minimal API с чистой архитектурой для тестирования через Postman.

## 🚀 Технологии

- **.NET 8** - Minimal API
- **SQLite** - База данных
- **Entity Framework Core** - ORM
- **JWT** - Аутентификация
- **Swagger** - Документация API
- **Serilog** - Логирование
- **BCrypt** - Хеширование паролей
- **Docker** - Контейнеризация

## 📋 Функциональность

### 🔐 Аутентификация
- `POST /auth/login` - Вход в систему
- `POST /auth/refresh` - Обновление токена
- `POST /auth/logout` - Выход из системы

### 👤 Пользователи
- `GET /users` - Список пользователей (с пагинацией)
- `GET /users/{id}` - Получить пользователя по ID
- `POST /users` - Создать пользователя
- `PATCH /users/{id}` - Частично обновить пользователя
- `DELETE /users/{id}` - Удалить пользователя
- `GET /users/{id}/orders` - Заказы пользователя

### 📦 Продукты
- `GET /products` - Список продуктов (с фильтрацией по имени)
- `GET /products/{id}` - Получить продукт по ID
- `POST /products` - Создать продукт
- `PATCH /products/{id}` - Частично обновить продукт
- `DELETE /products/{id}` - Удалить продукт

### 🛒 Заказы
- `POST /orders` - Создать заказ

## 🏗️ Архитектура

Проект использует чистую архитектуру с разделением на слои:

```
PostmanAPI/
├── Domain/           # Сущности и бизнес-логика
│   └── Entities/
├── Application/      # Интерфейсы сервисов и DTOs
│   ├── DTOs/
│   └── Services/
├── Infrastructure/   # Реализация сервисов и доступ к данным
│   ├── Data/
│   └── Services/
└── Endpoints/        # Конфигурация эндпоинтов
```

## 🚀 Быстрый старт

### 🐳 Запуск через Docker (рекомендуется)

1. **Собрать Docker образ**
```bash
docker build -t postmanapi .
```

2. **Запустить контейнер**
```bash
docker run -d -p 8080:8080 --name postmanapi-container postmanapi
```

3. **Открыть Swagger UI**
   - Перейти на http://localhost:8080
   - Документация API будет доступна сразу

4. **Остановить контейнер**
```bash
docker stop postmanapi-container
```

### 📬 Импорт настроек в Postman

Для удобного тестирования API в проекте есть готовые файлы для Postman:

1. **Импортировать коллекцию**
   - Откройте Postman
   - Нажмите `Import` → `Files`
   - Выберите файл `Postman Task.postman_collection.json`
   - Коллекция содержит все эндпоинты с готовыми запросами

2. **Импортировать переменные окружения**
   - В Postman нажмите на иконку шестеренки (Manage Environments)
   - Нажмите `Import` 
   - Выберите файл `Task Env.postman_environment.json`
   - Установите переменную `base_url` в значение `http://localhost:8080`

3. **Использовать тестовые данные**
   - В папке `Users` есть запрос для создания пользователя
   - Используйте данные из файла `test-data.json`:
     ```json
     {
       "name": "John Doe",
       "email": "john@example.com", 
       "password": "password123"
     }
     ```

### 💻 Локальный запуск (альтернатива)

1. **Клонировать репозиторий**
```bash
git clone <repository-url>
cd PostmanAPI
```

2. **Создать .env файл (опционально)**
```bash
cp .env.example .env
```
> Примечание: .env файл не обязателен для локального запуска, так как все настройки уже есть в appsettings.json

3. **Восстановить зависимости**
```bash
dotnet restore
```

4. **Запустить приложение**
```bash
cd PostmanAPI
dotnet run
```

5. **Открыть Swagger UI**
   - Перейти на https://localhost:5001
   - Документация API будет доступна сразу

## 🧪 Тестирование

### Тестовые данные

При первом запуске автоматически создаются тестовые пользователи:

| Email | Пароль | Имя |
|-------|--------|-----|
| ivan@example.com | password123 | Иван Иванов |
| maria@example.com | password123 | Мария Петрова |
| alex@example.com | password123 | Алексей Сидоров |

### 🔄 Пример использования API

1. **Авторизация**
```json
POST /auth/login
{
  "email": "john@example.com",
  "password": "password123"
}
```

2. **Получение токена и использование в заголовках**
```
Authorization: Bearer <your-access-token>
```

3. **Частичное обновление пользователя (PATCH)**
```json
PATCH /users/1
{
  "name": "John Updated"
}
```

4. **Фильтрация продуктов по имени**
```
GET /products?filter=name:laptop
```

5. **Создание заказа**
```json
POST /orders
{
  "userId": 1,
  "productIds": [1, 2]
}
```

## ⚙️ Конфигурация

### Переменные окружения

Скопируйте `.env.example` в `.env` и настройте:

```env
ConnectionStrings__DefaultConnection=Data Source=postmanapi.db
Jwt__Key=SuperSecretKeyForJWTTokenGeneration123456789
Jwt__Issuer=PostmanAPI
Jwt__Audience=PostmanAPI
ASPNETCORE_ENVIRONMENT=Development
```


## 📄 Лицензия

MIT License - см. файл [LICENSE](LICENSE)
