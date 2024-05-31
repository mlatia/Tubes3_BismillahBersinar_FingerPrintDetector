using System.Windows.Forms;
using System.Drawing;

namespace FingerPrintDetector
{
    partial class MainForm
    {
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
            this.AlgorithmComboBox.Location = new System.Drawing.Point(435, 470);
            this.AlgorithmComboBox.Name = "AlgorithmComboBox";
            this.AlgorithmComboBox.Size = new System.Drawing.Size(150, 40);
            this.AlgorithmComboBox.Font = new System.Drawing.Font("Ubuntu Condensed", 15F);
            this.AlgorithmComboBox.TabIndex = 4;
            // 
            // SearchTimeText
            // 
            this.SearchTimeText.AutoSize = true;
            this.SearchTimeText.Location = new System.Drawing.Point(815, 540);
            this.SearchTimeText.Name = "SearchTimeText";
            this.SearchTimeText.Size = new System.Drawing.Size(131, 30);
            this.SearchTimeText.TabIndex = 5;
            this.SearchTimeText.Text = "Waktu Pencarian: xxx ms";
            this.SearchTimeText.Font = new System.Drawing.Font("Ubuntu Condensed", 15F);
            this.SearchTimeText.BackColor = Color.Transparent;

            // 
            // MatchPercentageText
            // 
            this.MatchPercentageText.AutoSize = true;
            this.MatchPercentageText.Location = new System.Drawing.Point(815, 570);
            this.MatchPercentageText.Name = "MatchPercentageText";
            this.MatchPercentageText.Size = new System.Drawing.Size(151, 30);
            this.MatchPercentageText.TabIndex = 6;
            this.MatchPercentageText.Text = "Persentase Kecocokkan: xx%";
            this.MatchPercentageText.Font = new System.Drawing.Font("Ubuntu Condensed", 15F);
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
