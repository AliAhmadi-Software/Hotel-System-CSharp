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
    public partial class frmKarbar : Form
    {
        public frmKarbar()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data source =(local);initial catalog =Hoteldari ; integrated security=true");
        SqlCommand cmd = new SqlCommand();

        void Display()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Karbar";
            adp.Fill(ds, "Karbar");
            dgvKarbar.DataSource = ds;
            dgvKarbar.DataMember = "Karbar";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "insert into Karbar(UserName,Password)values(@a,@b)";
            cmd.Parameters.AddWithValue("@a",txtUserName.Text);
            cmd.Parameters.AddWithValue("@b", txtPassword.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Display();
            MessageBox.Show("اطلاعات با موفقیت ثبت شد");
        }

        private void frmKarbar_Load(object sender, EventArgs e)
        {
            Display();
            dgvKarbar.Columns[0].HeaderText = "کد";
            dgvKarbar.Columns[1].HeaderText = "نام کاربری";
            dgvKarbar.Columns[2].HeaderText = "کلمه عبور";
            dgvKarbar.Columns[1].Width = 100;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(dgvKarbar.SelectedCells[0].Value);
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "Delete from Karbar where id =@N";
            cmd.Parameters.AddWithValue("@N", x);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Display();
            MessageBox.Show("حذف با موفقیت انجام شد");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
