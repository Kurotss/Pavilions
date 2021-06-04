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
using System.Windows.Shapes;
using Pavilions.Classes;

namespace Pavilions
{
	/// <summary>
	/// Логика взаимодействия для ManagerCMenu.xaml
	/// </summary>
	public partial class ManagerCMenu : Window
	{
		public ManagerCMenu()
		{
			InitializeComponent();
			Manager.cMenu = this;
			MainFrame.Navigate(new MenuC());
		}
	}
}
