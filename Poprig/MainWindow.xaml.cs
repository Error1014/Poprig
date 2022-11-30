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


namespace Poprig
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Frame FrameMainWindow;
        public MainWindow()
        {
            InitializeComponent();
            FrameMainWindow = MainFrame;
            FrameMainWindow.Content = new Pages.ListAgentPage(1);
        }

        private void NavigateAgentPage(object sender, RoutedEventArgs e)
        {
            FrameMainWindow.Content = new Pages.ListAgentPage(1);
        }
        private void NavigateCalculatorPage(object sender, RoutedEventArgs e)
        {
            FrameMainWindow.Content = new Pages.CalculatorPage();
        }
    }
}
