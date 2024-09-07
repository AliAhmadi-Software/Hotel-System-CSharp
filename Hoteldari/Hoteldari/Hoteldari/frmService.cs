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
    public partial class frmService : Form
    {
        public frmService()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data source =(local);initial catalog =Hoteldari ; integrated security=true");
        SqlCommand cmd = new SqlCommand();

        private void frmService_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "Insert into Service (NameService,Mablagh,Tozih)Values(@a,@b,@c)";
            cmd.Parameters.AddWithValue("@a",txtNameService.Text);
            cmd.Parameters.AddWithValue("@b", txtMablagh.Text);
            cmd.Parameters.AddWithValue("@c", txtTozih.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("ثبت با موفقیت انجام شد");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdite_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "Update Service Set NameService='" + txtNameService.Text + "',Mablagh='" + txtMablagh.Text + "',Tozih='" + txtTozih.Text + "' where IdService=" + lblId.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("ویرایش با موفقیت انجام شد");
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            new frmListService().ShowDialog();
        }
    }
}
