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
    public partial class frmPardakht : Form
    {
        public frmPardakht()
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
            adp.SelectCommand.CommandText = "select * from Pardakht";
            adp.Fill(ds, "Pardakht");
            dgvPardakht.DataSource = ds;
            dgvPardakht.DataMember = "Pardakht";

            dgvPardakht.Columns[0].HeaderText = "کد";
            dgvPardakht.Columns[1].HeaderText = "شماره حساب";
            dgvPardakht.Columns[2].HeaderText = "پرداخت به";
            dgvPardakht.Columns[3].HeaderText = "مبلغ پرداختی";
            dgvPardakht.Columns[4].HeaderText = "تاریخ پرداخت";
            dgvPardakht.Columns[5].HeaderText = "توضیحات";
        }
        private void frmPardakht_Load(object sender, EventArgs e)
        {
            Display();
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            mskTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string str;
            int str1;
            con.Open();
            SqlCommand sqlcmd = new SqlCommand("Select Mablagh from Hesab where ShomareHesab='"+txtShomareHesab.Text+"'",con);
            str = Convert.ToString((int)sqlcmd.ExecuteScalar());
            str1 = Convert.ToInt32(txtMablagh.Text);

            if (txtMablagh.Value > Convert.ToInt32(str))
            {
                MessageBox.Show("مبلغ مورد نظر بیشتر از مبلغ حساب است");
            }
            else
            {
                int b = int.Parse(str) - str1;
                string update = "update Hesab set Mablagh='" + b + "' where ShomareHesab='" + txtShomareHesab.Text + "'";
                SqlCommand com = new SqlCommand(update, con);
                com.ExecuteNonQuery();

                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into Pardakht (ShomareHesab,NameShakhs,Mablagh,Tarikh,Tozih)values(@a,@b,@c,@d,@e)";
                cmd.Parameters.AddWithValue("@a", txtShomareHesab.Text);
                cmd.Parameters.AddWithValue("@b", txtName.Text);
                cmd.Parameters.AddWithValue("@c", txtMablagh.Text);
                cmd.Parameters.AddWithValue("@d", mskTarikh.Text);
                cmd.Parameters.AddWithValue("@e", txtTozih.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                Display();
                MessageBox.Show("عملیات با موفقیت انجام شد");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(dgvPardakht.SelectedCells[0].Value);
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "Delete from Pardakht where idPardakht=@N";
            cmd.Parameters.AddWithValue("@N",x);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Display();
            MessageBox.Show("عملیات با موفقیت انجام شد");
        }

        private void btnEdite_Click(object sender, EventArgs e)
        {
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "update Pardakht set ShomareHesab='" + txtShomareHesab.Text + "',NameShakhs='" + txtName.Text + "',Mablagh='" + txtMablagh.Text + "',Tarikh='" + mskTarikh.Text + "',Tozih='" + txtTozih.Text + "' where idPardakht="+ lblId.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Display();
            MessageBox.Show("عملیات با موفقیت انجام شد");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvPardakht_MouseUp(object sender, MouseEventArgs e)
        {
            lblId.Text = dgvPardakht[0, dgvPardakht.CurrentRow.Index].Value.ToString();
            txtShomareHesab.Text = dgvPardakht[1, dgvPardakht.CurrentRow.Index].Value.ToString();
            txtName.Text = dgvPardakht[2, dgvPardakht.CurrentRow.Index].Value.ToString();
            txtMablagh.Text = dgvPardakht[3, dgvPardakht.CurrentRow.Index].Value.ToString();
            mskTarikh.Text = dgvPardakht[4, dgvPardakht.CurrentRow.Index].Value.ToString();
            txtTozih.Text = dgvPardakht[5, dgvPardakht.CurrentRow.Index].Value.ToString();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            StiReport Report = new StiReport();
            Report.Load("Report/rptPardakht.mrt");
            Report.Compile();
            Report["strName"] = txtName.Text;
            Report.ShowWithRibbonGUI();
        }
    }
}
