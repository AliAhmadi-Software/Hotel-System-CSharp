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
    public partial class frmChekD : Form
    {
        public frmChekD()
        {
            InitializeComponent();
        }


        SqlConnection con = new SqlConnection("Data source =(local);initial catalog =Hoteldari ; integrated security=true");
        SqlCommand cmd = new SqlCommand();

        private void btnSave_Click(object sender, EventArgs e)
        {
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "Insert into ChekD (ShomareHesab,Tarikh,SarResid,Mablagh,Vaziyat,NameShakhs,Tozih)Values(@a,@b,@c,@d,@e,@f,@h)";
            cmd.Parameters.AddWithValue("@a", txtShomareHesab.Text);
            cmd.Parameters.AddWithValue("@b", mskTarikh.Text);
            cmd.Parameters.AddWithValue("@c", mskSarResid.Text);
            cmd.Parameters.AddWithValue("@d", txtMablagh.Text);
            cmd.Parameters.AddWithValue("@e", cmbVaziyat.Text);
            cmd.Parameters.AddWithValue("@f", txtName.Text);
            cmd.Parameters.AddWithValue("@h", txtTozih.Text);
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
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "Update ChekD set ShomareHesab='" + txtShomareHesab.Text + "',Tarikh='" + mskTarikh.Text + "',SarResid='" + mskSarResid.Text + "',Mablagh='" + txtMablagh.Text + "',Vaziyat='" + cmbVaziyat.Text + "',NameShakhs='" + txtName.Text + "',Tozih='" + txtTozih.Text + "' where IdChekD="+ lblId.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("عملیات با موفقیت انجام شد");
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            new frmListChekD().ShowDialog();
        }

        private void frmChekD_Load(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            mskTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");
            mskSarResid.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");
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
