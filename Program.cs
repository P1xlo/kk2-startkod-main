ShoppingList list = new ShoppingList("items.txt");
list.Load();

while (true)
{
    Console.WriteLine();
    list.Print();
    Console.WriteLine();
    Console.WriteLine("1. Lägg till vara");
    Console.WriteLine("2. Ta bort vara");
    Console.WriteLine("3. Spara");
    Console.WriteLine("4. Sök vara");
    Console.WriteLine("5. Avsluta");
    Console.Write("Välj: ");

    int choice; 
    while(!int.TryParse(Console.ReadLine(),out choice) || choice < 1 || choice > 5)
        Console.WriteLine("Skriv ett nummer mellan 1 och 5");

    if (choice == 1)
    {
        Console.Write("Namn: ");
        string name = Console.ReadLine();
        Console.Write("Pris: ");
        int price;
        while(!int.TryParse(Console.ReadLine(), out price) || price <= 0)
            Console.WriteLine("Skriv ett nummber som är över 0");
        list.Add(new Item(name, price));
    }
    else if (choice == 2)
    {
        Console.Write("Nummer: ");
        int number;
        while(!int.TryParse(Console.ReadLine(), out number) || number <= 0 || number > list.ListCount())
            Console.WriteLine($"Skriv ett nummer mellan 1 och {list.ListCount()}");
        list.RemoveAt(number);
    }
    else if (choice == 3)
    {
        list.Save();
    }
    else if (choice == 4)
    {
        Console.Write("Namn att söka efter: ");
        string wanted = Console.ReadLine();
        Item found = list.Find(wanted);

        if (found == null)
        {
            Console.WriteLine("Varan finns inte i listan.");
        }
        else
        {
            Console.WriteLine($"Hittade: {found}");
        }
    }
    else if (choice == 5)
    {
        break;
    }
}
