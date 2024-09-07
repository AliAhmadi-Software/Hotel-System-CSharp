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
    public partial class frmInfo : Form
    {
        public frmInfo()
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
            adp.SelectCommand.CommandText = "select * from Info";
            adp.Fill(ds,"Info");
            dgvInfo.DataSource = ds;
            dgvInfo.DataMember = "Info";
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "insert into Info(NameMalek,NameHotel,Tel,Mobile,Address) values(@a,@b,@c,@d,@e)";
            cmd.Parameters.AddWithValue("@a", txtNameMalek.Text);
            cmd.Parameters.AddWithValue("@b", txtNameHotel.Text);
            cmd.Parameters.AddWithValue("@c", txtTel.Text);
            cmd.Parameters.AddWithValue("@d", txtMobile.Text);
            cmd.Parameters.AddWithValue("@e", txtAddress.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Display();
            MessageBox.Show("عملیات ثبت با موفقیت انجام شد");
        }

        private void frmInfo_Load(object sender, EventArgs e)
        {
            Display();

            dgvInfo.Columns[0].HeaderText = "کد هتل";
            dgvInfo.Columns[1].HeaderText = "مالک هتل";
            dgvInfo.Columns[2].HeaderText = "نام هتل";
            dgvInfo.Columns[3].HeaderText = "تلفن ثابت";
            dgvInfo.Columns[4].HeaderText = "تلفن همراه";
            dgvInfo.Columns[5].HeaderText = "آدرس هتل";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(dgvInfo.SelectedCells[0].Value);
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "Delete from info where IdHotel =@N";
            cmd.Parameters.AddWithValue("@N",x);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Display();
            MessageBox.Show("حذف با موفقیت انجام شد");
        }

        private void dgvInfo_MouseUp(object sender, MouseEventArgs e)
        {
            txtId.Text = dgvInfo[0, dgvInfo.CurrentRow.Index].Value.ToString();
            txtNameMalek.Text = dgvInfo[1, dgvInfo.CurrentRow.Index].Value.ToString();
            txtNameHotel.Text = dgvInfo[2, dgvInfo.CurrentRow.Index].Value.ToString();
            txtTel.Text = dgvInfo[3, dgvInfo.CurrentRow.Index].Value.ToString();
            txtMobile.Text = dgvInfo[4, dgvInfo.CurrentRow.Index].Value.ToString();
            txtAddress.Text = dgvInfo[5, dgvInfo.CurrentRow.Index].Value.ToString();
        }

        private void btnEdite_Click(object sender, EventArgs e)
        {
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "Update Info Set NameMalek='" + txtNameMalek.Text + "',NameHotel='" + txtNameHotel.Text + "',Tel='" + txtTel.Text + "',Mobile='" + txtMobile.Text + "',Address='" + txtAddress.Text + "' where IdHotel=" + txtId.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("عملیات ویرایش با موفقیت انجام شد");
            Display();
        }
    }
}
