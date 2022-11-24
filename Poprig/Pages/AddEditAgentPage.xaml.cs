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

namespace Poprig.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditAgentPage.xaml
    /// </summary>
    public partial class AddEditAgentPage : Page
    {
        Agent agent = new Agent();
        private bool isEdit = false;
        public AddEditAgentPage(Agent SelectAgent)
        {
            InitializeComponent();
            if (SelectAgent!=null)
            {
                agent = SelectAgent;
                ShowDataAgent();
                isEdit = true;
            }
        }
        private void ShowDataAgent()
        {
            TitleBox.Text = agent.Title;
            PrioritetBox.Text = agent.Prioritet.ToString();
            AdresBox.Text = agent.UrAdres;
            DirectorBox.Text = agent.ContactPerson;
            INNBox.Text = agent.INN;
            KPPBox.Text = agent.KPP;
            PhoneBox.Text = agent.Phone;
            EmailBox.Text = agent.Email;

        }
        private void SaveResult(object sender, RoutedEventArgs e)
        {
            
            agent.Title = TitleBox.Text;
            //agent.TypeAgent
            //agent.Logo
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
            MainWindow.FrameMainWindow.Content = new Pages.ListAgentPage();
        }
        private void DeleteResult(object sender, RoutedEventArgs e)
        {

            if (agent!=null)
            {
                MyDB_41_Derbin_2Entities.Context.Agent.Remove(agent);
            }
            MyDB_41_Derbin_2Entities.Context.SaveChanges();
            MainWindow.FrameMainWindow.Content=new Pages.ListAgentPage();

        }

        private void TextInputInt(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789 ,".IndexOf(e.Text) < 0;
        }
    
    }
}
