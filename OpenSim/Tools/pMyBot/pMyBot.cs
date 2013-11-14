using System;
using System.Reflection;
using System.Threading;
using log4net;
using log4net.Config;
using Nini.Config;
using OpenSim.Framework;
using OpenSim.Framework.Console;

namespace pMyBot
{
    /// <summary>
    /// Event Type for the bots
    /// </summary>
    public enum EventType : int
    {
        NONE = 0,
        CONNECTED = 1,
        DISCONNECTED = 2
    }

    class pMyBot
    {
        
        private static readonly ILog m_log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [STAThread]
        static void Main(string[] args)
        {
            XmlConfigurator.Configure(); // read xxx.exe.config

            // parse configs
            IConfig config = ParseConfig(args);

            if (config.Get("help") != null || config.Get("loginuri") == null)
            {
                Help();
            }
            else if (config.Get("firstname") == null || config.Get("lastname") == null || config.Get("password") == null)
            {
                Console.WriteLine("ERROR: You must supply a firstname, lastname and password for the bots.");
            }
            else
            {

            }

            Console.ReadLine();
        }

        private static IConfig ParseConfig(String[] args)
        {
            //Set up our nifty config..  thanks to nini
            ArgvConfigSource cs = new ArgvConfigSource(args);

            cs.AddSwitch("Startup", "botcount", "n");
            cs.AddSwitch("Startup", "loginuri", "l");
            cs.AddSwitch("Startup", "firstname","f");
            cs.AddSwitch("Startup", "lastname","l");
            cs.AddSwitch("Startup", "password","p");
            cs.AddSwitch("Startup", "behaviours", "b");
            cs.AddSwitch("Startup", "help", "h");

            IConfig ol = cs.Configs["Startup"];
            return ol;
        }

        private static void Help()
        {
            // Added the wear command. This allows the bot to wear real clothes instead of default locked ones.
            // You can either say no, to not load anything, yes, to load one of the default wearables, a folder
            // name, to load an specific folder, or save, to save an avatar with some already existing wearables
            // worn to the folder MyAppearance/FirstName_LastName, and the load it.
            Console.WriteLine(
                "usage: pCampBot <-loginuri loginuri> [OPTIONS]\n" +
                "Spawns a set of bots to test an OpenSim region\n\n" +
                "  -l, -loginuri      loginuri for sim to log into (required)\n" +
                "  -n, -botcount      number of bots to start (default: 1)\n" +
                "  -firstname         first name for the bots\n" +
                "  -lastname          lastname for the bots.  Each lastname will have _<bot-number> appended, e.g. Ima Bot_0\n" +
                "  -password          password for the bots\n" +
                "  -b, behaviours     behaviours for bots.  Comma separated, e.g. p,g.  Default is p\n" +
                "    current options are:\n" +
                "       p (physics)\n" +
                "       g (grab)\n" +
                "       t (teleport)\n" +
                "  -wear              set appearance folder to load from (default: no)\n" +
                "  -h, -help          show this message");
        }

    }
}
