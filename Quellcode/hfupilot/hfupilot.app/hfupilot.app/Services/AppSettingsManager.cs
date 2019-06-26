using Newtonsoft.Json.Linq;

namespace hfupilot.app.Services
{
    class AppSettingsManager
    {
        // stores the instance of the singleton
        private static AppSettingsManager _instance;

        // variable to store your appsettings in memory for quick and easy access
        private JObject _secrets;

        // constants needed to locate and access the App Settings file
        private const string Namespace = "App";
        private const string Filename = "appsettings.json";

        // Creates the instance of the singleton
        private AppSettingsManager()
        {
            //var assembly = IntrospectionExtensions.GetTypeInfo(typeof(AppSettingsManager)).Assembly;
            //var stream = assembly.GetManifestResourceStream($"{Namespace}.{FileName}");
            //using (var reader = new StreamReader(stream))
            //{
            //    var json = reader.ReadToEnd();
            //    _secrets = JObject.Parse(json);
            //}
        }

        // Accesses the instance or creates a new instance
        public static AppSettingsManager Settings
        {
            get
            {
                return null;
            }
        }

        // Used to retrieved setting values
        //public string this[string name]
        //{
        //    return string.empty;
        //}
    }
}
