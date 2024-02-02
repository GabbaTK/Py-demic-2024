namespace Py_demic
{
    partial class formModel
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formModel));
            this.lblTitle = new System.Windows.Forms.Label();
            this.lbxModels = new System.Windows.Forms.ListBox();
            this.btnModelLoad = new System.Windows.Forms.Button();
            this.btnModelUnload = new System.Windows.Forms.Button();
            this.lblCurrentModelText = new System.Windows.Forms.Label();
            this.lblCurrentModel = new System.Windows.Forms.Label();
            this.lblErrorText = new System.Windows.Forms.Label();
            this.trbNPeople = new System.Windows.Forms.TrackBar();
            this.lblCustomNPeople = new System.Windows.Forms.Label();
            this.lblCustomModel = new System.Windows.Forms.Label();
            this.lblCustomPInfected = new System.Windows.Forms.Label();
            this.trbPInfected = new System.Windows.Forms.TrackBar();
            this.lblCustomTInfected = new System.Windows.Forms.Label();
            this.trbTInfected = new System.Windows.Forms.TrackBar();
            this.lblCustomTHealed = new System.Windows.Forms.Label();
            this.trbTHealed = new System.Windows.Forms.TrackBar();
            this.btnChangeLanguage = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trbNPeople)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbPInfected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbTInfected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbTHealed)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.lblTitle.Font = new System.Drawing.Font("Showcard Gothic", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.lblTitle.Location = new System.Drawing.Point(283, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(223, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Py-demic";
            // 
            // lbxModels
            // 
            this.lbxModels.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lbxModels.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbxModels.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbxModels.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.lbxModels.FormattingEnabled = true;
            this.lbxModels.ItemHeight = 21;
            this.lbxModels.Location = new System.Drawing.Point(40, 73);
            this.lbxModels.Name = "lbxModels";
            this.lbxModels.Size = new System.Drawing.Size(240, 294);
            this.lbxModels.TabIndex = 1;
            // 
            // btnModelLoad
            // 
            this.btnModelLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnModelLoad.FlatAppearance.BorderSize = 0;
            this.btnModelLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModelLoad.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnModelLoad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnModelLoad.Location = new System.Drawing.Point(40, 388);
            this.btnModelLoad.Name = "btnModelLoad";
            this.btnModelLoad.Size = new System.Drawing.Size(108, 44);
            this.btnModelLoad.TabIndex = 2;
            this.btnModelLoad.Text = "Load";
            this.btnModelLoad.UseVisualStyleBackColor = false;
            this.btnModelLoad.Click += new System.EventHandler(this.click_btnModelLoad);
            // 
            // btnModelUnload
            // 
            this.btnModelUnload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnModelUnload.FlatAppearance.BorderSize = 0;
            this.btnModelUnload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModelUnload.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnModelUnload.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnModelUnload.Location = new System.Drawing.Point(172, 388);
            this.btnModelUnload.Name = "btnModelUnload";
            this.btnModelUnload.Size = new System.Drawing.Size(108, 44);
            this.btnModelUnload.TabIndex = 3;
            this.btnModelUnload.Text = "Unload";
            this.btnModelUnload.UseVisualStyleBackColor = false;
            this.btnModelUnload.Click += new System.EventHandler(this.click_btnModelUnload);
            // 
            // lblCurrentModelText
            // 
            this.lblCurrentModelText.AutoSize = true;
            this.lblCurrentModelText.Font = new System.Drawing.Font("Lucida Sans Unicode", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCurrentModelText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.lblCurrentModelText.Location = new System.Drawing.Point(286, 73);
            this.lblCurrentModelText.Name = "lblCurrentModelText";
            this.lblCurrentModelText.Size = new System.Drawing.Size(141, 22);
            this.lblCurrentModelText.TabIndex = 4;
            this.lblCurrentModelText.Text = "Current model:";
            // 
            // lblCurrentModel
            // 
            this.lblCurrentModel.AutoSize = true;
            this.lblCurrentModel.Font = new System.Drawing.Font("Lucida Sans Unicode", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCurrentModel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.lblCurrentModel.Location = new System.Drawing.Point(286, 95);
            this.lblCurrentModel.Name = "lblCurrentModel";
            this.lblCurrentModel.Size = new System.Drawing.Size(44, 22);
            this.lblCurrentModel.TabIndex = 5;
            this.lblCurrentModel.Text = "N/A";
            // 
            // lblErrorText
            // 
            this.lblErrorText.AutoSize = true;
            this.lblErrorText.Font = new System.Drawing.Font("Lucida Sans Unicode", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblErrorText.ForeColor = System.Drawing.Color.Red;
            this.lblErrorText.Location = new System.Drawing.Point(40, 445);
            this.lblErrorText.Name = "lblErrorText";
            this.lblErrorText.Size = new System.Drawing.Size(211, 23);
            this.lblErrorText.TabIndex = 6;
            this.lblErrorText.Text = "<insert error text>";
            this.lblErrorText.Visible = false;
            // 
            // trbNPeople
            // 
            this.trbNPeople.Location = new System.Drawing.Point(517, 144);
            this.trbNPeople.Maximum = 100;
            this.trbNPeople.Name = "trbNPeople";
            this.trbNPeople.Size = new System.Drawing.Size(270, 45);
            this.trbNPeople.TabIndex = 7;
            this.trbNPeople.TickFrequency = 5;
            this.trbNPeople.ValueChanged += new System.EventHandler(this.valChange_trbNPeople);
            // 
            // lblCustomNPeople
            // 
            this.lblCustomNPeople.AutoSize = true;
            this.lblCustomNPeople.Font = new System.Drawing.Font("Lucida Sans Unicode", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCustomNPeople.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.lblCustomNPeople.Location = new System.Drawing.Point(520, 118);
            this.lblCustomNPeople.Name = "lblCustomNPeople";
            this.lblCustomNPeople.Size = new System.Drawing.Size(190, 22);
            this.lblCustomNPeople.TabIndex = 8;
            this.lblCustomNPeople.Text = "Number of people: 0";
            // 
            // lblCustomModel
            // 
            this.lblCustomModel.AutoSize = true;
            this.lblCustomModel.Font = new System.Drawing.Font("Lucida Sans Unicode", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCustomModel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.lblCustomModel.Location = new System.Drawing.Point(520, 73);
            this.lblCustomModel.Name = "lblCustomModel";
            this.lblCustomModel.Size = new System.Drawing.Size(143, 22);
            this.lblCustomModel.TabIndex = 9;
            this.lblCustomModel.Text = "Custom model:";
            // 
            // lblCustomPInfected
            // 
            this.lblCustomPInfected.AutoSize = true;
            this.lblCustomPInfected.Font = new System.Drawing.Font("Lucida Sans Unicode", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCustomPInfected.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.lblCustomPInfected.Location = new System.Drawing.Point(520, 192);
            this.lblCustomPInfected.Name = "lblCustomPInfected";
            this.lblCustomPInfected.Size = new System.Drawing.Size(186, 22);
            this.lblCustomPInfected.TabIndex = 10;
            this.lblCustomPInfected.Text = "Infected percent: 0%";
            // 
            // trbPInfected
            // 
            this.trbPInfected.Location = new System.Drawing.Point(517, 217);
            this.trbPInfected.Maximum = 100;
            this.trbPInfected.Name = "trbPInfected";
            this.trbPInfected.Size = new System.Drawing.Size(270, 45);
            this.trbPInfected.TabIndex = 11;
            this.trbPInfected.TickFrequency = 5;
            this.trbPInfected.ValueChanged += new System.EventHandler(this.valChanged_trbPInfected);
            // 
            // lblCustomTInfected
            // 
            this.lblCustomTInfected.AutoSize = true;
            this.lblCustomTInfected.Font = new System.Drawing.Font("Lucida Sans Unicode", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCustomTInfected.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.lblCustomTInfected.Location = new System.Drawing.Point(520, 265);
            this.lblCustomTInfected.Name = "lblCustomTInfected";
            this.lblCustomTInfected.Size = new System.Drawing.Size(193, 22);
            this.lblCustomTInfected.TabIndex = 12;
            this.lblCustomTInfected.Text = "Infected time: 0 days";
            // 
            // trbTInfected
            // 
            this.trbTInfected.Location = new System.Drawing.Point(517, 290);
            this.trbTInfected.Maximum = 100;
            this.trbTInfected.Name = "trbTInfected";
            this.trbTInfected.Size = new System.Drawing.Size(270, 45);
            this.trbTInfected.TabIndex = 13;
            this.trbTInfected.TickFrequency = 5;
            this.trbTInfected.ValueChanged += new System.EventHandler(this.valChange_trbTInfected);
            // 
            // lblCustomTHealed
            // 
            this.lblCustomTHealed.AutoSize = true;
            this.lblCustomTHealed.Font = new System.Drawing.Font("Lucida Sans Unicode", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCustomTHealed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.lblCustomTHealed.Location = new System.Drawing.Point(520, 338);
            this.lblCustomTHealed.Name = "lblCustomTHealed";
            this.lblCustomTHealed.Size = new System.Drawing.Size(182, 22);
            this.lblCustomTHealed.TabIndex = 14;
            this.lblCustomTHealed.Text = "Healed time: 0 days";
            // 
            // trbTHealed
            // 
            this.trbTHealed.Location = new System.Drawing.Point(517, 363);
            this.trbTHealed.Maximum = 100;
            this.trbTHealed.Name = "trbTHealed";
            this.trbTHealed.Size = new System.Drawing.Size(270, 45);
            this.trbTHealed.TabIndex = 15;
            this.trbTHealed.TickFrequency = 5;
            this.trbTHealed.ValueChanged += new System.EventHandler(this.valChange_trbTHealed);
            // 
            // btnChangeLanguage
            // 
            this.btnChangeLanguage.BackgroundImage = global::Py_demic.Properties.Resources.flag_hr;
            this.btnChangeLanguage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChangeLanguage.FlatAppearance.BorderSize = 0;
            this.btnChangeLanguage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangeLanguage.Location = new System.Drawing.Point(739, 9);
            this.btnChangeLanguage.Name = "btnChangeLanguage";
            this.btnChangeLanguage.Size = new System.Drawing.Size(54, 24);
            this.btnChangeLanguage.TabIndex = 16;
            this.btnChangeLanguage.UseVisualStyleBackColor = true;
            this.btnChangeLanguage.Click += new System.EventHandler(this.click_btnChangeLanguage);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnStart.Location = new System.Drawing.Point(301, 388);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(205, 44);
            this.btnStart.TabIndex = 17;
            this.btnStart.Text = "Start the simulation";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.click_btnStart);
            // 
            // formModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(800, 477);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnChangeLanguage);
            this.Controls.Add(this.trbTHealed);
            this.Controls.Add(this.lblCustomTHealed);
            this.Controls.Add(this.trbTInfected);
            this.Controls.Add(this.lblCustomTInfected);
            this.Controls.Add(this.trbPInfected);
            this.Controls.Add(this.lblCustomPInfected);
            this.Controls.Add(this.lblCustomModel);
            this.Controls.Add(this.lblCustomNPeople);
            this.Controls.Add(this.trbNPeople);
            this.Controls.Add(this.lblErrorText);
            this.Controls.Add(this.lblCurrentModel);
            this.Controls.Add(this.lblCurrentModelText);
            this.Controls.Add(this.btnModelUnload);
            this.Controls.Add(this.btnModelLoad);
            this.Controls.Add(this.lbxModels);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formModel";
            this.Text = "Py-demic";
            ((System.ComponentModel.ISupportInitialize)(this.trbNPeople)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbPInfected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbTInfected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbTHealed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblTitle;
        private ListBox lbxModels;
        private Button btnModelLoad;
        private Button btnModelUnload;
        private Label lblCurrentModelText;
        private Label lblCurrentModel;
        private Label lblErrorText;
        private TrackBar trbNPeople;
        private Label lblCustomNPeople;
        private Label lblCustomModel;
        private Label lblCustomPInfected;
        private TrackBar trbPInfected;
        private Label lblCustomTInfected;
        private TrackBar trbTInfected;
        private Label lblCustomTHealed;
        private TrackBar trbTHealed;
        private Button btnChangeLanguage;
        private Button btnStart;
    }
}