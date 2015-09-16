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

namespace HLife
{
    public class Game
    {
        private static Game _instance = null;

        /// <summary>
        /// Singleton instance for this class.
        /// </summary>
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

        public PopulationController PersonController { get; set; }

        public ActionController ActionController { get; set; }

        public PropController PropController { get; set; }

        public WindowController WindowController { get; set; }

        public ResourceController ResourceController { get; set; }

        public DialogController DialogController { get; set; }

        /// <summary>
        /// General game settings.
        /// </summary>
        public Settings Settings { get; set; }

        /// <summary>
        /// Player Person object.
        /// </summary>
        public Player Player { get; set; }

        public City City { get; set; }

        /// <summary>
        /// Singleton constructor.
        /// </summary>
        private Game()
        {
            this.PersonController = new PopulationController();
            this.ActionController = new ActionController();
            this.PropController = new PropController();
            this.WindowController = new WindowController();
            this.ResourceController = new ResourceController();
            this.DialogController = new DialogController();
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
            this.PersonController.Initialize();
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
            this.PersonController.GeneratePopulation().ForEach(e => e.Location = Location.Get("Home.Bedroom"));

            // Display all of the windows.
            // Requires the Player.
            this.WindowController.Initialize();

            // Set the player's initial position.
            // Requires the WindowController.
            Player.Location = Location.Get("Home.Bedroom");

            Action.Get("Bed.Sleep").Performed += (sender, e) => Debug.WriteLine(e.Doer.Name + " slept in the bed.");

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
            this.PersonController.People.Where(e => e != Game.Instance.Player).ToList().ForEach(e => e.Update());
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

            this.Player.LoadPlayerStats();
        }

        public void MoveTime(int seconds)
        {
            this.Date = this.Date.AddSeconds(seconds);
        }
    }
}
