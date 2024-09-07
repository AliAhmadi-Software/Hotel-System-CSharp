using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Stimulsoft.Report;

namespace Hoteldari
{
    public partial class frmVariz : Form
    {
        public frmVariz()
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
            adp.SelectCommand.CommandText = "select * from Variz";
            adp.Fill(ds, "Variz");
            dgvVariz.DataSource = ds;
            dgvVariz.DataMember = "Variz";

            dgvVariz.Columns[0].HeaderText = "کد";
            dgvVariz.Columns[1].HeaderText = "شماره حساب";
            dgvVariz.Columns[2].HeaderText = "دریافت";
            dgvVariz.Columns[3].HeaderText = "مبلغ واریزی";
            dgvVariz.Columns[4].HeaderText = "تاریخ واریز";
            dgvVariz.Columns[5].HeaderText = "توضیحات";
        }

        private void frmVariz_Load(object sender, EventArgs e)
        {
            Display();
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            mskTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "insert into Variz(ShomareHesab,NameShakhs,Mablagh,Tarikh,Tozih)values(@a,@b,@c,@d,@e)";
            cmd.Parameters.AddWithValue("@a", txtShomareHesab.Text);
            cmd.Parameters.AddWithValue("@b", txtName.Text);
            cmd.Parameters.AddWithValue("@c", txtMablagh.Text);
            cmd.Parameters.AddWithValue("@d", mskTarikh.Text);
            cmd.Parameters.AddWithValue("@e", txtTozih.Text);
            con.Open();
            cmd.ExecuteNonQuery();
           // con.Close();
            Display();
   
            string str;
            int str1;
            SqlCommand sqlcmd = new SqlCommand("Select Mablagh from Hesab where ShomareHesab='"+txtShomareHesab.Text+"'",con);
            str = Convert.ToString((int)sqlcmd.ExecuteScalar());
            str1 = Convert.ToInt32(txtMablagh.Text);

            int b = int.Parse(str) + str1;
            string update = "update Hesab set Mablagh='" + b + "' where ShomareHesab='" + txtShomareHesab.Text + "'";
            SqlCommand com = new SqlCommand(update, con);
            com.ExecuteNonQuery();
            MessageBox.Show("واریز به حساب مورد نظر با موفقیت انجام شد");
            con.Close();

            //*********************************
            lblId.Text = "";
            txtShomareHesab.Text = "";
            txtMablagh.Text = "";
            txtName.Text = "";
            mskTarikh.Text = "";
            txtTozih.Text = "";
            //*********************************
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(dgvVariz.SelectedCells[0].Value);
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "Delete from Variz where IdVariz=@N";
            cmd.Parameters.AddWithValue("@N",x);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Display();
            MessageBox.Show("عملیات حذف با موفقیت انجام شد");
            //*********************************
            lblId.Text = "";
            txtShomareHesab.Text = "";
            txtMablagh.Text = "";
            txtName.Text = "";
            mskTarikh.Text = "";
            txtTozih.Text = "";
            //*********************************
        }

        private void btnEdite_Click(object sender, EventArgs e)
        {
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "Update Variz Set ShomareHesab='" + txtShomareHesab.Text + "',NameShakhs='" + txtName.Text + "',Mablagh='" + txtMablagh.Text + "',tarikh='" + mskTarikh.Text + "',Tozih='" + txtTozih.Text + "' where IdVariz=" + lblId.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Display();
            MessageBox.Show("عملیات ویرایش با موفقیت انجام شد");
            //*********************************
            lblId.Text = "";
            txtShomareHesab.Text = "";
            txtMablagh.Text = "";
            txtName.Text = "";
            mskTarikh.Text = "";
            txtTozih.Text = "";
            //*********************************
        }

        private void dgvVariz_MouseUp(object sender, MouseEventArgs e)
        {
            lblId.Text = dgvVariz[0, dgvVariz.CurrentRow.Index].Value.ToString();
            txtShomareHesab.Text = dgvVariz[1, dgvVariz.CurrentRow.Index].Value.ToString();
            txtName.Text = dgvVariz[2, dgvVariz.CurrentRow.Index].Value.ToString();
            txtMablagh.Text = dgvVariz[3, dgvVariz.CurrentRow.Index].Value.ToString();
            mskTarikh.Text = dgvVariz[4, dgvVariz.CurrentRow.Index].Value.ToString();
            txtTozih.Text = dgvVariz[5, dgvVariz.CurrentRow.Index].Value.ToString();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            StiReport Report = new StiReport();
            Report.Load("Report/rptVariz.mrt");
            Report.Compile();
            Report["strName"] = txtName.Text;
            Report.ShowWithRibbonGUI();
        }
    }
}
