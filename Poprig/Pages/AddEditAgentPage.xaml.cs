using Microsoft.Win32;
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

namespace Poprig.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditAgentPage.xaml
    /// </summary>
    public partial class AddEditAgentPage : Page
    {
        Agent agent = null;
        private int numPage = 1;
        private bool isEdit = false;
        public AddEditAgentPage(Agent SelectAgent, int numPage)
        {
            InitializeComponent();
            this.numPage=numPage;
            if (SelectAgent!=null)
            {
                agent = SelectAgent;
                ShowDataAgent();
                isEdit = true;
                TypeBox.SelectedIndex = (int)agent.TypeID - 1;
            }
            TypeBox.SelectedValuePath = "ID";
            TypeBox.DisplayMemberPath = "Title";
            TypeBox.ItemsSource = MyDB_41_Derbin_2Entities.GetContext().TypeAgent.ToList();
            if (agent==null)
            {
                TypeBox.SelectedIndex = 0;
            }
        }
        private void ShowDataAgent()
        {
            TitleBox.Text = agent.Title;
            PrioritetBox.Text = agent.Prioritet.ToString();
            AdresBox.Text = agent.UrAdres;
            LogoBox.Source = new BitmapImage(new Uri(agent.AllPathLogo, UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };

            DirectorBox.Text = agent.ContactPerson;
            INNBox.Text = agent.INN;
            KPPBox.Text = agent.KPP;
            PhoneBox.Text = agent.Phone;
            EmailBox.Text = agent.Email;

        }
        private void SaveResult(object sender, RoutedEventArgs e)
        {
            agent = new Agent();
            agent.Title = TitleBox.Text;
            agent.TypeID = (int)TypeBox.SelectedValue;
            
            agent.Prioritet = PrioritetBox.Text == ""? 0: int.Parse(PrioritetBox.Text);
            agent.UrAdres = AdresBox.Text;
            agent.ContactPerson = DirectorBox.Text;
            agent.INN = INNBox.Text;
            agent.KPP = KPPBox.Text;
            agent.Phone = PhoneBox.Text;
            agent.Email = EmailBox.Text;

            try
            {
                if (isEdit == false)
                {
                    MyDB_41_Derbin_2Entities.Context.Agent.Add(agent);
                }

                MyDB_41_Derbin_2Entities.Context.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
            MainWindow.FrameMainWindow.Content = new Pages.ListAgentPage(numPage);
        }
        private void DeleteResult(object sender, RoutedEventArgs e)
        {

            if (agent!=null)
            {
                MyDB_41_Derbin_2Entities.Context.Agent.Remove(agent);
            }
            MyDB_41_Derbin_2Entities.Context.SaveChanges();
            MainWindow.FrameMainWindow.Content=new Pages.ListAgentPage(numPage);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextInputInt(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void AddLogo(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofdPicture = new OpenFileDialog();
            ofdPicture.Filter =
            "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*";
            ofdPicture.FilterIndex = 1;

            if (ofdPicture.ShowDialog() == true)
            {
                LogoBox.Source = new BitmapImage(new Uri(ofdPicture.FileName));
                agent.AllPathLogo = ofdPicture.FileName;
            }
        }

        private void NavigateBack(object sender, RoutedEventArgs e)
        {
            MainWindow.FrameMainWindow.Content = new Pages.ListAgentPage(numPage);
        }

    }
}
