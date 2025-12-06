## Описание предметной области

Реализована объектная модель для риэлторского агентства, включающая три основных сущности:

- **RealEstate** (`RealEstate`) — 10 объектов недвижимости различных типов и назначений (квартиры, дома, офисы, склады, земельные участки и др.)
- **Counterparty** (`Counterparty`) — 5 клиентов с полными идентификационными и контактными данными
- **Application** (`Application`) — 10 заявок на покупку и продажу недвижимости

## Выполненные задачи

### 1. Модель данных
Объекты недвижимости классифицируются с использованием enum-типов:

- `RealEstateType` — архитектурный тип недвижимости (Apartment, House, Cottage, Office, Townhouse, Shop, Warehouse, Garage, Land)
- `RealEstatePurpose` — назначение недвижимости (Residential, Commercial, Industrial, Agricultural, Recreational, Utility)
- `ApplicationType` — тип операции (Purchase, Sale)

### 2. Тестовые данные
- Подготовлен класс `FixtureDataClass` с тестовыми данными:
  - 10 объектов недвижимости
  - 10 клиентов
  - 10 заявок на покупку/продажу недвижимости

### 3. Unit-тесты
- Реализованы тесты с использованием xUnit для проверки бизнес-логики:

1. **`GetSellersByPeriod`** — вывод всех продавцов, оставивших заявки за заданный период
2. **`Top5Clients`** — вывод топ 5 клиентов по количеству заявок (отдельно для покупки и продажи)
3. **`RequestsByEstateType`** — вывод количества заявок по каждому типу недвижимости
4. **`ClientsWithMinTransaction`** — вывод клиентов, открывших заявки с минимальной стоимостью
5. **`ClientsByEstateType`** — вывод клиентов, ищущих недвижимость заданного типа, упорядоченных по ФИО

### 4. Инфраструктура
- Реализованы классы для работы с данными:
  - AppDbContext — контекст EF Core
  - DbSeed — первичное заполнение БД
  - DbRepository — реализация интерфейса IRepository

### 5. Контракты
- DTO-модели для операций создания/редактирования/чтения
- MappingProfile - Маппер для DTO

### 6. API
- Реализует контроллеры:
  - ApplicationsController
  - RealEstatesController
  - CounterpartiesController
  - AnalyticsController

### 7. Kafka
- Инфраструктура worker-сервисов:
  - KafkaProducerWorker — генерирует заявки через ApplicationGenerator и публикует их в Kafka топик
  - KafkaConsumerWorker — читает сообщения из Kafka, маппит DTO в сущности и сохраняет в БД, раполагается на уровне API.
- Конфигурация через environment variables:
  - KAFKA_TOPIC — имя топика
  - KAFKA_PRODUCE_DELAY_MS — задержка между сообщениями
  - KAFKA_GROUP_ID — идентификатор consumer group
  - KAFKA_FETCH_MIN_BYTES - параметр batching

### 8. AppHost
Конфигурация и запуск приложения с использованием контейнеров

## Результат
Создана полноценная предметная модель риэлторского агентства, оснащённая:
- архитектурой уровня Domain → Application → Infrastructure → Api → Kafka
- системой DTO и маппингом через AutoMapper
- реализацией репозиториев и EF Core контекста
- REST-API с контроллерами для всех сущностей
- unit-тестами на xUnit
- Kafka продюсером и консьюмером для обработки заявок
- возможностью масштабирования через настройки batching и consumer group в Kafka
