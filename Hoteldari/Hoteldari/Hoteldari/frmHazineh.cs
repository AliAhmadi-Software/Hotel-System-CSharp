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
    public partial class frmHazineh : Form
    {
        public frmHazineh()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data source =(local);initial catalog =Hoteldari ; integrated security=true");
        SqlCommand cmd = new SqlCommand();

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into Hazineh (Sharh,Mablagh,Tarikh,NameShakhs,Tozih)Values(@a,@b,@c,@d,@e)";
                cmd.Parameters.AddWithValue("@a", txtSharh.Text);
                cmd.Parameters.AddWithValue("@b", txtMablagh.Text);
                cmd.Parameters.AddWithValue("@c", mskTarikh.Text);
                cmd.Parameters.AddWithValue("@d", txtName.Text);
                cmd.Parameters.AddWithValue("@e", txtTozih.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("عملیات با موفقیت انجام شد");
            }
            catch
            {
                MessageBox.Show("مشکلی پیش آمده است");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmHazineh_Load(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            mskTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");
            if (lblId.Text=="0")
            {
                btnEdite.Enabled = false;
            }
            else
            {
                btnEdite.Enabled = true;
            }
        }

        private void btnEdite_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "Update Hazineh Set Sharh='" + txtSharh.Text + "',Mablagh='" + txtMablagh.Text + "',Tarikh='" + mskTarikh.Text + "',NameShakhs='" + txtName.Text + "',Tozih='" + txtTozih.Text + "' where idHazineh ="+ lblId.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("ویرایش انجام شد");
            }
            catch
            {
                MessageBox.Show("مشکلی پیش آمده است");
            }
        }
    }
}
