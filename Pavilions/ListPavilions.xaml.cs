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
using Pavilions.Classes;

namespace Pavilions
{
	/// <summary>
	/// Логика взаимодействия для ListPavilions.xaml
	/// </summary>
	public partial class ListPavilions : Page
	{
		public int IdShop { get; set; }
		public ListPavilions(int id)
		{
			InitializeComponent();
			IdShop = id;
			CenterPavilionsContext db = new();
			DataPavilions.ItemsSource = db.PavilionsCentersLists.Where(p => p.IdShopCenter == id).ToList();
		}

		private void OpenChangeInfo(object sender, DataGridBeginningEditEventArgs e)
		{
			CenterPavilionsContext db = new();
			PavilionsCentersList row = (PavilionsCentersList)e.Row.Item;
			Pavilion pavilion = db.Pavilions.Find(row.IdPavilion);
			Manager.cMenu.MainFrame.Navigate(new ShowInfoPavilion(pavilion));
		}

		private void DeletePavilion(object sender, RoutedEventArgs e)
		{
			if (DataPavilions.SelectedIndex == -1)
			{
				MessageBox.Show("Не выбраны элементы для удаления");
			}
			else
			{
				MessageBoxResult result = MessageBox.Show("Вы точно хотите удалить эти элементы?", "Подтверждение", MessageBoxButton.YesNo);
				if (result == MessageBoxResult.Yes)
				{
					CenterPavilionsContext db = new();
					foreach (var item in DataPavilions.SelectedItems)
					{
						PavilionsCentersList row = (PavilionsCentersList)item;
						Pavilion pavilion = db.Pavilions.Find(row.IdPavilion);
						pavilion.StatusPavilion = "Удален";
					}
					try
					{
						db.SaveChanges();
					}
					catch
					{
						MessageBox.Show("Нельзя удалить/отредактровать павильон со статусом 'Арендован' или 'Забронирован'");
					}
					DataPavilions.ItemsSource = db.PavilionsCentersLists.Where(p => p.IdShopCenter == IdShop).ToList();
				}
			}
		}

		private void AddPavilion(object sender, RoutedEventArgs e)
		{
			Manager.cMenu.MainFrame.Navigate(new ShowInfoPavilion(IdShop));
		}

		private void FilterFloor(object sender, TextChangedEventArgs e)
		{
			CenterPavilionsContext db = new();
			if (Regex.IsMatch(FloorBox.Text, @"^\d+$"))
			DataPavilions.ItemsSource = db.PavilionsCentersLists.Where(p => p.IdShopCenter == IdShop).Where(p =>
			p.NumberFloor == int.Parse(FloorBox.Text)).ToList();
		}

		private void FilterStatus(object sender, SelectionChangedEventArgs e)
		{
			CenterPavilionsContext db = new();
			MessageBox.Show(StatusBox.Text);
			TextBlock block = (TextBlock)StatusBox.SelectedItem;
			DataPavilions.ItemsSource = db.PavilionsCentersLists.Where(p => p.IdShopCenter == IdShop).Where(
				p => p.StatusPavilion == block.Text).ToList();
		}
	}
}
