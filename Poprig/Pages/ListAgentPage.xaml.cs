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
    /// Логика взаимодействия для ListAgentPage.xaml
    /// </summary>
    public partial class ListAgentPage : Page
    {
        private int numPages = 1;
        private int valuePages;
        private int valueAgentInPage = 10;
        private List<Agent> agents=new List<Agent>();
        List<Run> numerationPage = new List<Run>();
        public ListAgentPage(int numPage)
        {
            InitializeComponent();
            numPages= numPage;
            numerationPage.Add(NumPage1);
            numerationPage.Add(NumPage2);
            numerationPage.Add(NumPage3);
            numerationPage.Add(NumPage4);
            var dataAgent = MyDB_41_Derbin_2Entities.GetContext().Agent.ToList();
            foreach (var item in dataAgent)
            {
                agents.Add(item);
            }
            valuePages = agents.Count/ valueAgentInPage + (agents.Count% valueAgentInPage == 0 ? 0:1);
            ///Продажи за год
            foreach (var item in agents)
            {
                if (item.LogoPath!="")
                {
                    item.AllPathLogo = "/Resources/AgentLogo/" + item.LogoPath;
                }
                else
                {
                    item.AllPathLogo = "/Resources/picture.png";
                }
                int? d = MyDB_41_Derbin_2Entities.GetContext().Sales
                    .Where(x=>x.AgentID==item.ID )
                    .Sum(s=>s.Value);
                item.SalesOnYear = (int)(d == null ? 0 : d);
            }
            GetSkidka();
            GetPartList();
            UpdateSelectPage();


        }
        private void GetPartList()
        {
            List<Agent> agentsOnePage = new List<Agent>();
            int num = 0;
            for (int i = 0 + valueAgentInPage*(numPages-1); i < valueAgentInPage + valueAgentInPage * (numPages - 1); i++)
            {
                if (i < agents.Count)
                {
                    agentsOnePage.Add(agents[i]);
                }
            }
            ListViewAgent.ItemsSource = agentsOnePage;
        }

        private List<Agent> GetSkidka()
        {
            foreach (var item in agents)
            {
                int? sales = MyDB_41_Derbin_2Entities.GetContext().Sales
                    .Where(x => x.AgentID == item.ID)
                    .Sum(s => s.Value);
                sales = (int)(sales == null ? 0 : sales);
                if (sales < 10000)
                {
                    item.Skidka = 0;
                }
                else if (sales < 50000)
                {
                    item.Skidka = 5;
                }
                else if (sales < 150000)
                {
                    item.Skidka = 10;
                }
                else if (sales < 500000)
                {
                    item.Skidka = 20;
                }
                else
                {
                    item.Skidka = 25;
                }
            }
            return agents;
        }


        private void NavigateListAgentNumb(object sender, MouseButtonEventArgs e)
        {
            string text = (sender as Run).Text;
            int koef = numPages - int.Parse(text);
            numPages -= koef;
            UpdateSelectPage();
        }
        private void NavigateListAgentArow(object sender, MouseButtonEventArgs e)
        {
            string text = (sender as Run).Text;
            if (text == "<")
            {
                if (numPages > 1)
                {
                    numPages--;
                    if (int.Parse(numerationPage[0].Text) > 1)
                    {
                        foreach (var item in numerationPage)
                        {
                            item.Text = (int.Parse(item.Text) - 1).ToString();
                        }
                    }

                }
            }
            else
            {
                if (numPages < valuePages)
                {
                    numPages++;
                    if (int.Parse(numerationPage[3].Text) < valuePages)
                    {
                        foreach (var item in numerationPage)
                        {
                            item.Text = (int.Parse(item.Text) + 1).ToString();
                        }
                    }
                }
            }
            UpdateSelectPage();


        }

        private void UpdateSelectPage()
        {
            foreach (var item in numerationPage)
            {
                if (item.Text == numPages.ToString())
                {
                    item.TextDecorations = TextDecorations.Underline;
                }
                else
                {
                    item.TextDecorations = null;
                }
            }
            GetPartList();

        }


        private void AddNewAgent(object sender, RoutedEventArgs e)
        {
            MainWindow.FrameMainWindow.Content = new Pages.AddEditAgentPage(null, numPages);
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.FrameMainWindow.Content = new Pages.AddEditAgentPage((sender as Grid).DataContext as Agent, numPages);
        }

        private void WriteText(object sender, TextCompositionEventArgs e)
        {
            string text = (sender as TextBox).Text;
        }
    }
}
