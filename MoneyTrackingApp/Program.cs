


using MoneyTrackingApp;

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("***********************************");
Console.WriteLine("*      Money Tracking App      *");
Console.WriteLine("***********************************");
Console.ForegroundColor = ConsoleColor.White;
string userSelection;
Console.ForegroundColor = ConsoleColor.Blue;


do
{
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("Your Account Balance is : "+ItemRepository.Balance);
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.DarkBlue;
    Console.WriteLine("********************");
    Console.WriteLine("* Select an action *");
    Console.WriteLine("********************");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("1: Add an item");
    Console.WriteLine("2: View all item");
    Console.WriteLine("3: View only incomes");
    Console.WriteLine("4: View only expenses");
    Console.WriteLine("5: Save data");
    Console.WriteLine("6: Load data");
    Console.WriteLine("7: Edit item");
    Console.WriteLine("8: Remove item");
    Console.WriteLine("0: Quit application");
    Console.ForegroundColor = ConsoleColor.DarkBlue;
    Console.Write("Your selection: ");
    userSelection = Console.ReadLine() ?? string.Empty;
    Console.ForegroundColor = ConsoleColor.White;
    switch (userSelection)
    {
        case "1":
            Service.AddItem();
            break;
        case "2":
            Service.ViewAllItems();
            break;
        case "3":
            ItemRepository.ViewOnlyIncomes();
            break;
        case "4":
            ItemRepository.ViewOnlyExpenses();
            break;
        case "5":
            ItemRepository.SaveItems();
            break;
        case "6":
            ItemRepository.LoadItems();
            break;
        case "7":
            ItemRepository.EditItem();
            break;
        case "8":
            ItemRepository.RemoveItem();
            break;
        case "0": 
            break;
        default:
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Invalid selection. Please try again.");
            Console.ForegroundColor = ConsoleColor.White;
            break;
    }
}
while (userSelection != "0");

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Thanks for using the application");