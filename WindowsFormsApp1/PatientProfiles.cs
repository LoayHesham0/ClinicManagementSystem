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
    public partial class PatientProfiles : Form
    {
        public PatientProfiles()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
        SqlCommand command;
        private void updateList(string query)
        {
            command = con.CreateCommand();
            command.CommandText = "SELECT account_id, account_name, account_type FROM account WHERE account_type=2 AND (account_name LIKE @query OR account_phone LIKE @query)";
            command.Parameters.AddWithValue("@query", query + "%");
            con.Open();

            SqlDataReader reader = command.ExecuteReader();
            listBox1.Items.Clear();

            while (reader.Read())
            {
                listBox1.Items.Add(new account(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
            }

            con.Close();
        }

        private void updateCombo()
        {

            command = con.CreateCommand();
            command.CommandText = "SELECT insurance_company_name FROM insurance";
           
            con.Open();

            command.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);
            comboBox1.Items.Clear();
            foreach(DataRow dr in dt.Rows)
                comboBox1.Items.Add(dr["insurance_company_name"].ToString());

            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;

            con.Close();
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            updateList(textBox4.Text);
        }

        private void PatientProfiles_Load(object sender, EventArgs e)
        {
            updateList("");
            updateCombo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Inputs Validation
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Please check the inputs!");
                return;
            }

            //Account Creation
            command = con.CreateCommand();
            command.CommandText = "INSERT INTO account (account_name, account_phone, account_notes, account_type, account_creation_date, account_dob, insurance_company, account_paid) VALUES (@name, @phone, @notes, 2, @date, @dob, @insurance_company, @paid)";
            command.Parameters.AddWithValue("@name", textBox1.Text);
            command.Parameters.AddWithValue("@phone", textBox2.Text);
            command.Parameters.AddWithValue("@notes", textBox3.Text);
            command.Parameters.AddWithValue("@date", DateTime.Now);
            command.Parameters.AddWithValue("@dob", dateTimePicker1.Value);
            command.Parameters.AddWithValue("@insurance_company", comboBox1.SelectedItem.ToString());
            command.Parameters.AddWithValue("@paid", textBox6.Text);

            con.Open();

            if (command.ExecuteNonQuery() > 0)
                MessageBox.Show("Account was created!");
            else
                MessageBox.Show("Failed to create the account!");
            con.Close();
            updateList("");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0 || listBox1.SelectedIndex >= listBox1.Items.Count)
                return;
            int account_id = ((account)listBox1.SelectedItem).getID();
            command = con.CreateCommand();
            command.CommandText = "SELECT account_name, account_dob, account_phone, account_notes, account_creation_date FROM account WHERE account_id=@id";
            command.Parameters.AddWithValue("@id", account_id);

            con.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                textBox7.Text = account_id.ToString();
                textBox8.Text = reader.GetString(0);

                DateTime dob = new DateTime();
                if (DateTime.TryParse(reader.GetValue(1).ToString(), out dob))
                    dateTimePicker2.Value = dob;
                textBox9.Text = reader.GetString(2);
                textBox10.Text = reader.GetString(3);
                textBox11.Text = reader.GetValue(4).ToString();
            }

            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Inputs Validation
            if (textBox9.Text == "" || textBox8.Text == "")
            {
                MessageBox.Show("Please check the inputs!");
                return;
            }

            //Editing the account
            command = con.CreateCommand();
            command.CommandText = "UPDATE account SET account_name = @name, account_phone = @phone, account_dob = @dob, account_notes = @notes WHERE account_id = @id";
            command.Parameters.AddWithValue("@name", textBox8.Text);
            command.Parameters.AddWithValue("@phone", textBox9.Text);
            command.Parameters.AddWithValue("@dob", dateTimePicker2.Value.ToString());
            command.Parameters.AddWithValue("@notes", textBox10.Text);
            command.Parameters.AddWithValue("@id", textBox7.Text);

            con.Open();

            if (command.ExecuteNonQuery() > 0)
                MessageBox.Show("Account was updated!");
            else
                MessageBox.Show("Failed to updated the account!");

            con.Close();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            updatePrice();
        }
        private void updatePrice()
        {
            if (textBox6.Text != "")
            {
                command = con.CreateCommand();
                command.CommandText = "SELECT insurance_company_discount FROM insurance WHERE insurance_company_name = @name";
                command.Parameters.AddWithValue("@name", comboBox1.SelectedItem.ToString());
                con.Open();
                var x = Int32.Parse(command.ExecuteScalar().ToString());
                con.Close();
                var y = Int32.Parse(textBox6.Text.ToString());
                var z = y - (y * x / 100);
                textBox5.Text = z.ToString();
            }
            else
            {
                textBox5.Text = "";
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            updatePrice();
        }
    }
}
