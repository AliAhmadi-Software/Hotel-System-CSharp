using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hoteldari
{
    public partial class frmTax : Form
    {
        public frmTax()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data source =(local);initial catalog =Hoteldari ; integrated security=true");
        SqlCommand cmd = new SqlCommand();

        private void btnSave_Click(object sender, EventArgs e)
        {
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "Insert into Tax(Tax)values(@a)";
            cmd.Parameters.AddWithValue("@a", dblTax.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("اطلاعات ثبت شد ");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
