@startuml
'https://plantuml.com/sequence-diagram

actor "User"
participant "Nginx Api Gateway"
participant "Auth Api"
participant "User Api"

User -> "Nginx Api Gateway" : Запрос /uri с JWT

"Nginx Api Gateway" -> "Auth Api" : Проверка JWT на валидность

alt #LightBlue JWT is provided and valid

"Auth Api" -> "Nginx Api Gateway" : Авторизован - 200
"Nginx Api Gateway" -> "User Api" : Запрос /uri с JWT
"User Api" -> "User" : Успешный ответ

else #Pink

"Auth Api" -> "User" : Не авторизован - 401
"User" -> "Auth Api" : Запрос /auth/login

alt #LightBlue

  "Auth Api" -> "User" : Новый JWT
  "User" -> "Nginx Api Gateway" : Запрос /uri с JWT
  "Nginx Api Gateway" -> "Auth Api" : Проверка JWT на валидность
  "Auth Api" -> "Nginx Api Gateway" : Авторизован - 200
  "Nginx Api Gateway" -> "User Api" : Запрос /uri с JWT
  "User Api" -> "User" : Успешный ответ

else #Pink

  "Auth Api" -> "User": Логин или пароль неверный

end

end
@enduml