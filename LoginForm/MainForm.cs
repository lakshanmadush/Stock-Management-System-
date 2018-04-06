using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginForm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void addCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report rep = new Report();
            rep.MdiParent = this;
            rep.Show();
        }

        private void paymentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void itemDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Item it = new Item();
            it.MdiParent = this;
            it.Show();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.MdiParent = this;
            f4.Show();
        }

        private void itemSectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Item it = new Item();
            it.MdiParent = this;
            it.Show();
        }

        private void stockManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Payment_OilDist po = new Payment_OilDist();
            po.MdiParent = this;
            po.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
