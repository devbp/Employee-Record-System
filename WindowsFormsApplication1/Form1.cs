using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using  MysqlConnector;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        string name,address;
        int age;
        DBConnect db;
        ListView listView1;
        ListViewItem itm;
        
        TextBox textBox1,textBox2,textBox3;
        public Form1()
        {
           
           InitializeComponent();
           db  = new DBConnect();
        }

        
        
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1= new TextBox();
            textBox1.Location= new Point(200,0);
            textBox1.Width = 100;
            textBox1.Multiline = true;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox1.BorderStyle = BorderStyle.Fixed3D;
            textBox1.Text="enter name";
            Controls.Add(textBox1);

            textBox2 = new TextBox();
            textBox2.Location= new Point(200,40);
            textBox2.Text = "enter age";
            Controls.Add(textBox2);

             textBox3 = new TextBox();
            textBox3.Location= new Point(200,80);
            textBox3.Text = "Address";
            Controls.Add(textBox3);

            Button insertbutton = new Button();
            insertbutton.Text = "Submit";
            insertbutton.Location=new Point(200,100);
            insertbutton.Click += new EventHandler(insertbutton_Click);
            Controls.Add(insertbutton);

            Button displaybutton = new Button();
            displaybutton.Text = "Display";
            displaybutton.Location = new Point(200, 200);
            displaybutton.Click += new EventHandler(displaybutton_Click);
            Controls.Add(displaybutton);

            listView1 = new ListView();
            listView1.Size = new Size(200, 400);
            listView1.Columns.Add("Name", 70);
            listView1.Columns.Add("Age", 10);
            listView1.Columns.Add("Address", 70);
            listView1.View = View.Details;
            Controls.Add(listView1);
            
        }

         void insertbutton_Click(object sender, EventArgs e)
         {

            try
            {
                age = Int32.Parse(textBox2.Text);
            }
            catch(FormatException)
            {
                MessageBox.Show("Age should be integer");
            }
            db.Insert(textBox1.Text,age,textBox3.Text); 
           
          }


         void displaybutton_Click(object sender, EventArgs e)
         {
             //Add items in the listview
             string[] arr = new string[4];
             //Add column header
            
             listView1.Items.Clear();
             List<string> list = new List<string>();
             list = db.GetCustomerInfo();
             for (int row = 0; row < list.Count; row = row + 3)
             {
                 arr[0] = list[row];
                 arr[1] = list[row + 1];
                 arr[2] = list[row + 2];
                 Console.WriteLine(list[row]+list[row+1]+list[row+2]);
                 itm = new ListViewItem(arr);
                 listView1.Items.Add(itm);

                 
             }

             
         }
    }
}
