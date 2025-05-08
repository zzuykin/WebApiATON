# WebApiATON

**WebApiATON** — это демонстрационный Web API-сервис на .NET 9, реализующий CRUD-операции над сущностью `User` с авторизацией через JWT-токены. Все методы доступны через Swagger UI и защищены в соответствии с ролями пользователей.

## 📦 Стек технологий

- ASP.NET Core 9
- Entity Framework Core + SQLite
- JWT (JSON Web Token) авторизация
- Swagger (Swashbuckle)

---

##  Запуск проекта

1. **Клонировать репозиторий:**
   ```bash
   git clone https://github.com/zzuykin/WebApiATON.git
   cd WebApiATON
2. Чтобы были доступны методы контроллеров User, необходимо авторизоваться как админ. Логин : admin, Пароль: admin
