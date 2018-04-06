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

namespace LoginForm
{
    public partial class Item : Form
    {
        SqlConnection sqlcon2 = new SqlConnection(@"Data Source=LAKSHAN-HP\SQLEXPRESS;Initial Catalog=MyDatabase;Integrated Security=True");


        public Item()
        {
            InitializeComponent();
        }

        private void Item_Load(object sender, EventArgs e)
        {
            Reset();
            FillDataGridView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (sqlcon2.State == ConnectionState.Closed)
                    sqlcon2.Open();
                if (button1.Text == "Add")
                {
                    SqlCommand sqlcmd = new SqlCommand("Additem", sqlcon2);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@mode", "Add");
                    sqlcmd.Parameters.AddWithValue("@Item_code", textBox1.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Item_name", textBox2.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Description", textBox3.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Selling_price", textBox4.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Added_by", textBox5.Text.Trim());
                    sqlcmd.ExecuteNonQuery();
                    MessageBox.Show("Added item successfully");
                }
                else
                {
                    SqlCommand sqlcmd = new SqlCommand("Additem", sqlcon2);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@mode", "Edit");
                    sqlcmd.Parameters.AddWithValue("@Item_code", textBox1.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Item_name", textBox2.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Description", textBox3.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Selling_price", textBox4.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Added_by", textBox5.Text.Trim());
                    sqlcmd.ExecuteNonQuery();
                    MessageBox.Show("Updated  item successfully");
                }
                Reset();
                FillDataGridView(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
            finally
            {
                sqlcon2.Close();
            }
        }
        void FillDataGridView()
        {
            if (sqlcon2.State == ConnectionState.Closed)
                sqlcon2.Open();
            SqlDataAdapter sqlda2 = new SqlDataAdapter("SearchViewitem", sqlcon2);
            sqlda2.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlda2.SelectCommand.Parameters.AddWithValue("@Item_code ", textBox6.Text.Trim());
            DataTable dtb2 = new DataTable();
            sqlda2.Fill(dtb2);
            dataGridView1.DataSource = dtb2;

            sqlcon2.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                FillDataGridView();
            }
            catch (Exception ex)
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
                button1.Text = "Update";
                button2.Enabled = true;
            }
        }
        void Reset()
        {
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = "";
            button1.Text = "Add";
            button2.Enabled = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                if (sqlcon2.State == ConnectionState.Closed)
                    sqlcon2.Open();
               
                
                    SqlCommand sqlcmd = new SqlCommand("ItemDeletion", sqlcon2);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                  
                    sqlcmd.Parameters.AddWithValue("@Item_name", textBox2.Text.Trim());
                   
                    sqlcmd.ExecuteNonQuery();
                    MessageBox.Show("Deleted item successfully");
                    Reset();
                    FillDataGridView();
                }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

}
