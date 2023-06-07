using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Operator
{
    public partial class Form1 : Form
    {
        private static Connection connect=new Connection();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("operator.txt"))
            {
                OperatorLoader.Load(connect, "operator.txt");
                RefreshList();
            }
            else
            {
                MessageBox.Show("Файл не существует.");
            }
        }
        public void RefreshList()
        {
            listBox1.Items.Clear();
            foreach (GlobalOperators operators in connect.GetOperator())
            {
                listBox1.Items.Add(operators.GetInfo());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Sorted = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string SearchComponent = textBox5.Text;
            if (SearchComponent != "")
            {
                List<Operator> searchResults = connect.SearchOperator(connect, SearchComponent);
                listBox1.Items.Clear();
                foreach (Operator contact in searchResults)
                {
                    listBox1.Items.Add(contact.GetInfo());
                }
            }
        }
        static void AddOperator(string TextName, string TextPrice, string TextCount, bool TextSell,string TextDate)
        {
            GlobalOperators operators = new GlobalOperators(TextName, Convert.ToInt32(TextPrice), Convert.ToDouble(TextCount), Convert.ToBoolean(TextSell),Convert.ToInt32(TextDate));
            connect.AddOperator(operators);            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OperatorLoader.Save(connect, "operator.txt");
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            string message;

            message = CheckName(textBox1.Text);

            if (message == "")
            {
                message = CheckNumberTextBox(textBox2.Text, textBox3.Text,textBox5.Text);

                if (message == "")
                {
                    if (comboBox1.SelectedIndex == -1)
                    {
                        MessageBox.Show("Поле для ввода наличия платы не выбрано");
                    }
                    else
                    {
                        bool NotFound = connect.CheckContainsOperator(textBox1.Text);
                        if (NotFound == true)
                        {
                            string name = textBox1.Text;
                            int price = Convert.ToInt32(textBox2.Text);
                            double count = Convert.ToDouble(textBox3.Text);
                            bool sell = Convert.ToBoolean(comboBox1.Text);
                            int date=Convert.ToInt32(textBox4.Text);
                            AddOperator(name, Convert.ToString(price), Convert.ToString(count), sell,date.ToString());
                            MessageBox.Show("Оператор добавлен.");
                            RefreshList();
                        }
                        else
                        {
                            MessageBox.Show("Такой оператор уже существует.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show(message);
                }
            }
            else
            {
                MessageBox.Show(message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                connect.RemoveOperator(connect, listBox1.SelectedIndex);
            }

            RefreshList();
        }
        static string CheckName(string TextName)
        {
            string message = "";

            if (TextName == "")
            {
                message = "Поле для ввода названия не может быть пустым.";
            }
            else
            {
                foreach (char symbol in TextName)
                {
                    if (!char.IsLetter(symbol) && symbol != ' ')
                    {
                        message = "Поле название может содержать только буквы.";
                        break;
                    }
                }
            }

            return message;
        }

        static string CheckNumberTextBox(string TextPrice, string TextCount,string TextDate)
        {
            string message = "";

            if (TextPrice == "" || TextCount == ""|| TextDate=="")
            {
                message = "Поля для ввода не могут быть пустым.";
            }
            else
            {
                if (message == "")
                {
                    foreach (char symbol in TextCount)
                    {
                        if (!char.IsDigit(symbol) && symbol != ',')
                        {
                            message = "Поле для ввода площади может содержать только дробное значение.";
                            break;
                        }

                    }

                    foreach (char symbol in TextPrice)
                    {
                        if (!char.IsDigit(symbol))
                        {
                            message = "Поле для ввода цены может содержать только цифры";
                            break;
                        }
                    }
                    foreach (char symbol in TextDate)
                    {
                        if (!char.IsDigit(symbol) && symbol != ',')
                        {
                            message = "Поле для ввода площади может содержать только дробное значение.";
                            break;
                        }

                    }
                }
            }
            return message;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] components = listBox1.SelectedItem.ToString().Split(';');
            string name = components[0];
            string price = components[1];
            string count = components[2];
            string sell = components[3];
            string date = components[4];
            GlobalOperators operators = new GlobalOperators(name, Convert.ToInt32(price), Convert.ToDouble(count), Convert.ToBoolean(sell),Convert.ToInt32(date));
            label5.Text="Плата:"+Convert.ToString(operators.OperatorQuality());
            label7.Text = "Плата за все дни:" + Convert.ToString(operators.GlobalQuality());
        }
    }
}
