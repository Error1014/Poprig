using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using WSUniversalLib.dll;

namespace Poprig.Pages
{
    /// <summary>
    /// Логика взаимодействия для CalculatorPage.xaml
    /// </summary>
    public partial class CalculatorPage : Page
    {
        private int productType;
        private int materialType;
        private int count; 
        private float width; 
        private float length;
        public CalculatorPage()
        {
            InitializeComponent();
        }

        private void IntInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Calculat(object sender, RoutedEventArgs e)
        {
            GetEnterDate();
            Calculation calculat = new Calculation();
            int value = calculat.GetQuantityForProduct(productType, materialType, count, width, length);
            if (value==-1)
            {
                MessageBox.Show("Вы ввели не верные данные, пожалуйста исправьте их");
            }
            else
            {
                MessageBox.Show("Необходимо " + value.ToString() + " единиц сырья");
            }
        }

        private void GetEnterDate()
        {
            productType  = int.Parse((TypeProductBox.SelectedIndex + 1).ToString());
            materialType = int.Parse((TypeMaterialBox.SelectedIndex + 1).ToString());
            count = int.Parse(CountBox.Text.ToString());
            width = int.Parse(WidthBox.Text.ToString());
            length = int.Parse(LengthBox.Text.ToString());

        }
    }
}
