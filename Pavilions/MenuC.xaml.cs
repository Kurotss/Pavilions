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
	/// Логика взаимодействия для MenuC.xaml
	/// </summary>
	public partial class MenuC : Page
	{
		public MenuC()
		{
			InitializeComponent();
		}
		private void OpenSC(object sender, RoutedEventArgs e)
		{
			Manager.cMenu.MainFrame.Navigate(new ListShoppingCenters());
		}

		private void OpenReserve(object sender, RoutedEventArgs e)
		{
			Manager.cMenu.MainFrame.Navigate(new ReservePage());
		}
	}
}
