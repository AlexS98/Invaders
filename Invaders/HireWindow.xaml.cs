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
using System.Windows.Shapes;

namespace Colonizators
{
    /// <summary>
    /// Interaction logic for HireWindow.xaml
    /// </summary>
    public partial class HireWindow : Window
    {
        public HireWindow()
        {
            InitializeComponent();
        }
        int Choose = 0;
        private void HireKnight_Click(object sender, RoutedEventArgs e)
        {
            Choose = 1;
            Data.Hire = Choose;
            this.Close();
        }

        private void HireSwordsman_Click(object sender, RoutedEventArgs e)
        {
            Choose = 2;
            Data.Hire = Choose;
            this.Close();
        }

        private void HireBowman_Click(object sender, RoutedEventArgs e)
        {
            Choose = 3;
            Data.Hire = Choose;
            this.Close();
        }
    }
}
