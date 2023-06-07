using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Operator
{
    internal class GlobalOperators : Operator
    {
        public int Date { get; set; }

        public GlobalOperators(string name,int price,double square,bool sell,int date)
        {
            Name = name;
            Price = price;
            Square = square;
            Sell = sell;
            Date = date;
        }        
        public virtual double GlobalQuality()
        {            
            return OperatorQuality()*Date;
        }
        public string GetInfo()
        {
            return $"{Name}; {Price}; {Square}; {Sell}; {Date}";
        }

        public string ToSave()
        {
            return $"{Name}: {Price}: {Square}: {Sell}: {Date}";
        }
    }
}
