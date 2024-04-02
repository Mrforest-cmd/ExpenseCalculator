using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpenseCalculator
{
    public class Expense
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public Expense(string name, decimal amount, DateTime date)
        {
            Name = name;
            Amount = amount;
            Date = date;
        }
    }

    public delegate void ExpenseAddedHandler(object sender, ExpenseAddedEventArgs args);

    public class ExpenseAddedEventArgs : EventArgs
    {
        public Expense Expense { get; set; }

        public ExpenseAddedEventArgs(Expense expense)
        {
            Expense = expense;
        }
    }

    class ExpenseCalculator
    {
        private List<Expense> expenses = new List<Expense>();

        public event ExpenseAddedHandler ExpenseAdded;

        public void AddExpense(string name, decimal amount, DateTime date)
        {
            Expense expense = new Expense(name, amount, date);
            expenses.Add(expense);

            OnExpenseAdded(expense);
        }


        public decimal CalculateTotalExpenses()
        {
            return expenses.Sum(e => e.Amount);
        }

        public decimal CalculateAverageExpense()
        {
            if (expenses.Count == 0)
                return 0;

            return expenses.Average(e => e.Amount);
        }

        public List<Expense> GetExpensesByDate(DateTime date)
        {
            return expenses.FindAll(e => e.Date.Date == date.Date);
        }

        protected virtual void OnExpenseAdded(Expense expense)
        {
            ExpenseAddedHandler handler = ExpenseAdded;
            if (handler != null)
            {
                handler(this, new ExpenseAddedEventArgs(expense));
            }
        }
    }

    class ExpenseView
    {
        private ExpenseCalculator calculator;

        public ExpenseView(ExpenseCalculator calculator)
        {
            this.calculator = calculator;
            calculator.ExpenseAdded += Calculator_ExpenseAdded;
        }

        private void Calculator_ExpenseAdded(object sender, ExpenseAddedEventArgs args)
        {
            Console.WriteLine($"Додано витрату: {args.Expense.Name} - {args.Expense.Amount:C} ({args.Expense.Date:d})");
        }

        public void DisplayTotalExpenses()
        {
            decimal totalExpenses = calculator.CalculateTotalExpenses();
            Console.WriteLine($"Загальна сума витрат: {totalExpenses:C}");
        }

        public void DisplayAverageExpense()
        {
            decimal averageExpense = calculator.CalculateAverageExpense();
            Console.WriteLine($"Середня витрата: {averageExpense:C}");
        }

        public void DisplayExpensesByDate(DateTime date)
        {
            List<Expense> expenses = calculator.GetExpensesByDate(date);
            Console.WriteLine($"Витрати за {date:d}:");
            foreach (Expense expense in expenses)
            {
                Console.WriteLine($"{expense.Name} - {expense.Amount:C}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            ExpenseCalculator calculator = new ExpenseCalculator();
            ExpenseView view = new ExpenseView(calculator);

            while (true)
            {
                Console.WriteLine("\nВиберіть дію:");
                Console.WriteLine("1. Додати витрату");
                Console.WriteLine("2. Відобразити загальну суму витрат");
                Console.WriteLine("3. Відобразити середню витрату");
                Console.WriteLine("4. Відобразити витрати за датою");
                Console.WriteLine("5. Вийти");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddExpense(calculator);
                        break;
                    case "2":
                        view.DisplayTotalExpenses();
                        break;
                    case "3":
                        view.DisplayAverageExpense();
                        break;
                    case "4":
                        DisplayExpensesByDate(view);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                        break;
                }
            }
        }

        static void AddExpense(ExpenseCalculator calculator)
        {
            Console.Write("Введіть назву витрати: ");
            string name = Console.ReadLine();

            Console.Write("Введіть суму витрати: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            Console.Write("Введіть дату витрати (рррр-мм-дд): ");
            DateTime date = DateTime.Parse(Console.ReadLine());

            calculator.AddExpense(name, amount, date);
        }

        static void DisplayExpensesByDate(ExpenseView view)
        {
            Console.Write("Введіть дату (рррр-мм-дд): ");
            DateTime date = DateTime.Parse(Console.ReadLine());
            view.DisplayExpensesByDate(date);
        }
    }
}