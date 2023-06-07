using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Operator
{
    internal class OperatorLoader
    {
        public static void Load(Connection connect, string FileName)
        {
            if (File.Exists(FileName))
            {
                string[] book = File.ReadAllLines(FileName);

                foreach (string line in book)
                {
                    string[] components = line.Split(':');
                    if (components.Length == 5)
                    {
                        string name = components[0];
                        int price = Convert.ToInt32(components[1]);
                        double count = Convert.ToDouble(components[2]);
                        bool sell = Convert.ToBoolean(components[3]);
                        int date = Convert.ToInt32(components[4]);
                        connect.AddOperator(new GlobalOperators(name, price, count, sell,date));
                    }
                }
            }
            else
            {
                MessageBox.Show("Файл не сущевтсвует.");
            }
        }

        public static void Save(Connection connect, string FileName)
        {
            StreamWriter streamwriter = new StreamWriter(FileName);

            foreach (GlobalOperators operators in connect.GetOperator().ToList())
            {
                streamwriter.WriteLine(operators.ToSave());
            }
            streamwriter.Close();
        }
                        
    }
}
