using Data;
using Data.Repo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Guna.UI2.Native.WinApi;

namespace DisKlinik
{
    public partial class Randevu : Form
    {
        public Randevu()
        {
            InitializeComponent();
        }
        ConnectionString MyCon = new ConnectionString();
        private void FillPatient()
        {
            SqlConnection connection = MyCon.GetCon();
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT HAd FROM HastaTbl", connection);
            SqlDataReader rdr;
            rdr = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("HAd", typeof(string));
            dt.Load(rdr);
            RAdCb.ValueMember = "HAd";
            RAdCb.DataSource = dt;
            connection.Close();
        }
        private void FillTreatment()
        {
            SqlConnection connection = MyCon.GetCon();
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT TAd FROM TedaviTbl", connection);
            SqlDataReader rdr;
            rdr = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TAd", typeof(string));
            dt.Load(rdr);
            RTedaviCb.ValueMember = "TAd";
            RTedaviCb.DataSource = dt;
            connection.Close();
        }
        private void Randevu_Load(object sender, EventArgs e)
        {
            FillPatient();
            FillTreatment();
            Members();
            Reset();
        }
        void Members()
        {
            Hastalar Hs = new Hastalar();
            string query = "SELECT * FROM RandevuTbl";
            DataSet ds = Hs.ShowSick(query);
            RandevuDGV.DataSource = ds.Tables[0];
        }
        void Filter()
        {
            Hastalar Hs = new Hastalar();
            string query = "SELECT * FROM RandevuTbl where Hasta like '%" + AraTb.Text + "%'";
            DataSet ds = Hs.ShowSick(query);
            RandevuDGV.DataSource = ds.Tables[0];
        }
        void Reset()
        {
            RAdCb.SelectedIndex = -1;
            RTedaviCb.SelectedIndex = -1;
            RTarihCb.Text = "";
            RSaatCb.SelectedIndex = -1;
        }
        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            string query = "insert into RandevuTbl values('" + RAdCb.SelectedValue.ToString() + "','" + RTedaviCb.SelectedValue.ToString() + "','" + RTarihCb.Text + "','" + RSaatCb.Text + "')";
            Randevular Hs = new Randevular();
            try
            {
                Hs.AddAppointment(query);
                MessageBox.Show("Randevu Başarıyla Eklendi");
                Members();
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        int key = 0;
        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Güncellenecek Randevuyu Seçiniz");
            }
            else
            {
                try
                {
                    string query = "UPDATE RandevuTbl set Hasta='" + RAdCb.SelectedValue.ToString() + "',Tedavi='" + RTedaviCb.SelectedValue.ToString() + "',RTarih='" + RTarihCb.Text + "',RSaat='" + RSaatCb.Text + "' where RId=" + key + ";";
                    Hs.DeletePatient(query);
                    MessageBox.Show("Randevu Günellendi");
                    Members();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void RandevuDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RAdCb.SelectedValue = RandevuDGV.SelectedRows[0].Cells[1].Value.ToString();
            RTedaviCb.SelectedValue = RandevuDGV.SelectedRows[0].Cells[2].Value.ToString();
            RTarihCb.Text = RandevuDGV.SelectedRows[0].Cells[3].Value.ToString();
            RSaatCb.Text = RandevuDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (RAdCb.SelectedIndex == -1)
            {
                key = 0;
            }
            else
            {

                key = Convert.ToInt32(RandevuDGV.SelectedRows[0].Cells[0].Value.ToString());
                RAdCb.Text = RandevuDGV.SelectedRows[0].Cells[1].Value.ToString();
            }
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Silinecek Randevuyu Seçiniz");
            }
            else
            {
                try
                {
                    string query = "DELETE FROM RandevuTbl where RId=" + key + "";
                    Hs.DeletePatient(query);
                    MessageBox.Show("Randevu Silindi");
                    Members();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            AnaSayfa ana = new AnaSayfa();
            ana.Show();
            this.Hide();
        }

        private void AraTb_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void guna2GradientButton9_Click(object sender, EventArgs e)
        {
            Hasta hst = new Hasta();
            hst.Show();
            this.Hide();
        }

        private void guna2GradientButton7_Click(object sender, EventArgs e)
        {
            Tedavi tdv = new Tedavi();
            tdv.Show();
            this.Hide();
        }

        private void guna2GradientButton6_Click(object sender, EventArgs e)
        {
            Receteler rct = new Receteler();
            rct.Show();
            this.Hide();
        }
    }
}
