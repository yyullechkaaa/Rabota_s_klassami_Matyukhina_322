using System;
using System.Collections.Generic;
using System.Linq;

public class ShelterAnimal
{
    public string Name { get; set; }
    public string AnimalType { get; set; }
    public int Age { get; set; }
    public bool HasVaccinations { get; set; }
    public DateTime AdmissionDate { get; set; }
    public bool IsAdopted { get; set; }

    public void DisplayInfo()
    {
        Console.WriteLine($"Кличка: {Name}");
        Console.WriteLine($"Вид: {AnimalType}");
        Console.WriteLine($"Возраст: {Age} лет");
        Console.WriteLine($"Прививки: {(HasVaccinations ? "Да" : "Нет")}");
        Console.WriteLine($"Дата поступления: {AdmissionDate:dd.MM.yyyy}");
        Console.WriteLine($"Статус: {(IsAdopted ? "Забрали домой" : "В приюте")}");
        Console.WriteLine(new string('-', 40));
    }

    public void MarkAsAdopted()
    {
        IsAdopted = true;
        Console.WriteLine($"Животное {Name} отмечено как забранное домой");
    }
}

public class Program
{
    private static List<ShelterAnimal> shelterAnimals = new List<ShelterAnimal>();

    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Тестовые данные
        shelterAnimals.Add(new ShelterAnimal
        {
            Name = "Барсик",
            AnimalType = "Кот",
            Age = 2,
            HasVaccinations = true,
            AdmissionDate = new DateTime(2024, 1, 15),
            IsAdopted = false
        });

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== ПРИЮТ ДЛЯ ЖИВОТНЫХ ===");
            Console.WriteLine("1. Добавить животное");
            Console.WriteLine("2. Показать всех животных");
            Console.WriteLine("3. Поиск по кличке");
            Console.WriteLine("4. Животные без прививок");
            Console.WriteLine("5. Отметить как забранное");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите действие: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddShelterAnimal();
                    break;
                case "2":
                    ShowAllShelterAnimals();
                    break;
                case "3":
                    SearchShelterAnimalByName();
                    break;
                case "4":
                    ShowAnimalsWithoutVaccinations();
                    break;
                case "5":
                    MarkAnimalAsAdopted();
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

    private static void AddShelterAnimal()
    {
        Console.Clear();
        Console.WriteLine("=== ДОБАВЛЕНИЕ ЖИВОТНОГО ===");

        var animal = new ShelterAnimal();

        Console.Write("Кличка: ");
        animal.Name = Console.ReadLine();

        Console.Write("Вид животного: ");
        animal.AnimalType = Console.ReadLine();

        Console.Write("Возраст: ");
        int age;
        while (!int.TryParse(Console.ReadLine(), out age) || age <= 0)
        {
            Console.Write("Введите корректный возраст: ");
        }
        animal.Age = age;

        Console.Write("Есть прививки? (y/n): ");
        animal.HasVaccinations = Console.ReadLine()?.ToLower() == "y";

        Console.Write("Дата поступления (дд.мм.гггг): ");
        DateTime admissionDate;
        while (!DateTime.TryParse(Console.ReadLine(), out admissionDate))
        {
            Console.Write("Введите корректную дату (дд.мм.гггг): ");
        }
        animal.AdmissionDate = admissionDate;

        animal.IsAdopted = false;

        shelterAnimals.Add(animal);
        Console.WriteLine("Животное добавлено!");
        Console.ReadKey();
    }

    private static void ShowAllShelterAnimals()
    {
        Console.Clear();
        Console.WriteLine("=== ВСЕ ЖИВОТНЫЕ ===");

        if (!shelterAnimals.Any())
        {
            Console.WriteLine("Животные не найдены.");
        }
        else
        {
            foreach (var animal in shelterAnimals)
            {
                animal.DisplayInfo();
            }
        }
        Console.ReadKey();
    }

    private static void SearchShelterAnimalByName()
    {
        Console.Clear();
        Console.WriteLine("=== ПОИСК ПО КЛИЧКЕ ===");

        Console.Write("Введите кличку: ");
        var name = Console.ReadLine();

        var animals = shelterAnimals.Where(a => a.Name.ToLower().Contains(name.ToLower())).ToList();

        if (!animals.Any())
        {
            Console.WriteLine("Животные не найдены.");
        }
        else
        {
            foreach (var animal in animals)
            {
                animal.DisplayInfo();
            }
        }
        Console.ReadKey();
    }

    private static void ShowAnimalsWithoutVaccinations()
    {
        Console.Clear();
        Console.WriteLine("=== ЖИВОТНЫЕ БЕЗ ПРИВИВОК ===");

        var animals = shelterAnimals.Where(a => !a.HasVaccinations && !a.IsAdopted).ToList();

        if (!animals.Any())
        {
            Console.WriteLine("Все животные имеют прививки.");
        }
        else
        {
            foreach (var animal in animals)
            {
                animal.DisplayInfo();
            }
        }
        Console.ReadKey();
    }

    private static void MarkAnimalAsAdopted()
    {
        Console.Clear();
        Console.WriteLine("=== ОТМЕТИТЬ КАК ЗАБРАННОЕ ===");

        var availableAnimals = shelterAnimals.Where(a => !a.IsAdopted).ToList();

        if (!availableAnimals.Any())
        {
            Console.WriteLine("Нет доступных для усыновления животных.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Доступные для усыновления животные:");
        for (int i = 0; i < availableAnimals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {availableAnimals[i].Name} ({availableAnimals[i].AnimalType})");
        }

        Console.Write("Введите номер животного: ");
        int index;
        while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > availableAnimals.Count)
        {
            Console.Write("Введите корректный номер: ");
        }

        availableAnimals[index - 1].MarkAsAdopted();
        Console.ReadKey();
    }
}