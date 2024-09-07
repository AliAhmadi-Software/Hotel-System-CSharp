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
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data source =(local);initial catalog =Hoteldari ; integrated security=true");
        SqlCommand cmd = new SqlCommand();

        private void btnSave_Click(object sender, EventArgs e)
        {
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "Insert into Menu(NameMenu,Mablagh,Tozih)values(@a,@b,@c)";
            cmd.Parameters.AddWithValue("@a",txtNameMenu.Text);
            cmd.Parameters.AddWithValue("@b", txtMablagh.Text);
            cmd.Parameters.AddWithValue("@c", txtTozih.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("اطلاعات با موفقیت ثبت شد");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdite_Click(object sender, EventArgs e)
        {
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "Update Menu Set NameMenu='"+txtNameMenu.Text+"',Mablagh='"+txtMablagh.Text+"',Tozih='"+txtTozih.Text+"' where IdMenu="+ lblId.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("اطلاعات با موفقیت ویرایش شد");
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            new frmListMenu().ShowDialog();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
           
        }
    }
}
