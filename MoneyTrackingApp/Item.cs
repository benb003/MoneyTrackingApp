namespace MoneyTrackingApp;

public class Item
{
    public string Title { get; set; }
    public int Amount { get; set; }
    public DateTime Date { get; set; }

    public ItemType Type;

    public Item(string title, int amount, DateTime date, ItemType type)
    {
        Title = title;
        Amount = amount;
        Date = date;
        Type = type;
    }
    
    public override string ToString()
    {
        return Type.ToString().PadRight(15) + Title.PadRight(15) + Amount.ToString().PadRight(15) +
               Date.ToShortDateString().PadRight(20);
    }
}