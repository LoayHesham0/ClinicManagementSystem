using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class company
    {
        public string name;
        public int discount;
        public string notes;
    public company (string name, int discount, string notes)
       {
            this.name= name;
            this.discount = discount;
            this.notes = notes;
       }
        public override string ToString()
        {
            return name.ToString() + " => " + "Discount Percentage : " + discount.ToString();
        }
    }
}
