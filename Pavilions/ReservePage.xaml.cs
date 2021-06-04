using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
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
using Microsoft.EntityFrameworkCore;
using Pavilions.Classes;

namespace Pavilions
{
	/// <summary>
	/// Логика взаимодействия для ReservePage.xaml
	/// </summary>
	public partial class ReservePage : Page
	{
		public bool IsPlan = false;
		public int IdShop { get; set; }
		public ReservePage()
		{
			InitializeComponent();
			CenterPavilionsContext db = new();
			foreach (Renter renter in db.Renters)
			{
				RenterList.Items.Add(renter.NameRenter);
			}
			var centers = db.ShoppingCenters.Where(p => p.StatusCenter != "Удален").Select(p => p.NameCenter);
			foreach (var item in centers.ToList())
			{
				CentersList.Items.Add(item.ToString());
			}
		}

		private void CreatePavilionsList(object sender, SelectionChangedEventArgs e)
		{
			CenterPavilionsContext db = new();
			var ShopCenter = db.ShoppingCenters.Where(p => p.NameCenter == CentersList.SelectedItem.ToString()).Select(p => new { p.IdShopCenter, p.StatusCenter });
			var StringShop = ShopCenter.FirstOrDefault();
			if (StringShop.StatusCenter == "План")
			{
				IsPlan = true;
				ReserveButton.Content = "Забронировать павильон";
			}
			else
			{
				IsPlan = false;
				ReserveButton.Content = "Арендовать павильон";
			}
			IdShop = StringShop.IdShopCenter;
			PavilionsList.Items.Clear();
			var pavilions = db.Pavilions.Where(p => p.IdShopCenter == StringShop.IdShopCenter).Where(p
				=> p.StatusPavilion == "Свободен").Select(p => p.NumberPavilion);
			foreach (var name in pavilions)
			{
				PavilionsList.Items.Add(name.ToString());
			}
		}

		private void ReservePavilion(object sender, RoutedEventArgs e)
		{
			bool AllCorrect = true;
			foreach (FrameworkElement element in FieldsPanel.Children)
			{
				if (element is ComboBox)
				{
					ComboBox box = (ComboBox)element;
					if (box.SelectedIndex == -1) AllCorrect = false;
				}
				if (element is DatePicker)
				{
					DatePicker date = (DatePicker)element;
					if (!Regex.IsMatch(date.Text, @"^\d\d\.\d\d\.\d\d\d\d$") || date.SelectedDate < DateTime.Now)
					{
						AllCorrect = false;
					}
				}
			}
			if (AllCorrect)
			{
				if (DateStart.SelectedDate > DateEnd.SelectedDate) AllCorrect = false;
				else
				{
					CenterPavilionsContext db = new();
					var renterSql = db.Renters.Where(p => p.NameRenter == RenterList.SelectedItem.ToString()).Select(p => new { p.IdRenter });
					var renter = renterSql.FirstOrDefault();
					SqlParameter idrenter = new ("@idrenter", renter.IdRenter);
					SqlParameter idstaffer = new ("@idstaffer", Manager.IdStaffer);
					SqlParameter startdate = new ("@startdate", DateStart.SelectedDate);
					SqlParameter enddate = new ("@enddate", DateEnd.SelectedDate);
					SqlParameter status;
					if (IsPlan) status = new("@status", "Забронирован");
					else status = new("@status", "Арендован");
					var pavilionSql = db.Pavilions.Where(p => p.NumberPavilion == PavilionsList.SelectedItem.ToString()).Where(p
						=> p.IdShopCenter == IdShop).Select(p => new { p.IdPavilion } );
					var pavilion = pavilionSql.FirstOrDefault();
					SqlParameter idpavilion = new ("@idpavilion", pavilion.IdPavilion);
					db.Database.ExecuteSqlRaw("ReservePavillion @idrenter, @idpavilion, @startdate, @enddate, @idstaffer, @status", idrenter, idpavilion, startdate,
						enddate, idstaffer, status);
					db.SaveChanges();
					if (IsPlan) MessageBox.Show("Павильон забронирован!");
					else MessageBox.Show("Павильон арендован!");
					Manager.cMenu.MainFrame.Navigate(new MenuC());
				}
			}
			if (!AllCorrect) MessageBox.Show("Неверные данные");
		}
	}
}
