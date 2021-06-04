using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Pavilions.Classes;

namespace Pavilions
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public int i = 0;
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Login(object sender, RoutedEventArgs e)
		{
			if (i >= 3 && CaptchaBox.Text != CaptchaBlock.Text)
			{
				MessageBox.Show("Неверные данные");
				Random random = new();
				CaptchaBlock.Text = random.Next(99999).ToString();
			}
			else
			{
				CenterPavilionsContext db = new();
				var users = db.Users.Where(p => p.LoginUser.ToLower() == LoginBox.Text.ToLower()).Where(p => p.PasswordUser == Password.Password).ToList();
				if (users.Count == 0)
				{
					MessageBox.Show("Нет пользователя с такими данными");
					i++;
					if (i >= 3)
					{
						Random random = new();
						CaptchaBlock.Text = random.Next(99999).ToString();
						Captcha.Visibility = Visibility.Visible;
						CaptchaBox.Visibility = Visibility.Visible;
						Grid.SetRow(LoginButton, 10);
					}
				}
				else
				{
					foreach (User user in users)
					{
						Manager.IdStaffer = user.IdStaffer;
						switch (user.IdRole)
						{
							case 3:
								ManagerCMenu menu = new();
								menu.Show();
								this.Close();
								break;
						}
					}
				}
			}
		}
	}
}
