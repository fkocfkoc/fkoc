using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DomuzKumbara.Exception_Library;
using DomuzKumbara.Concrete;

namespace DomuzKumbara
{
    public partial class KumbaraForm : Form
    {
        Kumbara kumbara = new Kumbara(75000);

        private readonly List<KagitPara> KagitParalar = new List<KagitPara>();
        private readonly List<BozukPara> BozukParalar = new List<BozukPara>();
        private readonly BindingList<Para> atilanKagitParalar = new BindingList<Para>();
        private readonly BindingList<Para> atilanBozukParalar = new BindingList<Para>();

        Para secilen;
        string atilacak;
        bool katlandiMi = false;
        int kirilmaSayisi = 0;
        double birikenMiktar = 0;
        double toplamHacim = 0;
        private Button btnKatla;
        private Label lblBozukPara;
        private ComboBox cmbBozukPara;
        private Label label2;
        private Button btnSalla;
        private Button btnParaAt;
        private Button btnKir;
        private ComboBox cmbKagitPara;
        private Label lblFazladan;
        private Label lblParaHacmi;
        private Label lblBozukParalar;
        private DataGridView dgvAtilanBozukParalar;
        private Label lblMiktar;
        private Label lblKagitParalar;
        private DataGridView dgvAtilanKagitParalar;
        private PictureBox pictureBox2;
        double fazlaHacim = 0;

        public KumbaraForm()
        {
            InitializeComponent();
            
            ParalariOlusturEkle();

        }
        private void ParalariOlusturEkle()
        {
            BozukParalar.Add(new BozukPara() { Ad = "1 Kuruş", Deger = 0.01, Cap = 16.50, Yukseklik = 1.35 });
            BozukParalar.Add(new BozukPara() { Ad = "5 Kuruş", Deger = 0.05, Cap = 17.50, Yukseklik = 1.65 });
            BozukParalar.Add(new BozukPara() { Ad = "10 Kuruş", Deger = 0.10, Cap = 18.50, Yukseklik = 1.65 });
            BozukParalar.Add(new BozukPara() { Ad = "25 Kuruş", Deger = 0.25, Cap = 20.50, Yukseklik = 1.65 });
            BozukParalar.Add(new BozukPara() { Ad = "50 Kuruş", Deger = 0.50, Cap = 23.85, Yukseklik = 1.90 });
            BozukParalar.Add(new BozukPara() { Ad = "1 Lira", Deger = 1.0, Cap = 26.15, Yukseklik = 1.90 });

            KagitParalar.Add(new KagitPara() { Ad = "5 Lira", Deger = 5.0, En = 64.0, Boy = 130.0, Yukseklik = 0.25 });
            KagitParalar.Add(new KagitPara() { Ad = "10 Lira", Deger = 10.0, En = 64.0, Boy = 136.0, Yukseklik = 0.25 });
            KagitParalar.Add(new KagitPara() { Ad = "20 Lira", Deger = 20.0, En = 68.0, Boy = 142.0, Yukseklik = 0.25 });
            KagitParalar.Add(new KagitPara() { Ad = "50 Lira", Deger = 50.0, En = 68.0, Boy = 148.0, Yukseklik = 0.25 });
            KagitParalar.Add(new KagitPara() { Ad = "100 Lira", Deger = 100.0, En = 72.0, Boy = 154.0, Yukseklik = 0.25 });
            KagitParalar.Add(new KagitPara() { Ad = "200 Lira", Deger = 200.0, En = 72.0, Boy = 160.0, Yukseklik = 0.25 });

            cmbKagitPara.Items.Add("Seçiniz");
            foreach (var item in KagitParalar)
            {
                cmbKagitPara.Items.Add(item.Ad);
            }
            cmbKagitPara.SelectedIndex = 0;
            cmbBozukPara.Items.Add("Seçiniz");
            foreach (var item in BozukParalar)
            {
                cmbBozukPara.Items.Add(item.Ad);
            }
            cmbBozukPara.SelectedIndex = 0;
        }
        private void cmbKagitPara_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKagitPara.SelectedIndex > 0)
            {
                cmbBozukPara.Enabled = false;
                btnKatla.Visible = true;
                atilacak = cmbKagitPara.SelectedItem.ToString();
                foreach (var item in KagitParalar)
                {
                    if (atilacak == item.Ad)
                    {
                        secilen = (KagitPara)item;
                    }
                }
            }
        }
        private void cmbBozukPara_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBozukPara.SelectedIndex > 0)
            {
                cmbKagitPara.Enabled = false;
                atilacak = cmbBozukPara.SelectedItem.ToString();
                foreach (var item in BozukParalar)
                {
                    if (atilacak == item.Ad)
                    {
                        secilen = (BozukPara)item;
                    }
                }
            }
        }
        
        
        private void VerileriSifirla()
        {
            cmbBozukPara.SelectedIndex = 0;
            cmbKagitPara.SelectedIndex = 0;
            katlandiMi = false;
            btnKatla.Text = "Katla!";
            btnKatla.Enabled = true;
            btnKatla.Visible = false;
            secilen = null;
            cmbBozukPara.Enabled = true;
            cmbKagitPara.Enabled = true;
        }

        private void btnKatla_Click(object sender, EventArgs e)
        {
            KagitPara katlanacak = (KagitPara)secilen;
            if (katlanacak != null)
            {
                katlandiMi = true;
                katlanacak.Katla(katlanacak.Hacim());
                btnKatla.Text = "Katlandı!";
                btnKatla.Enabled = false;
            }
            
        }
        private void btnSalla_Click(object sender, EventArgs e)
        {
            if (toplamHacim > fazlaHacim)
            {
                toplamHacim = toplamHacim - kumbara.Salla(fazlaHacim);
                kumbara.ParaEkle(-kumbara.Salla(fazlaHacim));
            }
           
          
            fazlaHacim = 0;
            btnSalla.Enabled = false;
            HacimleriYazdir();
            VerileriSifirla();
        }
        private void btnKir_Click(object sender, EventArgs e)
        {
            if (kirilmaSayisi == 0)
            {
                try
                {
                    if (birikenMiktar > 0)
                    {

                        KumbaraFormu(atilanKagitParalar, atilanBozukParalar, birikenMiktar);
                    }
                    else
                    {
                        throw new KumbaraBosException();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                btnKir.Text = "Yapıştır!";
                btnParaAt.Enabled = false;
                btnKatla.Enabled = false;
                btnSalla.Enabled = false;
                atilanKagitParalar.Clear();
                atilanBozukParalar.Clear();
                kirilmaSayisi++;
                cmbBozukPara.Enabled = false;
                cmbKagitPara.Enabled = false;
                KumbaraSifirla();
               
            }
            else if (kirilmaSayisi == 1)
            {
                btnKir.Text = "Kır!";
                cmbBozukPara.Enabled = true;
                cmbKagitPara.Enabled = true;
                btnParaAt.Enabled = true;
                btnKatla.Enabled = true;
                btnSalla.Enabled = true;
                kirilmaSayisi++;
                KumbaraSifirla();
               
            }
            else if (kirilmaSayisi > 1)
            {
                try
                {
                    if (birikenMiktar > 0)
                    {

                        KumbaraFormu(atilanKagitParalar, atilanBozukParalar, birikenMiktar);

                    }
                    else
                    {
                        throw new KumbaraBosException();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                btnKir.Text = "Kırılamaz!";
                btnParaAt.Enabled = false;
                btnKir.Enabled = false;
                btnKatla.Enabled = false;
                btnSalla.Enabled = false;
                KumbaraSifirla();
                
                try
                {
                    throw new KumbaraKullanilamazException();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Close();
            }
        }

        private void KumbaraSifirla()
        {
            birikenMiktar = 0;
            kumbara.ParaMiktari = 0;
            toplamHacim = 0;
            fazlaHacim = 0;
            lblParaHacmi.Text = "Paraların kapladığı hacim: 0";
            lblFazladan.Text = "Paraların kapladığı fazladan hacim: 0";
            
        }
       
        public void HacimleriYazdir()
        {
            toplamHacim = Math.Round(toplamHacim, 2);
            lblParaHacmi.Text = "Paraların kapladığı hacim: " + toplamHacim.ToString()+ " ₺";
            fazlaHacim = Math.Round(fazlaHacim, 2);
            lblFazladan.Text = "Paraların kapladığı fazladan hacim: " + fazlaHacim.ToString()+ " ₺";
        }

        

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnKatla = new System.Windows.Forms.Button();
            this.lblBozukPara = new System.Windows.Forms.Label();
            this.cmbBozukPara = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSalla = new System.Windows.Forms.Button();
            this.btnParaAt = new System.Windows.Forms.Button();
            this.btnKir = new System.Windows.Forms.Button();
            this.cmbKagitPara = new System.Windows.Forms.ComboBox();
            this.lblFazladan = new System.Windows.Forms.Label();
            this.lblParaHacmi = new System.Windows.Forms.Label();
            this.lblBozukParalar = new System.Windows.Forms.Label();
            this.dgvAtilanBozukParalar = new System.Windows.Forms.DataGridView();
            this.lblMiktar = new System.Windows.Forms.Label();
            this.lblKagitParalar = new System.Windows.Forms.Label();
            this.dgvAtilanKagitParalar = new System.Windows.Forms.DataGridView();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAtilanBozukParalar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAtilanKagitParalar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnKatla
            // 
            this.btnKatla.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnKatla.ForeColor = System.Drawing.Color.Black;
            this.btnKatla.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnKatla.Location = new System.Drawing.Point(193, 51);
            this.btnKatla.Name = "btnKatla";
            this.btnKatla.Size = new System.Drawing.Size(115, 32);
            this.btnKatla.TabIndex = 29;
            this.btnKatla.Text = "Katla!";
            this.btnKatla.UseVisualStyleBackColor = false;
            this.btnKatla.Visible = false;
            this.btnKatla.Click += new System.EventHandler(this.btnKatla_Click_1);
            // 
            // lblBozukPara
            // 
            this.lblBozukPara.AutoSize = true;
            this.lblBozukPara.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBozukPara.ForeColor = System.Drawing.Color.Black;
            this.lblBozukPara.Location = new System.Drawing.Point(53, 111);
            this.lblBozukPara.Name = "lblBozukPara";
            this.lblBozukPara.Size = new System.Drawing.Size(80, 15);
            this.lblBozukPara.TabIndex = 28;
            this.lblBozukPara.Text = "Bozuk Para";
            this.lblBozukPara.Click += new System.EventHandler(this.lblBozukPara_Click);
            // 
            // cmbBozukPara
            // 
            this.cmbBozukPara.ForeColor = System.Drawing.Color.Black;
            this.cmbBozukPara.FormattingEnabled = true;
            this.cmbBozukPara.Location = new System.Drawing.Point(56, 131);
            this.cmbBozukPara.Name = "cmbBozukPara";
            this.cmbBozukPara.Size = new System.Drawing.Size(121, 21);
            this.cmbBozukPara.TabIndex = 27;
            this.cmbBozukPara.SelectedIndexChanged += new System.EventHandler(this.cmbBozukPara_SelectedIndexChanged_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(53, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 15);
            this.label2.TabIndex = 26;
            this.label2.Text = "Kağıt Para";
            // 
            // btnSalla
            // 
            this.btnSalla.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSalla.BackColor = System.Drawing.Color.Lime;
            this.btnSalla.ForeColor = System.Drawing.Color.Black;
            this.btnSalla.Location = new System.Drawing.Point(56, 203);
            this.btnSalla.Name = "btnSalla";
            this.btnSalla.Size = new System.Drawing.Size(142, 63);
            this.btnSalla.TabIndex = 25;
            this.btnSalla.Text = "Çalkala!";
            this.btnSalla.UseVisualStyleBackColor = false;
            this.btnSalla.Click += new System.EventHandler(this.btnSalla_Click_1);
            // 
            // btnParaAt
            // 
            this.btnParaAt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnParaAt.ForeColor = System.Drawing.Color.Black;
            this.btnParaAt.Location = new System.Drawing.Point(193, 121);
            this.btnParaAt.Name = "btnParaAt";
            this.btnParaAt.Size = new System.Drawing.Size(115, 31);
            this.btnParaAt.TabIndex = 24;
            this.btnParaAt.Text = "Para At!";
            this.btnParaAt.UseVisualStyleBackColor = false;
            this.btnParaAt.Click += new System.EventHandler(this.btnParaAt_Click_1);
            // 
            // btnKir
            // 
            this.btnKir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnKir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnKir.ForeColor = System.Drawing.Color.Black;
            this.btnKir.Location = new System.Drawing.Point(193, 203);
            this.btnKir.Name = "btnKir";
            this.btnKir.Size = new System.Drawing.Size(128, 63);
            this.btnKir.TabIndex = 23;
            this.btnKir.Text = "Parçala!";
            this.btnKir.UseVisualStyleBackColor = false;
            this.btnKir.Click += new System.EventHandler(this.btnKir_Click_1);
            // 
            // cmbKagitPara
            // 
            this.cmbKagitPara.ForeColor = System.Drawing.Color.Black;
            this.cmbKagitPara.FormattingEnabled = true;
            this.cmbKagitPara.Location = new System.Drawing.Point(53, 62);
            this.cmbKagitPara.Name = "cmbKagitPara";
            this.cmbKagitPara.Size = new System.Drawing.Size(124, 21);
            this.cmbKagitPara.TabIndex = 22;
            this.cmbKagitPara.SelectedIndexChanged += new System.EventHandler(this.cmbKagitPara_SelectedIndexChanged_1);
            // 
            // lblFazladan
            // 
            this.lblFazladan.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblFazladan.AutoSize = true;
            this.lblFazladan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblFazladan.ForeColor = System.Drawing.Color.Black;
            this.lblFazladan.Location = new System.Drawing.Point(64, 336);
            this.lblFazladan.Name = "lblFazladan";
            this.lblFazladan.Size = new System.Drawing.Size(320, 17);
            this.lblFazladan.TabIndex = 32;
            this.lblFazladan.Text = "Paraların çalkalandıktan sonra kapladığı hacim:  0";
            // 
            // lblParaHacmi
            // 
            this.lblParaHacmi.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblParaHacmi.AutoSize = true;
            this.lblParaHacmi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblParaHacmi.ForeColor = System.Drawing.Color.Black;
            this.lblParaHacmi.Location = new System.Drawing.Point(64, 314);
            this.lblParaHacmi.Name = "lblParaHacmi";
            this.lblParaHacmi.Size = new System.Drawing.Size(182, 17);
            this.lblParaHacmi.TabIndex = 31;
            this.lblParaHacmi.Text = "Paraların kapladığı hacim: 0";
            // 
            // lblBozukParalar
            // 
            this.lblBozukParalar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBozukParalar.AutoSize = true;
            this.lblBozukParalar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBozukParalar.ForeColor = System.Drawing.Color.Black;
            this.lblBozukParalar.Location = new System.Drawing.Point(642, 29);
            this.lblBozukParalar.Name = "lblBozukParalar";
            this.lblBozukParalar.Size = new System.Drawing.Size(190, 15);
            this.lblBozukParalar.TabIndex = 37;
            this.lblBozukParalar.Text = " Kumbaradaki Bozuk Paralar";
            this.lblBozukParalar.Click += new System.EventHandler(this.lblBozukParalar_Click);
            // 
            // dgvAtilanBozukParalar
            // 
            this.dgvAtilanBozukParalar.AllowUserToAddRows = false;
            this.dgvAtilanBozukParalar.AllowUserToDeleteRows = false;
            this.dgvAtilanBozukParalar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAtilanBozukParalar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAtilanBozukParalar.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvAtilanBozukParalar.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgvAtilanBozukParalar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAtilanBozukParalar.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvAtilanBozukParalar.Location = new System.Drawing.Point(645, 59);
            this.dgvAtilanBozukParalar.MultiSelect = false;
            this.dgvAtilanBozukParalar.Name = "dgvAtilanBozukParalar";
            this.dgvAtilanBozukParalar.ReadOnly = true;
            this.dgvAtilanBozukParalar.RowHeadersVisible = false;
            this.dgvAtilanBozukParalar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAtilanBozukParalar.Size = new System.Drawing.Size(181, 207);
            this.dgvAtilanBozukParalar.TabIndex = 36;
            this.dgvAtilanBozukParalar.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAtilanBozukParalar_CellContentClick);
            // 
            // lblMiktar
            // 
            this.lblMiktar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMiktar.AutoSize = true;
            this.lblMiktar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblMiktar.Location = new System.Drawing.Point(512, 318);
            this.lblMiktar.Name = "lblMiktar";
            this.lblMiktar.Size = new System.Drawing.Size(202, 20);
            this.lblMiktar.TabIndex = 35;
            this.lblMiktar.Text = "Kumbara Biriken Miktar: 0 ₺";
            this.lblMiktar.Click += new System.EventHandler(this.lblMiktar_Click);
            // 
            // lblKagitParalar
            // 
            this.lblKagitParalar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblKagitParalar.AutoSize = true;
            this.lblKagitParalar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblKagitParalar.ForeColor = System.Drawing.Color.Black;
            this.lblKagitParalar.Location = new System.Drawing.Point(401, 29);
            this.lblKagitParalar.Name = "lblKagitParalar";
            this.lblKagitParalar.Size = new System.Drawing.Size(180, 15);
            this.lblKagitParalar.TabIndex = 34;
            this.lblKagitParalar.Text = "Kumbaradaki Kağıt Paralar";
            this.lblKagitParalar.Click += new System.EventHandler(this.lblKagitParalar_Click);
            // 
            // dgvAtilanKagitParalar
            // 
            this.dgvAtilanKagitParalar.AllowUserToAddRows = false;
            this.dgvAtilanKagitParalar.AllowUserToDeleteRows = false;
            this.dgvAtilanKagitParalar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAtilanKagitParalar.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvAtilanKagitParalar.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgvAtilanKagitParalar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAtilanKagitParalar.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvAtilanKagitParalar.Location = new System.Drawing.Point(404, 59);
            this.dgvAtilanKagitParalar.MultiSelect = false;
            this.dgvAtilanKagitParalar.Name = "dgvAtilanKagitParalar";
            this.dgvAtilanKagitParalar.ReadOnly = true;
            this.dgvAtilanKagitParalar.RowHeadersVisible = false;
            this.dgvAtilanKagitParalar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAtilanKagitParalar.Size = new System.Drawing.Size(174, 207);
            this.dgvAtilanKagitParalar.TabIndex = 33;
            this.dgvAtilanKagitParalar.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAtilanKagitParalar_CellContentClick);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DomuzKumbara.Properties.Resources.ampul;
            this.pictureBox2.Location = new System.Drawing.Point(451, 310);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(43, 28);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 38;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // KumbaraForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(918, 372);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lblBozukParalar);
            this.Controls.Add(this.dgvAtilanBozukParalar);
            this.Controls.Add(this.lblMiktar);
            this.Controls.Add(this.lblKagitParalar);
            this.Controls.Add(this.dgvAtilanKagitParalar);
            this.Controls.Add(this.lblFazladan);
            this.Controls.Add(this.lblParaHacmi);
            this.Controls.Add(this.btnKatla);
            this.Controls.Add(this.lblBozukPara);
            this.Controls.Add(this.cmbBozukPara);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSalla);
            this.Controls.Add(this.btnParaAt);
            this.Controls.Add(this.btnKir);
            this.Controls.Add(this.cmbKagitPara);
            this.Name = "KumbaraForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Domuz Kumbara Uygulaması";
            this.Load += new System.EventHandler(this.KumbaraForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAtilanBozukParalar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAtilanKagitParalar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btnKatla_Click_1(object sender, EventArgs e)
        {
            KagitPara katlanacak = (KagitPara)secilen;
            if (katlanacak != null)
            {
                katlandiMi = true;
                katlanacak.Katla(katlanacak.Hacim());
                btnKatla.Text = "Katlandı!";
                btnKatla.Enabled = false;
            }
        }

        private void btnSalla_Click_1(object sender, EventArgs e)
        {
            if (toplamHacim > fazlaHacim)
            {
                toplamHacim = toplamHacim - kumbara.Salla(fazlaHacim);
                kumbara.ParaEkle(-kumbara.Salla(fazlaHacim));
            }
            
            
            fazlaHacim = 0;
            btnSalla.Enabled = false;
            HacimleriYazdir();
            VerileriSifirla();
        }

        private void btnKir_Click_1(object sender, EventArgs e)
        {
            
            if (kirilmaSayisi == 0)
            {
                try
                {
                    if (birikenMiktar > 0)
                    {

                        KumbaraFormu(atilanKagitParalar, atilanBozukParalar, birikenMiktar);
                    }
                    else
                    {
                        throw new KumbaraBosException();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                btnKir.Text = "Yapıştır!";
                btnParaAt.Enabled = false;
                btnKatla.Enabled = false;
                btnSalla.Enabled = false;
                atilanKagitParalar.Clear();
                atilanBozukParalar.Clear();
                kirilmaSayisi++;
                cmbBozukPara.Enabled = false;
                cmbKagitPara.Enabled = false;
                KumbaraSifirla();
               
            }
            else if (kirilmaSayisi == 1)
            {
                btnKir.Text = "Kır!";
                cmbBozukPara.Enabled = true;
                cmbKagitPara.Enabled = true;
                btnParaAt.Enabled = true;
                btnKatla.Enabled = true;
                btnSalla.Enabled = true;
                kirilmaSayisi++;
                KumbaraSifirla();
              
            }
            else if (kirilmaSayisi > 1)
            {
                try
                {
                    if (birikenMiktar > 0)
                    {
                        KumbaraFormu(atilanKagitParalar, atilanBozukParalar, birikenMiktar);
                    }
                    else
                    {
                        throw new KumbaraBosException();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                btnKir.Text = "Kırılamaz!";
                btnParaAt.Enabled = false;
                btnKir.Enabled = false;
                btnKatla.Enabled = false;
                btnSalla.Enabled = false;
                KumbaraSifirla();
                
                try
                {
                    throw new KumbaraKullanilamazException();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Close();
            }
        }

        private void btnParaAt_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (secilen == null)
                {
                    throw new AtilacakParaSecilmediException();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            if (secilen is BozukPara)
            {
                BozukPara bozukpara = (BozukPara)secilen;
                try
                {
                    if (toplamHacim + bozukpara.Hacim() + bozukpara.FazladanHacim(secilen.Hacim()) > kumbara.KumbaraHacmi)
                    {
                        throw new KumbaraDolduException();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    VerileriSifirla();
                }
                atilanBozukParalar.Add(bozukpara);
                birikenMiktar += bozukpara.Deger;
                toplamHacim += bozukpara.Hacim() + bozukpara.FazladanHacim(bozukpara.Hacim());
                fazlaHacim += bozukpara.FazladanHacim(bozukpara.Hacim());

                kumbara.ParaEkle(bozukpara.Hacim() + bozukpara.FazladanHacim(bozukpara.Hacim()));
               
                HacimleriYazdir();
            }
            else if (secilen is KagitPara)
            {
                KagitPara kagitpara = (KagitPara)secilen;
                try
                {
                    if (toplamHacim + kagitpara.Hacim() + kagitpara.FazladanHacim(secilen.Hacim()) > kumbara.KumbaraHacmi)
                    {
                        throw new KumbaraDolduException();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    VerileriSifirla();
                }
                try
                {
                    if (katlandiMi == false)
                    {
                        throw new ParaKatlanmadiException();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                atilanKagitParalar.Add(kagitpara);
                birikenMiktar += kagitpara.Deger;
                toplamHacim += kagitpara.Hacim() + kagitpara.FazladanHacim(kagitpara.Hacim());
                fazlaHacim += kagitpara.FazladanHacim(kagitpara.Hacim());

                kumbara.ParaEkle(kagitpara.Hacim() + kagitpara.FazladanHacim(kagitpara.Hacim()));
               
                HacimleriYazdir();
            }
            btnSalla.Enabled = true;
            VerileriSifirla();
        }

        private void cmbKagitPara_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbKagitPara.SelectedIndex > 0)
            {
                cmbBozukPara.Enabled = false;
                btnKatla.Visible = true;
                atilacak = cmbKagitPara.SelectedItem.ToString();
                foreach (var item in KagitParalar)
                {
                    if (atilacak == item.Ad)
                    {
                        secilen = (KagitPara)item;
                    }
                }
            }
        }

        private void cmbBozukPara_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbBozukPara.SelectedIndex > 0)
            {
                cmbKagitPara.Enabled = false;
                atilacak = cmbBozukPara.SelectedItem.ToString();
                foreach (var item in BozukParalar)
                {
                    if (atilacak == item.Ad)
                    {
                        secilen = (BozukPara)item;
                    }
                }
            }
        }

        private void dgvAtilanBozukParalar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblKagitParalar_Click(object sender, EventArgs e)
        {

        }

        private void lblBozukParalar_Click(object sender, EventArgs e)
        {

        }
        private void acilisMetni()
        {
            MessageBox.Show("İlgili proje içerisinde 2 temel nesne bulunmaktadır: Kumbara ve Para\n" + "Kumbarada kağıt paraların fazla yer kaplamaması için para katlanmalıdır.\n");
        }

        private void KumbaraForm_Load(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker) this.acilisMetni);
       
        }

        private void dgvAtilanKagitParalar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblMiktar_Click(object sender, EventArgs e)
        {

        }


        public void KumbaraFormu(BindingList<Para> kagit, BindingList<Para> bozuk, double biriken)
        {
            DataGuncelle(kagit, bozuk, biriken);
        }
        public void DataGuncelle(BindingList<Para> kagitlar, BindingList<Para> bozuklar, double birikmis)
        {
            lblMiktar.Text = "Biriken Miktar: " + birikmis.ToString() + " ₺";
            dgvAtilanKagitParalar.DataSource = null;
            dgvAtilanKagitParalar.DataSource = kagitlar.ToList();
            dgvAtilanBozukParalar.DataSource = null;
            dgvAtilanBozukParalar.DataSource = bozuklar.ToList();
        }

        private void lblBozukPara_Click(object sender, EventArgs e)
        {

        }

       

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Kumbarada biriken tutarı görmek için kumbarayı parçalayınız.");
        }
    }
}
