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
    public partial class Receteler : Form
    {
        public Receteler()
        {
            InitializeComponent();
        }

        void Members()
        {
            Hastalar Hs = new Hastalar();
            string query = "SELECT * FROM ReceteTbl";
            DataSet ds = Hs.ShowSick(query);
            ReceteDGV.DataSource = ds.Tables[0];
        }
        void Filter()
        {
            Hastalar Hs = new Hastalar();
            string query = "SELECT * FROM ReceteTbl where HasAd like '%" + AraTb.Text + "%'";
            DataSet ds = Hs.ShowSick(query);
            ReceteDGV.DataSource = ds.Tables[0];
        }
        void Reset()
        {
            RecAdCb.SelectedItem = "";
            RecTedTb.Text = "";
            RecTutarTb.Text = "";
            RecIlacTb.Text = "";
            RecMiktarTb.Text = "";
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
            RecAdCb.ValueMember = "HAd";
            RecAdCb.DataSource = dt;
            connection.Close();
        }
        int key = 0;
        private void FillThreatment()
        {
            SqlConnection connection = MyCon.GetCon();
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM RandevuTbl where Hasta='" + RecAdCb.SelectedValue.ToString() + "'", connection);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(command);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                RecTedTb.Text = dr["Tedavi"].ToString();
            }
            connection.Close();
        }
        private void FillPrice()
        {
            SqlConnection connection = MyCon.GetCon();
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM TedaviTbl where TAd='" + RecTedTb.Text + "'", connection);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(command);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                RecTutarTb.Text = dr["TUcret"].ToString();
            }
            connection.Close();
        }
        private void Receteler_Load(object sender, EventArgs e)
        {
            FillPatient();
            Members();
            Reset();
        }

        private void RecAdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FillThreatment();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            AnaSayfa ana = new AnaSayfa();
            ana.Show();
            this.Hide();
        }

        private void RecTutarTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void RecTedTb_TextChanged(object sender, EventArgs e)
        {
            FillPrice();
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            string query = "insert into ReceteTbl values('" + RecAdCb.SelectedValue.ToString() + "','" + RecTedTb.Text + "'," + RecTutarTb.Text + ",'" + RecIlacTb.Text + "'," + RecMiktarTb.Text + ")";
            Hastalar Hs = new Hastalar();
            try
            {
                Hs.AddSick(query);
                MessageBox.Show("Recete Başarıyla Eklendi");
                Members();
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReceteDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ReceteDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RecAdCb.Text = ReceteDGV.SelectedRows[0].Cells[1].Value.ToString();
            RecTedTb.Text = ReceteDGV.SelectedRows[0].Cells[2].Value.ToString();
            RecTutarTb.Text = ReceteDGV.SelectedRows[0].Cells[3].Value.ToString();
            RecIlacTb.Text = ReceteDGV.SelectedRows[0].Cells[4].Value.ToString();
            RecMiktarTb.Text = ReceteDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (RecAdCb.Text == "")
            {
                key = 0;
            }
            else
            {

                key = Convert.ToInt32(ReceteDGV.SelectedRows[0].Cells[0].Value.ToString());
                RecAdCb.Text = ReceteDGV.SelectedRows[0].Cells[1].Value.ToString();
            }
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Silinecek Reçeteyi Seçiniz");
            }
            else
            {
                try
                {
                    string query = "DELETE FROM ReceteTbl where RecId=" + key + "";
                    Hs.DeletePatient(query);
                    MessageBox.Show("Reçete Silindi");
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

        private void AraTb_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }
        Bitmap bitmap;
        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            int height = ReceteDGV.Height;
            ReceteDGV.Height = ReceteDGV.RowCount * ReceteDGV.RowTemplate.Height * 2;
            bitmap = new Bitmap(ReceteDGV.Width, ReceteDGV.Height);
            ReceteDGV.DrawToBitmap(bitmap, new Rectangle(0, 10, ReceteDGV.Width, ReceteDGV.Height));
            ReceteDGV.Height = height;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0);
        }

        private void guna2GradientButton10_Click(object sender, EventArgs e)
        {
            Hasta hst = new Hasta();
            hst.Show();
            this.Hide();
        }

        private void guna2GradientButton8_Click(object sender, EventArgs e)
        {
            Tedavi tdv = new Tedavi();
            tdv.Show();
            this.Hide();
        }

        private void guna2GradientButton9_Click(object sender, EventArgs e)
        {
            Randevu rnd = new Randevu();
            rnd.Show();
            this.Hide();
        }
    }
}

