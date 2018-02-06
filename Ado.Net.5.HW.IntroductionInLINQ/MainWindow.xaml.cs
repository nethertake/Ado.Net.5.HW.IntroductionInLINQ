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
using System.Data.SqlClient;
using System.Data;

namespace Ado.Net._5.HW.IntroductionInLINQ
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string _connectionString = @"Data Source = DESKTOP-PG10UGI\SQLEXPRESS; Initial Catalog = CRCMS_new; User Id = sa; Password = Mc123456";
        public MainWindow()
        {
            InitializeComponent();
            GetData();
        }

        public void GetData()
        {
            SqlConnection con = new SqlConnection(_connectionString);
            SqlDataAdapter da = new SqlDataAdapter("select * from Area", con);
            DataSet dataset = new DataSet();
            da.Fill(dataset);

            List<Area> areas = new List<Area>();
            foreach (DataRow row in dataset.Tables[0].Rows)
            {
                Area area = new Area();
                area.AreaId = Int32.Parse(row["AreaId"].ToString());
                area.TypeArea = Int32.Parse(row["TypeArea"].ToString());
                area.Name = row["Name"].ToString();
                area.ParentId = Int32.Parse(row["ParentId"].ToString());
                area.NoSplit = row["NoSplit"].ToString();
                area.AssemblyArea = row["AssemblyArea"].ToString();
                area.FullName = row["FullName"].ToString();
                area.MultipleOrders = row["MultipleOrders"].ToString();
                area.HiddenArea = row["HiddenArea"].ToString();
                area.IP = row["IP"].ToString();
                area.PavilionId = Int32.Parse(row["PavilionId"].ToString());
                area.TypeId = Int32.Parse(row["TypeId"].ToString());
                area.OrderExecution = Int32.Parse(row["OrderExecution"].ToString());
                area.Dependence = Int32.Parse(row["Dependence"].ToString());
                area.WorkingPeople = Int32.Parse(row["WorkingPeople"].ToString());
                area.ComponentTypeId = Int32.Parse(row["ComponentTypeId"].ToString());
                area.GroupId = Int32.Parse(row["GroupId"].ToString());
                area.Segment = row["Segment"].ToString();

                areas.Add(area);
            }
            ListViewArea.ItemsSource = areas;


            //задание a
            var query1 = areas.Where(n => n.TypeArea == 1).OrderBy(o => o.Name).Take(10).Select(s => new
            {
                s.Name,
                s.FullName,
                s.IP
            });


            //задание b
            var query2 = areas.Where(w => w.ParentId == 0).Select(s => new
                {         s.Name,
                          s.FullName,
                });

            //задание с

            int[] Pavilion = new[] { 1, 2, 3, 4, 5, 6 };
            var query4 = areas.Where(w => Pavilion.Where(b => b % 2 == 0).Contains(w.PavilionId)).Select(s => new
            {
                s.PavilionId,
                s.Name,
                s.FullName,
                s.IP
            });

            //задание e

            var query5 = from p in areas
                where p.WorkingPeople > 1
                let PavilionId = Pavilion
                select p;

       

        }

        static bool GetEven(int number)
        {
            if (number % 2 != 0) return false;
            else return true;
        }

    }

   
}
