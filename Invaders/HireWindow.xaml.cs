using Invaders.GameModels.Additional;
using Invaders.UIHelpers;
using System;
using System.Threading;
using System.Windows;

namespace Invaders
{
    /// <summary>
    /// Interaction logic for HireWindow.xaml
    /// </summary>
    public partial class HireWindow : Window
    {
        public event EventHandler<HireEventArgs> HireWarior;
        public HireWindow()
        {
            InitializeComponent();
        }
        int Choose = 0;
        private void HireKnight_Click(object sender, RoutedEventArgs e)
        {
            OnHireWarior(new HireEventArgs("HireWindow", "MainWindow", "Knight"));
            Close();
        }

        private void HireSwordsman_Click(object sender, RoutedEventArgs e)
        {
            OnHireWarior(new HireEventArgs("HireWindow", "MainWindow", "Swordsman"));
            Close();
        }

        private void HireBowman_Click(object sender, RoutedEventArgs e)
        {
            OnHireWarior(new HireEventArgs("HireWindow", "MainWindow", "Bowman"));
            Close();
        }

        protected virtual void OnHireWarior(HireEventArgs e)
        {
            e.Raise(this, ref HireWarior);
        }
    }
}
