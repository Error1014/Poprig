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
        public ListAgentPage()
        {
            InitializeComponent();
            var dataAgent = MyDB_41_Derbin_2Entities.GetContext().Agent.ToList();
            foreach (var item in dataAgent)
            {
                agents.Add(item);
            }
            valuePages = agents.Count/ valueAgentInPage + (agents.Count% valueAgentInPage == 0 ? 0:1);//считаем количество страниц
            ///Продажи за год
            foreach (var item in agents)
            {
                item.LogoPath = "/Resources/AgentLogo/"+item.LogoPath;
                int? d = MyDB_41_Derbin_2Entities.GetContext().Sales
                    .Where(x=>x.AgentID==item.ID)
                    .Sum(s=>s.Value);
                item.SalesOnYear = (int)(d == null ? 0 : d);
            }
            GetSkidka();
            GetPartList();
            ListViewAgent.ItemsSource = GetValueDataPage();
            UpdateNumPagesNavigate();
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
                if (item.SalesOnYear<10000)
                {
                    item.Skidka = 0;
                }
                else if (item.SalesOnYear < 50000)
                {
                    item.Skidka = 5;
                }
                else if (item.SalesOnYear < 150000)
                {
                    item.Skidka = 10;
                }
                else if (item.SalesOnYear < 500000)
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



        private void ClickNewList(object sender, MouseButtonEventArgs e)
        {
            Run run = sender as Run;
            if (run.Text == "<")
            {
                if (numPages > 1)
                {
                    numPages--;
                }
            }
            else if (run.Text == ">")
            {
                if (numPages < valuePages)
                {
                    numPages++;
                }
            }
            else
            {
                numPages = int.Parse(run.Text);
            }
            ListViewAgent.ItemsSource = GetValueDataPage();
            UpdateNumPagesNavigate();

        }

        private void UpdateNumPagesNavigate()
        {
            List<Run> runs = new List<Run>();
            runs.Add(NumPage1);
            runs.Add(NumPage2);
            runs.Add(NumPage3);
            runs.Add(NumPage4);
            if (numPages +3 < valuePages)
            {
                for (int i = 0; i < runs.Count; i++)
                {
                    runs[i].Text = (numPages+i).ToString();
                    if (runs[i].Text==numPages.ToString())
                    {
                        runs[i].TextDecorations = TextDecorations.Underline;
                    }
                    else
                    {
                        runs[i].TextDecorations = null;
                    }
                }
            }
            else
            {
                for (int i = 0; i < runs.Count; i++)
                {
                    runs[i].Text = (valuePages -(3 - i)).ToString();
                    if (runs[i].Text == numPages.ToString())
                    {
                        runs[i].TextDecorations = TextDecorations.Underline;
                    }
                    else
                    {
                        runs[i].TextDecorations = null;
                    }
                }
            }


        }
    }
}
