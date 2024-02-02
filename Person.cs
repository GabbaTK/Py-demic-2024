namespace Py_demic
{
    public class Person
    {
        public int id;
        public double x;
        public double y;
        public double lastX;
        public double lastY;
        public int gotoX;
        public int gotoY;
        public int[] color;
        public string type;
        public bool canInfect;
        public bool canBeInfected;
        public double timeTillChange;
        public bool vaccineInfinite;

        private bool wallColliding(Bitmap canvas, int x, int y)
        {
            Color pixelColor = canvas.GetPixel(x, y);

            if (pixelColor == Color.FromArgb(0, 0, 0, 0)) { return false; } else { return true; }
        }

        private double pointDistance(double AX, double AY, int BX, int BY)
        {
            double distanceX = Math.Abs(AX - BX);
            double distanceY = Math.Abs(AY - BY);

            return Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
        }

        private bool randomChance(int chance)
        {
            Random rand = new();

            double randomNum = rand.NextDouble() * 100;

            if (randomNum < chance) { return true; } else { return false; }
        }

        private double time()
        {
            DateTime now = DateTime.UtcNow;
            DateTime currentYear = new DateTime(now.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (now - currentYear).TotalSeconds;
        }

        private void stepType(Model model)
        {
            if (this.timeTillChange < time())
            {
                if (this.type == "infected")
                {
                    this.type = "healed";
                    this.color = new int[] { 255, 255, 255 };
                    this.canInfect = false;
                    this.canBeInfected = false;
                    this.timeTillChange = time() + model.healedTime + model.modelRandomness();

                    if (randomChance(model.lethalityChance))
                    {
                        this.type = "dead";
                        this.color = new int[] { 50, 50, 50 };
                    }
                } else if (this.type == "healed")
                {
                    this.type = "normal";
                    this.color = new int[] { 0, 0, 255 };
                    this.canInfect = false;
                    this.canBeInfected = true;
                } else if (this.type == "vaccinated")
                {
                    if (this.vaccineInfinite) { return; }

                    this.type = "normal";
                    this.color = new int[] { 0, 0, 255 };
                    this.canInfect = false;
                    this.canBeInfected = true;
                }
            }
        }

        private void changeGoto(Model model)
        {
            if (pointDistance(this.x, this.y, this.gotoX, this.gotoY) < 1)
            {
                Random rand = new();

                // So the people dont move out of bound I subtract half of the size of the person from the edges to ensure that they are always inside the border
                this.gotoX = rand.Next((int)(500 * model.peopleScale), model.sizeX - (int)(500 * model.peopleScale));
                this.gotoY = rand.Next((int)(1000 * model.peopleScale), model.sizeY - (int)(1000 * model.peopleScale));
            }
        }

        private void forceChangeGoto(Model model)
        {
            Random rand = new();

            // So the people dont move out of bound I subtract half of the size of the person from the edges to ensure that they are always inside the border
            this.gotoX = rand.Next((int)(500 * model.peopleScale), model.sizeX - (int)(500 * model.peopleScale));
            this.gotoY = rand.Next((int)(1000 * model.peopleScale), model.sizeY - (int)(1000 * model.peopleScale));
        }

        private void move(double speed)
        {
            this.lastX = this.x;
            this.lastY = this.y;

            double distanceX = this.gotoX - this.x;
            double distanceY = this.gotoY - this.y;

            double distance = Math.Sqrt(distanceX * distanceX + distanceY * distanceY);

            double moveX = distanceX / distance;
            double moveY = distanceY / distance;

            if (double.IsNaN(moveX)) { moveX = 0; }
            if (double.IsNaN(moveY)) { moveY = 0; }

            moveX *= speed;
            moveY *= speed;

            this.x += moveX;
            this.y += moveY;
        }

        private void checkColision(Model model)
        {
            foreach (Person person in model.people)
            {
                if (person == this) { continue; }

                bool inX = (person.x - 1000 * model.peopleScale <= this.x) && (this.x <= 1000 * model.peopleScale + person.x); // To check if the person is touching, you need to check the distance from the centers, which is the total distance (the height of the model (1000px))
                bool inY = (person.y - 2000 * model.peopleScale <= this.y) && (this.y <= 2000 * model.peopleScale + person.y); // To check if the person is touching, you need to check the distance from the centers, which is the total distance (the width of the model (2000px))

                if (inX && inY)
                {
                    if (person.type == "dead") { continue; }

                    this.x = this.lastX;
                    this.y = this.lastY;
                    forceChangeGoto(model);

                    if (this.canBeInfected)
                    {
                        if (person.canInfect)
                        {
                            this.type = "infected";
                            this.color = new int[] { 255, 0, 0 };
                            this.canBeInfected = false;
                            this.canInfect = randomChance(model.infectiveChance);
                            this.timeTillChange = time() + model.infectedTime + model.modelRandomness();
                        }
                    }
                }
            }

            if (wallColliding(model.collisionCanvas, (int)this.x, (int)this.y)) {
                this.x = this.lastX;
                this.y = this.lastY;
                forceChangeGoto(model);
            }
        }

        public Person()
        {
            // The person image scale is 1000x 2000y

            this.id = 0;
            this.x = 0;
            this.y = 0;
            this.lastX = 0;
            this.lastY = 0;
            this.gotoX = 0;
            this.gotoY = 0;
            this.color = new int[] { 0, 0, 255 };
            this.type = "normal";
            this.canInfect = false;
            this.canBeInfected = true;
            this.timeTillChange = 0;
            this.vaccineInfinite = true;
        }

        public void step(Model model)
        {
            // Events regardless of type
            move(model.peopleTravelSpeed);
            changeGoto(model);

            // Events if type is alive
            if (this.type != "dead")
            {
                stepType(model);
                checkColision(model);
            }
        }
    }
}
