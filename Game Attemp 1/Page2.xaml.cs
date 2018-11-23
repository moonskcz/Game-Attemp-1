using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Game_Attemp_1
{
    /// <summary>
    /// Interakční logika pro Page1.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        private Frame frame;

        public Player player;
        public Page2()
        {

            player = ((MainWindow)App.Current.MainWindow).player;

            InitializeComponent();

            labul1.Content = player.Name;

            RefreshUI();

            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(@"maxresdefault.jpg", UriKind.Relative));
            kanvas.Background = ib;

            App.Current.MainWindow.KeyDown += new System.Windows.Input.KeyEventHandler(Page_KeyDown);
            

        }

        public Page2(Frame frame) : this()
        {
            this.frame = frame;
        }

        public void RefreshUI()
        {
            RefreshStats();
            RefreshInv();
            RefreshCombatStats();
        }

        public void RefreshCombatStats()
        {
            Attack.Content = player.Properties["Attack"];
            Defense.Content = player.Properties["Defence"];
        }

        public void RefreshStats()
        {
            Health.Content = player.Properties["Health"];
            Inteligence.Content = player.Properties["Inteligence"] / 10;
            Strenght.Content = player.Properties["Strenght"] / 10;
            Speed.Content = player.Properties["Speed"] / 10;
            Perception.Content = player.Properties["Perception"] / 10;
            Money.Content = player.Properties["Money"];
        }

        public void RefreshInv()
        {
            for (int i = Inventory.Items.Count - 1; i >= 0; i--)
            {
                Inventory.Items.RemoveAt(i);
            }

            foreach (Item charItem in player.Inventory)
            {
                Inventory.Items.Add(charItem.Name);
            }

            for (int i = Equiped.Items.Count - 1; i >= 0; i--)
            {
                Equiped.Items.RemoveAt(i);
            }

            foreach (ItemOffensive charItem in player.Equiped.Values)
            {
                Equiped.Items.Add(charItem.Zone);
                Equiped.Items.Add(charItem.Name);
                Equiped.Items.Add("");
            }
        }

        private bool MiniTick()
        {
            Point p = Mouse.GetPosition(kanvas);
            bool moved = false;

            if (Keyboard.IsKeyDown(Key.Left))
            {
                int i = ((int)PlayerObject.GetValue(Grid.ColumnProperty) - 1 < 0 ? 0 : (int)PlayerObject.GetValue(Grid.ColumnProperty) - 1);
                PlayerObject.SetValue(Grid.ColumnProperty, i);
                moved = true;
                Canvas.SetLeft(rectangul, Canvas.GetLeft(rectangul) - 1);
            }
            if (Keyboard.IsKeyDown(Key.Right))
            {
                int i = ((int)PlayerObject.GetValue(Grid.ColumnProperty) + 1 > GameGrid.ColumnDefinitions.Count - 1 ? GameGrid.ColumnDefinitions.Count - 1 : (int)PlayerObject.GetValue(Grid.ColumnProperty) + 1);
                PlayerObject.SetValue(Grid.ColumnProperty, i);
                moved = true;
                Canvas.SetLeft(rectangul, Canvas.GetLeft(rectangul) + 1);

            }
            if (Keyboard.IsKeyDown(Key.Up))
            {
                int i = ((int)PlayerObject.GetValue(Grid.RowProperty) - 1 < 0 ? 0 : (int)PlayerObject.GetValue(Grid.RowProperty) - 1);
                PlayerObject.SetValue(Grid.RowProperty, i);
                moved = true;
                Canvas.SetTop(rectangul, Canvas.GetTop(rectangul) - 1);

            }
            if (Keyboard.IsKeyDown(Key.Down))
            {
                int i = ((int)PlayerObject.GetValue(Grid.RowProperty) + 1 > GameGrid.RowDefinitions.Count - 1 ? GameGrid.RowDefinitions.Count - 1 : (int)PlayerObject.GetValue(Grid.RowProperty) + 1);
                PlayerObject.SetValue(Grid.RowProperty, i);
                moved = true;
                Canvas.SetTop(rectangul, Canvas.GetTop(rectangul) + 1);

            }

            if (moved)
            {
                player.PerformActivity("Speed", 1);
                RefreshStats();

                return true;
            } else
            {
                return false;
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (!(Inventory.SelectedItem is null))
            {
                player.Equip(Inventory.SelectedItem.ToString());
                RefreshInv();
                RefreshCombatStats();

            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if (!(Equiped.SelectedItem is null))
            {
                player.Unequip(Equiped.SelectedItem.ToString());
                RefreshInv();
                RefreshCombatStats();
            }

        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            MiniTick();
        }

        private void Inventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
