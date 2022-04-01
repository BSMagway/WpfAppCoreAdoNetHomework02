using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
using WpfAppAdoNetHomework02.Model;

namespace WpfAppCoreAdoNetHomework02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal MusicShopContext musicShopContext;
        internal string connectionString;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void Connect()
        {
            musicShopContext = new MusicShopContext(connectionString, connectToDBSelector.SelectedIndex);

            musicShopContext.CompactDiscs.Load();
            musicShopContext.SingerOrGroupNames.Load();
            musicShopContext.Genres.Load();
            musicShopContext.CompactDiscOnWarehouses.Load();

            musicShopGrid.ItemsSource = musicShopContext.CompactDiscOnWarehouses.Local.ToBindingList();

            addSelector.IsEnabled = true;
            aggregateSelector.IsEnabled = true;
            storageProcedureSelector.IsEnabled = true;

        }

        private void connectToDB_Click(object sender, RoutedEventArgs e)
        {
            switch (connectToDBSelector.SelectedIndex)
            {
                case 0:
                    connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=C:\USERS\BS_MA\SOURCE\REPOS\WPFAPPCOREADONETHOMEWORK02\WPFAPPCOREADONETHOMEWORK02\MUSICSHOPCORE.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                    break;
                case 1:
                    break;
                case 2:
                    break;
                default:
                    break;
            }

            Connect();
        }

        private void connectToDBSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            connectToDB.IsEnabled = true;
        }

        private void addInDb_Click(object sender, RoutedEventArgs e)
        {
            switch (addSelector.SelectedIndex)
            {
                case 0:
                    AddSingerOrGroup();
                    break;
                case 1:
                    AddGenre();
                    break;
                case 2:
                    AddCompactDisc();
                    break;
                case 3:
                    AddCompactDiscInWarehouse();
                    break;
                default:
                    break;
            }
        }

        private void AddSingerOrGroup()
        {
            AddWord addSinger = new AddWord();
            addSinger.textBlock.Text = "Enter singer or group:";
            addSinger.ShowDialog();
            if (!string.IsNullOrEmpty(addSinger.textBox.Text))
            {
                SingerOrGroupName singerOrGroupName = new SingerOrGroupName(addSinger.textBox.Text);
                musicShopContext.SingerOrGroupNames.Add(singerOrGroupName);
                musicShopContext.SaveChanges();
            }
        }
        private void AddGenre()
        {
            AddWord addGenre = new AddWord();
            addGenre.textBlock.Text = "Enter genre:";
            addGenre.ShowDialog();
            if (!string.IsNullOrEmpty(addGenre.textBox.Text))
            {
                Genre genre = new Genre(addGenre.textBox.Text);
                musicShopContext.Genres.Add(genre);
                musicShopContext.SaveChanges();
            }
        }
        private void AddCompactDisc()
        {
            AddCompactDisc compactDiscAdd = new AddCompactDisc();

            var singers = musicShopContext.SingerOrGroupNames.Select(x => x.Name).ToList();

            foreach (var singerItem in singers)
            {
                compactDiscAdd.comboBoxSingerId.Items.Add(singerItem);
            }

            var genres = musicShopContext.Genres.Select(x => x.Name).ToList();

            foreach (var genreItem in genres)
            {
                compactDiscAdd.comboBoxGenreId.Items.Add(genreItem);
            }
            compactDiscAdd.ShowDialog();

            if (!string.IsNullOrEmpty(compactDiscAdd.textBoxAlbumName.Text) && compactDiscAdd.comboBoxSingerId.SelectedItem != null && compactDiscAdd.comboBoxGenreId.SelectedItem != null)
            {
                var singerId = musicShopContext.SingerOrGroupNames.First(y => y.Name == compactDiscAdd.comboBoxSingerId.SelectedItem.ToString());
                var genreId = musicShopContext.Genres.First(y => y.Name == compactDiscAdd.comboBoxGenreId.SelectedItem.ToString());
                CompactDisc genre = new CompactDisc(compactDiscAdd.textBoxAlbumName.Text, singerId.Id, genreId.Id);
                musicShopContext.CompactDiscs.Add(genre);
                musicShopContext.SaveChanges();
            }
        }
        private void AddCompactDiscInWarehouse()
        {
            AddCompactDiscInWarehouse compactDiscInWarehouse = new AddCompactDiscInWarehouse();

            var compactDiscs = musicShopContext.CompactDiscs.Select(x => x.AlbumName).ToList();

            foreach (var compactDiscItem in compactDiscs)
            {
                compactDiscInWarehouse.comboBoxCompactDiscId.Items.Add(compactDiscItem);
            }


            compactDiscInWarehouse.ShowDialog();

            if (compactDiscInWarehouse.comboBoxCompactDiscId.SelectedItem != null && !string.IsNullOrEmpty(compactDiscInWarehouse.textBoxQuantity.Text) && !string.IsNullOrEmpty(compactDiscInWarehouse.textBoxPrice.Text))
            {
                var compactDiscId = musicShopContext.CompactDiscs.First(y => y.AlbumName == compactDiscInWarehouse.comboBoxCompactDiscId.SelectedItem.ToString());

                CompactDiscOnWarehouse compactDiscAddWarehouse = new CompactDiscOnWarehouse(compactDiscId.Id, int.Parse(compactDiscInWarehouse.textBoxQuantity.Text), double.Parse(compactDiscInWarehouse.textBoxPrice.Text));
                musicShopContext.CompactDiscOnWarehouses.Add(compactDiscAddWarehouse);
                musicShopContext.SaveChanges();
            }


        }

        private void addSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            addInDb.IsEnabled = true;
        }

        private void aggregateButton_Click(object sender, RoutedEventArgs e)
        {
            switch (aggregateSelector.SelectedIndex)
            {
                case 0:
                    aggregateResult.Text = musicShopContext.CompactDiscOnWarehouses.Sum(x => x.Quantity).ToString();
                    break;
                case 1:
                    aggregateResult.Text = musicShopContext.CompactDiscOnWarehouses.Average(x => x.Price).ToString();
                    break;
                case 2:
                    aggregateResult.Text = musicShopContext.SingerOrGroupNames.Count().ToString();
                    break;
                default:
                    break;
            }
        }

        private void aggregateSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            aggregateButton.IsEnabled = true;
        }

        private void storageProcedureButton_Click(object sender, RoutedEventArgs e)
        {
            SqlParameter param = new SqlParameter();

            switch (storageProcedureSelector.SelectedIndex)
            {
                case 0:
                    param.ParameterName = "@param1";
                    param.SqlDbType = System.Data.SqlDbType.Int;
                    param.Direction = System.Data.ParameterDirection.Output;
                    musicShopContext.Database.ExecuteSqlRaw("GetSumDisc @param1 OUT", param);
                    storageProcedureResult.Text = param.Value.ToString();
                    break;
                case 1:
                    param.ParameterName = "@param1";
                    param.SqlDbType = System.Data.SqlDbType.Float;
                    param.Direction = System.Data.ParameterDirection.Output;
                    musicShopContext.Database.ExecuteSqlRaw("GetMaxPrice @param1 OUT", param);
                    storageProcedureResult.Text = param.Value.ToString();
                    break;
                case 2:
                    param.ParameterName = "@param1";
                    param.SqlDbType = System.Data.SqlDbType.Float;
                    param.Direction = System.Data.ParameterDirection.Output;
                    musicShopContext.Database.ExecuteSqlRaw("GetFullPrice @param1 OUT", param);
                    storageProcedureResult.Text = param.Value.ToString();
                    break;
                default:
                    break;
            }
        }

        private void storageProcedureSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            storageProcedureButton.IsEnabled = true;
        }


    }

}
