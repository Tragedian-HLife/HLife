using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using System.Drawing;
using System.IO;
using System.Reflection;
using ImageMagick;
using System.Windows.Controls;
using Newtonsoft.Json;
using System.Xml.Serialization;
using HLife.Dialogs;
using HLife.GameEntities.People;
using HLife.Actions;
using HLife.GameEntities.People.AI;
using HLife.GameEntities.Locations;
using HLife.GameEntities.Props;

namespace HLife
{
    public class Game
    {
        [JsonIgnore]
        [XmlIgnore]
        private static Game _instance = null;

        /// <summary>
        /// Singleton instance for this class.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public static Game Instance
        {
            get
            {
                // Check if we already have a GameController instance.
                if (Game._instance == null)
                {
                    // If not, create a new one and set it.
                    Game._instance = new Game();
                }

                // Return the GameController instance.
                return Game._instance;
            }

            protected set
            {
                Game._instance = value;
            }
        }

        public static bool HasStarted { get; private set; }

        /// <summary>
        /// Current date and time.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Player Person object.
        /// </summary>
        public Player Player { get; set; }
        
        public City City { get; set; }

        public PopulationController PopulationController { get; set; }
        
        public ActionController ActionController { get; set; }
        
        public PropController PropController { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public WindowController WindowController { get; set; }
        
        public ResourceController ResourceController { get; set; }
        
        public DialogController DialogController { get; set; }
        
        public AIController AIController { get; set; }

        /// <summary>
        /// General game settings.
        /// </summary>
        public Settings Settings { get; set; }

        /// <summary>
        /// Singleton constructor.
        /// </summary>
        private Game()
        {
            this.PopulationController = new PopulationController();
            this.ActionController = new ActionController();
            this.PropController = new PropController();
            this.WindowController = new WindowController();
            this.ResourceController = new ResourceController();
            this.DialogController = new DialogController();
            this.AIController = new AIController();
        }

        /// <summary>
        /// Initializes the GameController instance.
        /// </summary>
        public void Initialize(string cityName)
        {
            // 12:00.00 PM, January 1, 2000
            this.Date = new DateTime(2000, 1, 1, 12, 0, 0);

            // Get a temporary City object.
            this.City = new City();
            this.City.DisplayName = cityName;

            // Initialize what the City requires.
            this.ActionController.Initialize();
            this.PropController.Initialize();

            // Initialize the City.
            this.City = City.LoadXml(cityName);
            this.City.CreateNavMap();

            // Initialize what requires the City.
            this.PopulationController.Initialize();
            this.DialogController.Initialize();

            // Create the Player.
            // Requires the PersonController.
            this.Player = new Player();
            this.Player.Physique.Sex = Sexes.Male;
            this.Player.FirstName = "Player";
            this.Player.LastName = "Character";
            this.Player.Image = @"Male\Male_0.png";
            this.Player.Inventory.Add(this.PropController.CreateProp("Cellphone"));

            // Generate the population.
            // Requires the PersonController.
            this.PopulationController.GeneratePopulation().ForEach(e => e.Location = MiscUtilities.GetRandomEntry<Location>(Location.GetAll(this.City)));

            this.PopulationController.People.RemoveRange(1, this.PopulationController.People.Count - 2);

            this.PopulationController.GeneratePopulation().ForEach(e => e.Location = Location.Get("Home.Bedroom"));

            // Initialize the AI.
            this.AIController.Initialize();

            // Display all of the windows.
            // Requires the Player.
            this.WindowController.Initialize();

            // Set the player's initial position.
            // Requires the WindowController.
            Player.MoveToLocation(Location.Get("Home.Bedroom"), false);

            //Action.Get("Bed.Sleep").Performed += (sender, e) => Debug.WriteLine(e.Doer.Name + " slept in the bed.");
            this.Player.Stats.GetItem("Energy").FuzzyExample();

            // Synchronize everything.
            // Requires the Player and the WindowController.
            this.Synchronize();
        }

        /// <summary>
        /// Start the game.
        /// </summary>
        public static void Run(string cityName)
        {
            // Don't restart the game.
            if(!Game.HasStarted)
            {
                // Make this function idempotent.
                Game.HasStarted = true;
                
                //HLife.App app = new HLife.App();
                //app.InitializeComponent();

                // Get the main game window and add it to the WindowController.
                Game.Instance.WindowController.Windows.Add(MainWindow.Instance);

                // Initialize the game.
                Game.Instance.Initialize(cityName);

                // Start the main window.
                //Application.Run(main);
                //app.Run();
            }
        }

        public void Update()
        {
            this.AIController.Update();

            Debug.WriteLine("Threaded: " + this.AIController.ThreadTime);

            this.Synchronize();

            this.WindowController.Update();
        }

        public void Output(string message, bool skipEndLines = false)
        {
            //WindowController.Get<OutputWindow>().output.AppendText(message);

            if (!skipEndLines)
            {
                //WindowController.Get<OutputWindow>().output.AppendText("\n\n");
            }
        }

        public void Synchronize()
        {
            Label lblDate = (Label)LogicalTreeHelper.FindLogicalNode(WindowController.Get<MainWindow>(), "lbl_Date");
            lblDate.Content = this.Date.ToString("MMMM dd, hh:mm:ss");

            // Repopulate the occupant and inventory panels.
            Game.Instance.PropController.PopulatePropList();
            Game.Instance.PopulationController.PopulateOccupantList();
        }

        public void MoveTime(int seconds)
        {
            this.Date = this.Date.AddSeconds(seconds);
        }

        public static void Load(Game newGame)
        {
            Game.Instance = newGame;
        }
    }
}
