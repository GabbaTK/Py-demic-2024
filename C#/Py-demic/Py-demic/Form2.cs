namespace Py_demic
{
    public partial class formSimulation : Form
    {
        Model? model;
        System.Windows.Forms.Timer timer = new();

        public formSimulation()
        {
            InitializeComponent();

            timer.Tick += frameUpdate;
            timer.Start();
        }

        public void setModel(Model model)
        {
            this.model = model;

            // Set the form size to the model size
            this.Size = new Size(model.sizeX, model.sizeY + 25); // 25 is around the size of the top bar (title minimize maximize close)
            // Set the timer interval to the model frameRate
            timer.Interval = 1000 / model.simulationRate;
        }

        private void frameUpdate(object? sender, EventArgs e)
        {
            // Step the data for each person
            foreach (Person person in model.people)
            {
                person.step(model);
            }

            // Draw the people (render the frame)
            Bitmap canvas = model.drawPeople();

            // Show the frame
            pbxSimulation.Image = canvas;
        }
    }
}
