
## Файловый менеджер



Описание: простой консольный файловый менеджер написанный на C#.


### Функционал и команды

- ls показать содержимое директории.
- cd [index] перейти в директорию.
- cd- выйти с текущей директории.
- p [стр] переход на следующую страницу.
- cp [index] [адрес] копировать файл или содержимое директории.
- rm [index] удалить объект.
- file [index] открывает файл формата TXT с последующим выводом информации в окно консоли.
- exit выключение программы.

## Особенности

Далее скромный список оссобенностей программы, мало вероятно - но возможно он будет дополняться.

- Index что бы указать объекта для манипуляции достаточно в комманде указать его индекс. 
```sh
Пример
cp 1 C:\Users\Sarol\Downloads //указали объект на копирование в папку Downloads
```



## Методы

##### PrintDirectoriesFiles
Метод составляет список содержимого директории с последующим выводом в консоль.



##### CommandHandler
Метод деления команды консоли на 2 и более объекта.


##### Coppy
Метод копирования содержимого директории или отдельных файлов.


##### Dellet
Метод удаления челых директорий или отдельных файлов.

## Известные ошибки
- нет защиты от некоректного ввода данных 
```sh
cd qwe //ошибка
```
- В процессе копирования при обнаружение дубликатов - происходит небольшое подтормаживание.
- Опять про копирование, нет защиты от порчи объекта копирования.