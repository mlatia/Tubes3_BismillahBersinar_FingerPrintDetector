using System.Windows.Forms;
using System.Drawing;

namespace FingerPrintDetector
{
    partial class MainForm
    {
        private System.Windows.Forms.Label NIKLabel;
        private System.Windows.Forms.Panel NIKPanel;
        private System.Windows.Forms.Label NamaLabel;
        private System.Windows.Forms.Panel NamaPanel;
        private System.Windows.Forms.Label LahirLabel;
        private System.Windows.Forms.Panel LahirPanel;
        private System.Windows.Forms.Label TanggalLabel;
        private System.Windows.Forms.Panel TanggalPanel;
        private System.Windows.Forms.Label KelaminLabel;
        private System.Windows.Forms.Panel KelaminPanel;
        private System.Windows.Forms.Label GoldarLabel;
        private System.Windows.Forms.Panel GoldarPanel;
        private System.Windows.Forms.Label AlamatLabel;
        private System.Windows.Forms.Panel AlamatPanel;
        private System.Windows.Forms.Label AgamaLabel;
        private System.Windows.Forms.Panel AgamaPanel;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Panel StatusPanel;
        private System.Windows.Forms.Label KerjaLabel;
        private System.Windows.Forms.Panel KerjaPanel;
        private System.Windows.Forms.Label KewarganegaraanLabel;
        private System.Windows.Forms.Panel KewarganegaraanPanel;
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox SimilarImagePictureBox;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;

            // Menyesuaikan ukuran jendela
            this.Size = Screen.PrimaryScreen.Bounds.Size;
            this.WindowState = FormWindowState.Maximized;

            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackgroundImage = Image.FromFile("assets\\background.png");
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.UploadImageButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.SearchButton = new System.Windows.Forms.Button();
            this.UploadedImage = new System.Windows.Forms.PictureBox();
            this.AlgorithmComboBox = new System.Windows.Forms.ComboBox();
            this.SearchTimeText = new System.Windows.Forms.Label();
            this.MatchPercentageText = new System.Windows.Forms.Label();
            this.SimilarImagePictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.UploadedImage)).BeginInit();
            this.SuspendLayout();
            //
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(350, 470);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(40, 40); // Ukuran gambar
            this.RefreshButton.TabIndex = 1;
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            this.RefreshButton.Image = Image.FromFile("assets\\refreshicon.png"); // Ganti dengan path ikon Anda          
            // 
            // UploadImageButton
            // 
            this.UploadImageButton.Location = new System.Drawing.Point(200, 470);
            this.UploadImageButton.Name = "UploadImageButton";
            this.UploadImageButton.Size = new System.Drawing.Size(150, 40);
            this.UploadImageButton.TabIndex = 0;
            this.UploadImageButton.Text = "Upload Image";
            this.UploadImageButton.Font = new System.Drawing.Font("Ubuntu Condensed", 15F);
            this.UploadImageButton.UseVisualStyleBackColor = true;
            this.UploadImageButton.BackColor = ColorTranslator.FromHtml("#333A6E");
            this.UploadImageButton.ForeColor = ColorTranslator.FromHtml("#FFFFFF");
            this.UploadImageButton.Click += new System.EventHandler(this.UploadImageButton_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(350, 550);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(150, 40);
            this.SearchButton.TabIndex = 2;
            this.SearchButton.Text = "SEARCH";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Font = new System.Drawing.Font("Ubuntu Condensed", 15F, FontStyle.Bold);
            this.SearchButton.BackColor = ColorTranslator.FromHtml("#333A6E");
            this.SearchButton.ForeColor = ColorTranslator.FromHtml("#FFFFFF");
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // UploadedImage
            // 
            this.UploadedImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UploadedImage.Location = new System.Drawing.Point(103, 105);
            this.UploadedImage.Name = "UploadedImage";
            this.UploadedImage.Size = new System.Drawing.Size(280, 330);
            this.UploadedImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.UploadedImage.TabIndex = 3;
            this.UploadedImage.TabStop = false;
            
            // SimilarImagePictureBox
            this.SimilarImagePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SimilarImagePictureBox.Location = new System.Drawing.Point(433, 105); // Sesuaikan posisi X dan Y
            this.SimilarImagePictureBox.Name = "SimilarImagePictureBox";
            this.SimilarImagePictureBox.Size = new System.Drawing.Size(280, 330); // Sesuaikan lebar dan tinggi
            this.SimilarImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.SimilarImagePictureBox.TabIndex = 7;
            this.SimilarImagePictureBox.TabStop = false;
            this.Controls.Add(this.SimilarImagePictureBox);
            // AlgorithmComboBox
            // 
            this.AlgorithmComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AlgorithmComboBox.FormattingEnabled = true;
            this.AlgorithmComboBox.BackColor = ColorTranslator.FromHtml("#333A6E");
            this.AlgorithmComboBox.ForeColor = ColorTranslator.FromHtml("#FFFFFF");

            this.AlgorithmComboBox.Items.AddRange(new object[] {
            "BM",
            "KMP"});
            this.AlgorithmComboBox.SelectedIndex = 1;
            this.AlgorithmComboBox.Location = new System.Drawing.Point(435, 470);
            this.AlgorithmComboBox.Name = "AlgorithmComboBox";
            this.AlgorithmComboBox.Size = new System.Drawing.Size(150, 40);
            this.AlgorithmComboBox.Font = new System.Drawing.Font("Ubuntu Condensed", 15F);
            this.AlgorithmComboBox.TabIndex = 4;

            this.NIKPanel = new System.Windows.Forms.Panel();
            this.NIKPanel.BackColor = System.Drawing.Color.White;
            this.NIKPanel.Location = new System.Drawing.Point(820, 220);
            this.NIKPanel.Size = new System.Drawing.Size(350, 30);
            this.NIKPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NIKPanel.Padding = new Padding(10, 0, 0, 0);
            this.Controls.Add(this.NIKPanel);

            this.NIKLabel = new System.Windows.Forms.Label();
            this.NIKLabel.AutoSize = true;
            this.NIKLabel.Location = new System.Drawing.Point(5, 5); 
            this.NIKLabel.Name = "NIKLabel";
            this.NIKLabel.TabIndex = 8;
            this.NIKLabel.Text = "NIK: ";
            this.NIKPanel.Controls.Add(this.NIKLabel);

            this.NamaPanel = new System.Windows.Forms.Panel();
            this.NamaPanel.BackColor = System.Drawing.Color.White;
            this.NamaPanel.Location = new System.Drawing.Point(820, 255);
            this.NamaPanel.Size = new System.Drawing.Size(350, 30);
            this.NamaPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NamaPanel.Padding = new Padding(10, 0, 0, 0);
            this.Controls.Add(this.NamaPanel);

            this.NamaLabel = new System.Windows.Forms.Label();
            this.NamaLabel.AutoSize = true;
            this.NamaLabel.Location = new System.Drawing.Point(5, 5); 
            this.NamaLabel.Name = "NamaLabel";
            this.NamaLabel.TabIndex = 8;
            this.NamaLabel.Text = "Nama: ";
            this.NamaPanel.Controls.Add(this.NamaLabel);

            this.LahirPanel = new System.Windows.Forms.Panel();
            this.LahirPanel.BackColor = System.Drawing.Color.White;
            this.LahirPanel.Location = new System.Drawing.Point(820, 290);
            this.LahirPanel.Size = new System.Drawing.Size(350, 30);
            this.LahirPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LahirPanel.Padding = new Padding(10, 0, 0, 0);
            this.Controls.Add(this.LahirPanel);

            this.LahirLabel = new System.Windows.Forms.Label();
            this.LahirLabel.AutoSize = true;
            this.LahirLabel.Location = new System.Drawing.Point(5, 5); 
            this.LahirLabel.Name = "LahirLabel";
            this.LahirLabel.TabIndex = 8;
            this.LahirLabel.Text = "Tempat Lahir: ";
            this.LahirPanel.Controls.Add(this.LahirLabel);

            this.TanggalPanel = new System.Windows.Forms.Panel();
            this.TanggalPanel.BackColor = System.Drawing.Color.White;
            this.TanggalPanel.Location = new System.Drawing.Point(820, 325);
            this.TanggalPanel.Size = new System.Drawing.Size(350, 30);
            this.TanggalPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TanggalPanel.Padding = new Padding(10, 0, 0, 0);
            this.Controls.Add(this.TanggalPanel);

            this.TanggalLabel = new System.Windows.Forms.Label();
            this.TanggalLabel.AutoSize = true;
            this.TanggalLabel.Location = new System.Drawing.Point(5, 5); 
            this.TanggalLabel.Name = "TanggalLabel";
            this.TanggalLabel.TabIndex = 8;
            this.TanggalLabel.Text = "Tanggal Lahir: ";
            this.TanggalPanel.Controls.Add(this.TanggalLabel);

            this.KelaminPanel = new System.Windows.Forms.Panel();
            this.KelaminPanel.BackColor = System.Drawing.Color.White;
            this.KelaminPanel.Location = new System.Drawing.Point(820, 360);
            this.KelaminPanel.Size = new System.Drawing.Size(350, 30);
            this.KelaminPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KelaminPanel.Padding = new Padding(10, 0, 0, 0);
            this.Controls.Add(this.KelaminPanel);

            this.KelaminLabel = new System.Windows.Forms.Label();
            this.KelaminLabel.AutoSize = true;
            this.KelaminLabel.Location = new System.Drawing.Point(5, 5); 
            this.KelaminLabel.Name = "KelaminLabel";
            this.KelaminLabel.TabIndex = 8;
            this.KelaminLabel.Text = "Jenis Kelamin: ";
            this.KelaminPanel.Controls.Add(this.KelaminLabel);

            this.GoldarPanel = new System.Windows.Forms.Panel();
            this.GoldarPanel.BackColor = System.Drawing.Color.White;
            this.GoldarPanel.Location = new System.Drawing.Point(820, 395);
            this.GoldarPanel.Size = new System.Drawing.Size(350, 30);
            this.GoldarPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GoldarPanel.Padding = new Padding(10, 0, 0, 0);
            this.Controls.Add(this.GoldarPanel);

            this.GoldarLabel = new System.Windows.Forms.Label();
            this.GoldarLabel.AutoSize = true;
            this.GoldarLabel.Location = new System.Drawing.Point(5, 5); 
            this.GoldarLabel.Name = "GoldarLabel";
            this.GoldarLabel.TabIndex = 8;
            this.GoldarLabel.Text = "Golongan Darah: ";
            this.GoldarPanel.Controls.Add(this.GoldarLabel);

            this.AlamatPanel = new System.Windows.Forms.Panel();
            this.AlamatPanel.BackColor = System.Drawing.Color.White;
            this.AlamatPanel.Location = new System.Drawing.Point(820, 430);
            this.AlamatPanel.Size = new System.Drawing.Size(350, 30);
            this.AlamatPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AlamatPanel.Padding = new Padding(10, 0, 0, 0);
            this.Controls.Add(this.AlamatPanel);

            this.AlamatLabel = new System.Windows.Forms.Label();
            this.AlamatLabel.AutoSize = true;
            this.AlamatLabel.Location = new System.Drawing.Point(5, 5); 
            this.AlamatLabel.Name = "AlamatLabel";
            this.AlamatLabel.TabIndex = 8;
            this.AlamatLabel.Text = "Alamat: ";
            this.AlamatPanel.Controls.Add(this.AlamatLabel);

            this.AgamaPanel = new System.Windows.Forms.Panel();
            this.AgamaPanel.BackColor = System.Drawing.Color.White;
            this.AgamaPanel.Location = new System.Drawing.Point(820, 465);
            this.AgamaPanel.Size = new System.Drawing.Size(350, 30);
            this.AgamaPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AgamaPanel.Padding = new Padding(10, 0, 0, 0);
            this.Controls.Add(this.AgamaPanel);

            this.AgamaLabel = new System.Windows.Forms.Label();
            this.AgamaLabel.AutoSize = true;
            this.AgamaLabel.Location = new System.Drawing.Point(5, 5); 
            this.AgamaLabel.Name = "AgamaLabel";
            this.AgamaLabel.TabIndex = 8;
            this.AgamaLabel.Text = "Agama: ";
            this.AgamaPanel.Controls.Add(this.AgamaLabel);

            this.StatusPanel = new System.Windows.Forms.Panel();
            this.StatusPanel.BackColor = System.Drawing.Color.White;
            this.StatusPanel.Location = new System.Drawing.Point(820, 500);
            this.StatusPanel.Size = new System.Drawing.Size(350, 30);
            this.StatusPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatusPanel.Padding = new Padding(10, 0, 0, 0);
            this.Controls.Add(this.StatusPanel);

            this.StatusLabel = new System.Windows.Forms.Label();
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(5, 5); 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.TabIndex = 8;
            this.StatusLabel.Text = "Status Perkawinan: ";
            this.StatusPanel.Controls.Add(this.StatusLabel);

            this.KerjaPanel = new System.Windows.Forms.Panel();
            this.KerjaPanel.BackColor = System.Drawing.Color.White;
            this.KerjaPanel.Location = new System.Drawing.Point(820, 535);
            this.KerjaPanel.Size = new System.Drawing.Size(350, 30);
            this.KerjaPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KerjaPanel.Padding = new Padding(10, 0, 0, 0);
            this.Controls.Add(this.KerjaPanel);

            this.KerjaLabel = new System.Windows.Forms.Label();
            this.KerjaLabel.AutoSize = true;
            this.KerjaLabel.Location = new System.Drawing.Point(5, 5); 
            this.KerjaLabel.Name = "KerjaLabel";
            this.KerjaLabel.TabIndex = 8;
            this.KerjaLabel.Text = "Pekerjaan: ";
            this.KerjaPanel.Controls.Add(this.KerjaLabel);

            this.KewarganegaraanPanel = new System.Windows.Forms.Panel();
            this.KewarganegaraanPanel.BackColor = System.Drawing.Color.White;
            this.KewarganegaraanPanel.Location = new System.Drawing.Point(820, 570);
            this.KewarganegaraanPanel.Size = new System.Drawing.Size(350, 30);
            this.KewarganegaraanPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KewarganegaraanPanel.Padding = new Padding(10, 0, 0, 0);
            this.Controls.Add(this.KewarganegaraanPanel);

            this.KewarganegaraanLabel = new System.Windows.Forms.Label();
            this.KewarganegaraanLabel.AutoSize = true;
            this.KewarganegaraanLabel.Location = new System.Drawing.Point(5, 5); 
            this.KewarganegaraanLabel.Name = "KewarganegaraanLabel";
            this.KewarganegaraanLabel.TabIndex = 8;
            this.KewarganegaraanLabel.Text = "Kewarganegaraan: ";
            this.KewarganegaraanPanel.Controls.Add(this.KewarganegaraanLabel);

            // 
            // SearchTimeText
            // 
            this.SearchTimeText.AutoSize = true;
            this.SearchTimeText.Location = new System.Drawing.Point(815, 610);
            this.SearchTimeText.Name = "SearchTimeText";
            this.SearchTimeText.Size = new System.Drawing.Size(131, 30);
            this.SearchTimeText.TabIndex = 5;
            this.SearchTimeText.Text = "Waktu Pencarian: xxx ms";
            this.SearchTimeText.Font = new System.Drawing.Font("Ubuntu Condensed", 13F);
            this.SearchTimeText.BackColor = Color.Transparent;

            // 
            // MatchPercentageText
            // 
            this.MatchPercentageText.AutoSize = true;
            this.MatchPercentageText.Location = new System.Drawing.Point(815, 645);
            this.MatchPercentageText.Name = "MatchPercentageText";
            this.MatchPercentageText.Size = new System.Drawing.Size(151, 30);
            this.MatchPercentageText.TabIndex = 6;
            this.MatchPercentageText.Text = "Persentase Kecocokkan: xx%";
            this.MatchPercentageText.Font = new System.Drawing.Font("Ubuntu Condensed", 13F);
            this.MatchPercentageText.BackColor = Color.Transparent;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 261);
            this.Controls.Add(this.MatchPercentageText);
            this.Controls.Add(this.SearchTimeText);
            this.Controls.Add(this.AlgorithmComboBox);
            this.Controls.Add(this.UploadedImage);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.UploadImageButton);
            this.Name = "MainForm";
            this.Text = "FingerPrintDetector";
            ((System.ComponentModel.ISupportInitialize)(this.UploadedImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button UploadImageButton;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.PictureBox UploadedImage;
        private System.Windows.Forms.ComboBox AlgorithmComboBox;
        private System.Windows.Forms.Label SearchTimeText;
        private System.Windows.Forms.Label MatchPercentageText;
    }
}
