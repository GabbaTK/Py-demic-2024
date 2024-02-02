namespace Py_demic
{
    public class Translator
    {
        private Dictionary<String, String> translationHR;

        public Translator() 
        {
            // Initialize Croatian translation
            translationHR = new()
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
                { "Start the simulation", "Pokreni simulaciju" }
            };
        }

        public string translate(string text, string lang)
        {
            if (lang == "en")
            {
                return text;
            }
            else if (lang == "hr")
            {
                return translationHR[text];
            }

            return "N/A";
        }
    }
}
