using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UKK_BetaMart
{
    public partial class Form1 : Form
    {
        string alamat, query;
        OleDbConnection koneksi;
        OleDbCommand perintah;
        OleDbDataAdapter adapter;
        DataSet db = new DataSet();

        public Form1()
        {
            InitializeComponent(); alamat = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:/12 RPL 3/Bu Berlian/ukk betamart/BetaMart/BetaMartDB.accdb");
            koneksi = new OleDbConnection(alamat);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                query = string.Format("select*from barangtb");
                koneksi.Open();
                perintah = new OleDbCommand(query, koneksi);
                adapter = new OleDbDataAdapter(perintah);
                perintah.ExecuteNonQuery();
                db.Clear();
                adapter.Fill(db);
                koneksi.Close();
                dataGridView1.DataSource = db.Tables[0];
                dataGridView1.Columns[0].Width = 130;
                dataGridView1.Columns[0].HeaderText = "ID Barang";
                dataGridView1.Columns[1].Width = 130;
                dataGridView1.Columns[1].HeaderText = "Nama Barang";
                dataGridView1.Columns[2].Width = 130;
                dataGridView1.Columns[2].HeaderText = "Jenis Barang";
                dataGridView1.Columns[3].Width = 130;
                dataGridView1.Columns[3].HeaderText = "Jumlah Barang";
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
                {
                    query = string.Format("insert into barangtb values ({0},'{1}','{2}','{3}')", textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
                    koneksi.Open();
                    perintah = new OleDbCommand(query, koneksi);
                    adapter = new OleDbDataAdapter(perintah);
                    if (perintah.ExecuteNonQuery() != 1)
                    {
                        MessageBox.Show("Data Gagal Disimpan");
                        koneksi.Close();
                    }
                    else
                    {
                        MessageBox.Show("Data Berhasil Disimpan");
                        koneksi.Close();
                        Form1_Load(null, null);
                    }
                }
                else { MessageBox.Show("Lengkapi Data Tersebut"); }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "")
                {
                    query = string.Format("update barangtb set nama_barang='{0}',jenis_barang='{1}',jumlah_barang='{2}' where id_barang={3}", textBox2.Text, textBox3.Text, textBox4.Text, textBox1.Text);
                    koneksi.Open();
                    perintah = new OleDbCommand(query, koneksi);
                    adapter = new OleDbDataAdapter(perintah);
                    if (perintah.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Data Berhasil di Update");
                        koneksi.Close();
                        Form1_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Data Gagal di Update");
                        koneksi.Close();
                    }
                }
                else { MessageBox.Show("Kode Barang Kosong"); }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (MessageBox.Show("Apakah Anda yakin Ingin Menghapus?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    query = string.Format("delete from barangtb where id_barang={0}", textBox1.Text);
                    koneksi.Open();
                    perintah = new OleDbCommand(query, koneksi);
                    adapter = new OleDbDataAdapter(perintah);
                    if (perintah.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Data Berhasil di Hapus");
                        koneksi.Close();
                        Form1_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Data Gagal di Hapus");
                        koneksi.Close();
                    }
                }
            }
            else { MessageBox.Show("Kode Barang Kosong"); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "")
                {
                    query = string.Format("select*from barangtb where id_barang={0}", textBox1.Text);
                    db.Clear();
                    koneksi.Open();
                    perintah = new OleDbCommand(query, koneksi);
                    adapter = new OleDbDataAdapter(perintah);
                    perintah.ExecuteNonQuery();
                    adapter.Fill(db);
                    koneksi.Close();
                    if (db.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow kolom in db.Tables[0].Rows)
                        {
                            textBox1.Text = kolom["id_barang"].ToString();
                            textBox2.Text = kolom["nama_barang"].ToString();
                            textBox3.Text = kolom["jenis_barang"].ToString();
                            textBox4.Text = kolom["jumlah_barang"].ToString();
                        }
                        dataGridView1.DataSource = db.Tables[0];
                        textBox1.Enabled = false;
                        button1.Enabled = false;
                    }
                    else { MessageBox.Show("Data Tidak Ada"); }
                }
                else { MessageBox.Show("Kode Barang Kosong"); }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1_Load(null, null);
            dataGridView1.Refresh();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            button1.Enabled = true;
            textBox1.Enabled = true;
        }
    }
}
