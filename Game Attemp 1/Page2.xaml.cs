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

            //TickMaster();

            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(@"maxresdefault.jpg", UriKind.Relative));
            kanvas.Background = ib;

            /*
             * Stažené soubory/maxresdefault.jpg
             * 
             * ImageBrush ib = new ImageBrush();
ib.ImageSource = new BitmapImage(new Uri(@"Stažené soubory/maxresdefault.jpg", UriKind.Relative));
kanvas.Background = ib;
             *
             */
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

        private void TickMaster ()
        {
            DispatcherTimer DT = new DispatcherTimer();
            DT.Tick += new EventHandler(Tick);
            DT.Interval = new TimeSpan(0, 0, 0, 0, 1);
            DT.Start();
        }

        private void Tick (object sender, EventArgs e)
        {
            if (MiniTick())
            {
                Thread.Sleep(100);
            }
        }

        private bool MiniTick()
        {
            bool moved = false;

            if (Keyboard.IsKeyDown(Key.Left))
            {
                int i = ((int)PlayerObject.GetValue(Grid.ColumnProperty) - 1 < 0 ? 0 : (int)PlayerObject.GetValue(Grid.ColumnProperty) - 1);
                PlayerObject.SetValue(Grid.ColumnProperty, i);
                moved = true;

            }
            if (Keyboard.IsKeyDown(Key.Right))
            {
                int i = ((int)PlayerObject.GetValue(Grid.ColumnProperty) + 1 > GameGrid.ColumnDefinitions.Count - 1 ? GameGrid.ColumnDefinitions.Count - 1 : (int)PlayerObject.GetValue(Grid.ColumnProperty) + 1);
                PlayerObject.SetValue(Grid.ColumnProperty, i);
                moved = true;


            }
            if (Keyboard.IsKeyDown(Key.Up))
            {
                int i = ((int)PlayerObject.GetValue(Grid.RowProperty) - 1 < 0 ? 0 : (int)PlayerObject.GetValue(Grid.RowProperty) - 1);
                PlayerObject.SetValue(Grid.RowProperty, i);
                moved = true;


            }
            if (Keyboard.IsKeyDown(Key.Down))
            {
                int i = ((int)PlayerObject.GetValue(Grid.RowProperty) + 1 > GameGrid.RowDefinitions.Count - 1 ? GameGrid.RowDefinitions.Count - 1 : (int)PlayerObject.GetValue(Grid.RowProperty) + 1);
                PlayerObject.SetValue(Grid.RowProperty, i);
                moved = true;


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

        private void RepeatButton_Click(object sender, RoutedEventArgs e)
        {
            player.PerformActivity("Speed", 1);

            RefreshStats();
        }

        private void Page_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                int i = ((int)PlayerObject.GetValue(Grid.ColumnProperty) - 1 < 0 ? 0 : (int)PlayerObject.GetValue(Grid.ColumnProperty) - 1);
                PlayerObject.SetValue(Grid.ColumnProperty, i);
            }
            if (Keyboard.IsKeyDown(Key.Right))
            {
                int i = ((int)PlayerObject.GetValue(Grid.ColumnProperty) + 1 > GameGrid.ColumnDefinitions.Count - 1 ? GameGrid.ColumnDefinitions.Count - 1 : (int)PlayerObject.GetValue(Grid.ColumnProperty) + 1);
                PlayerObject.SetValue(Grid.ColumnProperty, i);
            }
            if (Keyboard.IsKeyDown(Key.Up))
            {
                int i = ((int)PlayerObject.GetValue(Grid.RowProperty) - 1 < 0 ? 0 : (int)PlayerObject.GetValue(Grid.RowProperty) - 1);
                PlayerObject.SetValue(Grid.RowProperty, i);
            }
            if (Keyboard.IsKeyDown(Key.Down))
            {
                int i = ((int)PlayerObject.GetValue(Grid.RowProperty) + 1 > GameGrid.RowDefinitions.Count - 1 ? GameGrid.RowDefinitions.Count - 1 : (int)PlayerObject.GetValue(Grid.RowProperty) + 1);
                PlayerObject.SetValue(Grid.RowProperty, i);
            }

            if (Keyboard.IsKeyDown(Key.Left) || Keyboard.IsKeyDown(Key.Right) || Keyboard.IsKeyDown(Key.Up) || Keyboard.IsKeyDown(Key.Down))
            {
                player.PerformActivity("Speed", 1);
                RefreshStats();

                Thread.Sleep(100);
            }
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            MiniTick();

        }
    }
}
