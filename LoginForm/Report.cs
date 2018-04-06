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
using LoginForm.Properties;

namespace LoginForm
{
    public partial class Report : Form
    {
        SqlConnection sqlcon5= new SqlConnection(@"Data Source=LAKSHAN-HP\SQLEXPRESS;Initial Catalog=MyDatabase;Integrated Security=True");
        

        public Report()
        {
            InitializeComponent();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            FillDataGridView();
        }
        void FillDataGridView()
        {
            if (sqlcon5.State == ConnectionState.Closed)
                sqlcon5.Open();
            SqlDataAdapter sqlda5 = new SqlDataAdapter("ViewDayReport", sqlcon5);
            sqlda5.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlda5.SelectCommand.Parameters.AddWithValue("@Date ", textBox1.Text.Trim());
            DataTable dtb5 = new DataTable();
            sqlda5.Fill(dtb5);
            dataGridView1.DataSource = dtb5;

            sqlcon5.Close();
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
            dataGridView1.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));
            e.Graphics.DrawImage(bm, 0, 0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
