using Internship_3_OOP;

var Library = new Dictionary<Contact, List<Call>>
{
    {new Contact("Petar Golem", 1, "favourite"), new List<Call>(){new Call()} }
};

int lastingCall = 0;
bool exit = false;

do
{
    Console.Clear();
    Console.WriteLine("1. Ispis svih kontakata\n2. Dodavanje novih kontakata u imenik\n" +
        "3. Brisanje kontakata iz imenika\n4. Editiranje preference kontakta\n" +
        "5. Upravljanje kontaktom\n6. Ispis svih poziva\n0. Izlaz iz aplikacije");
    var input= Console.ReadLine();

    if (input == "1")
    {
        WriteAllContacts(Library);
        Console.ReadKey();
    }
    else if (input == "2")
        NewContact(Library);
    else if (input == "3")
        DeleteContact(Library);
    else if (input == "4")
        EditPreference(Library);
    else if (input == "5")
        SubMenu(Library, lastingCall);
    else if (input == "6")
        WriteCallHistory(Library, lastingCall);
    else if (input == "0")
        exit = true;

} while (exit == false);

static void WriteAllContacts(Dictionary<Contact, List<Call>> Library)
{
    Console.Clear();
    Console.WriteLine("Postojeci kontakti:");

    foreach (var item in Library)
    {
        Console.WriteLine($"\t{item.Key.Name}\t{item.Key.Number}\t{item.Key.Preference}");
    }
    Console.WriteLine("\n");
}

static void NewContact(Dictionary<Contact, List<Call>> Library)
{
    var fillerContact = new Contact("a", 1, "a");
    fillerContact.newContact();
    int overlap = 0;

    foreach (var item in Library)
    {
        if (item.Key.Name == fillerContact.Name)
        {
            overlap = 1;
            Console.WriteLine("Kontakt vec postoji!");
            Console.ReadKey();
        }

        if (item.Key.Number == fillerContact.Number)
        {
            overlap = 1;
            Console.WriteLine("Broj je vec zauzet!");
            Console.ReadKey();
        }
    }

    if (overlap == 0)
        Library.Add(fillerContact, new List<Call>());
}

static void DeleteContact(Dictionary<Contact, List<Call>> Library)
{
    WriteAllContacts(Library);
    Console.WriteLine("Koji kontakt zelite izbrisati?");
    var contactDelete = Console.ReadLine();
    bool isContact = false;

    foreach (var item in Library)
    {
        if (item.Key.Name == contactDelete)
        {
            Library.Remove(item.Key);
            isContact = true;
        }

        if (!isContact)
        {
            Console.WriteLine("Kontakt ne postoji!");
            Console.ReadKey();
        }
    }
}

static void EditPreference(Dictionary<Contact, List<Call>> Library)
{
    WriteAllContacts(Library);
    Console.WriteLine("Kojem kontaktu zelite promijeniti preferencu?");
    var contactDelete = Console.ReadLine();
    bool isContact = false;

    foreach (var item in Library)
    {
        if (item.Key.Name == contactDelete)
        {
            Console.WriteLine($"Trenutna preferenca: {item.Key.Preference}");
            var fillerContact = new Contact(item.Key.Name, item.Key.Number, "a");
            fillerContact.setPreference();
            Library.Add(fillerContact, item.Value);
            Library.Remove(item.Key);
            break;

        }

        Console.WriteLine("Kontakt ne postoji!");
        Console.ReadKey();
    }
}

static void WriteCallHistory(Dictionary<Contact, List<Call>> Library, int lastingCall)
{
    Console.Clear();
    var check = IsOngoing(Library, lastingCall);

    foreach (var item in Library)
    {
        foreach (var subItem in item.Value)
        {
            Console.WriteLine($"{item.Key.Name}\t{item.Key.Number}\t{item.Key.Preference}\t--\t{subItem.CallTime}\t{subItem.CallState}");
        }
    }

    Console.ReadKey();
}

static void SubMenu(Dictionary<Contact, List<Call>> Library, int lastingCall)
{
    WriteAllContacts(Library);
    Console.WriteLine("S kojim kontaktom zelite upravljati?");
    var contactManip = Console.ReadLine();
    bool isContact = false;
    int newLastingCall = lastingCall;

    foreach (var item in Library)
    {
        if (item.Key.Name == contactManip)
        {
            isContact = true;
        }
    }

    if (!isContact)
    {
        Console.WriteLine("Kontakt ne postoji!");
        Console.ReadKey();
    }

    while (isContact == true)
    {
        Console.Clear();
        Console.WriteLine("1. Ispis svih poziva sa tim kontaktom\n2. Kreiranje novog poziva\n3. Izlaz iz podmenua");
        var input = Console.ReadLine();

        if (input == "1")
        {
            WriteAllCalls(Library, contactManip, lastingCall);
        }
        else if (input == "2")
        {
            newLastingCall=CreateNewCall(Library, contactManip, newLastingCall);
        }
        else if (input == "3")
            isContact = false;
    }
}

static void WriteAllCalls(Dictionary<Contact, List<Call>> Library, string contactManip, int lastingCall)
{
    var check = IsOngoing(Library, lastingCall);

    foreach (var item in Library)
    {
        if (item.Key.Name==contactManip)
        {
            var fillerList = new List<Call>(item.Value);
            var sortedList = fillerList.OrderBy(o => o.CallTime).ToList();

            foreach (var subItem in sortedList)
            {
                Console.WriteLine($"\t{subItem.CallTime}\t{subItem.CallState}");
            }
        }
    }

    Console.ReadKey();
}

static int CreateNewCall(Dictionary<Contact, List<Call>> Library, string contactManip, int lastingCall)
{
    foreach(var item in Library)
    {
        if (item.Key.Name==contactManip && item.Key.Preference!="blocked")
        {
            var fillerCall = new Call();
            fillerCall.newCall();

            if(fillerCall.CallState=="u tijeku" && IsOngoing(Library, lastingCall) == false)
            {
                item.Value.Add(fillerCall);
                var randomNum = new Random();
                lastingCall = randomNum.Next(1, 21);
                Console.WriteLine($"Poziv je u tijeku, trajat ce {lastingCall} sekundi." +
                    $" Za to vrijeme drugi pozivi nece moci biti u tijeku.");
                Console.ReadKey();
            }
            else if (fillerCall.CallState == "u tijeku" && IsOngoing(Library, lastingCall) == true)
            {
                Console.WriteLine("Samo jedan poziv moz biti u tijeku odjednom!");
                Console.ReadKey();
            }
            else
            {
                item.Value.Add(fillerCall);
                Console.WriteLine($"Poziv je {fillerCall.CallState}.");
                Console.ReadKey();
            }
        }
        else if(item.Key.Name == contactManip && item.Key.Preference == "blocked")
        {
            Console.WriteLine("Ovaj kontakt je blokiran, ne mozete obavljati pozive sa njime!");
            Console.ReadKey();
        }
    }

    return lastingCall;
}

static bool IsOngoing(Dictionary<Contact, List<Call>> Library, int lastingCall)
{
    bool emergencyBreak = false;

    foreach (var item in Library)
    {
        foreach (var subItem in item.Value)
        {
            if(subItem.CallState == "u tijeku" && lastingCall > (int)(DateTime.Now - subItem.CallTime).TotalSeconds)
            {
                return true;
            }
            else if(subItem.CallState == "u tijeku" && lastingCall < (int)(DateTime.Now - subItem.CallTime).TotalSeconds)
            {
                emergencyBreak = true;
                var fillerCall=new Call();
                fillerCall.callSet(subItem.CallTime, "zavrsen");
                item.Value.Remove(subItem);
                item.Value.Add(fillerCall);
                return false;
            }
        }

        if (emergencyBreak == true)
            break;
    }
    return false;
}