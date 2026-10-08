// Holds the items and takes care of loading and saving them.
class ShoppingList
{
    private List<Item> items = new List<Item>();
    private string path;

    public ShoppingList(string path)
    {
        this.path = path;
    }

    public void Add(Item item)
    {
        items.Add(item);
    }

    // Removes the item the user sees as number 1, 2, 3 ...
    public void RemoveAt(int number)
    {
        items.RemoveAt(number - 1);
    }

    // Adds up the price of every item on the list.
    public int Total()
    {
        int sum = 0;
        // made i = 0 instead of 1
        for (int i = 0; i < items.Count; i++)
        {
            sum += items[i].Price;
        }

        return sum;
    }

    // Looks up an item by its name. Returns null if there is no such item.
    public Item Find(string name)
    {
        foreach (Item item in items)
        {
            if (item.Name == name)
            {
                return item;
            }
        }

        return null;
    }

    public void Print()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i]}");
        }

        Console.WriteLine($"Totalt: {Total()} kr");
    }

    // Writes one item per line, as "price;name".
    public void Save()
    {
        List<string> lines = new List<string>();

        foreach (Item item in items)
        {
            lines.Add($"{item.Price};{item.Name}");
        }
        //changed from WriteAllText to WriteAllLines match with the change in load()
        File.WriteAllLines(path, lines);
        

        Console.WriteLine("Listan är sparad.");
    }

    // Reads the file back into the list.
    public void Load()
    {
        //changed to ReadAllLines to remove the text.Split('\n') as it left a '\r' behind that messed with the program.
        //And  ReadAllLines  \r\n
        foreach (string line in File.ReadAllLines(path))
        {
            //if the line is empty it is skipped
            if (line.Trim() == "") continue;

            string[] parts = line.Split(';');
            //The .Trim() removes any leftover \r that would break the program.
            items.Add(new Item(parts[1].Trim(), int.Parse(parts[0])));
        }
    }

    public int ListCount()
    {
        return items.Count;
    }
}
