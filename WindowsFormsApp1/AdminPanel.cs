﻿using System;
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
    public partial class AdminPanel : Form
    {
        public AdminPanel()
        {
            InitializeComponent();
        }
        private void updateList(string query)
        {
            SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
            SqlCommand command = con.CreateCommand();
            con.Open();
            command.CommandText = "SELECT account_id, account_name, account_type FROM account WHERE account_type in (0, 1) AND (account_name LIKE @query OR account_phone LIKE @query) ORDER BY account_type";
            command.Parameters.AddWithValue("@query", query + "%");

            SqlDataReader reader = command.ExecuteReader();

            listBox1.Items.Clear();
            while (reader.Read())
                listBox1.Items.Add(new account(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));

            con.Close();
        }

      
       

       

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {
            updateList(textBox4.Text);
        }

        private void AdminPanel_Load_1(object sender, EventArgs e)
        {
            updateList("");

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
            SqlCommand command = con.CreateCommand();
            command.CommandText = "INSERT INTO [user] (user_username, user_password) VALUES(@username, @password)";
            command.Parameters.AddWithValue("@username", textBox1.Text);
            command.Parameters.AddWithValue("@password", utils.hashPassword(textBox2.Text));
            con.Open();
            if (command.ExecuteNonQuery() > 0)
            {
                //We created the record in user table
                command.CommandText = "SELECT user_id FROM [user] WHERE user_username = @username";
                int user_id = (int)command.ExecuteScalar();

                command.CommandText = "INSERT INTO account (account_user_id, account_name, account_type, account_notes, account_creation_date) VALUES (@user_id, @name, @type, @notes, @date)";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@name", textBox3.Text);
                command.Parameters.AddWithValue("@type", comboBox1.SelectedIndex);
                command.Parameters.AddWithValue("@notes", textBox12.Text);
                command.Parameters.AddWithValue("@date", DateTime.Now);

                if (command.ExecuteNonQuery() > 0)
                {
                    //All good => Account created!
                    MessageBox.Show("Account was successfully created!");
                }
                else
                    MessageBox.Show("Error while creating the account!");
            }
            else
                MessageBox.Show("Error while creating the account!");
            con.Close();
            updateList("");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
                return;

            SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
            SqlCommand command = con.CreateCommand();
            command.CommandText = "DELETE FROM [user] WHERE user_username=@username";
            command.Parameters.AddWithValue("@username", textBox6.Text);
            con.Open();
            if (command.ExecuteNonQuery() > 0)
                MessageBox.Show("Account was deleted!");
            else
                MessageBox.Show("Account was not deleted!");
            con.Close();
            updateList("");
            textBox7.Clear();
            textBox6.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox5.Clear();
            textBox11.Clear();
            textBox10.Clear();
            textBox13.Clear();
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            int account_id;
            try
            {
                account_id = ((account)listBox1.SelectedItem).getID();
            }
            catch (Exception)
            {
                return;
            }

            SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
            SqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT user_username, account_name, account_dob, account_phone, account_type, account_notes, account_creation_date FROM [user], account WHERE user_id = account_user_id AND account_id=@id";
            command.Parameters.AddWithValue("@id", account_id);
            con.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                textBox6.Text = account_id.ToString();
                textBox7.Text = reader.GetValue(0).ToString();
                textBox8.Text = reader.GetValue(1).ToString();
                textBox9.Text = reader.GetValue(2).ToString();
                textBox5.Text = reader.GetValue(3).ToString();

                if (reader.GetInt32(4) == 0)
                    textBox11.Text = "Secretary";
                else
                    textBox11.Text = "Doctor";

                textBox13.Text = reader.GetValue(5).ToString();
                textBox10.Text = reader.GetValue(6).ToString();
            }

            con.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}