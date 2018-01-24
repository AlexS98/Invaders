using System.Windows;

namespace Invaders
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
