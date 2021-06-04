using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Pavilions
{
	/// <summary>
	/// Логика взаимодействия для ShowInfoPavilion.xaml
	/// </summary>
	public partial class ShowInfoPavilion : Page
	{
		public int IsAdd = 0;
		public int IdCenter { get; set; }
		public int IdPavilion { get; set; }
		public string NumberPavilion { get; set; }
		public int FloorPavilion { get; set; }
		public double AreaPavilion { get; set; }
		public int CostPavilion { get; set; }
		public double KdsPavilion { get; set; }
		public ShowInfoPavilion(Pavilion pavilion)
		{
			InitializeComponent();
			this.IdPavilion = pavilion.IdPavilion;
			this.NumberPavilion = pavilion.NumberPavilion;
			this.FloorPavilion = (int)pavilion.NumberFloor;
			this.AreaPavilion = (double)pavilion.Area;
			this.CostPavilion = (int)pavilion.CostOfArea;
			this.KdsPavilion = (double)pavilion.ValAddRatio;
			this.DataContext = this;
			StatusInput.Text = pavilion.StatusPavilion;
		}

		public ShowInfoPavilion(int id)
		{
			InitializeComponent();
			IsAdd = 1;
			IdCenter = id;
		}

		private void SaveInfo(object sender, RoutedEventArgs e)
		{
			bool AllCorrect = true;
			foreach (FrameworkElement element in FieldsInput.Children)
			{
				if (element is TextBox)
				{
					if (element.Name == "NumberPavilionInput")
					{
						if (!Regex.IsMatch(NumberPavilionInput.Text, @"^\d+[а-яА-Я]+$") || NumberPavilionInput.Text.Length < 2)
						{
							AllCorrect = false;
							MessageBox.Show("a");
						}
					}
					else if (element.Name == "FloorInput")
					{
						if (!Regex.IsMatch(FloorInput.Text, @"^\d+$")) { AllCorrect = false; MessageBox.Show("б"); }
					}
					else
					{
						TextBox box = (TextBox)element;
						if (!Regex.IsMatch(box.Text, @"^\d+\,\d+$") && !Regex.IsMatch(box.Text, @"^\d+$"))
						{
							AllCorrect = false;
							MessageBox.Show("в");
						}
					}
				}
			}
			if (StatusInput.SelectedIndex == -1) AllCorrect = false;
			if (double.Parse(KdsInput.Text) < 0.1) AllCorrect = false;
			if (AllCorrect)
			{
				CenterPavilionsContext db = new();
				Pavilion pavilion = new();
				if (IsAdd == 0) pavilion = db.Pavilions.Find(IdPavilion);
				pavilion.NumberPavilion = NumberPavilionInput.Text;
				pavilion.NumberFloor = int.Parse(FloorInput.Text);
				pavilion.Area = double.Parse(AreaInput.Text);
				pavilion.CostOfArea = double.Parse(CostInput.Text);
				pavilion.ValAddRatio = double.Parse(KdsInput.Text);
				pavilion.StatusPavilion = StatusInput.Text;
				if (IsAdd == 1)
				{
					pavilion.IdShopCenter = IdCenter;
					db.Pavilions.Add(pavilion);
				}
				try
				{
					db.SaveChanges();
					if (IsAdd == 0) MessageBox.Show("Данные обновлены");
					else MessageBox.Show("Данные добавлены");
				}
				catch
				{
					MessageBox.Show("Нельзя удалить/отредактровать павильон со статусом 'Арендован' или 'Забронирован'");
				}
			}
			else MessageBox.Show("Некорректные данные");
		}
	}
}
