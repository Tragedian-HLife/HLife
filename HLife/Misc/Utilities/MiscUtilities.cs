using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace HLife
{
    /// <summary>
    /// Collection of random utilities.
    /// </summary>
    public static class MiscUtilities
    {
        /// <summary>
        /// Global random to avoid repeat values.
        /// </summary>
        public static Random Rand = new Random();

        /// <summary>
        /// Gets a random member of an enum.
        /// </summary>
        /// <typeparam name="T">Enum to pull from.</typeparam>
        /// <returns>Random member of T.</returns>
        public static T GetRandomEnum<T>()
        {
            Array values = Enum.GetValues(typeof(T));

            return (T)values.GetValue(MiscUtilities.Rand.Next(values.Length));
        }

        public static T GetRandomEntry<T>(List<T> list)
        {
            if (list.Count == 0)
            {
                return default(T);
            }

            return list[MiscUtilities.Rand.Next(list.Count)];
        }

        /// <summary>
        /// Perform a deep Copy of the object, using Json as a serialisation method.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T CloneJson<T>(this T source)
        {
            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            // TODO: Make threadsafe.

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source, new JsonSerializerSettings {
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                TypeNameHandling = TypeNameHandling.All,
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                //ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            }));
        }
        
        public static int NodalDistance(this Location start, Location end)
        {
            return start.PathfindTo(end).Count();
        }

        public static double TravelTime(this Location start, Location end)
        {
            double time = 0;

            List<Location> path = start.PathfindTo(end);

            if (path.Count > 0)
            {
                path.RemoveAt(0);

                foreach (Location node in path)
                {
                    time += start.Edges.First(e => e.Node == node.DisplayName).Cost;

                    start = node;
                }
            }

            return time;
        }
    }

    public static class IconHelper
    {
        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int x, int y, int width, int height, uint flags);

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);

        const int GWL_EXSTYLE           = -20;
        const int WS_EX_DLGMODALFRAME   = 0x0001;
        const int SWP_NOSIZE            = 0x0001;
        const int SWP_NOMOVE            = 0x0002;
        const int SWP_NOZORDER          = 0x0004;
        const int SWP_FRAMECHANGED      = 0x0020;
        const uint WM_SETICON           = 0x0080;

        public static void RemoveIcon(Window window)
        {
            // Get this window's handle
            IntPtr hwnd = new WindowInteropHelper(window).Handle;

            // Change the extended window style to not show a window icon
            int extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_DLGMODALFRAME);

            // Update the window's non-client area to reflect the changes
            SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED);
        }

        public static List<T> Clone<T>(this List<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
}
