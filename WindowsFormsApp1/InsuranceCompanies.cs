using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class InsuranceCompanies : Form
    {
        public InsuranceCompanies()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
        SqlCommand command;

        private void button3_Click(object sender, EventArgs e)
        {
            //Inputs Validation
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Please check the inputs!");
                return;
            }

            //Account Creation
            command = con.CreateCommand();
            command.CommandText = "INSERT INTO insurance (insurance_company_name, insurance_company_discount, insurance_company_notes) VALUES (@name, @discount, @notes)";
            command.Parameters.AddWithValue("@name", textBox1.Text);
            command.Parameters.AddWithValue("@discount", textBox2.Text);
            command.Parameters.AddWithValue("@notes", textBox3.Text);
            

            con.Open();

            if (command.ExecuteNonQuery() > 0)
                MessageBox.Show("Company was Addeed!");
            else
                MessageBox.Show("Failed to add the company!");
            con.Close();
            updateList();
        }
        private void updateList()
        {
            command = con.CreateCommand();
                command.CommandText = "SELECT insurance_company_name, insurance_company_discount, insurance_company_notes FROM insurance";
            con.Open();

            SqlDataReader reader = command.ExecuteReader();
            listBox1.Items.Clear();
            while (reader.Read())
            {
                string name = reader.GetString(0);
                int discount = reader.GetInt32(1);
                string notes = reader.GetString(2);
                listBox1.Items.Add(new company (name, discount, notes));
            }

            con.Close();
        }

        private void InsuranceCompanies_Load(object sender, EventArgs e)
        {
            updateList();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0 || listBox1.SelectedIndex >= listBox1.Items.Count)
                return;
            company comp = (company)listBox1.SelectedItem;
            textBox4.Text = comp.name.ToString();
            textBox5.Text = comp.discount.ToString();
            textBox6.Text = comp.notes.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            command = con.CreateCommand();
            command.CommandText = "UPDATE insurance SET insurance_company_discount = @discount WHERE insurance_company_name = @name";
            command.Parameters.AddWithValue("@name", textBox4.Text);
            command.Parameters.AddWithValue("@discount", textBox5.Text);


            con.Open();

            if (command.ExecuteNonQuery() > 0)
                MessageBox.Show("Insurance company was updated!");
            else
                MessageBox.Show("Failed to update the insurance company!");

            con.Close();
            updateList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            command = con.CreateCommand();
            command.CommandText = "DELETE FROM insurance WHERE insurance_company_name = @name";
            command.Parameters.AddWithValue("@name", textBox4.Text);
            con.Open();
            if (command.ExecuteNonQuery() > 0)
                MessageBox.Show("Insurance company was deleted!");
            else
                MessageBox.Show("Failed to delete the insurance company!");
            con.Close();
            updateList();
        }
    }
    
}
