**Общая информация**
Данный плагин реализован в целях выполнения тестового заданий.
Суть плагина заключается в том, что он находит прилегающие друг к другу квартиры с одинаковым количеством комнат и через один меняет закрашивание комнат в квартире на полутон.

## 1. Установка плагина
Необходимо содержимое каталога Addins поместить в системный каталог, где размещаются плагины
Примерный путь:
C:\ProgramData\Autodesk\Revit\Addins\2019
Далее при старте соглашаемся запускать данный плагин
Плагин добавляется дополнительным пунктом меню "Custom plugin", вызов функционала осуществляется через нажатие на кнопку "color the floor plan"

## 2. Доработка плагина
После открытия проекта удаляем зависимости RevitAPI и RevitAPIUI и вновь добвляем ссылки на эти библиотеки.
Примерное расположение(пусть установки Revit):
C:\Program Files\Autodesk\Revit 2019
Далее билдим проект.
Для отладки используем плагин для Revit именуемый "Add-in Manager", подгрузив через него сбилженную библиотеку.

