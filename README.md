# QuestTracking API

**Версия:** Part 1 (API)  
_Фронтенд (Part 2) будет добавлен позже._

---

## Endpoints

### QuestController (`/api/Quest`)
- **POST /add-quest**  
  - Отправляет: `RequestQuest`  
  - Принимает: `{ Status, Data, Message }`
- **GET /get-all**  
  - Отправляет: —  
  - Принимает: `{ Status, Data[], Message }`
- **GET /get-by-user-email/{Email}**  
  - Отправляет: параметр `Email`  
  - Принимает: `{ Status, Data[], Message }`
- **GET /get-by-id/{Id}**  
  - Отправляет: параметр `Id`  
  - Принимает: `{ Status, Data, Message }`
- **DELETE /delete-by-id/{Id}**  
  - Отправляет: параметр `Id`  
  - Принимает: `{ Status, Data:null, Message }`
- **PUT /update-by-id/{Id}**  
  - Отправляет: параметр `Id` + `RequestQuest`  
  - Принимает: `{ Status, Data, Message }`

### UsersController (`/api/Users`)
- **POST /add/**  
  - Отправляет: `RequestUser`  
  - Принимает: `{ Status, Data, Message }`
- **GET /get-all/**  
  - Отправляет: —  
  - Принимает: `{ Status, Data[], Message }`
- **GET /get-by-email/{Email}**  
  - Отправляет: параметр `Email`  
  - Принимает: `{ Status, Data, Message }`
- **DELETE /delete-by-email/{Email}**  
  - Отправляет: параметр `Email`  
  - Принимает: `{ Status, Data:null, Message }`
- **PUT /update-by-email/{Email}**  
  - Отправляет: параметр `Email` + `RequestUser`  
  - Принимает: `{ Status, Data, Message }`

---

## Описание

RESTful API для управления сущностями **Quest** и **User**.
