using System;
using System.Collections.Generic;
using System.Linq;

public class TaxiCar
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public int Mileage { get; set; }
    public bool IsInService { get; set; }
    public string Driver { get; set; }

    public void DisplayInfo()
    {
        Console.WriteLine($"Автомобиль: {Brand} {Model}");
        Console.WriteLine($"Год выпуска: {Year}");
        Console.WriteLine($"Пробег: {Mileage} км");
        Console.WriteLine($"Состояние: {(IsInService ? "В работе" : "На ремонте")}");
        Console.WriteLine($"Водитель: {Driver}");
        Console.WriteLine(new string('-', 40));
    }

    public void UpdateMileage(int newMileage)
    {
        if (newMileage >= Mileage)
        {
            Mileage = newMileage;
            Console.WriteLine($"Пробег обновлен: {Mileage} км");
        }
        else
        {
            Console.WriteLine("Новый пробег не может быть меньше текущего!");
        }
    }

    public void SetStatus(bool inService)
    {
        IsInService = inService;
        Console.WriteLine($"Статус изменен: {(IsInService ? "В работе" : "На ремонте")}");
    }
}

public class Program
{
    private static List<TaxiCar> taxiCars = new List<TaxiCar>();

    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Тестовые данные
        taxiCars.Add(new TaxiCar
        {
            Brand = "Toyota",
            Model = "Camry",
            Year = 2020,
            Mileage = 50000,
            IsInService = true,
            Driver = "Иванов П.С."
        });

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== ТАКСОПАРК ===");
            Console.WriteLine("1. Добавить автомобиль");
            Console.WriteLine("2. Показать все автомобили");
            Console.WriteLine("3. Поиск по водителю");
            Console.WriteLine("4. Обновить пробег");
            Console.WriteLine("5. Изменить статус");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите действие: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTaxiCar();
                    break;
                case "2":
                    ShowAllTaxiCars();
                    break;
                case "3":
                    SearchTaxiCarByDriver();
                    break;
                case "4":
                    UpdateTaxiCarMileage();
                    break;
                case "5":
                    ChangeTaxiCarStatus();
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

    private static void AddTaxiCar()
    {
        Console.Clear();
        Console.WriteLine("=== ДОБАВЛЕНИЕ АВТОМОБИЛЯ ===");

        var car = new TaxiCar();

        Console.Write("Марка: ");
        car.Brand = Console.ReadLine();

        Console.Write("Модель: ");
        car.Model = Console.ReadLine();

        Console.Write("Год выпуска: ");
        int year;
        while (!int.TryParse(Console.ReadLine(), out year) || year < 1900 || year > DateTime.Now.Year)
        {
            Console.Write("Введите корректный год: ");
        }
        car.Year = year;

        Console.Write("Пробег: ");
        int mileage;
        while (!int.TryParse(Console.ReadLine(), out mileage) || mileage < 0)
        {
            Console.Write("Введите корректный пробег: ");
        }
        car.Mileage = mileage;

        Console.Write("В работе? (y/n): ");
        car.IsInService = Console.ReadLine()?.ToLower() == "y";

        Console.Write("Водитель: ");
        car.Driver = Console.ReadLine();

        taxiCars.Add(car);
        Console.WriteLine("Автомобиль добавлен!");
        Console.ReadKey();
    }

    private static void ShowAllTaxiCars()
    {
        Console.Clear();
        Console.WriteLine("=== ВСЕ АВТОМОБИЛИ ===");

        if (!taxiCars.Any())
        {
            Console.WriteLine("Автомобили не найдены.");
        }
        else
        {
            foreach (var car in taxiCars)
            {
                car.DisplayInfo();
            }
        }
        Console.ReadKey();
    }

    private static void SearchTaxiCarByDriver()
    {
        Console.Clear();
        Console.WriteLine("=== ПОИСК ПО ВОДИТЕЛЮ ===");

        Console.Write("Введите ФИО водителя: ");
        var driver = Console.ReadLine();

        var cars = taxiCars.Where(c => c.Driver.ToLower().Contains(driver.ToLower())).ToList();

        if (!cars.Any())
        {
            Console.WriteLine("Автомобили не найдены.");
        }
        else
        {
            foreach (var car in cars)
            {
                car.DisplayInfo();
            }
        }
        Console.ReadKey();
    }

    private static void UpdateTaxiCarMileage()
    {
        Console.Clear();
        Console.WriteLine("=== ОБНОВЛЕНИЕ ПРОБЕГА ===");

        ShowAllTaxiCars();

        if (!taxiCars.Any())
        {
            Console.ReadKey();
            return;
        }

        Console.Write("Введите номер автомобиля: ");
        int index;
        while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > taxiCars.Count)
        {
            Console.Write("Введите корректный номер: ");
        }

        Console.Write("Введите новый пробег: ");
        int newMileage;
        while (!int.TryParse(Console.ReadLine(), out newMileage) || newMileage < 0)
        {
            Console.Write("Введите корректный пробег: ");
        }

        taxiCars[index - 1].UpdateMileage(newMileage);
        Console.ReadKey();
    }

    private static void ChangeTaxiCarStatus()
    {
        Console.Clear();
        Console.WriteLine("=== ИЗМЕНЕНИЕ СТАТУСА ===");

        ShowAllTaxiCars();

        if (!taxiCars.Any())
        {
            Console.ReadKey();
            return;
        }

        Console.Write("Введите номер автомобиля: ");
        int index;
        while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > taxiCars.Count)
        {
            Console.Write("Введите корректный номер: ");
        }

        Console.Write("Автомобиль в работе? (y/n): ");
        bool inService = Console.ReadLine()?.ToLower() == "y";

        taxiCars[index - 1].SetStatus(inService);
        Console.ReadKey();
    }
}