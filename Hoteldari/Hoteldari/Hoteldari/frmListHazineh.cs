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
    public partial class frmListHazineh : Form
    {
        public frmListHazineh()
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
            adp.SelectCommand.CommandText = "select * from Hazineh where Tarikh Between '" + mskSarResid1.Text + "' AND '" + mskSarResid2.Text + "' ";
            adp.Fill(ds,"Hazineh");
            dgvHazineh.DataSource = ds;
            dgvHazineh.DataMember = "Hazineh";

            dgvHazineh.Columns[0].HeaderText = "کد هزینه";
            dgvHazineh.Columns[1].HeaderText = "شرح هزینه";
            dgvHazineh.Columns[2].HeaderText = "مبلغ هزینه";
            dgvHazineh.Columns[3].HeaderText = "تاریخ هزینه";
            dgvHazineh.Columns[4].HeaderText = "نام شخص";
            dgvHazineh.Columns[5].HeaderText = "توضیحات";
        }

        private void frmListHazineh_Load(object sender, EventArgs e)
        {
            Display();

            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            mskSarResid1.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");
            mskSarResid2.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(dgvHazineh.SelectedCells[0].Value);
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "Delete from Hazineh where idHazineh =@N";
            cmd.Parameters.AddWithValue("@N",x);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Display();
            MessageBox.Show("حذف انجام شد");
        }

        private void btnEdite_Click(object sender, EventArgs e)
        {
            frmHazineh frm = new frmHazineh();
            frm.lblId.Text = dgvHazineh[0, dgvHazineh.CurrentRow.Index].Value.ToString();
            frm.txtSharh.Text = dgvHazineh[1, dgvHazineh.CurrentRow.Index].Value.ToString();
            frm.txtMablagh.Text = dgvHazineh[2, dgvHazineh.CurrentRow.Index].Value.ToString();
            frm.mskTarikh.Text = dgvHazineh[3, dgvHazineh.CurrentRow.Index].Value.ToString();
            frm.txtName.Text = dgvHazineh[4, dgvHazineh.CurrentRow.Index].Value.ToString();
            frm.txtTozih.Text = dgvHazineh[5, dgvHazineh.CurrentRow.Index].Value.ToString();
            frm.ShowDialog();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Display();
        }

        private void mskSarResid1_TextChanged(object sender, EventArgs e)
        {
            Display();
        }

        private void mskSarResid2_TextChanged(object sender, EventArgs e)
        {
            Display();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPardakht_Click(object sender, EventArgs e)
        {
            frmPardakht frm = new frmPardakht();
            frm.txtMablagh.Text = dgvHazineh.CurrentRow.Cells[2].Value.ToString();
            frm.txtTozih.Text = " پرداخت نقدی هزینه با شرح " + dgvHazineh.CurrentRow.Cells[1].Value.ToString();
            frm.ShowDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            StiReport Report = new StiReport();
            Report.Load("Report/rptHazineh.mrt");
            Report.Compile();
            Report["strTarikh1"] = mskSarResid1.Text;
            Report["strTarikh2"] = mskSarResid2.Text;
            Report.ShowWithRibbonGUI();

        }
    }
}
