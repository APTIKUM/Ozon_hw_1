# Ozon homework 1

Консольное приложение для формирования отчёта по клиентам на основе CSV-файла с заказами.

## Структура проекта

- `Domain` — сущности, отчёты и доменные исключения.
- `Application` — сервис отчётов и интерфейсы.
- `Infrastructure` — чтение CSV и форматирование/запись отчёта.
- `Presentation` — точка входа и парсер аргументов командной строки.
- `Tests` — unit-тесты на NUnit и FluentAssertions.

## Запуск

```bash
dotnet run --project Presentation -- console 1010 1020
dotnet run --project Presentation -- file 1010 1020
```

Первый аргумент задаёт способ вывода:

- `console` — вывод отчёта в консоль.
- `file` — сохранение отчёта в файл.

Оставшиеся аргументы — ID клиентов.

## Тесты

```bash
dotnet test
```
