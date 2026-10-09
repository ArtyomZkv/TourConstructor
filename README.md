# TourConstructor

Сервис собирает маршрут однодневной поездки из точек (музеи, кафе, гиды) под запрос пользователя.
Пилот: Нижегородская область.

## Структура

Clean Architecture, проекты в папке `src/`:

| Проект | Назначение |
|---|---|
| `TourConstructor.Domain` | Сущности и value objects. Ни на что не ссылается |
| `TourConstructor.Application` | Планировщик маршрута и интерфейсы (`IPlaceRepository`, `ITravelTimeProvider`) |
| `TourConstructor.Infrastructure` | Реализации: EF Core + PostgreSQL, OSRM |
| `TourConstructor.Api` | ASP.NET Core Web API |

## Требования

- .NET 10 SDK
- Docker (для OSRM)

## Сборка

```powershell
dotnet build TourConstructor.slnx
```

## OSRM

Время в пути между точками считает локальный сервер [OSRM](https://github.com/Project-OSRM/osrm-backend).
Планировщик использует сервис `/table`. Данные графа лежат в папке `osrm/`, она исключена из git.

### Подготовка графа (один раз)

1. Скачайте экстракт Приволжского федерального округа с [Geofabrik](https://download.geofabrik.de/russia/volga-fed-district.html)
   (`volga-fed-district-latest.osm.pbf`) и положите его в папку `osrm/`.
   Ниже в командах используется имя файла `volga-fed-district-260915.osm.pbf`. Если у вашего файла другое имя, подставьте его.

2. Выполните команды из папки `osrm/` (PowerShell):

```powershell
# 1. Построить граф из карты (долго, требует много памяти)
docker run -v "${PWD}:/data" ghcr.io/project-osrm/osrm-backend osrm-extract -p /opt/car.lua /data/volga-fed-district-260915.osm.pbf

# 2. Разбить граф на ячейки
docker run -v "${PWD}:/data" ghcr.io/project-osrm/osrm-backend osrm-partition /data/volga-fed-district-260915.osrm

# 3. Проставить веса
docker run -v "${PWD}:/data" ghcr.io/project-osrm/osrm-backend osrm-customize /data/volga-fed-district-260915.osrm
```

### Запуск сервера

Из папки `osrm/`:

```powershell
docker run -p 5000:5000 -v "${PWD}:/data" ghcr.io/project-osrm/osrm-backend osrm-routed --algorithm mld /data/volga-fed-district-260915.osrm
```

Проверка: запрос матрицы времён между двумя точками в Нижнем Новгороде (координаты в порядке `долгота,широта`):

```powershell
curl.exe "http://localhost:5000/table/v1/driving/44.00,56.32;43.94,56.29"
```

В ответе должно быть поле `durations` с временем в секундах.
