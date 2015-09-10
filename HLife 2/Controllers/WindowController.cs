using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HLife_2
{
    public class WindowController
        : Controller
    {
        public List<Form> Windows { get; set; }

        public List<ToolStripItem> MenuWindows { get; set; }

        public WindowController()
        {
            Windows = new List<Form>();
            MenuWindows = new List<ToolStripItem>();
            
            this.Add<OutputWindow>();
            this.Add<MapWindow>();
        }

        public override void Initialize()
        {
            this.Add<PlayerInfoWindow>(Game.Instance.Player);
            WindowController.Get<PlayerInfoWindow>(Game.Instance.Player).UpdateWindow();

            this.ShowAll();

            ToolStrip menuStrip = (ToolStrip)WindowController.Get<Hlife2>().Controls.Find("menuStrip", true)[0];
            ToolStripMenuItem windowsStrip = ((ToolStripMenuItem)((ToolStripMenuItem)menuStrip.Items.Find("toolStrip_view", false)[0]).DropDownItems.Find("toolStrip_windows", false)[0]);

            ToolStripItem showAll = windowsStrip.DropDownItems.Add("Show All");
            showAll.Click += (sender, e) => ShowAll();

            ToolStripItem hideAll = windowsStrip.DropDownItems.Add("Hide All");
            hideAll.Click += (sender, e) => HideAll();

            foreach (Form form in this.Windows.Where(e => e.GetType() != typeof(Hlife2)))
            {
                ToolStripItem item = windowsStrip.DropDownItems.Add(form.Text);
                
                item.Click += (sender, e) => HandleMenuToggle(sender, form);

                ((ToolStripMenuItem)item).CheckState = CheckState.Checked;

                MenuWindows.Add(item);
            }
        }

        public override void Update()
        { }

        public void HandleMenuToggle(object sender, Form form)
        {
            if (!form.Visible)
            {
                ((ToolStripMenuItem)sender).CheckState = CheckState.Checked;

                form.Show();
                form.BringToFront();
            }
            else
            {
                ((ToolStripMenuItem)sender).CheckState = CheckState.Unchecked;

                form.Hide();
            }
        }

        /// <summary>
        /// Show all of the extra windows.
        /// </summary>
        public void ShowAll()
        {
            foreach (Form form in this.Windows.Where(e => e.GetType() != typeof(Hlife2)))
            {
                form.Show();
                form.BringToFront();
            }

            foreach (ToolStripItem item in this.MenuWindows)
            {
                ((ToolStripMenuItem)item).CheckState = CheckState.Checked;
            }
        }

        /// <summary>
        /// Hide all the extra windows.
        /// </summary>
        public void HideAll()
        {
            foreach (Form form in this.Windows.Where(e => e.GetType() != typeof(Hlife2)))
            {
                form.Hide();
            }

            foreach(ToolStripItem item in this.MenuWindows)
            {
                ((ToolStripMenuItem)item).CheckState = CheckState.Unchecked;
            }
        }

        public static T Get<T>() where T : Form
        {
            return (T)Game.Instance.WindowController.Windows.Find(e => e.GetType() == typeof(T));
        }

        public static T Get<T>(object tag) where T : Form
        {
            foreach (T form in Game.Instance.WindowController.Windows.Where(e => e.GetType() == typeof(T)))
            {
                if (form.Tag == tag)
                {
                    return form;
                }
            }

            return null;
        }

        /// <summary>
        /// Brings each of the windows to the front and then brings the final focal window to the front again.
        /// Used to recover all the windows if they fall behind external windows.
        /// </summary>
        /// <param name="focalForm">Which window should be focused at the end.</param>
        public void BringAllWindowsToFront(Form focalForm)
        {
            this.Windows.ForEach(e => e.BringToFront());

            focalForm.BringToFront();
        }

        /// <summary>
        /// Add a window by type.
        /// </summary>
        /// <typeparam name="T">Type of window to create.</typeparam>
        /// <param name="tag">Custom tag data to give the window.</param>
        /// <returns>New window.</returns>
        public T Add<T>(object tag = null) where T:Form
        {
            T newForm = (T)Activator.CreateInstance<T>();

            newForm.Tag = tag;

            this.Windows.Add(newForm);

            return newForm;
        }
    }
}
