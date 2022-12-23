using BepInEx;
using BepInEx.Logging;

namespace ConfigExamples
{
    [BepInPlugin(myGUID, pluginName, versionString)]
    public class Main : BaseUnityPlugin
    {
        #region BEPINEX
        private const string myGUID = "com.example.BepInExExampleConfig";
        private const string pluginName = "Config Example";
        private const string versionString = "1.0.0";

        internal static ManualLogSource logger;
        #endregion

        public void Awake()
        {
            logger = Logger;
            ExampleConfig.Initialize(Config);
        }
    }

}
