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
    public partial class frmHesab : Form
    {
        public frmHesab()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data source =(local);initial catalog =Hoteldari ; integrated security=true");
        SqlCommand cmd = new SqlCommand();

        private void btnSave_Click(object sender, EventArgs e)
        {
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "insert into Hesab (NameHesab,ShomareHesab,Mablagh,Tozih)values(@a,@b,@c,@d)";
            cmd.Parameters.AddWithValue("@a", txtNameHesab.Text);
            cmd.Parameters.AddWithValue("@b", txtShomare.Text);
            cmd.Parameters.AddWithValue("@c", txtMablagh.Text);
            cmd.Parameters.AddWithValue("@d", txtTozih.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("عملیات با موفقیت انجام شد");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdite_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "Update Hesab set NameHesab='"+txtNameHesab.Text+"',ShomareHesab='"+txtShomare.Text+"',Mablagh='"+txtMablagh.Text+"',Tozih='"+txtTozih.Text+"' where IdHesab="+ lblId.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("عملیات با موفقیت انجام شد");
        }

        private void frmHesab_Load(object sender, EventArgs e)
        {
            if (lblId.Text=="0")
            {
                btnEdite.Enabled = false;
            }
            else
            {
                btnEdite.Enabled = true;
            }
        }
    }
}
