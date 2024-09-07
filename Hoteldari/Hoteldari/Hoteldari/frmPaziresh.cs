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
    public partial class frmPaziresh : Form
    {
        public frmPaziresh()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data source =(local);initial catalog =Hoteldari ; integrated security=true");
        SqlCommand cmd = new SqlCommand();

        string Address;
        string Tel;

        private void frmPaziresh_Load(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            mskTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");
            AzTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");
            TaTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");

            cmd.Connection = con;
            cmd.CommandText = "select Tax from Tax";
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                dblTax.Text = dr["Tax"].ToString();
            }
            con.Close();
            //*******************
            
            cmd.CommandText = "select * from Info";
            SqlDataReader dr1;
            con.Open();
            dr1 = cmd.ExecuteReader();
            if (dr1.Read())
            {
                Address = dr1["Address"].ToString();
                Tel = dr1["Tel"].ToString();
            }
            con.Close();
        }

        private void btnOtag_Click(object sender, EventArgs e)
        {
            if (txtCodeOtag.Text !="")
            {
                SqlDataReader DR;
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from Otag where CodeOtag=@S";
                cmd.Parameters.Add("S", txtCodeOtag.Text);
                con.Open();
                DR = cmd.ExecuteReader();
                if (DR.Read())
                {
                    //txtCodeOtag.Text = DR["CodeOtag"].ToString();
                    txtGroup.Text = DR["NameGroup"].ToString();
                    txtEmkanat.Text = DR["Emkanat"].ToString();
                    txtMablagh.Text = DR["Mablagh"].ToString();
                    lblVaziyat.Text = DR["Vaziyat"].ToString();

                    if (lblVaziyat.Text=="پر")
                    {
                        MessageBox.Show("اتاق مورد نظر شما در حال حاظر پر می باشد");

                        txtCodeOtag.Text = "";
                        txtGroup.Text = "";
                        txtEmkanat.Text = "";
                        txtMablagh.Text = "";
                        lblVaziyat.Text = "";
                    }
                }
                else
                {
                    txtCodeOtag.Text = "";
                    txtGroup.Text = "";
                    txtEmkanat.Text = "";
                    txtMablagh.Text = "";
                    lblVaziyat.Text = "";
                }
                con.Close();

            }
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            if (txtCodeService.Text != "")
            {
                SqlDataReader DR;
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from Service where IdService=@S";
                cmd.Parameters.Add("S", txtCodeService.Text);
                con.Open();
                DR = cmd.ExecuteReader();
                if (DR.Read())
                {
                    txtCodeService.Text = DR["IdService"].ToString();
                    txtNameService.Text = DR["NameService"].ToString();
                    txtMablaghService.Text = DR["Mablagh"].ToString();
                }
                else
                {
                    txtCodeService.Text = "";
                    txtNameService.Text = "";
                    txtMablaghService.Text = "";
                }
                con.Close();

            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (txtCodeMenu.Text != "")
            {
                SqlDataReader DR;
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from Menu where IdMenu=@S";
                cmd.Parameters.Add("S", txtCodeMenu.Text);
                con.Open();
                DR = cmd.ExecuteReader();
                if (DR.Read())
                {
                    txtCodeMenu.Text = DR["IdMenu"].ToString();
                    txtNameMenu.Text = DR["NameMenu"].ToString();
                    txtMablaghMenu.Text = DR["Mablagh"].ToString();
                }
                else
                {
                    txtCodeMenu.Text = "";
                    txtNameMenu.Text = "";
                    txtMablaghMenu.Text = "";
                }
                con.Close();

            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDay_Click(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            string FirstDate = AzTarikh.Text;
            string EndDate = TaTarikh.Text;

            Int16 StartYear = Convert.ToInt16(FirstDate.Substring(0,4));
            Int16 StartMonth = Convert.ToInt16(FirstDate.Substring(5,2));
            Int16 StartDay = Convert.ToInt16(FirstDate.Substring(8,2));

            Int16 EndYear = Convert.ToInt16(EndDate.Substring(0, 4));
            Int16 EndMonth = Convert.ToInt16(EndDate.Substring(5, 2));
            Int16 EndDay = Convert.ToInt16(EndDate.Substring(8, 2));

            DateTime StartDateTime = p.ToDateTime(StartYear, StartMonth, StartDay,0,0,0,0);
            DateTime EndDateTime = p.ToDateTime(EndYear, EndMonth, EndDay, 0, 0, 0, 0);

            TimeSpan Difference = EndDateTime - StartDateTime;

            int Days = Difference.Days;
            txtDay.Text = Days.ToString();
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCodeService.Text=="")
                {
                    MessageBox.Show("سرویسی انتخاب نشده است");
                    return;
                }
                else
                {
                    dgvFactor.Rows.Add(txtCodeService.Text, txtNameService.Text, txtTedadService.Text, txtMablaghService.Text);
                    txtCodeService.Text = "";
                    txtNameService.Text = "";
                    txtMablaghService.Text = "";

                    int JameService = 0;
                    for (int i = 0; i < dgvFactor.Rows.Count; i++)
                    {
                        JameService = (Convert.ToInt32(dgvFactor.Rows[i].Cells[2].Value) * Convert.ToInt32(dgvFactor.Rows[i].Cells[3].Value));
                        dgvFactor.Rows[i].Cells[4].Value = JameService;
                    }
                }
         
            }
            catch (Exception)
            {
                MessageBox.Show("مشکلی پیش آمده است");
            }
 

        }

        private void btnAddMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCodeMenu.Text == "")
                {
                    MessageBox.Show("سرویسی انتخاب نشده است");
                    return;
                }
                else
                {
                    dgvFactor.Rows.Add(txtCodeMenu.Text, txtNameMenu.Text, txtTedadMenu.Text, txtMablaghMenu.Text);
                    txtCodeMenu.Text = "";
                    txtNameMenu.Text = "";
                    txtMablaghMenu.Text = "";

                    int JameMenu = 0;
                    for (int i = 0; i < dgvFactor.Rows.Count; i++)
                    {
                        JameMenu = (Convert.ToInt32(dgvFactor.Rows[i].Cells[2].Value) * Convert.ToInt32(dgvFactor.Rows[i].Cells[3].Value));
                        dgvFactor.Rows[i].Cells[4].Value = JameMenu;
                    }
                }

            }
            catch (Exception)
            {
                MessageBox.Show("مشکلی پیش آمده است");
            }
        }

        private void btnSum_Click(object sender, EventArgs e)
        {
            try
            {

            int JameKolFactor = 0;
            int JameMablaghKol = 0;
            int JameKolService = 0;
            int Maliyat = 0;

            int JameServiceMenu = 0;
            for (int i = 0; i < dgvFactor.Rows.Count; i++)
            {
                JameServiceMenu = JameServiceMenu + (Convert.ToInt32(dgvFactor.Rows[i].Cells[2].Value) * Convert.ToInt32(dgvFactor.Rows[i].Cells[3].Value));
            }
            txtKolService.Value = JameServiceMenu;

            int SumOtag = txtDay.Value * txtMablagh.Value;
            txtKolOtag.Text = SumOtag.ToString();

            JameMablaghKol = txtKolService.Value + txtKolOtag.Value;
            txtMablaghKol.Value = JameMablaghKol;

            Maliyat = Convert.ToInt32(txtMablaghKol.Value * dblTax.Value);
            txtMaliyat.Value = Maliyat;

            JameKolFactor = txtKolService.Value + txtMaliyat.Value + txtKolOtag.Value;
            txtMablaghNahaye.Value = JameKolFactor;
            }
            catch (Exception)
            {
                MessageBox.Show("مشکلی پیش آمده است");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvFactor.Rows.Count==0)
                {
                    MessageBox.Show("مقداری برای حذف وجود ندارد");
                }
                else
                {
                    dgvFactor.Rows.RemoveAt(dgvFactor.CurrentRow.Index);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("مشکلی پیش آمده است");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand sqlcmd1 = new SqlCommand("select Vaziyat from Otag where CodeOtag ='" + txtCodeOtag.Text + "'", con);


                string updateVaziyat = "update Otag set Vaziyat='" + "پر" + "' where CodeOtag='" + txtCodeOtag.Text + "'";
                SqlCommand cmd1 = new SqlCommand(updateVaziyat, con);
                cmd1.ExecuteNonQuery();

                con.Close();

                for (int i = 0; i < dgvFactor.Rows.Count; i++)
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.CommandText = "insert into Paziresh(CodePaziresh,Tarikh,CodeOtag,NameGroup,Emkanat,Mablagh,AzTarikh,TaTarikh,IdService,NameService,TedadService,MablaghService,MablaghOtag,MablaghServiceMenu,Maliyat,MablaghFactor,MablaghNahaye)values(@b,@c,@d,@e,@f,@g,@h,@i,@j,@k,@l,@m,@n,@o,@p,@q,@r)";
                    cmd.Parameters.AddWithValue("@b", CodePaziresh.Text);
                    cmd.Parameters.AddWithValue("@c", mskTarikh.Text);
                    cmd.Parameters.AddWithValue("@d", txtCodeOtag.Text);
                    cmd.Parameters.AddWithValue("@e", txtGroup.Text);
                    cmd.Parameters.AddWithValue("@f", txtEmkanat.Text);
                    cmd.Parameters.AddWithValue("@g", txtMablagh.Text);
                    cmd.Parameters.AddWithValue("@h", AzTarikh.Text);
                    cmd.Parameters.AddWithValue("@i", TaTarikh.Text);
             
                    cmd.Parameters.AddWithValue("@j", Convert.ToInt32(dgvFactor.Rows[i].Cells[0].Value));
                    cmd.Parameters.AddWithValue("@k", dgvFactor.Rows[i].Cells[1].Value);
                    cmd.Parameters.AddWithValue("@l", Convert.ToInt32(dgvFactor.Rows[i].Cells[2].Value));
                    cmd.Parameters.AddWithValue("@m", Convert.ToInt32(dgvFactor.Rows[i].Cells[3].Value));


                    cmd.Parameters.AddWithValue("@n", txtKolOtag.Text);
                    cmd.Parameters.AddWithValue("@o", txtKolService.Text);
                    cmd.Parameters.AddWithValue("@p", txtMaliyat.Text);
                    cmd.Parameters.AddWithValue("@q", txtMablaghKol.Text);
                    cmd.Parameters.AddWithValue("@r", txtMablaghNahaye.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("ثبت با موفقیت انجام شد");

         
                }
       
            }
            catch (Exception)
            {
                
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            StiReport Report = new StiReport();
            Report.Load("Report/rptPaziresh.mrt");
            Report.Compile();
            Report["CodePaziresh"] = Convert.ToInt32(CodePaziresh.Text);
            Report["strAddress"] = Address;
            Report["strTel"] = Tel;
            Report.ShowWithRibbonGUI();
        }

        private void btnVariz_Click(object sender, EventArgs e)
        {
            frmVariz frm = new frmVariz();
            frm.txtMablagh.Text = txtMablaghNahaye.Text;
            frm.mskTarikh.Text = mskTarikh.Text;
            frm.txtTozih.Text = "پرداخت مبلغ فاکتور پذیرش به صورت نقدی به شماره پذیرش " + CodePaziresh.Text;
            frm.ShowDialog();
        }

        private void btnChekD_Click(object sender, EventArgs e)
        {
            frmChekD frm = new frmChekD();
            frm.txtMablagh.Text = txtMablaghNahaye.Text;
            frm.mskTarikh.Text = mskTarikh.Text;
            frm.txtTozih.Text = "پرداخت مبلغ فاکتور پذیرش به صورت چک به شماره پذیرش " + CodePaziresh.Text;
            frm.ShowDialog();
        }
    }
}
