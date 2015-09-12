using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace HLife
{
    public class PersonController
        : Controller
    {
        public List<Person> People { get; set; }

        public List<string> MaleNames { get; set; }

        public List<string> FemaleNames { get; set; }

        public List<string> LastNames { get; set; }

        public PersonController()
        {
            People = new List<Person>();
        }

        public override void Initialize()
        {
            this.MaleNames = FileUtilities.ReadFile(Game.Instance.ResourceController.BuildPath(@"\Resources\Names\Male.csv"));
            this.FemaleNames = FileUtilities.ReadFile(Game.Instance.ResourceController.BuildPath(@"\Resources\Names\Female.csv"));
            this.LastNames = FileUtilities.ReadFile(Game.Instance.ResourceController.BuildPath(@"\Resources\Names\Surname.csv"));
        }

        public override void Update()
        { }

        public Person GetPerson(Guid id)
        {
            foreach(Person per in this.People)
            {
                if(per.Id == id)
                {
                    return per;
                }
            }

            return null;
        }

        public Person GetPerson(string name)
        {
            foreach (Person per in this.People)
            {
                if (per.FirstName + " " + per.LastName == name)
                {
                    return this.GetPerson(per.Id);
                }
            }

            return null;
        }

        public List<Person> GetPeopleAtLocation(Location loc)
        {
            List<Person> atLoc = new List<Person>();

            foreach (Person per in this.People)
            {
                if (per.Location == loc)
                {
                    atLoc.Add(per);
                }
            }

            return atLoc;
        }

        public string GenerateFirstName(Sex sex)
        {
            if (sex == Sex.Male)
            {
                return this.MaleNames[MiscUtilities.Rand.Next(this.MaleNames.Count)];
            }
            else
            {
                return this.FemaleNames[MiscUtilities.Rand.Next(this.FemaleNames.Count)];
            }
        }

        public string GenerateLastName()
        {
            return this.LastNames[MiscUtilities.Rand.Next(this.LastNames.Count)];
        }

        public string GenerateFullName(Sex sex)
        {
            return this.GenerateFirstName(sex) + " " + this.GenerateLastName();
        }

        public Person GeneratePerson(Sex sex)
        {
            Person newPerson = new Person();
            newPerson.FirstName = this.GenerateFirstName(sex);
            newPerson.LastName = this.GenerateLastName();
            newPerson.Physique.Sex = sex;
            newPerson.Physique.Age = MiscUtilities.Rand.Next(18, 70);
            newPerson.Physique.EyeColor = MiscUtilities.GetRandomEnum<EyeColors>();
            newPerson.Physique.HairColor = MiscUtilities.GetRandomEnum<HairColors>();
            newPerson.Physique.HairLength = MiscUtilities.GetRandomEnum<HairLengths>();
            newPerson.Image = newPerson.Physique.Sex.ToString() + @"\" + newPerson.Physique.Sex.ToString() + "_0.png";

            return newPerson;
        }

        public Person GeneratePerson()
        {
            return this.GeneratePerson(MiscUtilities.GetRandomEnum<Sex>());
        }

        public List<Person> GeneratePopulation()
        {
            List<Person> population = new List<Person>();

            for (int i = 0; i < 10; i++)
            {
                population.Add(this.GeneratePerson());
            }

            return population;
        }

        public void PopulatePersonList()
        {
            WrapPanel wrap_Occupants = (WrapPanel)LogicalTreeHelper.FindLogicalNode(WindowController.Get<MainWindow>(), "wrap_Occupants");

            wrap_Occupants.Children.Clear();

            foreach (Person occupant in Game.Instance.PersonController.GetPeopleAtLocation(Game.Instance.Player.Location).FindAll(e => e != Game.Instance.Player))
            {
                GroupBox panel = new GroupBox();
                panel.Width = wrap_Occupants.Width - 40;
                panel.Height = 80;
                //panel.Margin = new Padding(10);
                panel.Header = occupant.Name;
                wrap_Occupants.Children.Add(panel);

                Image pb = new Image();
                pb.Width = panel.Width * .25;
                pb.Height = 50;
                pb.Stretch = System.Windows.Media.Stretch.Uniform;
                pb.Source = new BitmapImage(new Uri(
                    Game.Instance.ResourceController.BuildPath(@"\Assets\Images\Characters\BodyParts\" + 
                    (occupant.Physique.Sex == Sex.Futanari ? "Female" : occupant.Physique.Sex.ToString()) + 
                    @"Heads_Cropped\" + occupant.Physique.Sex.ToString()[0] + "_Head" + MiscUtilities.Rand.Next(100) + ".png")));
                pb.Margin = new Thickness(10, 20, 0, 0);
                /*
                pb.DoubleClick += (sender, e) =>
                {
                    Game.Instance.WindowController.Add<PersonInformationWindow>(occupant).Show();
                    WindowController.Get<PersonInformationWindow>(occupant).UpdateWindow();
                };
                */
                panel.Content = pb;

                // TODO: Fix this for WPF.
                /*
                ContextMenu menu = new ContextMenu();

                MenuItem actions = new MenuItem("Actions");

                menu.MenuItems.Add(actions);


                foreach (Action action in Action.GetAll("Person"))
                {
                    MenuItem displayAction = new MenuItem(action.DisplayName);

                    displayAction.Click += (sender, e) => Game.Instance.Player.HandlePropAction(Game.Instance.Player, null, action, occupant);

                    actions.MenuItems.Add(displayAction);
                }


                pb.ContextMenu = menu;

                ToolTip tooltip = new ToolTip();
                tooltip.ToolTipTitle = occupant.FirstName + " " + occupant.LastName;
                tooltip.SetToolTip(pb, "{PLACEHOLDER}");
                */
            }
        }
    }
}
