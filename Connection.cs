using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Operator
{
    internal class Connection
    {
        List<Operator> OperatorList;
        public Connection()
        {
            OperatorList = new List<Operator>();
        }

        public void AddOperator(GlobalOperators operators)
        {
            OperatorList.Add(operators);
        }

        public bool CheckContainsOperator(string operatorname)
        {
            bool NotFound = true;

            foreach (GlobalOperators Contact in OperatorList)
            {
                if (Contact.Name.Contains(operatorname))
                {
                    NotFound = false;
                    break;
                }
            }
            return NotFound;
        }

        public List<Operator> GetOperator() { return OperatorList; }

        public List<Operator> SearchOperator(Connection connect, string operatorname)
        {
            bool search = false;
            List<Operator> searchResults = new List<Operator>();
            foreach (GlobalOperators operators in connect.GetOperator())
            {
                if (operators.Name.Contains(operatorname))
                {
                    searchResults.Add(operators);
                    search = true;
                }
            }

            if (search == false)
            {
                MessageBox.Show("Не найдено.");
            }

            return searchResults;
        }

        public void RemoveOperator(Connection connect,int index)
        {
            OperatorList.RemoveAt(index);
        }
    }
}
