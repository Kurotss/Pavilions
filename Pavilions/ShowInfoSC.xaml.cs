using System;
using System.Collections.Generic;
using System.Drawing;
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
using Microsoft.Win32;
using Pavilions.Classes;

namespace Pavilions
{
	/// <summary>
	/// Логика взаимодействия для ShowInfoSC.xaml
	/// </summary>
	public partial class ShowInfoSC : Page
	{
		public int IsAdd = 0;
		public int IdSC { get; set; }
		public string NameCenter { get; set; }
		public double KdsCenter { get; set; }
		public int CostCenter { get; set; }
		public string CityCenter { get; set; }
		public int FloorsCenter { get; set; }
		public int CountCenter { get; set; }
		public byte[] CenterBytes { get; set; }
		public ShowInfoSC(ShoppingCenter center)
		{
			InitializeComponent();
			this.IdSC = center.IdShopCenter;
			this.NameCenter = center.NameCenter;
			this.KdsCenter = (double)center.ValAddRatio;
			this.CostCenter = center.CostOfConstruction;
			this.CityCenter = center.City;
			this.FloorsCenter = center.Floors;
			this.CountCenter = center.CountOfPavilions;
			this.CenterBytes = center.ImageCenter;
			StatusInput.Text = center.StatusCenter;
			this.DataContext = this;
			Picture.Source = Manager.CreateSource(center.ImageCenter);
		}

		public ShowInfoSC()
		{
			InitializeComponent();
			IsAdd = 1;
		}

		private void LoadImage(object sender, RoutedEventArgs e)
		{
			OpenFileDialog dialog = new() { Filter = "Image files(*.png) |*.png" };
			if ((bool)dialog.ShowDialog())
			{
				System.Drawing.Image image = System.Drawing.Image.FromFile(dialog.FileName);
				ImageConverter converter = new();
				CenterBytes = (byte[])converter.ConvertTo(image, typeof(byte[]));
				Picture.Source = Manager.CreateSource(CenterBytes);
			}
		}

		private void NewCenter(object sender, RoutedEventArgs e)
		{
			bool AllCorrect = true;
			foreach (FrameworkElement element in FieldsInput.Children)
			{
				if (element is TextBox)
				{
					if (element.Name == "NameCenterInput")
					{
						if (!Regex.IsMatch(NameCenterInput.Text, @"^[а-яА-Яa-zA-Z0-9]+") || NameCenterInput.Text.Length < 2)
						{
							AllCorrect = false;
						}
					}
					else if (element.Name == "CityInput")
					{
						if (!Regex.IsMatch(CityInput.Text, @"^[а-яА-Я]+") || CityInput.Text.Length < 2)
						{
							AllCorrect = false;
						}
					}
					else if (element.Name == "KdsInput")
					{
						if (!Regex.IsMatch(KdsInput.Text, @"^\d+\,\d+") && !Regex.IsMatch(KdsInput.Text, @"^\d+"))
						{
							AllCorrect = false;
						}
					}
					else
					{
						TextBox textBox = (TextBox)element;
						if (!Regex.IsMatch(textBox.Text, @"^\d+$")) AllCorrect = false;
					}
				}
			}
			if (StatusInput.SelectedIndex == -1) AllCorrect = false;
			if (IsAdd == 1)
			{
				if (CenterBytes is null) AllCorrect = false;
			}
			if (AllCorrect)
			{
				CenterPavilionsContext db = new();
				ShoppingCenter center = new();
				if (IsAdd == 0) center = db.ShoppingCenters.Find(IdSC);
				center.NameCenter = NameCenterInput.Text;
				center.ValAddRatio = double.Parse(KdsInput.Text);
				center.CostOfConstruction = int.Parse(CostInput.Text);
				center.City = CityInput.Text;
				center.Floors = int.Parse(FloorsInput.Text);
				center.CountOfPavilions = int.Parse(CountInput.Text);
				center.ImageCenter = CenterBytes;
				center.StatusCenter = StatusInput.Text;
				if (IsAdd == 1) db.ShoppingCenters.Add(center);
				try
				{
					db.SaveChanges();
					if (IsAdd == 0) MessageBox.Show("Данные обновлены!");
					else MessageBox.Show("Данные добавлены!");
				}
				catch
				{
					MessageBox.Show("Нельзя установить статус план у того ТЦ, где есть забронированные павильоны");
				}
			}
			else MessageBox.Show("Некорректные данные");
		}

		private void OpenListPavilions(object sender, RoutedEventArgs e)
		{
			Manager.cMenu.MainFrame.Navigate(new ListPavilions(IdSC));
		}
	}
}
