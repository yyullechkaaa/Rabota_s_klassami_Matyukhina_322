using System;
using System.Collections.Generic;
using System.Linq;

public class SportCustomer
{
    public string FullName { get; set; }
    public int Age { get; set; }
    public string Product { get; set; }
    public string Size { get; set; }
    public decimal Price { get; set; }
    public string PaymentMethod { get; set; }

    public void DisplayInfo()
    {
        Console.WriteLine($"Покупатель: {FullName}");
        Console.WriteLine($"Возраст: {Age}");
        Console.WriteLine($"Товар: {Product}");
        Console.WriteLine($"Размер: {Size}");
        Console.WriteLine($"Цена: {Price:C}");
        Console.WriteLine($"Способ оплаты: {PaymentMethod}");
        Console.WriteLine(new string('-', 40));
    }
}

public class Program
{
    private static List<SportCustomer> sportCustomers = new List<SportCustomer>();

    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Тестовые данные
        sportCustomers.Add(new SportCustomer
        {
            FullName = "Сидоров Алексей",
            Age = 25,
            Product = "Кроссовки",
            Size = "42",
            Price = 5000,
            PaymentMethod = "Карта"
        });

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== СПОРТИВНЫЙ МАГАЗИН ===");
            Console.WriteLine("1. Добавить покупателя");
            Console.WriteLine("2. Показать всех покупателей");
            Console.WriteLine("3. Поиск по возрасту");
            Console.WriteLine("4. Сортировка по стоимости");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите действие: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddSportCustomer();
                    break;
                case "2":
                    ShowAllSportCustomers();
                    break;
                case "3":
                    SearchSportCustomerByAge();
                    break;
                case "4":
                    SortSportCustomersByPrice();
                    break;
                case "0":
                    Console.WriteLine("Выход из программы...");
                    return;
                default:
                    Console.WriteLine("Неверный выбор!");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static void AddSportCustomer()
    {
        Console.Clear();
        Console.WriteLine("=== ДОБАВЛЕНИЕ ПОКУПАТЕЛЯ ===");

        var customer = new SportCustomer();

        Console.Write("ФИО: ");
        customer.FullName = Console.ReadLine();

        Console.Write("Возраст: ");
        int age;
        while (!int.TryParse(Console.ReadLine(), out age) || age <= 0)
        {
            Console.Write("Введите корректный возраст: ");
        }
        customer.Age = age;

        Console.Write("Товар: ");
        customer.Product = Console.ReadLine();

        Console.Write("Размер: ");
        customer.Size = Console.ReadLine();

        Console.Write("Цена: ");
        decimal price;
        while (!decimal.TryParse(Console.ReadLine(), out price) || price <= 0)
        {
            Console.Write("Введите корректную цену: ");
        }
        customer.Price = price;

        Console.Write("Способ оплаты: ");
        customer.PaymentMethod = Console.ReadLine();

        sportCustomers.Add(customer);
        Console.WriteLine("Покупатель добавлен!");
        Console.ReadKey();
    }

    private static void ShowAllSportCustomers()
    {
        Console.Clear();
        Console.WriteLine("=== ВСЕ ПОКУПАТЕЛИ ===");

        if (!sportCustomers.Any())
        {
            Console.WriteLine("Покупатели не найдены.");
        }
        else
        {
            foreach (var customer in sportCustomers)
            {
                customer.DisplayInfo();
            }
        }
        Console.ReadKey();
    }

    private static void SearchSportCustomerByAge()
    {
        Console.Clear();
        Console.WriteLine("=== ПОИСК ПО ВОЗРАСТУ ===");

        Console.Write("Введите возраст: ");
        int age;
        while (!int.TryParse(Console.ReadLine(), out age) || age <= 0)
        {
            Console.Write("Введите корректный возраст: ");
        }

        var customers = sportCustomers.Where(c => c.Age == age).ToList();

        if (!customers.Any())
        {
            Console.WriteLine("Покупатели не найдены.");
        }
        else
        {
            foreach (var customer in customers)
            {
                customer.DisplayInfo();
            }
        }
        Console.ReadKey();
    }

    private static void SortSportCustomersByPrice()
    {
        Console.Clear();
        Console.WriteLine("=== СОРТИРОВКА ПО СТОИМОСТИ ===");

        var sortedCustomers = sportCustomers.OrderByDescending(c => c.Price).ToList();

        if (!sortedCustomers.Any())
        {
            Console.WriteLine("Покупатели не найдены.");
        }
        else
        {
            foreach (var customer in sortedCustomers)
            {
                customer.DisplayInfo();
            }
        }
        Console.ReadKey();
    }
}