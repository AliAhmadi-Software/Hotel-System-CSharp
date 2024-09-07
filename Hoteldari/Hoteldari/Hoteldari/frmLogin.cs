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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data source =(local);initial catalog =Hoteldari ; integrated security=true");
        SqlCommand cmd = new SqlCommand();

        private void btnIn_Click(object sender, EventArgs e)
        {
            int i = 0;

            cmd = new SqlCommand("select Count(*) from Karbar where UserName=@N AND Password=@F", con);
            cmd.Parameters.AddWithValue("@N",txtUserName.Text);
            cmd.Parameters.AddWithValue("@F", txtPassword.Text);
            con.Open();
            i = (int)cmd.ExecuteScalar();
            con.Close();

            if (i > 0)
            {
                new Form1().ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("نام کاربری یا کلمه عبور وارد شده صحیح نیست");
            }
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
