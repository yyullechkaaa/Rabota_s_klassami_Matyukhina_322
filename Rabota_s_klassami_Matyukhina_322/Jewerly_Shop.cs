using System;
using System.Collections.Generic;
using System.Linq;

public class JewelryCustomer
{
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string JewelryType { get; set; }
    public string Material { get; set; }
    public decimal Price { get; set; }
    public decimal DiscountPercent { get; set; }

    public decimal CalculateFinalPrice()
    {
        return Price * (1 - DiscountPercent / 100);
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Покупатель: {FullName}");
        Console.WriteLine($"Телефон: {PhoneNumber}");
        Console.WriteLine($"Украшение: {JewelryType}");
        Console.WriteLine($"Материал: {Material}");
        Console.WriteLine($"Цена: {Price:C}");
        Console.WriteLine($"Скидка: {DiscountPercent}%");
        Console.WriteLine($"Итоговая стоимость: {CalculateFinalPrice():C}");
        Console.WriteLine(new string('-', 40));
    }
}

public class Program
{
    private static List<JewelryCustomer> jewelryCustomers = new List<JewelryCustomer>();

    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Тестовые данные
        jewelryCustomers.Add(new JewelryCustomer
        {
            FullName = "Петрова Мария",
            PhoneNumber = "+79123456789",
            JewelryType = "Кольцо",
            Material = "Золото",
            Price = 25000,
            DiscountPercent = 10
        });

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== ЮВЕЛИРНЫЙ МАГАЗИН ===");
            Console.WriteLine("1. Добавить покупателя");
            Console.WriteLine("2. Показать всех покупателей");
            Console.WriteLine("3. Поиск по номеру телефона");
            Console.WriteLine("4. Общая прибыль магазина");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите действие: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddJewelryCustomer();
                    break;
                case "2":
                    ShowAllJewelryCustomers();
                    break;
                case "3":
                    SearchJewelryCustomerByPhone();
                    break;
                case "4":
                    CalculateTotalProfit();
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

    private static void AddJewelryCustomer()
    {
        Console.Clear();
        Console.WriteLine("=== ДОБАВЛЕНИЕ ПОКУПАТЕЛЯ ===");

        var customer = new JewelryCustomer();

        Console.Write("ФИО: ");
        customer.FullName = Console.ReadLine();

        Console.Write("Номер телефона: ");
        customer.PhoneNumber = Console.ReadLine();

        Console.Write("Тип украшения: ");
        customer.JewelryType = Console.ReadLine();

        Console.Write("Материал: ");
        customer.Material = Console.ReadLine();

        Console.Write("Цена: ");
        decimal price;
        while (!decimal.TryParse(Console.ReadLine(), out price) || price <= 0)
        {
            Console.Write("Введите корректную цену: ");
        }
        customer.Price = price;

        Console.Write("Скидка (%): ");
        decimal discount;
        while (!decimal.TryParse(Console.ReadLine(), out discount) || discount < 0 || discount > 100)
        {
            Console.Write("Введите корректную скидку (0-100%): ");
        }
        customer.DiscountPercent = discount;

        jewelryCustomers.Add(customer);
        Console.WriteLine("Покупатель добавлен!");
        Console.ReadKey();
    }

    private static void ShowAllJewelryCustomers()
    {
        Console.Clear();
        Console.WriteLine("=== ВСЕ ПОКУПАТЕЛИ ===");

        if (!jewelryCustomers.Any())
        {
            Console.WriteLine("Покупатели не найдены.");
        }
        else
        {
            foreach (var customer in jewelryCustomers)
            {
                customer.DisplayInfo();
            }
        }
        Console.ReadKey();
    }

    private static void SearchJewelryCustomerByPhone()
    {
        Console.Clear();
        Console.WriteLine("=== ПОИСК ПО НОМЕРУ ТЕЛЕФОНА ===");

        Console.Write("Введите номер телефона: ");
        var phone = Console.ReadLine();

        var customers = jewelryCustomers.Where(c => c.PhoneNumber.Contains(phone)).ToList();

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

    private static void CalculateTotalProfit()
    {
        Console.Clear();
        Console.WriteLine("=== ОБЩАЯ ПРИБЫЛЬ МАГАЗИНА ===");

        var totalProfit = jewelryCustomers.Sum(c => c.CalculateFinalPrice());
        Console.WriteLine($"Общая прибыль: {totalProfit:C}");
        Console.ReadKey();
    }
}