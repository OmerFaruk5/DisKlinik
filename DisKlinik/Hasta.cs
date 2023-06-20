using Data.Repo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisKlinik
{
    public partial class Hasta : Form
    {
        public Hasta()
        {
            InitializeComponent();
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            string query = "insert into HastaTbl values('" + HAdSoyadTb.Text + "','" + HTelefonTb.Text + "','" + HAdresTb.Text + "','" + HDogumTarihCb.Text + "','" + HCinsiyetCb.SelectedItem.ToString() + "','" + HAlerjiTb.Text + "')";
            Hastalar Hs = new Hastalar();
            try
            {
                Hs.AddSick(query);
                MessageBox.Show("Hasta Başarıyla Eklendi");
                Members();
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        void Members()
        {
            Hastalar Hs = new Hastalar();
            string query = "SELECT * FROM HastaTbl";
            DataSet ds = Hs.ShowSick(query);
            HastaDGV.DataSource = ds.Tables[0];
        }
        void Filter()
        {
            Hastalar Hs = new Hastalar();
            string query = "SELECT * FROM HastaTbl where HAd like '%"+AraTb.Text+"%'";
            DataSet ds = Hs.ShowSick(query);
            HastaDGV.DataSource = ds.Tables[0];
        }
        void Reset()
        {
            HAdSoyadTb.Text = "";
            HTelefonTb.Text = "";
            HAdresTb.Text = "";
            HDogumTarihCb.Text = "";
            HCinsiyetCb.SelectedItem = "";
            HAlerjiTb.Text = "";
        }

        private void Hasta_Load(object sender, EventArgs e)
        {
            Members();
        }
        int key = 0;
        private void HastaDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            HAdSoyadTb.Text = HastaDGV.SelectedRows[0].Cells[1].Value.ToString();
            HTelefonTb.Text = HastaDGV.SelectedRows[0].Cells[2].Value.ToString();
            HAdresTb.Text = HastaDGV.SelectedRows[0].Cells[3].Value.ToString();
            HDogumTarihCb.Text = HastaDGV.SelectedRows[0].Cells[4].Value.ToString();
            HCinsiyetCb.SelectedItem = HastaDGV.SelectedRows[0].Cells[5].Value.ToString();
            HAlerjiTb.Text = HastaDGV.SelectedRows[0].Cells[6].Value.ToString();
            if (HAdSoyadTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(HastaDGV.SelectedRows[0].Cells[0].Value.ToString());
                HAdSoyadTb.Text = HastaDGV.SelectedRows[0].Cells[1].Value.ToString();

            }
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Silinecek Hastayı Seçiniz");
            }
            else
            {
                try
                {
                    string query = "DELETE FROM HastaTbl where HId=" + key + "";
                    Hs.DeletePatient(query);
                    MessageBox.Show("Hasta Silindi");
                    Members();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }

        }


        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Güncellenecek Hastayı Seçiniz");
            }
            else
            {
                try
                {
                    string query = "UPDATE HastaTbl set HAd='" + HAdSoyadTb.Text + "',HTelefon='" + HTelefonTb.Text + "',HAdres='" + HAdresTb.Text + "',HDTarih='" + HDogumTarihCb.Text + "',HCinsiyet='" + HCinsiyetCb.SelectedItem.ToString() + "',HAlerji='" + HAlerjiTb.Text + "' where HId=" + key + ";";
                    Hs.UpdatePatient(query);
                    MessageBox.Show("Hasta Günellendi");
                    Members();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            AnaSayfa ana= new AnaSayfa();
            ana.Show();
            this.Hide();
        }

        private void AraTb_TextChanged(object sender, EventArgs e)
        {
            Filter(); 
        }

        private void guna2GradientButton8_Click(object sender, EventArgs e)
        {
            Randevu rnd = new Randevu();
            rnd.Show();
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
