# Deadliner

### ТЗ

Deadliner - инструмент для управления дедлайнами и другими событями, связанными с задачами, выполняемыми в группе.

#### Основные сущности

* Пользователь;
* Группа - набор пользователей, один пользователь может принадлежать нескольким группам;
* Супер-группа - набор групп;
* Задача - какое-либо действие обладающее моментом создания и ограниченным временем выполнения, привязанное к одной группе, может иметь подзадачи;
* Событие - кратковременная задача, может быть привязано к конкретной задаче;
* Календарь - визуализация календаря с отображением задач и событий всех групп, в которых состоит пользователь.

#### Описание

Пользователи могут объединяться в группы, создавать общие задачи, следить за их выполнением и общей нагрузкой в каждый момент времени. Например, можно использовать приложение в контексте обучения: для каждого предмета есть отдельная группа, в ней создаются задачи - домашние задания или классные работы, кроме дедлайна у задачи может быть мягкий дедлайн, могут быть дополнительные точки-события, например, когда меняется стратегия начисления баллов. Также событиями и соответствующими заданиями могут быть тесты, зачеты и экзамены. Каждый ученик может отмечать, когда начал делать задание, когда закончил, смотреть на текущую загруженность в текущий момент и в ближайшем будущем. 
При этом создать задание или событие достаточно одному члену группы и оно отобразится у всех, это может быть или преподаватель, или один из учеников.

#### Функциональность

* Каждый пользователь может создавать группы и присоединяться к другим
* Группа может быть публичной или приватной с ключом доступа
* Супер-группа может быть публичной или приватной
* Каждый пользователь имеет приватную личную группу
* Задачи и события можно создавать внутри группы
* Задачи и события можно редактировать
* Можно отмечать начало выполнения, паузу и конец выполнения задания 
* Можно смотреть статистику загруженности по количеству невыполненных задач
* После выполнения задания оно не учитывается в статистике загруженности
* В календаре пользователи видят все свои задачи
* Основной формат календаря - месяц, возможно имеет место переключение на формат недели

#### Структура

* __Хранилище данных__: база данных с доступом в формате REST сервиса 
* __Интерфейс__: `.Net` или веб 
* __Статистика__: отдельный сервис с REST API

#### Интерфейс

##### Календарь с задачами 

Это основная страница.

Функции: 
* можно перейти к каждой из видимых задач
* можно добавить новую задачу
* можно создать новую группу и супер-группу

##### Создание задачи или события

* Выбор группы
* Установка даты и времени начала и конца
* Название
* Описание
* Для задачи: добавление событий
* Для события: можно указать периодичность и конечную дату

Те же параметры для редактирования и еще удаление.

##### Просмотр задачи или события

* Отображены: название, описание, даты
* Можно начать, завершить или приостановить выполнение

##### Просмотр / создание / редактирование группы

* Название, описание, приватность
* Список участников
* Список задач и событий
* Для приватной: ключ доступа

##### Просмотр / создание / редактирование супер-группы

* Название
* Список групп
