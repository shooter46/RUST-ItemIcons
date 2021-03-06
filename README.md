# ItemIcons
Плагин предназначен для облегчения доступа к иконкам игровых предметов.  
Иконки всех предметов уже есть в игре, так что нет необходимости грузить их на сервер через ImageLibrary (или что-то другое), а потом ещё заставлять игрока скачивать их себе.

## Как это работает?
В конфиге указаны пути к спрайтам предметов, которые можно использовать в кастомных интерфейсах.  
Плагин будет скачивать свежий конфиг с этого репозитория при каждом запуске. Вручную обновлять ничего не надо.

## Плюсы и минусы
\+ Моментальное отображение без прогрузок  
\+ Нет фризов при одновременном отображении большого количества иконок  
\- Качество иконок зависит от качества графики  
[Сравнение скорости отображения](https://www.youtube.com/watch?v=2b0iXnMMMPc)
## API
```csharp
(string)ItemIcon(string shortname, bool placeholder = true) // Возвращает путь к спрайту указанного предмета
```
Если `placeholder` равен `true`, то в случае отсутвия иконки будет возвращена строчка `assets/content/textures/missing_icon.png`, в ином случае вернётся `null`.

Подсовывать это в кастомном интерфейсе надо в переменную `Sprite`, не в `Png`!
