using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operator
{
    internal class Operator
    {
        public string Name { get; set; }

        public int Price { get; set; }

        public double Square { get; set; }

        public bool Sell { get; set; }

        public Operator() { }
        public Operator(string name, int price, double square, bool sell)
        {
            Name = name;
            Price = price;
            Square = square;
            Sell = sell;
        }

        public double Quality()
        {
            return 100 * Square / Price;
        }
        public virtual double OperatorQuality()
        {
            if (Sell == true)
            {
                return Math.Round(Quality() + 1.5);
            }
            else
            {
                return Math.Round(Quality() + 0.7);
            }

        }
        public string GetInfo()
        {
            return $"{Name}; {Price}; {Square}; {Sell}";
        }

        public string ToSave()
        {
            return $"{Name}: {Price}: {Square}: {Sell}";
        }
    }
}
