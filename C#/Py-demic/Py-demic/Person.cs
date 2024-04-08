namespace Py_demic
{
    public class Person
    {
        public int routeId;
        public int id;
        public double x;
        public double y;
        public double lastX;
        public double lastY;
        public double gotoX;
        public double gotoY;
        public int[] color;
        public string type;
        public bool canInfect;
        public bool canBeInfected;
        public double timeTillChange;
        public bool vaccineInfinite;
        public bool otherWaiting; // When 2 people collide, and one or both are following the route, one should wait, while the other one follows the route
        public int changeGotoMinX;
        public int changeGotoMinY;
        public int changeGotoMaxX;
        public int changeGotoMaxY;
        public int originAreaMinX;
        public int originAreaMinY;
        public int originAreaMaxX;
        public int originAreaMaxY;

        private Random rand = new();

        private double coughTimeDelay;
        private Dictionary<String, Object>? route;
        private List<Object>? waypoints;
        private bool onRoute;
        private bool routeAvailable;
        private bool waiting;
        private double waitingTime;
        private int waypointId;
        private Dictionary<String, Object> waypoint;
        private bool nextWaypoint;

        private bool wallColliding(Bitmap canvas, int x, int y)
        {
            Color pixelColor = canvas.GetPixel(x, y);

            if (pixelColor == Color.FromArgb(0, 0, 0, 0)) { return false; } else { return true; }
        }

        private double pointDistance(double AX, double AY, double BX, double BY)
        {
            double distanceX = Math.Abs(AX - BX);
            double distanceY = Math.Abs(AY - BY);

            return Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
        }

        private bool randomChance(double chance)
        {
            double randomNum = rand.NextDouble() * 100;

            if (randomNum < chance) { return true; } else { return false; }
        }

        private double time()
        {
            DateTime now = DateTime.UtcNow;
            DateTime currentYear = new DateTime(now.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (now - currentYear).TotalSeconds;
        }
        
        private void infectByCough(Model model)
        {
            if (this.coughTimeDelay < time())
            {
                this.coughTimeDelay = time() + 0.5 + model.modelRandomness(); // 0.5 is for half a second so it must wait half a second before checking if it can be infected

                foreach (Dictionary<String, Double> cough in model.coughs)
                {
                    double distanceX = Math.Abs(this.x - cough["x"]);
                    double distanceY = Math.Abs(this.y - cough["y"]);

                    double distance = Math.Sqrt(distanceX * distanceX + distanceY * distanceY);

                    // Not in the cough zone
                    if (distance > model.coughRange) { continue; }

                    double scaledDistance = distance / model.coughRange; // scaledDistance is a 0 to 1 distance, 1 being at the edge, and 0 in the middle

                    // Get the chance between the dropoff[0] and dropoff[1]
                    double chance = (model.coughInfectionDropoff[1] - model.coughInfectionDropoff[0]) * scaledDistance + model.coughInfectionDropoff[0];

                    if (randomChance(chance))
                    {
                        // Infected by the cough
                        this.type = "infected";
                        if (randomChance(model.infectiveChance)) { this.canInfect = true; this.color = new int[] { 255, 0, 0 }; } else { this.canInfect = false; this.color = new int[] { 150, 0, 0 }; }
                        this.canBeInfected = false;
                        this.timeTillChange = time() + (model.infectedTime * model.secondsPerDay) + model.modelRandomness();

                        return;
                    }
                }
            }
        }

        private Dictionary<String, Double>? createCough(Model model)
        {
            // Will not cough
            if (!randomChance(model.coughChance)) { return null; }

            // Will cough
            if (this.coughTimeDelay < time())
            {
                this.coughTimeDelay = time() + 1 + model.modelRandomness(); // 1 is for 1 second so it must wait 1 second before coughing first

                Dictionary<String, Double> cough = new()
                {
                    { "x", this.x },
                    { "y", this.y },
                    { "time", time() + model.coughTime }
                };

                return cough;
            }

            return null;
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
                    this.timeTillChange = time() + (model.healedTime * model.secondsPerDay) + model.modelRandomness();

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
            if (pointDistance(this.x, this.y, this.gotoX, this.gotoY) < 2)
            {
                // So the people dont move out of bound I subtract half of the size of the person from the edges to ensure that they are always inside the border
                this.gotoX = rand.Next(this.changeGotoMinX + (int)(500 * model.peopleScale), this.changeGotoMaxX - (int)(500 * model.peopleScale));
                this.gotoY = rand.Next(this.changeGotoMinY + (int)(1000 * model.peopleScale), this.changeGotoMaxY - (int)(1000 * model.peopleScale));
            }
        }

        private void forceChangeGoto(Model model)
        {
            // So the people dont move out of bound I subtract half of the size of the person from the edges to ensure that they are always inside the border
            this.gotoX = rand.Next(this.changeGotoMinX + (int)(500 * model.peopleScale), this.changeGotoMaxX - (int)(500 * model.peopleScale));
            this.gotoY = rand.Next(this.changeGotoMinY + (int)(1000 * model.peopleScale), this.changeGotoMaxY - (int)(1000 * model.peopleScale));
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

        private void routeMove(Model model)
        {
            // Not on route and a route is possibly available
            if (!this.onRoute && this.routeAvailable)
            {               
                // Get the route with the id that is the same as the persons routeId
                foreach (Dictionary<String, Object> route in model.routes)
                {
                    if ((int)route["id"] == this.routeId)
                    {
                        this.route       =   route;
                        this.waypoints   =   (List<Object>)route["waypoints"];

                        break;
                    }
                }

                // No route for this routeId
                if (this.route == null) { this.routeAvailable = false; return; }

                this.onRoute = true;
                this.waypointId = 0;

                // Get the waypoint for the person to go to
                foreach (Object objectWaypoint in this.waypoints)
                {
                    Dictionary<String, Object> waypoint = (Dictionary<String, Object>)objectWaypoint;

                    if ((int)waypoint["id"] == ((List<int>)this.route["order"])[0])
                    {
                        this.waypoint = waypoint;
                        break;
                    }
                }

                this.gotoX = rand.Next(((List<int>)this.waypoint["pos"])[0] - (int)this.waypoint["range"], ((List<int>)this.waypoint["pos"])[0] + (int)this.waypoint["range"]);
                this.gotoY = rand.Next(((List<int>)this.waypoint["pos"])[1] - (int)this.waypoint["range"], ((List<int>)this.waypoint["pos"])[1] + (int)this.waypoint["range"]);
            }

            // On route and not waiting
            if (this.onRoute && !this.waiting)
            {
                // Check if its going to the next waypoint
                if (this.nextWaypoint)
                {
                    this.nextWaypoint = false;
                    this.waypointId++;

                    // Check if there are no more waypoint
                    if (this.waypointId == ((List<int>)this.route["order"]).Count()) { this.waypointId = 0; } // Set the next waypoint to be the first one

                    // Get the waypoint for the person to go to
                    foreach (Object objectWaypoint in this.waypoints)
                    {
                        Dictionary<String, Object> waypoint = (Dictionary<String, Object>)objectWaypoint;

                        if ((int)waypoint["id"] == ((List<int>)this.route["order"])[this.waypointId])
                        {
                            this.waypoint = waypoint;
                            break;
                        }
                    }

                    this.gotoX = rand.Next(((List<int>)this.waypoint["pos"])[0] - (int)this.waypoint["range"], ((List<int>)this.waypoint["pos"])[0] + (int)this.waypoint["range"]);
                    this.gotoY = rand.Next(((List<int>)this.waypoint["pos"])[1] - (int)this.waypoint["range"], ((List<int>)this.waypoint["pos"])[1] + (int)this.waypoint["range"]);
                }

                // Check if it got to the waypoint
                else if (pointDistance(this.x, this.y, this.gotoX, this.gotoY) < 2)
                {
                    this.nextWaypoint   =   true;
                    // Set the specified delay before getting the new waypoint
                    this.waiting        =   true;
                    this.waitingTime    =   ((List<int>)this.route["delays"])[this.waypointId] + time();
                }
            }
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

                    if (this.routeAvailable)
                    {
                        if (!this.otherWaiting)
                        {
                            this.x                =   this.lastX;
                            this.y                =   this.lastY;
                            this.waiting          =   true;
                            this.waitingTime      =   time() + 0.3;
                            person.otherWaiting   =   true;
                        }
                    } 
                    else
                    {
                        this.x   =   this.lastX;
                        this.y   =   this.lastY;

                        forceChangeGoto(model);
                    }

                    if (this.canBeInfected)
                    {
                        if (person.canInfect)
                        {
                            this.type = "infected";
                            if (randomChance(model.infectiveChance)) { this.canInfect = true; this.color = new int[] { 255, 0, 0 }; } else { this.canInfect = false; this.color = new int[] { 150, 0, 0 }; }
                            this.canBeInfected = false;
                            this.timeTillChange = time() + (model.infectedTime * model.secondsPerDay) + model.modelRandomness();
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

        private void removeWait()
        {
            if (this.waiting && this.waitingTime < time())
            {
                this.waiting        =   false;
                this.otherWaiting   =   false;
            }
        }

        public Person()
        {
            // The person image scale is 1000x 2000y

            this.id                 =   0;
            this.x                  =   0;
            this.y                  =   0;
            this.lastX              =   0;
            this.lastY              =   0;
            this.gotoX              =   0;
            this.gotoY              =   0;
            this.color              =   new int[] { 0, 0, 255 };
            this.type               =   "normal";
            this.canInfect          =   false;
            this.canBeInfected      =   true;
            this.timeTillChange     =   0;
            this.vaccineInfinite    =   true;
            this.routeId            =   0;
            this.onRoute            =   false;
            this.routeAvailable     =   true;
            this.waiting            =   false;
            this.waitingTime        =   0;
            this.waypointId         =   0;
            this.nextWaypoint       =   false;
            this.changeGotoMinX     =   0;
            this.changeGotoMinY     =   0;
            this.changeGotoMaxX     =   0;
            this.changeGotoMaxY     =   0;
            this.originAreaMinX     =   0;
            this.originAreaMinY     =   0;
            this.originAreaMaxX     =   0;
            this.originAreaMaxY     =   0;
        }

        public void step(Model model)
        {
            // Events if type is alive
            if (this.type != "dead")
            {
                if (!this.waiting) { move(model.peopleTravelSpeed); }

                if (model.routes != null) { routeMove(model); } else { this.routeAvailable = false; }
                if (!this.routeAvailable) { changeGoto(model); }

                removeWait();

                stepType(model);
                checkColision(model);
            }

            // Events if can infect
            if (this.canInfect)
            {
                Dictionary<String, Double>? cough = createCough(model);
                if (cough != null) { model.coughs.Add(cough); }
            }

            // Events if type is normal
            if (this.type == "normal")
            {
                infectByCough(model);
            }
        }
    }
}
