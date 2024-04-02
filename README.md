# ExpenseCalculator

ExpenseCalculator є простим додатком для обліку витрат, написаним на C#. Він дозволяє користувачеві додавати витрати, відображати загальну суму витрат, середню витрату та витрати за конкретною датою.

## Функціональність

- Додавання витрати з назвою, сумою та датою
- Обчислення загальної суми витрат
- Обчислення середньої витрати
- Відображення списку витрат за конкретною датою

## Структура проекту

Проект складається з наступних основних класів:

1. `Expense`: Клас, що представляє окрему витрату з властивостями `Name` (назва), `Amount` (сума), `Date` (дата).
2. `ExpenseAddedEventArgs`: Клас, що успадковується від `EventArgs` і використовується для передачі даних події `ExpenseAdded`.
3. `ExpenseCalculator`: Основний клас, що містить логіку для додавання витрат, обчислення загальної суми та середньої витрати, а також отримання витрат за датою. Має подію `ExpenseAdded`, яка спрацьовує при додаванні нової витрати.
4. `ExpenseView`: Клас, який відповідає за взаємодію з користувачем, відображення меню та результатів різних операцій.
5. `Program`: Головний клас, що містить точку входу в програму та цикл головного меню.

## Використання

1. Запустіть додаток ExpenseCalculator.exe.
2. У головному меню виберіть потрібну дію, введіть необхідні дані та натисніть Enter.
3. Для виходу з програми виберіть пункт меню "Вийти".
