using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp29
{
    [Serializable]
    public class Payments //: IComparable<Payments>
    {
        public double WaterBill { get; set; }
        public double EnergyBill { get; set; }
        public double GasBill { get; set; }
        public Payments() { }
        public void AddBills(Payments pay)
        {
            this.WaterBill = pay.WaterBill;
            this.EnergyBill = pay.EnergyBill;
            this.GasBill = pay.GasBill;
        }

        public override string ToString()
        {
            return $"Bill for Water: {WaterBill} grn, Bill for Energy: {EnergyBill} grn, Bill for Gas: {GasBill} grn.";
        }
        /*
        public int CompareTo(Payments other)
        {
            return WaterBill.CompareTo(other.WaterBill);
        }
        */
    }
}
