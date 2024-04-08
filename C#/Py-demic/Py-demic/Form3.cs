namespace Py_demic
{
    public partial class formResults : Form
    {
        private Model model;
        private Translator translator;

        public formResults()
        {
            InitializeComponent();
        }

        public void init(Model model, Translator translator)
        {
            this.model = model;
            this.translator = translator;

            lblSimResText.Text = translator.translate("Simulation results:");
        }

        public void dayChange()
        {
            int deadPeople = 0;
            int normalPeople = 0;
            int infectedPeople = 0;
            int healedPeople = 0;
            int vaccinatedPeople = 0;

            foreach (Person person in model.people)
            {
                if (person.type == "dead")
                {
                    deadPeople++;
                } else if (person.type == "normal")
                {
                    normalPeople++;
                } else if (person.type == "infected")
                {
                    infectedPeople++;
                } else if (person.type == "healed")
                {
                    healedPeople++;
                } else if (person.type == "vaccinated")
                {
                    vaccinatedPeople++;
                }
            }

            int alivePeople = normalPeople + infectedPeople + vaccinatedPeople + healedPeople;

            lblSimResAlive.BeginInvoke((MethodInvoker)delegate ()        { lblSimResAlive.Text        =   String.Format(translator.translate("Alive: {0}"),        alivePeople.ToString()); });
            lblSimResDead.BeginInvoke((MethodInvoker)delegate ()         { lblSimResDead.Text         =   String.Format(translator.translate("Dead: {0}"),         deadPeople.ToString()); });
            lblSimResNormal.BeginInvoke((MethodInvoker)delegate ()       { lblSimResNormal.Text       =   String.Format(translator.translate("Normal: {0}"),       normalPeople.ToString()); });
            lblSimResInfected.BeginInvoke((MethodInvoker)delegate ()     { lblSimResInfected.Text     =   String.Format(translator.translate("Infected: {0}"),     infectedPeople.ToString()); });
            lblSimResHealed.BeginInvoke((MethodInvoker)delegate ()       { lblSimResHealed.Text       =   String.Format(translator.translate("Healed: {0}"),       healedPeople.ToString()); });
            lblSimResVaccinated.BeginInvoke((MethodInvoker)delegate ()   { lblSimResVaccinated.Text   =   String.Format(translator.translate("Vaccinated: {0}"),   vaccinatedPeople.ToString()); });
        }
    }
}
