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
using System.Text.RegularExpressions;
namespace LoginForm
{
    public partial class Form4 : Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Data Source=LAKSHAN-HP\SQLEXPRESS;Initial Catalog=MyDatabase;Integrated Security=True;");
        public Form4()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                    sqlcon.Open();
                if (btnSave.Text == "Save")
                {
                    SqlCommand cmd2 = new SqlCommand("CustomerAdd", sqlcon);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@mode", "Add");
                    cmd2.Parameters.AddWithValue("@Cus_Name", textBox1.Text.Trim());
                    cmd2.Parameters.AddWithValue("@Address", textBox2.Text.Trim());
                    cmd2.Parameters.AddWithValue("@Cus_NIC", textBox3.Text.Trim());
                    cmd2.Parameters.AddWithValue("@Email", textBox4.Text.Trim());
                    cmd2.Parameters.AddWithValue("@Tel_No", textBox5.Text.Trim());
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Record Saved Successfully");
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("CustomerAdd", sqlcon);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@mode", "Edit");
                    cmd2.Parameters.AddWithValue("@Cus_Name", textBox1.Text.Trim());
                    cmd2.Parameters.AddWithValue("@Address", textBox2.Text.Trim());
                    cmd2.Parameters.AddWithValue("@Cus_NIC", textBox3.Text.Trim());
                    cmd2.Parameters.AddWithValue("@Email", textBox4.Text.Trim());
                    cmd2.Parameters.AddWithValue("@Tel_No", textBox5.Text.Trim());
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show(" Record Updated Successfully");
                }
                Reset();
                FillDataGridView();


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
            finally
            {
                sqlcon.Close();
            }

        }
        void FillDataGridView()
        {
            if (sqlcon.State == ConnectionState.Closed)
                sqlcon.Open();
            SqlDataAdapter sqlda = new SqlDataAdapter("SearchCustomer", sqlcon);
            sqlda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlda.SelectCommand.Parameters.AddWithValue("@Cus_Name", textBox6.Text.Trim());
            DataTable dtb = new DataTable();
            sqlda.Fill(dtb);
            dataGridView1.DataSource = dtb;

            sqlcon.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FillDataGridView();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
            
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                btnSave.Text = "Update";

                button2.Enabled = true;

            }

        }

          void Reset()
        {
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = "";
            btnSave.Text = "Save";
            button2.Enabled = false;
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            Reset();
            FillDataGridView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if(sqlcon.State == ConnectionState.Closed)
                    sqlcon.Open();
                
                
                    SqlCommand cmd2 = new SqlCommand("CustomerDeletion", sqlcon);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    
                   
                    cmd2.Parameters.AddWithValue("@Cus_NIC", textBox3.Text.Trim());
                   
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Deleted Successfully");
                
               
                Reset();
                FillDataGridView();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
            finally
            {
                sqlcon.Close();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            string pattern= "^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$";
            if(Regex.IsMatch(textBox4.Text,pattern))
            {
                errorProvider1.Clear();
            }
            else
            {
                errorProvider1.SetError(this.textBox4,"Please Provide Valid Email");
                return;
            }

        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            string pattern = @"\+[0-9]{2}[0-9]{9}";
            if (Regex.IsMatch(textBox5.Text, pattern))
            {
                errorProvider2.Clear();
            }
            else
            {
                errorProvider2.SetError(this.textBox5, "Please Provide Valid Phone Number");
                return;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            string pattern = @"^[0-9]{9}[vVxX]$";
            if (Regex.IsMatch(textBox3.Text, pattern))
            {
                errorProvider3.Clear();
            }
            else
            {
                errorProvider3.SetError(this.textBox3, "Please Enter Valid NIC Number");
                return;
            }
        }
    }
}
