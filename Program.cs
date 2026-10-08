ShoppingList list = new ShoppingList("items.txt");
if(!File.Exists("items.txt"))
    File.Create("items.txt").Dispose();
list.Load();

while (true)
{
    Console.Clear();
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
        while(name.Contains(';') || name =="")
        {
            Console.WriteLine("Namenet kan inte innehålla karaktären ';' eller vara tom");
            Console.Write("Namn: ");
            name = Console.ReadLine();
        }

        Console.Write("Pris: ");
        int price;
        while(!int.TryParse(Console.ReadLine(), out price) || price <= 0)
            Console.WriteLine("Skriv ett nummber som är över 0");
        list.Add(new Item(name, price));
    }
    else if (choice == 2)
    {
        if(list.ListCount() != 0)
        {
            Console.Write("Nummer: ");
            int number;
            while(!int.TryParse(Console.ReadLine(), out number) || number <= 0 || number > list.ListCount())
                Console.WriteLine($"Skriv ett nummer mellan 1 och {list.ListCount()}");
            list.RemoveAt(number);
        }
        else
            Console.WriteLine("Listan är tom");
        
        Console.Read();
    }
    else if (choice == 3)
    {
        list.Save();
        Console.Read();
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
        Console.Read();
    }
    else if (choice == 5)
    {
        break;
    }
}
