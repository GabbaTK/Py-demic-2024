namespace Py_demic
{
    partial class formResults
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
            this.lblSimResText = new System.Windows.Forms.Label();
            this.lblSimResAlive = new System.Windows.Forms.Label();
            this.lblSimResDead = new System.Windows.Forms.Label();
            this.lblSimResNormal = new System.Windows.Forms.Label();
            this.lblSimResInfected = new System.Windows.Forms.Label();
            this.lblSimResVaccinated = new System.Windows.Forms.Label();
            this.lblSimResHealed = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblSimResText
            // 
            this.lblSimResText.AutoSize = true;
            this.lblSimResText.Font = new System.Drawing.Font("Stencil", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSimResText.ForeColor = System.Drawing.Color.White;
            this.lblSimResText.Location = new System.Drawing.Point(12, 9);
            this.lblSimResText.Name = "lblSimResText";
            this.lblSimResText.Size = new System.Drawing.Size(320, 32);
            this.lblSimResText.TabIndex = 0;
            this.lblSimResText.Text = "Simulation results:";
            // 
            // lblSimResAlive
            // 
            this.lblSimResAlive.AutoSize = true;
            this.lblSimResAlive.Font = new System.Drawing.Font("Stencil", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSimResAlive.ForeColor = System.Drawing.Color.Green;
            this.lblSimResAlive.Location = new System.Drawing.Point(12, 68);
            this.lblSimResAlive.Name = "lblSimResAlive";
            this.lblSimResAlive.Size = new System.Drawing.Size(148, 32);
            this.lblSimResAlive.TabIndex = 1;
            this.lblSimResAlive.Text = "Alive: N/A";
            // 
            // lblSimResDead
            // 
            this.lblSimResDead.AutoSize = true;
            this.lblSimResDead.Font = new System.Drawing.Font("Stencil", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSimResDead.ForeColor = System.Drawing.Color.Gray;
            this.lblSimResDead.Location = new System.Drawing.Point(12, 100);
            this.lblSimResDead.Name = "lblSimResDead";
            this.lblSimResDead.Size = new System.Drawing.Size(143, 32);
            this.lblSimResDead.TabIndex = 2;
            this.lblSimResDead.Text = "Dead: N/A";
            // 
            // lblSimResNormal
            // 
            this.lblSimResNormal.AutoSize = true;
            this.lblSimResNormal.Font = new System.Drawing.Font("Stencil", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSimResNormal.ForeColor = System.Drawing.Color.Blue;
            this.lblSimResNormal.Location = new System.Drawing.Point(12, 132);
            this.lblSimResNormal.Name = "lblSimResNormal";
            this.lblSimResNormal.Size = new System.Drawing.Size(180, 32);
            this.lblSimResNormal.TabIndex = 3;
            this.lblSimResNormal.Text = "Normal: N/A";
            // 
            // lblSimResInfected
            // 
            this.lblSimResInfected.AutoSize = true;
            this.lblSimResInfected.Font = new System.Drawing.Font("Stencil", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSimResInfected.ForeColor = System.Drawing.Color.Red;
            this.lblSimResInfected.Location = new System.Drawing.Point(12, 164);
            this.lblSimResInfected.Name = "lblSimResInfected";
            this.lblSimResInfected.Size = new System.Drawing.Size(200, 32);
            this.lblSimResInfected.TabIndex = 4;
            this.lblSimResInfected.Text = "Infected: N/A";
            // 
            // lblSimResVaccinated
            // 
            this.lblSimResVaccinated.AutoSize = true;
            this.lblSimResVaccinated.Font = new System.Drawing.Font("Stencil", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSimResVaccinated.ForeColor = System.Drawing.Color.Green;
            this.lblSimResVaccinated.Location = new System.Drawing.Point(12, 228);
            this.lblSimResVaccinated.Name = "lblSimResVaccinated";
            this.lblSimResVaccinated.Size = new System.Drawing.Size(235, 32);
            this.lblSimResVaccinated.TabIndex = 5;
            this.lblSimResVaccinated.Text = "Vaccinated: N/A";
            // 
            // lblSimResHealed
            // 
            this.lblSimResHealed.AutoSize = true;
            this.lblSimResHealed.Font = new System.Drawing.Font("Stencil", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSimResHealed.ForeColor = System.Drawing.Color.White;
            this.lblSimResHealed.Location = new System.Drawing.Point(12, 196);
            this.lblSimResHealed.Name = "lblSimResHealed";
            this.lblSimResHealed.Size = new System.Drawing.Size(175, 32);
            this.lblSimResHealed.TabIndex = 6;
            this.lblSimResHealed.Text = "Healed: N/A";
            // 
            // formResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(408, 277);
            this.Controls.Add(this.lblSimResHealed);
            this.Controls.Add(this.lblSimResVaccinated);
            this.Controls.Add(this.lblSimResInfected);
            this.Controls.Add(this.lblSimResNormal);
            this.Controls.Add(this.lblSimResDead);
            this.Controls.Add(this.lblSimResAlive);
            this.Controls.Add(this.lblSimResText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "formResults";
            this.Text = "Simulation details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblSimResText;
        private Label lblSimResAlive;
        private Label lblSimResDead;
        private Label lblSimResNormal;
        private Label lblSimResInfected;
        private Label lblSimResVaccinated;
        private Label lblSimResHealed;
    }
}