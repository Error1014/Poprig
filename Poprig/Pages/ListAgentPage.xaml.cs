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
        private List<List<Agent>> listAgentForPages = new List<List<Agent>>();
        List<Run> numerationPage = new List<Run>();
        public ListAgentPage()
        {
            InitializeComponent();
            numerationPage.Add(NumPage1);
            numerationPage.Add(NumPage2);
            numerationPage.Add(NumPage3);
            numerationPage.Add(NumPage4);
            var dataAgent = MyDB_41_Derbin_2Entities.GetContext().Agent.ToList();
            foreach (var item in dataAgent)
            {
                agents.Add(item);
            }
            valuePages = agents.Count/ valueAgentInPage + (agents.Count% valueAgentInPage == 0 ? 0:1);//считаем количество страниц
            ///Продажи за год
            foreach (var item in agents)
            {
                item.AllPathLogo = "/Resources/AgentLogo/"+item.LogoPath;
                int? d = MyDB_41_Derbin_2Entities.GetContext().Sales
                    .Where(x=>x.AgentID==item.ID /* && DateTime.UtcNow - x.Date.Value<TimeSpan.FromDays(365)*/)
                    .Sum(s=>s.Value);
                item.SalesOnYear = (int)(d == null ? 0 : d);
            }
            GetSkidka();
            GetPartList();
            ListViewAgent.ItemsSource = GetValueDataPage();
            //UpdateNumPagesNavigate();
            UpdateSelectPage();


        }
        private void GetPartList()
        {
            List<Agent> list = new List<Agent>();
            int num = 0;
            foreach (var item in agents)
            {
                if (num!=0 && num % valueAgentInPage==0)
                {
                    listAgentForPages.Add(list);
                    list = new List<Agent>();
                }
                list.Add(item);
                num++;
                if (num == agents.Count)
                {
                    listAgentForPages.Add(list);
                }
            }
        }
        private List<Agent> GetValueDataPage()
        {
            List<Agent> list = new List<Agent>();
            list = listAgentForPages[numPages - 1];
            return list;
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
        }


        private void AddNewAgenr(object sender, RoutedEventArgs e)
        {
            MainWindow.FrameMainWindow.Content = new Pages.AddEditAgentPage(null);
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.FrameMainWindow.Content = new Pages.AddEditAgentPage((sender as Grid).DataContext as Agent);
        }
    }
}
