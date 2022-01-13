namespace HackSlashLootTrainer
{
    partial class FrmTrainer
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblHealth = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSetHealth = new System.Windows.Forms.Button();
            this.numNewHealth = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numNewHealth)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHealth
            // 
            this.lblHealth.AutoSize = true;
            this.lblHealth.Location = new System.Drawing.Point(12, 9);
            this.lblHealth.Name = "lblHealth";
            this.lblHealth.Size = new System.Drawing.Size(47, 13);
            this.lblHealth.TabIndex = 1;
            this.lblHealth.Text = "Health: -";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Set Health";
            // 
            // btnSetHealth
            // 
            this.btnSetHealth.Location = new System.Drawing.Point(201, 36);
            this.btnSetHealth.Name = "btnSetHealth";
            this.btnSetHealth.Size = new System.Drawing.Size(75, 23);
            this.btnSetHealth.TabIndex = 3;
            this.btnSetHealth.Text = "Set!";
            this.btnSetHealth.UseVisualStyleBackColor = true;
            this.btnSetHealth.Click += new System.EventHandler(this.btnSetHealth_Click);
            // 
            // numNewHealth
            // 
            this.numNewHealth.Location = new System.Drawing.Point(75, 38);
            this.numNewHealth.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numNewHealth.Name = "numNewHealth";
            this.numNewHealth.Size = new System.Drawing.Size(120, 20);
            this.numNewHealth.TabIndex = 5;
            this.numNewHealth.Value = new decimal(new int[] {
            999,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 74);
            this.Controls.Add(this.numNewHealth);
            this.Controls.Add(this.btnSetHealth);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblHealth);
            this.Name = "Form1";
            this.Text = "HackSlashTrainer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTrainer_FormClosing);
            this.Load += new System.EventHandler(this.FrmTrainer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numNewHealth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblHealth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSetHealth;
        private System.Windows.Forms.NumericUpDown numNewHealth;
    }
}

