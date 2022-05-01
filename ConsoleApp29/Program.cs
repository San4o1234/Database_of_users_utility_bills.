using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using static System.Console;

namespace ConsoleApp29    
{    
    class Program
    {
        
        public static SortedDictionary<Client, Payments> ClientBills = new SortedDictionary<Client, Payments>();
        
        static void ClientsList()
        {
            int n = 1;
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("Список користувачiв i платежiв ЖЕКу : \n");
            ForegroundColor = ConsoleColor.Green;
            foreach (KeyValuePair<Client, Payments> client in ClientBills)
            {
                WriteLine($"{n++}) {client.Key}; \n  {client.Value} \n");
            }
        }
        static void ResetBills(string name, Payments pay)
        {
            WriteLine($"Користувач: {name}, платежi: {pay}");
            foreach (Client client in ClientBills.Keys)
            {
                if (client.LastName == name)
                    (ClientBills[client] as Payments).AddBills(pay); 
            }
        }

        static void RemoveClient(string name)
        {
            foreach(Client client in ClientBills.Keys)
            {
                if (client.LastName == name)
                    ClientBills.Remove(client);
            }
        }
        static bool ContainsClient(string name)
        {
            foreach(Client client in ClientBills.Keys)
            {
                if (client.LastName == name)
                    return true;
                
            }
            return false;
        }

        static void MySerialize(SortedDictionary<Client, Payments> pairs)
        {
            BinaryFormatter serializer = new BinaryFormatter(); // (typeof(SortedDictionary<Client, Payments>));
            try
            {
                using(Stream fstream = File.Create("Base.bin"))
                {
                    serializer.Serialize(fstream, pairs); 
                }
                WriteLine("Save Base !!! ");
            }
            catch (Exception e)
            {
                WriteLine(e.Message);
            }
        }
        static SortedDictionary<Client, Payments> MyDeSerialize()
        {
            BinaryFormatter serializer = new BinaryFormatter(); // (typeof(SortedDictionary<Client, Payments>));
            SortedDictionary<Client, Payments> myBasePair = null;
            try
            {
                int n = 0;
                
                using(Stream fstream = File.OpenRead("Base.bin"))
                {
                    myBasePair = (SortedDictionary<Client, Payments>)serializer.Deserialize(fstream);
                }
                foreach(KeyValuePair<Client, Payments> pairs in myBasePair)
                {
                    WriteLine($"{++n}) {pairs.Key}; \n  {pairs.Value} \n");
                }                
            }
            catch (Exception e)
            {
                WriteLine(e.Message);
            }
            return myBasePair;
        }

        static void Menu()
        {
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("\n\t Привiт.\n\n Це база данних користувачiв i платежiв у ЖЕКу.");
           
            bool ext = false;

            do
            {
                ForegroundColor = ConsoleColor.Cyan;

                WriteLine("\n Введiть потрiбну функцiю ( ): \n ");
                WriteLine(" (View) - Вивести базу на екран. \n (Add) - Додати користувача. \n (Reset) - Змiнити платежi. \n (Save) - Зберегти базу користувачiв. " +
                    "\n (Load) - Завантажити базу користувачiв . \n (Del) - Видалити користувача. \n (Exit) - Вийти з програми.\n");

                try
                {
                    ForegroundColor = ConsoleColor.Green;
                    string input = ReadLine();
                    switch (input)
                    {
                        case "view":
                        case "View":
                            if (ClientBills.Count == 0)
                                WriteLine("\nБаза ще пуста, спершу додайте користувачiв, або завантажте базу.");
                            else
                            {
                                Clear();
                                WriteLine("\n");
                                ClientsList();
                            }
                            break;
                        case "add":
                        case "Add":
                            WriteLine("\nВведiть прiзвище користувача.");
                            string lnam = ReadLine();
                            WriteLine("\nВведiть iм'я користувача.");
                            string nam = ReadLine();
                            WriteLine("\nВведiть адрес користувача.");
                            string adrs = ReadLine();
                            if (ContainsClient(lnam))
                            {
                                Clear();
                                WriteLine("\n");
                                ForegroundColor = ConsoleColor.Red;
                                WriteLine("Такий користувач уже iснує.");
                            }
                            else
                            {
                                WriteLine("\nВведiть платежi користувача(грн.)  за Воду:");
                                int wat1 = int.Parse(ReadLine());
                                WriteLine("\nЗа Свiтло:");
                                int en1 = int.Parse(ReadLine());
                                WriteLine("\nЗа Газ:");
                                int gas1 = int.Parse(ReadLine());
                                ClientBills.Add(new Client { Name = nam, LastName = lnam, Addres = adrs }, new Payments { WaterBill = wat1, EnergyBill = en1, GasBill = gas1 });
                                Clear();
                                WriteLine("\n");
                                WriteLine("Користувача додано.");
                            }                            
                            break;
                        case "reset":
                        case "Reset":
                            WriteLine("\nВведiть прiзвище користувача.");
                            string key = ReadLine();
                            if (!ContainsClient(key))
                            {
                                Clear();
                                WriteLine("\n");
                                ForegroundColor = ConsoleColor.Red;
                                WriteLine("Такий користувач вiдсутнiй.");
                            }
                            else
                            {
                                WriteLine("\nВведiть платiж (грн.) за Воду:");
                                int wat = int.Parse(ReadLine());
                                WriteLine("\nВведiть платiж (грн.) за Світло:");
                                int en = int.Parse(ReadLine());
                                WriteLine("\nВведiть платiж (грн.) за Газ:");
                                int gas = int.Parse(ReadLine());
                                ResetBills(key, new Payments { WaterBill = wat, EnergyBill = en, GasBill = gas });
                                Clear();
                                WriteLine("\n");
                                WriteLine("Данi змiнено. ");
                            }
                            break;
                        case "save":
                        case "Save":
                            MySerialize(ClientBills);
                            Clear();
                            WriteLine("\n");
                            WriteLine("Базу користувачiв збережено.");
                            break;

                        case "load":
                        case "Load":
                            WriteLine();
                            ClientBills = MyDeSerialize();
                            Clear();
                            WriteLine("\n");
                            WriteLine("\nЗавантажена база користувачiв. Виберiть (View) щоб її переглянути.");
                            break;

                        case "del":
                        case "Del":
                            WriteLine("\nВведiть прiзвище користувача для видалення\n");
                            string del = ReadLine();
                            if (!ContainsClient(del))
                            {
                                ForegroundColor = ConsoleColor.Red;
                                WriteLine("\nТакий користувач вiдсутнiй.");
                            }
                            else
                            {
                                RemoveClient(del);
                                Clear();
                                WriteLine("\n");
                                WriteLine("Користувача видалено. ");
                            }
                            break;
                        case "Exit":
                        case "exit":
                            ext = true;
                            break;
                        default:
                            Clear();
                            WriteLine("\n");
                            throw new Exception("Неправильний ввiд функцiї.");
                    }
                }
                catch (Exception e)
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine(e.Message);
                }
            } while (!ext);
        }


        static void Main(string[] args)
        {
            Menu();
            WriteLine("\n\n");            
            
        }
    }
}
