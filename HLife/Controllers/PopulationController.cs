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
    public class PopulationController
        : Controller
    {
        public List<Person> People { get; set; }

        public List<string> MaleNames { get; set; }

        public List<string> FemaleNames { get; set; }

        public List<string> LastNames { get; set; }

        public PopulationController()
        {
            People = new List<Person>();

            this.MaleNames = new List<string>();
            this.FemaleNames = new List<string>();
            this.LastNames = new List<string>();
        }

        public override void Initialize()
        {
            foreach (Mod mod in ModController.GetModsByType("Names"))
            {
                this.MaleNames.AddRange(FileUtilities.ReadFile(mod.Directory + @"\Names\Male.csv"));
                this.FemaleNames.AddRange(FileUtilities.ReadFile(mod.Directory + @"\Names\Female.csv"));
                this.LastNames.AddRange(FileUtilities.ReadFile(mod.Directory + @"\Names\Surname.csv"));
            }
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

        public string GenerateFirstName(Sexes sex)
        {
            if (sex == Sexes.Male)
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

        public string GenerateFullName(Sexes sex)
        {
            return this.GenerateFirstName(sex) + " " + this.GenerateLastName();
        }

        public Person GeneratePerson(Sexes sex)
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
            newPerson.HeadImage = new BitmapImage(new Uri(
                    Game.Instance.ResourceController.BuildPath(@"\Assets\Images\Characters\BodyParts\" +
                    (newPerson.Physique.Sex == Sexes.Futanari ? "Female" : newPerson.Physique.Sex.ToString()) +
                    @"Heads_Cropped\" + newPerson.Physique.Sex.ToString()[0] + "_Head" + MiscUtilities.Rand.Next(100) + ".png")));

            return newPerson;
        }

        public Person GeneratePerson()
        {
            return this.GeneratePerson(MiscUtilities.GetRandomEnum<Sexes>());
        }

        public List<Person> GenerateFamily()
        {
            List<Person> family = new List<Person>();

            // Get a family surname.
            string familyName = this.GenerateLastName();

            // Retrieve the family size settings.
            int minFamilySize = 1;
            int maxFamilySize = 4;

            // Get a random family size between our bounds.
            int familySize = MiscUtilities.Rand.Next(minFamilySize, maxFamilySize + 1);

            // If it's a family of one, it should be an adult; a child can't live by itself.
            if (familySize == 1)
            {
                // Create a randomly-sexed person.
                Person adult = this.GeneratePerson();

                // Make them an adult.
                adult.Physique.Age = MiscUtilities.Rand.Next(18, 100);

                family.Add(adult);
            }
            else
            {
                // Get the number of adults in this family.
                // We need at least one adult since a family of minors can't exist.
                // We can also have a family of just adults, no problem.
                int numAdults = MiscUtilities.Rand.Next(1, familySize + 1);

                // Naturally, the remaining family size is reserved for children.
                int numChildren = familySize - numAdults;

                // Create the adults...
                for (int i = 0; i < numAdults; i++)
                {
                    // Create a randomly-sexed person.
                    Person adult = this.GeneratePerson();

                    // Make them an adult.
                    adult.Physique.Age = MiscUtilities.Rand.Next(18, 100);

                    family.Add(adult);
                }

                // Create the children...
                for (int i = 0; i < numChildren; i++)
                {
                    // Create a randomly-sexed person.
                    Person child = this.GeneratePerson();

                    // Make them a minor.
                    child.Physique.Age = MiscUtilities.Rand.Next(0, 18);

                    family.Add(child);
                }
            }

            family.ForEach(e => e.LastName = familyName);

            return family;
        }

        public List<Person> GeneratePopulation()
        {
            List<Person> population = new List<Person>();

            for (int i = 0; i < 500; i++)
            {
                population.AddRange(this.GenerateFamily());
            }

            return population;
        }

        public void PopulateOccupantList()
        {
            PagingControl pagination = (PagingControl)WindowController.Get<MainWindow>().FindName("pagination");
            pagination.Updated += (sender, e) => this.DrawOccupantList(pagination);
            pagination.SetData<Person>(Game.Instance.PopulationController.GetPeopleAtLocation(Game.Instance.Player.Location).FindAll(e => e != Game.Instance.Player), 25);
        }

        private void DrawOccupantList(PagingControl pagination)
        {
            int count = 0;
            
            WrapPanel wrap_Occupants = (WrapPanel)LogicalTreeHelper.FindLogicalNode(WindowController.Get<MainWindow>(), "wrap_Occupants");

            wrap_Occupants.Children.Clear();
            wrap_Occupants.Height = 0;

            foreach (Person occupant in pagination.GetData<Person>())
            {
                GroupBox panel = new GroupBox();
                panel.Width = wrap_Occupants.Width - 0;
                panel.Header = occupant.Name;
                wrap_Occupants.Children.Add(panel);

                Grid grid = new Grid();
                panel.Content = grid;

                StackPanel stack_Parent = new StackPanel();
                stack_Parent.Orientation = Orientation.Horizontal;
                grid.Children.Add(stack_Parent);

                Image pb = new Image();
                pb.Width = panel.Width * .25;
                pb.Height = 50;
                pb.Stretch = System.Windows.Media.Stretch.Uniform;
                pb.Source = occupant.HeadImage;
                pb.Margin = new Thickness(10, 20, 0, 0);
                pb.VerticalAlignment = VerticalAlignment.Top;
                pb.HorizontalAlignment = HorizontalAlignment.Left;

                pb.MouseLeftButtonDown += (sender2, e2) =>
                {
                    if (e2.ClickCount == 2)
                    {
                        Game.Instance.WindowController.Add<PersonInformationWindow>(occupant).Show();
                        WindowController.Get<PersonInformationWindow>(occupant).UpdateWindow();
                    }
                };

                stack_Parent.Children.Add(pb);



                StackPanel stack_Info = new StackPanel();
                stack_Parent.Children.Add(stack_Info);

                Label lbl_Sex = new Label();
                lbl_Sex.Content = "Apparent sex: " + (occupant.Physique.Sex == Sexes.Futanari ? Sexes.Female.ToString() : occupant.Physique.Sex.ToString());
                stack_Info.Children.Add(lbl_Sex);

                Label lbl_Age = new Label();
                lbl_Age.Content = "Apparent age: " + occupant.GetApparentAge().CaptializeWords();
                stack_Info.Children.Add(lbl_Age);




                ContextMenu menu = new ContextMenu();

                MenuItem actions = new MenuItem();
                actions.Header = "Actions";

                menu.Items.Add(actions);
                menu.Opened += (sender2, e2) =>
                {
                    foreach (Action action in Action.GetAll("Person"))
                    {
                        MenuItem displayAction = action.GetContextMenuItemPerson(new ActionEventArgs(Game.Instance.Player, occupant, null));

                        if (displayAction != null)
                        {
                            actions.Items.Add(displayAction);
                        }
                    }
                };

                pb.ContextMenu = menu;

                ToolTip tooltip = new ToolTip();
                tooltip.Content = occupant.FirstName + " " + occupant.LastName;
                pb.ToolTip = tooltip;

                count++;

                wrap_Occupants.Height += panel.Height;
            }
        }
    }
}
