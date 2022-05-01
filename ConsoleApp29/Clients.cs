using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp29
{
    [Serializable]
    public class Client : IComparable<Client>
    {
        public string Name { get; set; }
        public string LastName { get; set; }

        public string Addres { get; set; }

        public Client() { }
        public override string ToString()
        {
            return $"Прiзвище: {LastName}, Iм'я: {Name}, Адреса: {Addres}";
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public int CompareTo(Client other)
        {
            return LastName.CompareTo(other.LastName);
        }

    }
}
