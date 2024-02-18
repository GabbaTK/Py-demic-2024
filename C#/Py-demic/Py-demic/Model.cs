/*
 * TODO:
 * 
 * - Više simulacija u isto vrijeme (simulacija gradova)
 *      - Putovanje između gradova
 * - Karantena
 * 
 */







using Py_demic.Properties;
using System.Text.Json;

namespace Py_demic
{
    public class Model
    {
        public string type = "custom";
        public int sizeX;
        public int sizeY;
        public Bitmap? canvas;
        public Bitmap? collisionCanvas;
        public int NOfpeople; // Number of people
        public double peopleScale;
        public double peopleTravelSpeed;
        public List<JsonElement>? areas;
        public int randomness = 0;
        public int infectedTime;
        public int healedTime;
        public double infectiveChance = 100;
        public double lethalityChance = 0;
        public int? vaccineTime;
        public List<Person>? people = new(); // The actual people classes
        public int simulationRate = 45;
        public int secondsPerDay = 5; // The number of seconds in a simulation day
        public double coughChance = 0;
        public int coughRange = 0;
        public double coughTime = 0;
        public List<int> coughInfectionDropoff = new() { 0, 0 };
        public List<Dictionary<String, Double>> coughs = new();

        // Data for custom models
        public int infectedPercentage = 0;

        // Statistic data
        public int day = 0;
        public double dayChange = 0;

        private int lastDay = 0;

        // Variable used in multiple functions, so define it once
        private Color colorEmpty = Color.FromArgb(0, 0, 0, 0);
        private Random rand = new();

        private double time()
        {
            DateTime now = DateTime.UtcNow;
            DateTime currentYear = new DateTime(now.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (now - currentYear).TotalSeconds;
        }

        private bool randomChance(double chance)
        {
            double randomNum = rand.NextDouble() * 100;

            if (randomNum < chance) { return true; } else { return false; }
        }

        private Bitmap changeBitmapColor(Bitmap bitmap, Color newColor)
        {
            Bitmap newBitmap = (Bitmap)bitmap.Clone();

            for (int x = 0; x < newBitmap.Width; x++)
            {
                for (int y = 0; y < newBitmap.Height; y++)
                {
                    Color currentColor = newBitmap.GetPixel(x, y);

                    if (currentColor != colorEmpty)
                    {
                        newBitmap.SetPixel(x, y, newColor);
                    }
                }
            }

            return newBitmap;
        }
        private Bitmap drawPeople(Bitmap drawingBitmap)
        {
            //Bitmap drawingBitmap = (Bitmap)this.canvas.Clone();
            Size personSize = new((int)(1000 * this.peopleScale), (int)(2000 * this.peopleScale)); // 1000 is width, 2000 is height, multiply them by the scale to get the size of the image
            Bitmap personImageOrg = new(Resources.person, personSize);

            using (Graphics drawingGraphics = Graphics.FromImage(drawingBitmap))
            {
                foreach (Person person in this.people)
                {
                    Bitmap personImage = changeBitmapColor(personImageOrg, Color.FromArgb(person.color[0], person.color[1], person.color[2]));

                    // 500 is half of the person model width
                    // 1000 is half of the person model height
                    // The person.x/y is the center of the person, but DrawImage draws it from the start of the corner, so I need to subtract half of the scale from the position
                    drawingGraphics.DrawImage(personImage, (int)(person.x - 500 * this.peopleScale), (int)(person.y - 1000 * this.peopleScale));

                    personImage.Dispose();
                }
            }

            return drawingBitmap;
        }

        private Bitmap drawCoughs()
        {
            Bitmap drawingBitmap = new(this.canvas);
            Brush coughBrush = new SolidBrush(Color.FromArgb(127, 200, 20, 20));

            using (Graphics drawingGraphics = Graphics.FromImage(drawingBitmap))
            {
                foreach (Dictionary<String, Double> cough in this.coughs)
                {
                    drawingGraphics.FillEllipse(coughBrush, (int)(cough["x"] - this.coughRange), (int)(cough["y"] - this.coughRange), this.coughRange * 2, this.coughRange * 2);
                }
            }

            return drawingBitmap;
        }

        public string openModel(string modelName)
        {
            String modelContent = File.ReadAllText(modelName);
            Dictionary<String, JsonElement>? model = JsonSerializer.Deserialize<Dictionary<String, JsonElement>>(modelContent);

            if (model == null ) { return "null"; }

            // Return the missing key if its missing
            if (!model.ContainsKey("size_x")) { return "size_x"; }
            if (!model.ContainsKey("size_y")) { return "size_y"; }

            // Set the local variable to the model variable
            this.sizeX                   =   model["size_x"].GetInt32();
            this.sizeY                   =   model["size_y"].GetInt32();
            this.canvas                  =   new Bitmap(this.sizeX, this.sizeY);
            this.collisionCanvas         =   new Bitmap(this.sizeX, this.sizeY);

            // and so on...

            if (!model.ContainsKey("people")) { return "people"; }
            if (!model.ContainsKey("people_scale")) { return "people_scale"; }
            if (!model.ContainsKey("travel_speed")) { return "travel_speed"; }

            this.NOfpeople           =   model["people"].GetInt32();
            this.peopleScale         =   model["people_scale"].GetDouble();
            this.peopleTravelSpeed   =   model["travel_speed"].GetDouble();

            if (model.ContainsKey("lines"))
            {
                foreach (JsonElement drawLineJsonElement in model["lines"].EnumerateArray())
                {
                    // Converts a jsonElement containing Dictionary<String, JsonElement> to an actual Dictionary<String, JsonElement>
                    Dictionary<String, JsonElement> drawLine = drawLineJsonElement.EnumerateObject().ToDictionary(kvp => kvp.Name, kvp => kvp.Value);

                    if (!drawLine.ContainsKey("P1")) { return "P1"; }
                    if (!drawLine.ContainsKey("P2")) { return "P2"; }
                    if (!drawLine.ContainsKey("color")) { return "color"; }
                    if (!drawLine.ContainsKey("thickness")) { return "thickness"; }

                    // Gets each elements raw data, then converts that raw json data to a List<int> using JsonSerialiter.Deserialize
                    List<int> color        =   JsonSerializer.Deserialize<List<int>>(drawLine["color"].GetRawText());
                    List<int> drawLineP1   =   JsonSerializer.Deserialize<List<int>>(drawLine["P1"].GetRawText());
                    List<int> drawLineP2   =   JsonSerializer.Deserialize<List<int>>(drawLine["P2"].GetRawText());

                    if (color == null || drawLineP1 == null || drawLineP2 == null) { return "null"; }

                    // Clamp the values between 0 and edges
                    // 500 is half the size of the person model X
                    // 1000 is half the size of the person model Y
                    int clampedAX = Math.Clamp(drawLineP1[0] - (int)(500 * model["people_scale"].GetDouble()), 0, this.sizeX - 1); // This will be used in the collision canvas, the collision detection is in the middle of the person, so I removed the scale of the person from the position of the line to effectively create an invisible barrier
                    int clampedAY = Math.Clamp(drawLineP1[1] - (int)(1000 * model["people_scale"].GetDouble()), 0, this.sizeY - 1); // This will be used in the collision canvas, the collision detection is in the middle of the person, so I removed the scale of the person from the position of the line to effectively create an invisible barrier
                    int clampedBX = Math.Clamp(drawLineP2[0] + (int)(500 * model["people_scale"].GetDouble()), 0, this.sizeX - 1); // This will be used in the collision canvas, the collision detection is in the middle of the person, so I added the scale of the person from the position of the line to effectively create an invisible barrier
                    int clampedBY = Math.Clamp(drawLineP2[1] + (int)(1000 * model["people_scale"].GetDouble()), 0, this.sizeY - 1); // This will be used in the collision canvas, the collision detection is in the middle of the person, so I added the scale of the person from the position of the line to effectively create an invisible barrier

                    Pen pen = new(Color.FromArgb(color[0], color[1], color[2]), drawLine["thickness"].GetInt32());
                    using (Graphics graphics = Graphics.FromImage(this.canvas))            { graphics.DrawLine(pen, drawLineP1[0], drawLineP1[1], drawLineP2[0], drawLineP2[1]); }
                    using (Graphics graphics = Graphics.FromImage(this.collisionCanvas))   { graphics.DrawRectangle(pen, clampedAX, clampedAY, clampedBX - clampedAX, clampedBY - clampedAY); }
                }
            }

            if (!model.ContainsKey("areas")) { return "areas"; }

            // Gets the elements raw data, then converts that raw json data to a List<Dictionary<String, Object>> (list with dictionaries) using JsonSerialiter.Deserialize
            this.areas = JsonSerializer.Deserialize<List<JsonElement>>(model["areas"].GetRawText());

            if (!model.ContainsKey("randomness")) { return "randomness"; }
            if (!model.ContainsKey("infected_time")) { return "infected_time"; }
            if (!model.ContainsKey("healed_time")) { return "healed_time"; }

            this.randomness     =   model["randomness"].GetInt32();
            this.infectedTime   =   model["infected_time"].GetInt32();
            this.healedTime     =   model["healed_time"].GetInt32();

            // Non essential data
            if (model.ContainsKey("infective_chance"))
            {
                this.infectiveChance = model["infective_chance"].GetDouble();
            }
            if (model.ContainsKey("lethality_chance"))
            {
                this.lethalityChance = model["lethality_chance"].GetDouble();
            }
            if (model.ContainsKey("vaccine_time"))
            {
                this.vaccineTime = model["vaccine_time"].GetInt32();
            }
            if (model.ContainsKey("simulation_rate"))
            {
                this.simulationRate = model["simulation_rate"].GetInt32();
            }
            if (model.ContainsKey("seconds_per_day"))
            {
                this.secondsPerDay = model["seconds_per_day"].GetInt32();
            }
            if (model.ContainsKey("cough_chance"))
            {
                this.coughChance = model["cough_chance"].GetDouble();
            }
            if (model.ContainsKey("cough_range"))
            {
                this.coughRange = model["cough_range"].GetInt32();
            }
            if (model.ContainsKey("cough_time"))
            {
                this.coughTime = model["cough_time"].GetDouble();
            }
            if (model.ContainsKey("cough_infection_dropoff"))
            {
                // Converts a jsonElement containing List<int> to an actual List<int>
                this.coughInfectionDropoff = model["cough_infection_dropoff"].EnumerateArray().Select(element => element.GetInt32()).ToList();
            }

            this.type = "load";

            return "none";
        }

        public void spawnPeople()
        {
            if (this.type == "load")
            {
                int lastId = -1;

                // Model is loaded and not custom
                foreach (JsonElement jsonArea in this.areas)
                {
                    // Get the elements raw data, then converts that raw json data to a Dictionary<String, JsonElement> using JsonSerialiter.Deserialize
                    Dictionary<String, JsonElement> area = JsonSerializer.Deserialize<Dictionary<String, JsonElement>>(jsonArea.GetRawText());

                    int peopleInArea = (int)(this.NOfpeople * (area["probability"].GetInt32() / 100.0)); // Calculate the amount of people for each area if splitting evenly by the probability
                    int personId = 0;

                    for (int personIdInArea = 0; personIdInArea < peopleInArea; personIdInArea++)
                    {
                        personId = personIdInArea + lastId + 1; // If the last id was for example 3, then a new area is selected, the personId will be 0, so personId + lastId will be 3, but that person already exists, so we add 1

                        while (true)
                        {
                            List<int> areaStart = JsonSerializer.Deserialize<List<int>>(area["start"].GetRawText());
                            List<int> areaEnd = JsonSerializer.Deserialize<List<int>>(area["end"].GetRawText());

                            int posX = rand.Next(areaStart[0] + (int)(1000 * this.peopleScale), areaEnd[0] - (int)(1000 * this.peopleScale)); // To check if the person is touching, you need to check the distance from the centers, which is the total distance (the height of the model (1000px))
                            int posY = rand.Next(areaStart[1] + (int)(2000 * this.peopleScale), areaEnd[1] - (int)(2000 * this.peopleScale)); // To check if the person is touching, you need to check the distance from the centers, which is the total distance (the width of the model (2000px))

                            bool tooClose = false;

                            // Create the person that will be spawned
                            Person personSpawning = new();
                            personSpawning.id = personId;
                            personSpawning.x = posX;
                            personSpawning.y = posY;

                            // For the first person the list is empty, so it is null
                            if (this.people != null)
                            {
                                foreach (Person person in this.people)
                                {
                                    bool inX = (person.x - 1000 * this.peopleScale <= personSpawning.x) && (personSpawning.x <= 1000 * this.peopleScale + person.x); // The scale for the person image is 1000x 2000y
                                    bool inY = (person.y - 2000 * this.peopleScale <= personSpawning.y) && (personSpawning.y <= 2000 * this.peopleScale + person.y); // The scale for the person image is 1000x 2000y

                                    if (inX && inY)
                                    {
                                        tooClose = true;

                                        break;
                                    }
                                }
                            }

                            if (!tooClose)
                            {
                                // The person is not too close and can be spawned
                                // Add some other data to the person
                                personSpawning.lastX = posX;
                                personSpawning.lastY = posY;
                                personSpawning.gotoX = posX;
                                personSpawning.gotoY = posY;

                                if (area.ContainsKey("infected"))
                                {
                                    if (area["infected"].GetBoolean())
                                    {
                                        personSpawning.type = "infected";
                                        personSpawning.canBeInfected = false;
                                        if (randomChance(this.infectiveChance)) { personSpawning.canInfect = true; personSpawning.color = new int[] { 255, 0, 0 }; } else { personSpawning.canInfect = false; personSpawning.color = new int[] { 150, 0, 0 }; }
                                        personSpawning.timeTillChange = time() + (this.infectedTime * this.secondsPerDay) + modelRandomness();
                                    }
                                }
                                else if (area.ContainsKey("vaccinated"))
                                {
                                    if (area["vaccinated"].GetBoolean())
                                    {
                                        personSpawning.type = "vaccinated";
                                        personSpawning.canBeInfected = false;
                                        personSpawning.canInfect = false;
                                        personSpawning.color = new int[] { 0, 255, 0 };

                                        if (this.vaccineTime != null)
                                        {
                                            personSpawning.vaccineInfinite = false;
                                            personSpawning.timeTillChange = time() + (double)(this.vaccineTime * this.secondsPerDay) + modelRandomness(); // I need to convert double? (double/null) to double
                                        }
                                    }
                                }

                                this.people.Add(personSpawning);

                                break;
                            }
                        }
                    }

                    lastId = personId;
                }
            } else if (this.type == "custom")
            {
                // On loading the model it creates the canvas, but here we are using the custom model settings
                // So it doesnt create the canvas
                // And the personScale is null, but the settings here use it as if it was 0.025, so I set it to that
                this.sizeX               =   1250;
                this.sizeY               =   1250;
                this.canvas              =   new Bitmap(1250, 1250);
                this.collisionCanvas     =   new Bitmap(1250, 1250);
                this.peopleScale         =   0.025;
                this.peopleTravelSpeed   =   2;
                this.randomness          =   5;
                int infectedPeople       =   (int)(this.NOfpeople * (this.infectedPercentage / 100.0));

                for (int personId = 0; personId < this.NOfpeople; personId++)
                {
                    while (true)
                    {
                        // The canvas size for the custom model is 1250x1250, 
                        int posX = rand.Next(25, 1225); // The 25 and 1225 are the smaller boundries using the person model scale of 25x50 
                        int posY = rand.Next(50, 1200); // The 50 and 1200 are the smaller boundries using the person model scale of 25x50

                        bool tooClose = false;

                        // Create the person that will be spawned
                        Person personSpawning = new();
                        personSpawning.id = personId;
                        personSpawning.x = posX;
                        personSpawning.y = posY;

                        // For the first person the list is empty, so it is null
                        if (this.people != null)
                        {
                            foreach (Person person in this.people)
                            {
                                bool inX = (person.x - 25 <= personSpawning.x) && (personSpawning.x <= 25 + person.x);
                                bool inY = (person.y - 50 <= personSpawning.y) && (personSpawning.y <= 50 + person.y);

                                if (inX && inY)
                                {
                                    tooClose = true;

                                    break;
                                }
                            }
                        }

                        if (!tooClose)
                        {
                            personSpawning.lastX = posX;
                            personSpawning.lastY = posY;
                            personSpawning.gotoX = posX;
                            personSpawning.gotoY = posY;

                            if (infectedPeople > 0)
                            {
                                infectedPeople--;
                                personSpawning.type = "infected";
                                personSpawning.canBeInfected = false;
                                personSpawning.canInfect = true;
                                personSpawning.timeTillChange = time() + (this.infectedTime * this.secondsPerDay) + modelRandomness();
                                personSpawning.color = new int[] { 255, 0, 0 };
                            }

                            this.people.Add(personSpawning);

                            break;
                        }
                    }
                }
            }
        }
        
        public double modelRandomness()
        {
            double value = rand.NextDouble();
            value *= (this.randomness * 2); // self.data.randomness * 2 is so you can subtract randomness and get a number between -randomness and randomness
            value -= this.randomness;

            return value;
        }

        public void dayUpdate()
        {
            if (this.dayChange < time())
            {
                this.dayChange = time() + this.secondsPerDay;
                this.day++;
            }
        }
        
        public void dayUpdatedStep(formResults form)
        {
            if (this.day != this.lastDay)
            {
                this.lastDay = this.day;

                form.dayChange();
            }
        }

        public Bitmap renderFrame()
        {
            Bitmap drawingBitmap = (Bitmap)this.canvas.Clone();

            // Remove the cough if the time has expired
            List<Dictionary<String, Double>> unExpiredCoughs = new();

            foreach (Dictionary<String, Double> cough in this.coughs)
            {
                if (cough["time"] > time())
                {
                    // Cough hasnt expired yet
                    unExpiredCoughs.Add(cough);
                }
            }

            this.coughs = unExpiredCoughs;

            drawingBitmap = drawCoughs();
            drawingBitmap = drawPeople(drawingBitmap);

            return drawingBitmap;
        }
    }
}
