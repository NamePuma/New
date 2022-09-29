using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Npgsql;
using NpgsqlTypes;
using System.Windows.Threading;

namespace qwe
{
    public partial class MainWindow : Window
    {

          private Ban.Page3 page3;
          private Ban.Page4 page4;
          private Ban.Page5 page5;


        public MainWindow()
        {
            InitializeComponent();

            Strani.Navigate(new Ban.Page3());

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (page3 == null)
            {
                page3 = new Ban.Page3();
            }
     
            Strani.Navigate(page3);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(page4 == null)
            {
                page4 = new Ban.Page4();
            }

            Strani.Navigate(page4);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
            if(page5 == null)
            {
                page5 = new Ban.Page5();
            }
            Strani.Navigate(page5);
        }
    }
}

