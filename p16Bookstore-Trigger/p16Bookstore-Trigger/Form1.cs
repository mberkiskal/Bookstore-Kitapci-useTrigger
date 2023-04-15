using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace p16Bookstore_Trigger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn= new SqlConnection("Data Source=DESKTOP-23T2RIK\\SQLEXPRESS;Initial Catalog=p16Bookstore;Integrated Security=True");

        void listele()
        {
            SqlDataAdapter da= new SqlDataAdapter("select * from Table_Kitaplar",conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void sayac()
        {
            conn.Open();
            SqlCommand cmd1 = new SqlCommand("select*from Table_Sayac",conn);
            SqlDataReader dr = cmd1.ExecuteReader();
            while(dr.Read())
            {
                lblAdet.Text = dr[0].ToString();
            }
            conn.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
            sayac();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd2 = new SqlCommand("insert into Table_Kitaplar (AD,YAZAR,SAYFA,YAYINEVI,TUR) values (@p1,@p2,@p3,@p4,@p5)", conn);
            cmd2.Parameters.AddWithValue("@p1", txtAd.Text);
            cmd2.Parameters.AddWithValue("@p2", txtYazar.Text);
            cmd2.Parameters.AddWithValue("@p3", txtSayfaS.Text);
            cmd2.Parameters.AddWithValue("@p4", txtYayınevi.Text);
            cmd2.Parameters.AddWithValue("@p5", txtTur.Text);
            cmd2.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Kitap Başarıyla Eklendi!", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            sayac();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            txtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtYazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtSayfaS.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtYayınevi.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtTur.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd3 = new SqlCommand("delete Table_Kitaplar where ID=@p1", conn);
            cmd3.Parameters.AddWithValue("@p1", txtId.Text);
            cmd3.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Kitap Başarıyla Silindi!", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            sayac();
        }
    }
}
