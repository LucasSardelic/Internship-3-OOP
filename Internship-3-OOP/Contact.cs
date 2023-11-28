
using System;

namespace Internship_3_OOP
{
    public class Contact
    {
        public Contact(string name, int number, string preference)
        {
            Name = name;
            Number = number;
            Preference = preference;
        }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Preference { get; set; }

        public void newContact()
        {
            Console.WriteLine("Unesite ime i prezime: ");
            Name= Console.ReadLine();
            Console.WriteLine("Unesite broj telefona: ");
            Number=isInt();
            setPreference();
        }

        public void setPreference()
        {
            Console.WriteLine("Koja je vasa preferenca za ovaj kontakt?\n\t1-favorit\n\t2-normalan\n\t3-blokiran");

            do
            {
                int preferenceInquiry = 0;
                preferenceInquiry = isInt();

                switch (preferenceInquiry)
                {
                    case 1:
                        Preference = "favourite";
                        break;
                    case 2:
                        Preference = "normal";
                        break;
                    case 3:
                        Preference = "blocked";
                        break;
                    default:
                        break;
                }

                if (preferenceInquiry > 0 && preferenceInquiry < 4)
                    break;

                Console.WriteLine("Upisite broj od 1 do 3!");
            } while (true);
            
        }

        public int isInt()
        {
            int saidInt = 0;
            do
            {
                if (int.TryParse(Console.ReadLine(), out saidInt))
                    break;
                Console.WriteLine("Upisi broj");
            } while (true);
            return saidInt;
        }
    }
}
