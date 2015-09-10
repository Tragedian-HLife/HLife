using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HLife_2
{
    public class ComboBoxWithValue
        : ComboBox
    {
        public new List<ComboBoxItem> Items { get; set; }

        public void Add(string text, object value)
        {
            this.Items.Add(new ComboBoxItem() { Text = text, Value = value });
        }

        public void AddRange(string[] texts, object[] values)
        {
            for (int i = 0; i < texts.Count(); i++)
            {
                this.Items.Add(new ComboBoxItem() { Text = texts[i], Value = values[i] });
            }
        }
    }
}
