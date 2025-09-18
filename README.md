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
- `PUT /users/{id}` - Обновить пользователя
- `DELETE /users/{id}` - Удалить пользователя
- `GET /users/{id}/orders` - Заказы пользователя

### 📦 Продукты
- `GET /products` - Список продуктов (с фильтрацией)
- `GET /products/{id}` - Получить продукт по ID
- `POST /products` - Создать продукт
- `PUT /products/{id}` - Обновить продукт
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

### Локальный запуск

1. **Клонировать репозиторий**
```bash
git clone <repository-url>
cd PostmanAPI
```

2. **Восстановить зависимости**
```bash
dotnet restore
```

3. **Запустить приложение**
```bash
cd PostmanAPI
dotnet run
```

4. **Открыть Swagger UI**
   - Перейти на https://localhost:5001
   - Документация API будет доступна сразу

### Запуск в Docker

1. **Собрать образ**
```bash
docker build -t postmanapi .
```

2. **Запустить контейнер**
```bash
docker run -p 8080:8080 -p 8081:8081 postmanapi
```

3. **Открыть API**
   - HTTP: http://localhost:8080
   - HTTPS: https://localhost:8081

## 🧪 Тестирование

### Запуск тестов
```bash
dotnet test
```

### Тестовые данные

При первом запуске автоматически создаются тестовые пользователи:

| Email | Пароль | Имя |
|-------|--------|-----|
| ivan@example.com | password123 | Иван Иванов |
| maria@example.com | password123 | Мария Петрова |
| alex@example.com | password123 | Алексей Сидоров |

### Пример использования в Postman

1. **Авторизация**
```json
POST /auth/login
{
  "email": "ivan@example.com",
  "password": "password123"
}
```

2. **Получение токена и использование в заголовках**
```
Authorization: Bearer <your-access-token>
```

3. **Создание заказа**
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

### Настройки JWT

- **Access Token**: 15 минут
- **Refresh Token**: 7 дней
- **Алгоритм**: HS256

## 📊 Логирование

Логи сохраняются в:
- **Консоль** - для разработки
- **Файлы** - `logs/log-YYYYMMDD.txt`

## 🔒 Безопасность

- Пароли хешируются с помощью BCrypt
- JWT токены для авторизации
- Refresh токены хранятся в базе данных
- Валидация входных данных
- Обработка ошибок в JSON формате

## 📝 Коды ответов

- `200` - Успешно
- `201` - Создано
- `204` - Нет содержимого
- `400` - Неверный запрос
- `401` - Не авторизован
- `403` - Запрещено
- `404` - Не найдено
- `500` - Внутренняя ошибка сервера

## 🛠️ Разработка

### Добавление миграций
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Структура ответов на ошибки
```json
{
  "error": "User not found",
  "status": 404
}
```

## 📈 Производительность

- Время ответа: < 500мс
- Пагинация для больших списков
- Оптимизированные запросы к БД
- Логирование производительности

## 🤝 Вклад в проект

1. Форкните репозиторий
2. Создайте ветку для новой функции
3. Внесите изменения
4. Добавьте тесты
5. Создайте Pull Request

## 📄 Лицензия

MIT License - см. файл [LICENSE](LICENSE)
