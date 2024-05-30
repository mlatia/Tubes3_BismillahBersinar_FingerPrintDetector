namespace FingerPrintDetector
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

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
            this.UploadImageButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.SearchButton = new System.Windows.Forms.Button();
            this.UploadedImage = new System.Windows.Forms.PictureBox();
            this.AlgorithmComboBox = new System.Windows.Forms.ComboBox();
            this.SearchTimeText = new System.Windows.Forms.Label();
            this.MatchPercentageText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.UploadedImage)).BeginInit();
            this.SuspendLayout();
            // 
            // UploadImageButton
            // 
            this.UploadImageButton.Location = new System.Drawing.Point(30, 30);
            this.UploadImageButton.Name = "UploadImageButton";
            this.UploadImageButton.Size = new System.Drawing.Size(150, 30);
            this.UploadImageButton.TabIndex = 0;
            this.UploadImageButton.Text = "Upload Image";
            this.UploadImageButton.UseVisualStyleBackColor = true;
            this.UploadImageButton.Click += new System.EventHandler(this.UploadImageButton_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(30, 70);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(150, 30);
            this.RefreshButton.TabIndex = 1;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(30, 110);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(150, 30);
            this.SearchButton.TabIndex = 2;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // UploadedImage
            // 
            this.UploadedImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UploadedImage.Location = new System.Drawing.Point(200, 30);
            this.UploadedImage.Name = "UploadedImage";
            this.UploadedImage.Size = new System.Drawing.Size(200, 200);
            this.UploadedImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.UploadedImage.TabIndex = 3;
            this.UploadedImage.TabStop = false;
            // 
            // AlgorithmComboBox
            // 
            this.AlgorithmComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AlgorithmComboBox.FormattingEnabled = true;
            this.AlgorithmComboBox.Items.AddRange(new object[] {
            "BM",
            "KMP"});
            this.AlgorithmComboBox.Location = new System.Drawing.Point(30, 150);
            this.AlgorithmComboBox.Name = "AlgorithmComboBox";
            this.AlgorithmComboBox.Size = new System.Drawing.Size(150, 23);
            this.AlgorithmComboBox.TabIndex = 4;
            // 
            // SearchTimeText
            // 
            this.SearchTimeText.AutoSize = true;
            this.SearchTimeText.Location = new System.Drawing.Point(30, 180);
            this.SearchTimeText.Name = "SearchTimeText";
            this.SearchTimeText.Size = new System.Drawing.Size(131, 15);
            this.SearchTimeText.TabIndex = 5;
            this.SearchTimeText.Text = "Waktu Pencarian: xxx ms";
            // 
            // MatchPercentageText
            // 
            this.MatchPercentageText.AutoSize = true;
            this.MatchPercentageText.Location = new System.Drawing.Point(30, 200);
            this.MatchPercentageText.Name = "MatchPercentageText";
            this.MatchPercentageText.Size = new System.Drawing.Size(151, 15);
            this.MatchPercentageText.TabIndex = 6;
            this.MatchPercentageText.Text = "Persentase Kecocokkan: xx%";
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
