using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HLife.GUI.Controls
{
    /// <summary>
    /// Interaction logic for PagingControl.xaml
    /// </summary>
    public partial class PagingControl 
        : UserControl
    {
        public object Data { get; set; }

        public int ItemsPerPage { get; set; }

        private int NumberOfPages { get; set; }

        private int CurrentPage { get; set; }

        public EventHandler Updated;

        public PagingControl()
        {
            InitializeComponent();
        }

        public List<T> SetData<T>(List<T> data, int itemsPerPage)
        {
            this.Data = data;
            this.ItemsPerPage = itemsPerPage;

            this.NumberOfPages = (int)Math.Ceiling((double)((List<T>)this.Data).Count / (double)this.ItemsPerPage);

            this.CurrentPage = 0;

            this.Update();

            return this.GetData<T>();
        }

        public void GoToPage(int page)
        {
            this.CurrentPage = page;

            this.Update();
        }

        public void Update()
        {
            this.lblPageIndex.Content = this.CurrentPage + 1;
            this.lblNumberOfPages.Content = this.NumberOfPages;

            this.btnNextPage.IsEnabled = this.CurrentPage < this.NumberOfPages - 1;
            this.btnLastPage.IsEnabled = this.CurrentPage < this.NumberOfPages - 1;

            this.btnPreviousPage.IsEnabled = this.CurrentPage > 0;
            this.btnFirstPage.IsEnabled = this.CurrentPage > 0;

            if (Updated != null)
            {
                Updated(this, null);
            }
        }

        public List<T> GetData<T>()
        {
            return ((List<T>)this.Data).GetRange(this.CurrentPage * this.ItemsPerPage, Math.Min(this.ItemsPerPage, ((List<T>)this.Data).Count - (this.CurrentPage * this.ItemsPerPage)));
        }

        private void btnLastPage_Click(object sender, RoutedEventArgs e)
        {
            this.GoToPage(this.NumberOfPages - 1);
        }

        private void btnFirstPage_Click(object sender, RoutedEventArgs e)
        {
            this.GoToPage(0);
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            this.GoToPage(++this.CurrentPage);
        }

        private void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            this.GoToPage(--this.CurrentPage);
        }
    }
}
