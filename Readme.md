## Задание по приему данных

Предлагается реализовать процесс приёма данных с эмулятора датчиков метеостанции.   
Есть несколько устройств-датчиков метеостанций. Датчики снимают показания температуры, влажности, давления.  
Датчики присылают информацию через определённые промежутки времени одним сообщением с данными всех датчиков и идентификатором датчика.  
Данные поступают с датчиков с зашумлением (показания снятые датчиком могут несколько отличаться от реального значения).  
Небольшой шум может наблюдаться у любого аналогового датчика ![картинка](images/noise-data.png?raw=true), поэтому при приёме данных от датчиков необходимо применить некоторый фильтр (как вариант, можно использовать бегущее среднее, медианный фильтр или фильтра Калмана).  
Необходимо сохранить информацию с дачиков в базу данных.  
***Реализация должна подразумевать возможность высокой частоты поступления данных.***

Дано: консольное приложение (эмулятор станции), которое требует в виде аргументов при запуске указать некоторые параметры:  
--url url - базовый адрес апи, на которую будут отправляться данные  
--sleep секунды - сколько секунд делать перерыв между отправкой данных  
--instance идентификатор - строка идентификатора станции  
--stability число - значение в пределах от 2 до 10, определяющее изменяемость данных, генерируемых датчиками  
--with-noise - наличие опции указывает накладывать шум на показания датчиков перед отправкой.  
Приложение отправляет данные на 2 адреса (url/data, url/noiseData). Используется метод PUT. На первый адрес поступают показания "чистые", а на второй "с шумом" для возможности сравнения эффективности применяемого фильтра при приёме данных.

Формат отправляемых данных можно посмотреть в файле MeteoDataPackage.cs

Требуется сделать: REST API, содержащее:  
1. методы приёма "чистых" данных и "зашумлённых" данных. Сохранать необходимо при этом "чистые" данные, "зашумлённые" данные, "отфильтрованные" данные  
2. метод отображения статистики по обработанным данным (сколько было данных получено, от скольки метеостанций)  
3. метод получения данных всех 3 категорий по указанному идентификатору метеостанции в формате csv (время (или идентификатор пакета); "чистые" показания; показания зашумлённые; показания, которые были вычислены с применением фильтра). Данный метод будет применяться для дальнейшей визаулизации результатов обработки данных.  
В ходе реализации необходимо учитывать, что одновременно данные будут присылаться от нескольких метеостанций.