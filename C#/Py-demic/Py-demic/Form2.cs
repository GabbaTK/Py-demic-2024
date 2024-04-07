namespace Py_demic
{
    public partial class formSimulation : Form
    {
        private Model? model;
        private formResults results;
        private System.Windows.Forms.Timer timer = new();

        private double time()
        {
            DateTime now = DateTime.UtcNow;
            DateTime currentYear = new DateTime(now.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (now - currentYear).TotalSeconds;
        }

        private void setModel(Model model)
        {
            this.model = model;

            // Set the form size to the model size
            this.Size = new Size(model.sizeX, model.sizeY + 25); // 25 is around the size of the top bar (title minimize maximize close)

            // Set the timer interval to the model frameRate
            timer.Interval = model.simulationRate;
            timer.Start();

            model.dayChange = time() + model.secondsPerDay;
        }

        public formSimulation()
        {
            InitializeComponent();

            timer.Tick += frameUpdate;
        }

        public void init(Model model, formResults results)
        {
            setModel(model);
            this.results = results;
        }

        private void frameUpdate(object? sender, EventArgs e)
        {
            model.sizeX = this.Size.Width;
            model.sizeY = this.Size.Height;
            model.canvas = new(model.sizeX, model.sizeY);
            model.collisionCanvas = new(model.sizeX, model.sizeY);

            model.drawModelLines();
            model.dayUpdate();
            model.dayUpdatedStep(this.results);
            this.Text = $"Py-demic simulation, day {model.day}";

            model.quarantine();

            // Step the data for each person
            foreach (Person person in model.people)
            {
                person.step(model);
            }

            Bitmap canvas = model.renderFrame();

            // Show the frame
            pbxSimulation.Image = canvas;

            // Im not good with garbage collection
            // The program uses around 3.5gb memory without this command
            // But with the commands it uses around (at the time of writing) 36mb of memory
            // It might not be the best approach but it works for now
            GC.Collect();
        }
    }
}
