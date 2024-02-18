namespace Py_demic
{
    public class Translator
    {
        public string lang = "en";

        // Initialize Croatian translation
        private Dictionary<String, String> translationHR = new ()
            {
                { "Load", "Učitaj" },
                { "Unload", "Očisti" },
                { "Current model:", "Trenutačan model:" },
                { "custom", "jednostavan" },
                { "Custom model:", "Jednostavan model:" },
                { "Change model:", "Promijeni model:" },
                { "Number of people: {0}", "Broj ljudi: {0}" },
                { "Infected percent: {0}%", "Postotak zaraženih: {0}%" },
                { "Infected time: {0} days", "Vrijeme zaraženo: {0} dana" },
                { "Healed time: {0} days", "Vrijeme preboljeno: {0} dana" },
                { "Start the simulation", "Pokreni simulaciju" },
                { "Simulation results:", "Rezultati simulacije:" },
                { "Alive: {0}", "Živi: {0}" },
                { "Dead: {0}", "Preminuli: {0}" },
                { "Normal: {0}", "Normalni: {0}" },
                { "Infected: {0}", "Zaraženi: {0}" },
                { "Vaccinated: {0}", "Cijepljeni: {0}" }
            };

    public string translate(string text)
        {
            if (this.lang == "en")
            {
                return text;
            }
            else if (this.lang == "hr")
            {
                return translationHR[text];
            }

            return "N/A";
        }
    }
}
