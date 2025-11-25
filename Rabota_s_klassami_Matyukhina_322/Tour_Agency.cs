using System;
using System.Collections.Generic;
using System.Linq;

public class Tourist
{
    public string FullName { get; set; }
    public string PassportNumber { get; set; }
    public string Destination { get; set; }
    public DateTime TravelDate { get; set; }
    public int Duration { get; set; }
    public decimal Price { get; set; }

    public void DisplayInfo()
    {
        Console.WriteLine($"Турист: {FullName}");
        Console.WriteLine($"Паспорт: {PassportNumber}");
        Console.WriteLine($"Направление: {Destination}");
        Console.WriteLine($"Дата выезда: {TravelDate:dd.MM.yyyy}");
        Console.WriteLine($"Продолжительность: {Duration} дней");
        Console.WriteLine($"Стоимость: {Price:C}");
        Console.WriteLine(new string('-', 40));
    }

    public bool IsTravelInDateRange(DateTime startDate, DateTime endDate)
    {
        return TravelDate >= startDate && TravelDate <= endDate;
    }
}

public class Program
{
    private static List<Tourist> tourists = new List<Tourist>();

    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Добавляем тестовые данные
        tourists.Add(new Tourist
        {
            FullName = "Иванов Иван Иванович",
            PassportNumber = "123456789",
            Destination = "Турция",
            TravelDate = new DateTime(2024, 6, 15),
            Duration = 10,
            Price = 50000
        });

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== ТУРИСТИЧЕСКОЕ АГЕНТСТВО ===");
            Console.WriteLine("1. Добавить туриста");
            Console.WriteLine("2. Показать всех туристов");
            Console.WriteLine("3. Поиск по направлению");
            Console.WriteLine("4. Поиск по дате поездки");
            Console.WriteLine("5. Общая стоимость всех туров");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите действие: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTourist();
                    break;
                case "2":
                    ShowAllTourists();
                    break;
                case "3":
                    SearchTouristByDestination();
                    break;
                case "4":
                    SearchTouristByDate();
                    break;
                case "5":
                    CalculateTotalTourPrice();
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

    private static void AddTourist()
    {
        Console.Clear();
        Console.WriteLine("=== ДОБАВЛЕНИЕ ТУРИСТА ===");

        var tourist = new Tourist();

        Console.Write("ФИО: ");
        tourist.FullName = Console.ReadLine();

        Console.Write("Номер паспорта: ");
        tourist.PassportNumber = Console.ReadLine();

        Console.Write("Направление: ");
        tourist.Destination = Console.ReadLine();

        Console.Write("Дата выезда (дд.мм.гггг): ");
        DateTime travelDate;
        while (!DateTime.TryParse(Console.ReadLine(), out travelDate))
        {
            Console.Write("Введите корректную дату (дд.мм.гггг): ");
        }
        tourist.TravelDate = travelDate;

        Console.Write("Продолжительность (дней): ");
        int duration;
        while (!int.TryParse(Console.ReadLine(), out duration) || duration <= 0)
        {
            Console.Write("Введите корректную продолжительность: ");
        }
        tourist.Duration = duration;

        Console.Write("Стоимость: ");
        decimal price;
        while (!decimal.TryParse(Console.ReadLine(), out price) || price <= 0)
        {
            Console.Write("Введите корректную стоимость: ");
        }
        tourist.Price = price;

        tourists.Add(tourist);
        Console.WriteLine("Турист добавлен!");
        Console.ReadKey();
    }

    private static void ShowAllTourists()
    {
        Console.Clear();
        Console.WriteLine("=== ВСЕ ТУРИСТЫ ===");

        if (!tourists.Any())
        {
            Console.WriteLine("Туристы не найдены.");
        }
        else
        {
            foreach (var tourist in tourists)
            {
                tourist.DisplayInfo();
            }
        }
        Console.ReadKey();
    }

    private static void SearchTouristByDestination()
    {
        Console.Clear();
        Console.WriteLine("=== ПОИСК ПО НАПРАВЛЕНИЮ ===");

        Console.Write("Введите направление: ");
        var destination = Console.ReadLine();

        var foundTourists = tourists.Where(t => t.Destination.ToLower().Contains(destination.ToLower())).ToList();

        if (!foundTourists.Any())
        {
            Console.WriteLine("Туристы не найдены.");
        }
        else
        {
            foreach (var tourist in foundTourists)
            {
                tourist.DisplayInfo();
            }
        }
        Console.ReadKey();
    }

    private static void SearchTouristByDate()
    {
        Console.Clear();
        Console.WriteLine("=== ПОИСК ПО ДАТЕ ПОЕЗДКИ ===");

        Console.Write("Введите начальную дату (дд.мм.гггг): ");
        DateTime startDate;
        while (!DateTime.TryParse(Console.ReadLine(), out startDate))
        {
            Console.Write("Введите корректную дату (дд.мм.гггг): ");
        }

        Console.Write("Введите конечную дату (дд.мм.гггг): ");
        DateTime endDate;
        while (!DateTime.TryParse(Console.ReadLine(), out endDate))
        {
            Console.Write("Введите корректную дату (дд.мм.гггг): ");
        }

        var foundTourists = tourists.Where(t => t.IsTravelInDateRange(startDate, endDate)).ToList();

        if (!foundTourists.Any())
        {
            Console.WriteLine("Туристы не найдены в указанный период.");
        }
        else
        {
            foreach (var tourist in foundTourists)
            {
                tourist.DisplayInfo();
            }
        }
        Console.ReadKey();
    }

    private static void CalculateTotalTourPrice()
    {
        Console.Clear();
        Console.WriteLine("=== ОБЩАЯ СТОИМОСТЬ ВСЕХ ТУРОВ ===");

        var totalPrice = tourists.Sum(t => t.Price);
        Console.WriteLine($"Общая стоимость всех туров: {totalPrice:C}");
        Console.ReadKey();
    }
}