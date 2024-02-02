namespace Py_demic
{
    partial class formSimulation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formSimulation));
            this.pbxSimulation = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSimulation)).BeginInit();
            this.SuspendLayout();
            // 
            // pbxSimulation
            // 
            this.pbxSimulation.BackColor = System.Drawing.Color.Black;
            this.pbxSimulation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxSimulation.Location = new System.Drawing.Point(0, 0);
            this.pbxSimulation.Name = "pbxSimulation";
            this.pbxSimulation.Size = new System.Drawing.Size(800, 450);
            this.pbxSimulation.TabIndex = 1;
            this.pbxSimulation.TabStop = false;
            // 
            // formSimulation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pbxSimulation);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formSimulation";
            this.Text = "Py-demic simulation";
            ((System.ComponentModel.ISupportInitialize)(this.pbxSimulation)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox pbxSimulation;
    }
}