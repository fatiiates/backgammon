namespace backgammonGame
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.turnRock = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // turnRock
            // 
            this.turnRock.FlatAppearance.BorderSize = 0;
            this.turnRock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.turnRock.Location = new System.Drawing.Point(375, 259);
            this.turnRock.Name = "turnRock";
            this.turnRock.Size = new System.Drawing.Size(47, 48);
            this.turnRock.TabIndex = 2;
            this.turnRock.Text = "Geri Al";
            this.turnRock.UseVisualStyleBackColor = true;
            this.turnRock.Visible = false;
            this.turnRock.Click += new System.EventHandler(this.turnRock_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(798, 562);
            this.Controls.Add(this.turnRock);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Move += new System.EventHandler(this.Form1_Move);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button turnRock;
    }
}

