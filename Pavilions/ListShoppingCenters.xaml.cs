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
using Pavilions.Classes;

namespace Pavilions
{
	/// <summary>
	/// Логика взаимодействия для ListShoppingCenters.xaml
	/// </summary>
	public partial class ListShoppingCenters : Page
	{
		public ListShoppingCenters()
		{
			InitializeComponent();
			CenterPavilionsContext db = new();
			ListSC.ItemsSource = db.ShoppingCenters.ToList();
			foreach (StatusList status in db.StatusLists)
			{
				StatusBox.Items.Add(status.StatusCenter);
			}
			foreach (CityList city in db.CityLists)
			{
				CityBox.Items.Add(city.City);
			}
		}

		private void OpenChangeInfo(object sender, DataGridBeginningEditEventArgs e)
		{
			ShoppingCenter center = e.Row.Item as ShoppingCenter;
			Manager.cMenu.MainFrame.Navigate(new ShowInfoSC(center));
		}

		private void AddSC(object sender, RoutedEventArgs e)
		{
			Manager.cMenu.MainFrame.Navigate(new ShowInfoSC());
		}

		private void DeleteSC(object sender, RoutedEventArgs e)
		{
			if (ListSC.SelectedIndex == -1)
			{
				MessageBox.Show("Не выбраны элементы для удаления");
			}
			else
			{
				MessageBoxResult result = MessageBox.Show("Вы точно хотите удалить эти элементы?", "Подтверждение", MessageBoxButton.YesNo);
				if (result == MessageBoxResult.Yes)
				{
					CenterPavilionsContext db = new();
					foreach (var item in ListSC.SelectedItems)
					{
						ShoppingCenter center = (ShoppingCenter)item;
						db.ShoppingCenters.Remove(center);
					}
					db.SaveChanges();
					ListSC.ItemsSource = db.ShoppingCenters.ToList();
				}
			}
		}

		private void FilterStatus(object sender, SelectionChangedEventArgs e)
		{
			CenterPavilionsContext db = new();
			ListSC.ItemsSource = db.ShoppingCenters.Where(p => p.StatusCenter == StatusBox.SelectedItem.ToString()).ToList();
		}

		private void FilterCity(object sender, SelectionChangedEventArgs e)
		{
			CenterPavilionsContext db = new();
			ListSC.ItemsSource = db.ShoppingCenters.Where(p => p.City == CityBox.SelectedItem.ToString()).Where(p => p.StatusCenter != "Удален").ToList();
		}
	}
}
