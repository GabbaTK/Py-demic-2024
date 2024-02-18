using Py_demic.Properties;

namespace Py_demic
{
    public partial class formModel : Form
    {
        private string selectedModel    =   "custom";
        private Model model             =   new();
        private Translator translator   =   new();

        public formModel()
        {
            InitializeComponent();

            string[] models = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.json");

            lbxModels.BeginUpdate();
            foreach (string model in models)
            {
                lbxModels.Items.Add(Path.GetFileName(model));
            }
            lbxModels.EndUpdate();

            lblCurrentModel.Text = selectedModel;
        }

        private void click_btnModelLoad(object sender, EventArgs e)
        {
            lblErrorText.Visible = false;

            if (lbxModels.SelectedItems.Count == 0) { return; }

            selectedModel = lbxModels.SelectedItems[0].ToString();

            string openResult = model.openModel(selectedModel);

            if (openResult == "none") {
                // Successfully opened model

                lblCustomModel.Text = translator.translate("Change model:");
                if (selectedModel.Length <= 13)
                {
                    lblCurrentModel.Text = selectedModel;
                }
                else
                {
                    lblCurrentModel.Text = $"{selectedModel.Substring(0, 10)}...";
                }

                // Modify the sliders
                if (model.NOfpeople    < trbNPeople.Maximum)   { trbNPeople.Value = model.NOfpeople; }      else { lblCustomNPeople.Enabled = false; trbNPeople.Enabled = false; }
                if (model.infectedTime < trbTInfected.Maximum) { trbTInfected.Value = model.infectedTime; } else { lblCustomTInfected.Enabled = false; trbTInfected.Enabled = false; }
                if (model.healedTime   < trbTHealed.Maximum)   { trbTHealed.Value = model.healedTime; }     else { lblCustomTHealed.Enabled = false; trbTHealed.Enabled = false; }

                // Dissable non needed sliders
                lblCustomPInfected.Enabled   =   false;
                trbPInfected.Enabled         =   false;

                return;
            }

            // Something went wrong opening the model
            if (openResult == "null")
            {
                lblErrorText.Text        =   "Something in the model is null";
                lblErrorText.ForeColor   =   Color.Red;
                lblErrorText.Visible     =   true;
            } else
            {
                lblErrorText.Text        =   $"Model is missing key '{openResult}'";
                lblErrorText.ForeColor   =   Color.Red;
                lblErrorText.Visible     =   true;
            }
        }

        private void click_btnModelUnload(object sender, EventArgs e)
        {
            model = new();

            // Set some default values for the custom model
            model.type = "custom";

            selectedModel = "custom";
            lblCurrentModel.Text = translator.translate("custom");
            lblCustomModel.Text = translator.translate("Custom model:");

            // Load the slider data to the model
            model.NOfpeople            =   trbNPeople.Value;
            model.infectedPercentage   =   trbPInfected.Value;
            model.infectedTime         =   trbTInfected.Value;
            model.healedTime           =   trbTHealed.Value;

            // Enable disabled controls
            lblCustomPInfected.Enabled   =   true;
            trbPInfected.Enabled         =   true;
            lblCustomNPeople.Enabled     =   true;
            trbNPeople.Enabled           =   true;
            lblCustomTInfected.Enabled   =   true;
            trbTInfected.Enabled         =   true;
            lblCustomTHealed.Enabled     =   true;
            trbTHealed.Enabled           =   true;
        }

        private void valChange_trbNPeople(object sender, EventArgs e)
        {
            lblCustomNPeople.Text   =   String.Format(translator.translate("Number of people: {0}"), trbNPeople.Value);
            model.NOfpeople         =   trbNPeople.Value;
        }

        private void valChanged_trbPInfected(object sender, EventArgs e)
        {
            lblCustomPInfected.Text = String.Format(translator.translate("Infected percent: {0}%"), trbPInfected.Value);
            model.infectedPercentage = trbPInfected.Value;
        }

        private void valChange_trbTInfected(object sender, EventArgs e)
        {
            lblCustomTInfected.Text = String.Format(translator.translate("Infected time: {0} days"), trbTInfected.Value);
            model.infectedTime = trbTInfected.Value;
        }

        private void valChange_trbTHealed(object sender, EventArgs e)
        {
            lblCustomTHealed.Text = String.Format(translator.translate("Healed time: {0} days"), trbTHealed.Value);
            model.healedTime = trbTHealed.Value;
        }

        private void click_btnChangeLanguage(object sender, EventArgs e)
        {
            if (translator.lang == "en") {
                translator.lang = "hr";

                Bitmap img = Resources.flag_en; // Better to change the flag to the opposite language (what the user doesnt know)
                Bitmap scaled = new(img, btnChangeLanguage.Size.Width + 2, btnChangeLanguage.Size.Height + 2); // +2 because it doesnt fit exactly
                btnChangeLanguage.Image = scaled;
            } else {
                translator.lang = "en";

                Bitmap img = Resources.flag_hr; // Better to change the flag to the opposite language (what the user doesnt know)
                Bitmap scaled = new(img, btnChangeLanguage.Size.Width + 2, btnChangeLanguage.Size.Height + 2); // +2 because it doesnt fit exactly
                btnChangeLanguage.Image = scaled;
            }

            if (selectedModel == "custom") { lblCurrentModel.Text   =   translator.translate("custom"); }        // If the model is not loaded
            if (selectedModel == "custom") { lblCustomModel.Text    =   translator.translate("Custom model:"); } // If the model is not loaded
            if (selectedModel != "custom") { lblCustomModel.Text    =   translator.translate("Change model:"); } // If the model is loaded
            btnModelLoad.Text          =   translator.translate("Load");
            btnModelUnload.Text        =   translator.translate("Unload");
            btnStart.Text              =   translator.translate("Start the simulation");
            lblCurrentModelText.Text   =   translator.translate("Current model:");
            lblCustomNPeople.Text      =   String.Format(translator.translate("Number of people: {0}"), trbNPeople.Value);     // Also set the number to the slider value
            lblCustomPInfected.Text    =   String.Format(translator.translate("Infected percent: {0}%"), trbPInfected.Value);  // Also set the number to the slider value
            lblCustomTInfected.Text    =   String.Format(translator.translate("Infected time: {0} days"), trbTInfected.Value); // Also set the number to the slider value
            lblCustomTHealed.Text      =   String.Format(translator.translate("Healed time: {0} days"), trbTHealed.Value);     // Also set the number to the slider value
        }

        private void click_btnStart(object sender, EventArgs e)
        {
            model.spawnPeople();

            formSimulation simulation = new ();
            formResults results = new ();
            
            // Send the model to the simulation form
            simulation.init(model, results);
            results.init(model, this.translator);

            this.Hide();

            // Run the results form on a seperate thread
            Task.Run(() => results.ShowDialog());

            simulation.ShowDialog();

            this.Close();
        }
    }
}