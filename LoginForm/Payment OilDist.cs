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
using LoginForm.Models;
using LoginForm.Properties;

namespace LoginForm
{
    public partial class Payment_OilDist : Form
    {
        SqlConnection sqlcon3 = new SqlConnection(@"Data Source=LAKSHAN-HP\SQLEXPRESS;Initial Catalog=MyDatabase;Integrated Security=True");
        public Payment_OilDist()
        {
            InitializeComponent();
        }
        private List<CartItem> shoppingCart = new List<CartItem>();


        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Stock_Details_Enter(object sender, EventArgs e)
        {
            Reset();
            FillDataGridView();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                if (sqlcon3.State == ConnectionState.Closed)
                    sqlcon3.Open();
                if (btnadd.Text == "Add")
                {
                    SqlCommand sqlcmd3 = new SqlCommand("Addstock", sqlcon3);
                    sqlcmd3.CommandType = CommandType.StoredProcedure;
                    sqlcmd3.Parameters.AddWithValue("@mode", "Add");
                    sqlcmd3.Parameters.AddWithValue("@ItemName", comboBox3.Text.Trim());
                    sqlcmd3.Parameters.AddWithValue("@Stock_ID", Stockidtxt.Text.Trim());
                    sqlcmd3.Parameters.AddWithValue("@Quantity", Quantitytxt.Text.Trim());
                    sqlcmd3.Parameters.AddWithValue("@Buying_price", Buyingpricetxt.Text.Trim());
                    sqlcmd3.ExecuteNonQuery();
                    MessageBox.Show("Add successfully");
                }
                else
                {
                    SqlCommand sqlcmd3 = new SqlCommand("Addstock", sqlcon3);
                    sqlcmd3.CommandType = CommandType.StoredProcedure;
                    sqlcmd3.Parameters.AddWithValue("@mode", "Edit");
                    sqlcmd3.Parameters.AddWithValue("@ItemName", comboBox3.Text.Trim());
                    sqlcmd3.Parameters.AddWithValue("@Stock_ID", Stockidtxt.Text.Trim());
                    sqlcmd3.Parameters.AddWithValue("@Quantity", Quantitytxt.Text.Trim());
                    sqlcmd3.Parameters.AddWithValue("@Buying_price", Buyingpricetxt.Text.Trim());
                    sqlcmd3.ExecuteNonQuery();
                    MessageBox.Show("Updated successfully");
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
                sqlcon3.Close();
            }
        }
        void FillDataGridView()
        {
            if (sqlcon3.State == ConnectionState.Closed)
                sqlcon3.Open();
            SqlDataAdapter sqlda = new SqlDataAdapter("ViewStock", sqlcon3);
            sqlda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlda.SelectCommand.Parameters.AddWithValue("@Stock_ID", searchtxt.Text.Trim());
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            dataGridView2.DataSource = dt;
            sqlcon3.Close();
        }

        private void btnsearch_Click(object sender, EventArgs e)
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

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow.Index != -1)
            {
                Stockidtxt.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                Quantitytxt.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                Buyingpricetxt.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                btnadd.Text = "Update";
                btndelete.Enabled = true;
            }
        }
        void Reset()
        {
            Stockidtxt.Text = Quantitytxt.Text = Buyingpricetxt.Text = "";
            btnadd.Text = "Add";
            btndelete.Enabled = false;
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (sqlcon3.State == ConnectionState.Closed)
                    sqlcon3.Open();

                SqlCommand sqlcmd3 = new SqlCommand("StockDeletion", sqlcon3);
                sqlcmd3.CommandType = CommandType.StoredProcedure;
                sqlcmd3.Parameters.AddWithValue("@Stock_ID", Stockidtxt.Text.Trim());

                sqlcmd3.ExecuteNonQuery();
                MessageBox.Show("Deleted successfully");

                Reset();
                FillDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
        }

        private void Payment_OilDist_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedValueChanged -= comboBox1_SelectedValueChanged;
            comboBox1.ValueMember = "Selling_price";
            comboBox1.SelectedIndex = -1;
            comboBox1.SelectedValueChanged += comboBox1_SelectedValueChanged;

            
            
            
            
            
            
            
            // TODO: This line of code loads data into the 'myDatabaseDataSet.Table_2' table. You can move, or remove it, as needed.
            this.table_2TableAdapter.Fill(this.myDatabaseDataSet.Table_2);
            // TODO: This line of code loads data into the 'myDatabaseDataSet.Tbl_item' table. You can move, or remove it, as needed.
            this.tbl_itemTableAdapter.Fill(this.myDatabaseDataSet.Tbl_item);
            Reset();
            FillDataGridView();

            


        }

        private void neworderbtn_Click(object sender, EventArgs e)
        {
            neworderbtn.Enabled = false;
            printorderbtn.Enabled = true;
            btnemail.Enabled = true;
            btncancel.Enabled = true;

            groupBox1.Enabled = true;
            comboBox2.Focus();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            neworderbtn.Enabled = true;
            printorderbtn.Enabled = false;
            btnemail.Enabled = false;
            btncancel.Enabled = false;

            groupBox1.Enabled = false;
            comboBox2.Focus();
            
            comboBox1.SelectedIndex = -1;
            textbox2.Clear();
            textBox3.Clear();

            amounttxt.Text = "0";
            taxtxt.Text = "0";
            totaltxt.Text = "0";
            dataGridView1.DataSource = null;
            shoppingCart.Clear(); 


        }

        private void addtocartbtn_Click(object sender, EventArgs e)
        {
            string constring = @"Data Source=LAKSHAN-HP\SQLEXPRESS;Initial Catalog=MyDatabase;Integrated Security=True";
            string Query = "UPDATE Tbl_stock SET Quantity = Quantity - @Quantity where ItemName = @ItemName";
            using (SqlConnection conDataBase = new SqlConnection(constring))
            using (SqlCommand cmdDataBase = new SqlCommand(Query, conDataBase))


            {
                cmdDataBase.Parameters.AddWithValue("@Quantity", int.Parse(textbox2.Text));
                cmdDataBase.Parameters.AddWithValue("@ItemName", comboBox1.Text);
                conDataBase.Open();
                cmdDataBase.ExecuteNonQuery();
                cmdDataBase.Clone();
            }












            try
            {
                if (sqlcon3.State == ConnectionState.Closed)
                    sqlcon3.Open();


                SqlCommand sqlcmd3 = new SqlCommand("AddPayment", sqlcon3);
                sqlcmd3.CommandType = CommandType.StoredProcedure;
                sqlcmd3.Parameters.AddWithValue("@mode", "Add");
                sqlcmd3.Parameters.AddWithValue("@Customer", comboBox2.Text.Trim());
                sqlcmd3.Parameters.AddWithValue("@Item", comboBox1.Text.Trim());
                sqlcmd3.Parameters.AddWithValue("@Quantity", textbox2.Text.Trim());
                sqlcmd3.Parameters.AddWithValue("@UnitPrice", textBox3.Text.Trim());
                sqlcmd3.ExecuteNonQuery();
                MessageBox.Show("Add successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");

            }
            finally
            {
                sqlcon3.Close();
            }







         


            if (IsValidated())
            {
                CartItem item = new CartItem()
                {
                    Item_name=comboBox1.Text,
                    Quantity=Convert.ToInt16(textbox2.Text.Trim()),
                    Unit_Price=Convert.ToDecimal(textBox3.Text.Trim()),
                    TotalAmount= Convert.ToInt16(textbox2.Text.Trim())* 
                    Convert.ToDecimal(textBox3.Text.Trim())
                };
                shoppingCart.Add(item);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = shoppingCart;

                decimal totalAmount = shoppingCart.Sum(x => x.TotalAmount);
                amounttxt.Text = totalAmount.ToString();
                decimal totalSalesTax = (12 * totalAmount) / 100;
                taxtxt.Text = totalSalesTax.ToString();
                decimal totalpay = totalAmount + totalSalesTax;
                totaltxt.Text = totalpay.ToString();


                comboBox1.SelectedIndex = -1;
                textbox2.Clear();
                textBox3.Clear();


            }
        }
               

           private bool IsValidated()
                {
                    if (comboBox2.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("Customer name is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (comboBox1.SelectedIndex == -1)
                    {
                        MessageBox.Show("Item name is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (textbox2.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("Quantity is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textbox2.Focus();
                        return false;
                    }
                    else
                    {
                        int TempQuantity;
                        bool isNumeric = int.TryParse(textbox2.Text.Trim(), out TempQuantity);
                        if (!isNumeric)
                        {
                            MessageBox.Show("Quantity is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textbox2.Focus();
                            textbox2.Clear();
                            textbox2.Focus();
                            return false;
                        }
                    }
                    if (textBox3.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("Unit Price is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox3.Focus();
                        return false;
                    }
                    else
                    {
                        decimal n;
                        bool isDecimal = decimal.TryParse(textBox3.Text.Trim(), out n);
                        if (!isDecimal)
                        {
                            MessageBox.Show("Quantity is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textbox2.Focus();
                            textbox2.Clear();
                            textbox2.Focus();
                            return false;
                        }
                    }

                    return true;
                }

        





        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button==System.Windows.Forms.MouseButtons.Right)
            {
                var hti = dataGridView1.HitTest(e.Y, e.Y);
                dataGridView1.Rows[hti.RowIndex].Selected = true;
                contextMenuStrip1.Show(dataGridView1, e.X, e.Y);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            shoppingCart.RemoveAt(index);

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = shoppingCart;

            decimal totalAmount = shoppingCart.Sum(x => x.TotalAmount);
            amounttxt.Text = totalAmount.ToString();
            decimal totalSalesTax = (12 * totalAmount) / 100;
            taxtxt.Text = totalSalesTax.ToString();
            decimal totalpay = totalAmount + totalSalesTax;
            totaltxt.Text = totalpay.ToString();

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Image image = Resources.oildist;//clicked on quick action button (bulb) to using project properties.//
            e.Graphics.DrawImage(image,250,10,image.Width, image.Height);
            e.Graphics.DrawString(" Date:" + DateTime.Now.ToShortDateString(), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(25,340));
            e.Graphics.DrawString(" Customer name:" + comboBox2.Text.Trim(), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(25, 360));
            e.Graphics.DrawString
   ("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(20, 370));
            e.Graphics.DrawString(" Item name:" , new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(30, 385));
            e.Graphics.DrawString(" Quantity:", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(380, 385));
            e.Graphics.DrawString(" Unit Price:", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(510, 385));
            e.Graphics.DrawString(" Total Price:", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(660, 385));
            e.Graphics.DrawString
  ("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(20, 400));
            int yPos = 420;
            foreach (var i in shoppingCart)
            {
                e.Graphics.DrawString(i.Item_name, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(32, yPos));
                e.Graphics.DrawString(i.Quantity.ToString(), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(400, yPos));
                e.Graphics.DrawString(i.Unit_Price.ToString(), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(525, yPos));
                e.Graphics.DrawString(i.TotalAmount.ToString(), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(675, yPos));
                yPos += 30; 
            }
            e.Graphics.DrawString
 ("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(20, yPos));

            e.Graphics.DrawString(" Total Amount:   Rs" + amounttxt.Text.Trim(), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(550,yPos+30));
            e.Graphics.DrawString(" Sales Tax:      Rs" + taxtxt.Text.Trim(), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(550, yPos + 60));
            e.Graphics.DrawString(" Total to Pay:   Rs" + totaltxt.Text.Trim(), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(550, yPos + 90));




        }

        private void btnemail_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printorderbtn_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void Cusnametxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            decimal Selling_price = Convert.ToDecimal(comboBox1.SelectedValue);
            textBox3.Text = Selling_price.ToString();
        }

       

        
                                                                

    }
        }
    



