namespace MyBookDesk
{
    partial class frmHome
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHome));
            this.btnCliente = new System.Windows.Forms.Button();
            this.btnEditora = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnLivro = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCliente
            // 
            this.btnCliente.BackColor = System.Drawing.Color.Transparent;
            this.btnCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCliente.FlatAppearance.BorderSize = 0;
            this.btnCliente.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCliente.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCliente.ForeColor = System.Drawing.Color.Transparent;
            this.btnCliente.Location = new System.Drawing.Point(433, 318);
            this.btnCliente.Name = "btnCliente";
            this.btnCliente.Size = new System.Drawing.Size(159, 69);
            this.btnCliente.TabIndex = 9;
            this.btnCliente.TabStop = false;
            this.btnCliente.UseVisualStyleBackColor = false;
            this.btnCliente.Click += new System.EventHandler(this.btnCliente_Click);
            // 
            // btnEditora
            // 
            this.btnEditora.BackColor = System.Drawing.Color.Transparent;
            this.btnEditora.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditora.FlatAppearance.BorderSize = 0;
            this.btnEditora.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnEditora.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnEditora.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditora.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditora.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnEditora.Location = new System.Drawing.Point(627, 423);
            this.btnEditora.Name = "btnEditora";
            this.btnEditora.Size = new System.Drawing.Size(159, 58);
            this.btnEditora.TabIndex = 10;
            this.btnEditora.TabStop = false;
            this.btnEditora.UseVisualStyleBackColor = false;
            this.btnEditora.Click += new System.EventHandler(this.btnEditora_Click);
            // 
            // btnSair
            // 
            this.btnSair.BackColor = System.Drawing.Color.Transparent;
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSair.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.ForeColor = System.Drawing.Color.Transparent;
            this.btnSair.Location = new System.Drawing.Point(433, 423);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(159, 58);
            this.btnSair.TabIndex = 12;
            this.btnSair.TabStop = false;
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnLivro
            // 
            this.btnLivro.BackColor = System.Drawing.Color.Transparent;
            this.btnLivro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLivro.FlatAppearance.BorderSize = 0;
            this.btnLivro.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnLivro.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnLivro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLivro.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLivro.ForeColor = System.Drawing.Color.Transparent;
            this.btnLivro.Location = new System.Drawing.Point(627, 318);
            this.btnLivro.Name = "btnLivro";
            this.btnLivro.Size = new System.Drawing.Size(159, 69);
            this.btnLivro.TabIndex = 11;
            this.btnLivro.TabStop = false;
            this.btnLivro.UseVisualStyleBackColor = false;
            this.btnLivro.Click += new System.EventHandler(this.btnLivro_Click);
            // 
            // frmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnLivro);
            this.Controls.Add(this.btnEditora);
            this.Controls.Add(this.btnCliente);
            this.DoubleBuffered = true;
            this.Name = "frmHome";
            this.Text = "frmHome";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmHome_FormClosed);
            this.Load += new System.EventHandler(this.frmHome_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCliente;
        private System.Windows.Forms.Button btnEditora;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnLivro;
    }
}