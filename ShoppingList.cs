// Holds the items and takes care of loading and saving them.
class ShoppingList
{
    private List<Item> items = new List<Item>();
    private string path;

    public int Budget { get; }

    public ShoppingList(string path, int budget)
    {
        this.path = path;
        Budget = budget;
    }

    // Adds the item if it fits within the budget.
    // Returns false instead of throwing, because going over the budget is a normal answer, not an error.
    public bool Add(Item item)
    {
        if (Total() + item.Price > Budget)
        {
            return false;
        }

        items.Add(item);
        return true;
    }

    // Removes the item the user sees as number 1, 2, 3 ...
    public void RemoveAt(int number)
    {
        Console.WriteLine($"tog bort {items.ElementAt(number -1).Name} som kostade {items.ElementAt(number -1).Price}");
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

        // Writing can fail even when every item is valid, e.g. if the file is open
        // in another program (IOException) or is read-only (UnauthorizedAccessException).
        try
        {
            //changed from WriteAllText to WriteAllLines match with the change in load()
            File.WriteAllLines(path, lines);
            Console.WriteLine("Listan är sparad.");
        }
        catch (IOException)
        {
            Console.WriteLine("Kunde inte spara listan. Filen kan vara öppen i ett annat program.");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Kunde inte spara listan. Programmet har inte behörighet att skriva till filen.");
        }
    }

    // Reads the file back into the list.
    public void Load()
    {
        int skipped = 0;

        //changed to ReadAllLines to remove the text.Split('\n') as it left a '\r' behind that messed with the program.
        //And  ReadAllLines  \r\n
        foreach (string line in File.ReadAllLines(path))
        {
            //if the line is empty it is skipped
            if (line.Trim() == "") continue;

            string[] parts = line.Split(';');

            // A valid line is exactly "price;name" where the price is a number.
            if (parts.Length != 2 || !int.TryParse(parts[0], out int price))
            {
                skipped++;
                continue;
            }

            try
            {
                // Added straight to the list and not through Add(), so items that are
                // already saved are never lost because of the budget.
                //The .Trim() removes any leftover \r that would break the program.
                items.Add(new Item(parts[1].Trim(), price));
            }
            catch (ArgumentException)
            {
                // Empty name or negative price. ArgumentOutOfRangeException is caught here too.
                skipped++;
            }
        }

        if (skipped > 0)
        {
            Console.WriteLine($"{skipped} trasiga rader i {path} hoppades över.");
            Console.ReadLine();
        }
    }

    public int ListCount()
    {
        return items.Count;
    }
}
