using MoneyTrackingApp;

public class Service
{
    public static void AddItem()
    {
        Console.WriteLine("Do you want to create an income or expense? (for income press i for expense press e)");
        Console.Write("Your selection : ");
        string typeOfItem = Console.ReadLine() ?? String.Empty;
        if (!(typeOfItem.ToLower() == "i" || typeOfItem.ToLower() == "e"))
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Invalid Selection. Try Again.");
            Console.ForegroundColor = ConsoleColor.White;
            return;
        }
        Console.Write("Enter title : ");
        string title = Console.ReadLine() ?? String.Empty;
        Console.Write("Enter amount : ");
        bool isValidAmount = int.TryParse(Console.ReadLine(), out int amount);
        if (!isValidAmount)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Invalid amount. Try Again.");
            Console.ForegroundColor = ConsoleColor.White;
            return;
        }
        Console.Write("Enter date (format : yyyy-mm-dd) : ");
        bool isValidDate = DateTime.TryParse(Console.ReadLine(), out DateTime date);
        if (!isValidDate)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Invalid date. Try Again.");
            Console.ForegroundColor = ConsoleColor.White;
            return;
        }

        if (typeOfItem == "e")
        {
            Item item = new Item(title, amount, date, ItemType.Expense);
            ItemRepository.SaveItem(item);
            ItemRepository.Balance -= amount;
        }
        else if (typeOfItem == "i")
        {
            Item item = new Item(title, amount, date,ItemType.Income);
            ItemRepository.SaveItem(item);
            ItemRepository.Balance += amount;
        }
        else
        {
            Console.WriteLine("something wrong");
        }
        Console.WriteLine("Your item was added.");
    }

    public static void ViewAllItems()
    {
        ItemRepository.ShowAllItems();
    }

    
}