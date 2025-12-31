using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity; //
using System.Security.Cryptography.Xml;

namespace WindowsFormsApp21
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        void listele()
        {
            PersonsEntities pe=new PersonsEntities();

            var liste = pe.Employe.Include(p => p.City).Include(p => p.Town)
                .Select(p => new
                {
                    p.Id,
                    p.Ad,
                    p.Soyad,
                    p.Telefon,
                    p.Eposta,
                    CityName = p.City.CityName,
                    TownName = p.Town.TownName,
                    p.Detail
                })
                .ToList();

            dataGridView1.DataSource = liste;  
        }
        void sehirler()
        {
            PersonsEntities pe = new PersonsEntities();
            comboBox1.DisplayMember = "CityName";
            comboBox1.ValueMember = "Id";
            comboBox1.DataSource=pe.City.ToList();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
            sehirler();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PersonsEntities pe = new PersonsEntities();
            comboBox2.DisplayMember = "TownName";
            comboBox2.ValueMember = "Id";
            comboBox2.DataSource=pe.Town.Where(x=>x.CityId==(comboBox1.SelectedIndex+1)).ToList();

        }

        private void button1_Click(object sender, EventArgs e) // ekleme
        {
            Employe newpers=new Employe();
            newpers.Ad = textBox1.Text;
            newpers.Soyad = textBox2.Text;
            newpers.Telefon = textBox3.Text;    
            newpers.Eposta= textBox4.Text;
            newpers.CityId = (int)comboBox1.SelectedValue;
            newpers.TownId = (int)comboBox2.SelectedValue;
            newpers.Detail= textBox5.Text;

            PersonsEntities pe=new PersonsEntities();
            pe.Employe.Add(newpers);
            pe.SaveChanges();
            listele();
        }

        private void button2_Click(object sender, EventArgs e) //silme
        {
            PersonsEntities newpers=new PersonsEntities();
            var x= newpers.Employe.Find(secilenkayit);
            newpers.Employe.Remove(x);
            newpers.SaveChanges();
            listele();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        int secilenkayit;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            secilenkayit = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e) //güncelle
        {
            PersonsEntities persons = new PersonsEntities();
            var newpers = persons.Employe.Find(secilenkayit);


            newpers.Ad = textBox1.Text;
            newpers.Soyad = textBox2.Text;
            newpers.Telefon = textBox3.Text;
            newpers.Eposta = textBox4.Text;
            newpers.CityId = (int)comboBox1.SelectedValue;
            newpers.TownId = (int)comboBox2.SelectedValue;
            newpers.Detail = textBox5.Text;

            persons.SaveChanges();
            listele();

        }

        private void button4_Click(object sender, EventArgs e) //arama
        {
            PersonsEntities newpers = new PersonsEntities();
            var liste = newpers.Employe.Include(p => p.City).Include(p => p.Town)
            .Where(x=>x.Ad==textBox1.Text)
            .Select(p => new
            {
                p.Id,
                p.Ad,
                p.Soyad,
                p.Telefon,
                p.Eposta,
                CityName = p.City.CityName,
                TownName = p.Town.TownName,
                p.Detail
            })
            
            .ToList();

            dataGridView1.DataSource = liste;
            // dataGridView1.DataSource= newpers.Employe.Where(x=>x.Ad==textBox1.Text).ToList();
        }
    }
}
