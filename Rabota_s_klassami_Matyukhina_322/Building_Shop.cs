using System;
using System.Collections.Generic;
using System.Linq;

public class BuildingItem
{
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int MinStock { get; set; }

    public void DisplayInfo()
    {
        Console.WriteLine($"Товар: {Name}");
        Console.WriteLine($"Категория: {Category}");
        Console.WriteLine($"Цена: {Price:C}");
        Console.WriteLine($"Количество: {Quantity}");
        Console.WriteLine($"Минимальный остаток: {MinStock}");
        if (Quantity < MinStock)
        {
            Console.WriteLine("⚠️ ВНИМАНИЕ: Низкий остаток!");
        }
        Console.WriteLine(new string('-', 40));
    }

    public void ReduceQuantity(int amount)
    {
        if (amount <= Quantity)
        {
            Quantity -= amount;
            Console.WriteLine($"Количество уменьшено на {amount}. Остаток: {Quantity}");
        }
        else
        {
            Console.WriteLine("Недостаточно товара на складе!");
        }
    }

    public void CheckStock()
    {
        if (Quantity < MinStock)
        {
            Console.WriteLine($"⚠️ ВНИМАНИЕ: Товар '{Name}' имеет низкий остаток ({Quantity})!");
        }
    }
}

public class Program
{
    private static List<BuildingItem> buildingItems = new List<BuildingItem>();

    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Тестовые данные
        buildingItems.Add(new BuildingItem
        {
            Name = "Молоток",
            Category = "Инструмент",
            Price = 1500,
            Quantity = 15,
            MinStock = 5
        });

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== СТРОИТЕЛЬНЫЙ МАГАЗИН ===");
            Console.WriteLine("1. Добавить товар");
            Console.WriteLine("2. Показать все товары");
            Console.WriteLine("3. Поиск по названию");
            Console.WriteLine("4. Уменьшить количество товара");
            Console.WriteLine("5. Проверить остатки");
            Console.WriteLine("6. Удалить товар");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите действие: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddBuildingItem();
                    break;
                case "2":
                    ShowAllBuildingItems();
                    break;
                case "3":
                    SearchBuildingItemByName();
                    break;
                case "4":
                    ReduceBuildingItemQuantity();
                    break;
                case "5":
                    CheckAllBuildingItemsStock();
                    break;
                case "6":
                    DeleteBuildingItem();
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

    private static void AddBuildingItem()
    {
        Console.Clear();
        Console.WriteLine("=== ДОБАВЛЕНИЕ ТОВАРА ===");

        var item = new BuildingItem();

        Console.Write("Название товара: ");
        item.Name = Console.ReadLine();

        Console.Write("Категория: ");
        item.Category = Console.ReadLine();

        Console.Write("Цена: ");
        decimal price;
        while (!decimal.TryParse(Console.ReadLine(), out price) || price <= 0)
        {
            Console.Write("Введите корректную цену: ");
        }
        item.Price = price;

        Console.Write("Количество: ");
        int quantity;
        while (!int.TryParse(Console.ReadLine(), out quantity) || quantity < 0)
        {
            Console.Write("Введите корректное количество: ");
        }
        item.Quantity = quantity;

        Console.Write("Минимальный остаток: ");
        int minStock;
        while (!int.TryParse(Console.ReadLine(), out minStock) || minStock < 0)
        {
            Console.Write("Введите корректный минимальный остаток: ");
        }
        item.MinStock = minStock;

        buildingItems.Add(item);
        Console.WriteLine("Товар добавлен!");
        Console.ReadKey();
    }

    private static void ShowAllBuildingItems()
    {
        Console.Clear();
        Console.WriteLine("=== ВСЕ ТОВАРЫ ===");

        if (!buildingItems.Any())
        {
            Console.WriteLine("Товары не найдены.");
        }
        else
        {
            foreach (var item in buildingItems)
            {
                item.DisplayInfo();
            }
        }
        Console.ReadKey();
    }

    private static void SearchBuildingItemByName()
    {
        Console.Clear();
        Console.WriteLine("=== ПОИСК ПО НАЗВАНИЮ ===");

        Console.Write("Введите название товара: ");
        var name = Console.ReadLine();

        var items = buildingItems.Where(i => i.Name.ToLower().Contains(name.ToLower())).ToList();

        if (!items.Any())
        {
            Console.WriteLine("Товары не найдены.");
        }
        else
        {
            foreach (var item in items)
            {
                item.DisplayInfo();
            }
        }
        Console.ReadKey();
    }

    private static void ReduceBuildingItemQuantity()
    {
        Console.Clear();
        Console.WriteLine("=== УМЕНЬШЕНИЕ КОЛИЧЕСТВА ТОВАРА ===");

        ShowAllBuildingItems();

        if (!buildingItems.Any())
        {
            Console.ReadKey();
            return;
        }

        Console.Write("Введите номер товара (по порядку): ");
        int index;
        while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > buildingItems.Count)
        {
            Console.Write("Введите корректный номер: ");
        }

        Console.Write("Введите количество для уменьшения: ");
        int amount;
        while (!int.TryParse(Console.ReadLine(), out amount) || amount <= 0)
        {
            Console.Write("Введите корректное количество: ");
        }

        buildingItems[index - 1].ReduceQuantity(amount);
        Console.ReadKey();
    }

    private static void CheckAllBuildingItemsStock()
    {
        Console.Clear();
        Console.WriteLine("=== ПРОВЕРКА ОСТАТКОВ ===");

        bool hasLowStock = false;

        foreach (var item in buildingItems)
        {
            if (item.Quantity < item.MinStock)
            {
                item.CheckStock();
                hasLowStock = true;
            }
        }

        if (!hasLowStock)
        {
            Console.WriteLine("Все товары в норме.");
        }
        Console.ReadKey();
    }

    private static void DeleteBuildingItem()
    {
        Console.Clear();
        Console.WriteLine("=== УДАЛЕНИЕ ТОВАРА ===");

        ShowAllBuildingItems();

        if (!buildingItems.Any())
        {
            Console.ReadKey();
            return;
        }

        Console.Write("Введите номер товара для удаления: ");
        int index;
        while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > buildingItems.Count)
        {
            Console.Write("Введите корректный номер: ");
        }

        var item = buildingItems[index - 1];
        Console.Write($"Вы уверены, что хотите удалить товар '{item.Name}'? (y/n): ");
        var confirm = Console.ReadLine();

        if (confirm?.ToLower() == "y")
        {
            buildingItems.RemoveAt(index - 1);
            Console.WriteLine("Товар удален!");
        }
        else
        {
            Console.WriteLine("Удаление отменено.");
        }
        Console.ReadKey();
    }
}