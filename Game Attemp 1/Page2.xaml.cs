﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        private bool moveLeft = false;
        private bool moveRight = false;
        private bool moveUp = false;
        private bool moveDown = false;

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
            App.Current.MainWindow.KeyUp += new System.Windows.Input.KeyEventHandler(Page_KeyUp);

            //App.Current.MainWindow.MouseMove += new MouseEventHandler(mouseMove);

            MovementTimer();


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

            if (Keyboard.IsKeyDown(Key.W))
            {
                moveUp = true;
            }
            if (Keyboard.IsKeyDown(Key.S))
            {
                moveDown = true;
            }
            if (Keyboard.IsKeyDown(Key.A))
            {
                moveLeft = true;
            }
            if (Keyboard.IsKeyDown(Key.D))
            {
                moveRight = true;
            }
        }

        private void Page_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                moveUp = false;
            }
            if (e.Key == Key.S)
            {
                moveDown = false;
            }
            if (e.Key == Key.A)
            {
                moveLeft = false;
            }
            if (e.Key == Key.D)
            {
                moveRight = false;
            }
        }

        private void Movement (object sender, EventArgs e)
        {

            if (moveLeft)
            {
                int moveCoefficient = 2;
                if (moveUp || moveDown)
                {
                    moveCoefficient = 1;
                }
                Canvas.SetLeft(rectangul, Canvas.GetLeft(rectangul) - moveCoefficient);
            }
            if (moveRight)
            {
                int moveCoefficient = 2;
                if (moveUp || moveDown)
                {
                    moveCoefficient = 1;
                }
                Canvas.SetLeft(rectangul, Canvas.GetLeft(rectangul) + moveCoefficient);

            }
            if (moveUp)
            {
                int moveCoefficient = 2;
                if (moveLeft || moveRight)
                {
                    moveCoefficient = 1;
                }
                Canvas.SetTop(rectangul, Canvas.GetTop(rectangul) - moveCoefficient);

            }
            if (moveDown)
            {
                int moveCoefficient = 2;
                if (moveLeft || moveRight)
                {
                    moveCoefficient = 1;
                }
                if (CheckColision(kanvas))
                {
                    Canvas.SetTop(rectangul, Canvas.GetTop(rectangul) + moveCoefficient);

                }

            }

            Point pointToWindow = Mouse.GetPosition(this);

            ypos.Content = Canvas.GetTop(rectangul);
            xpos.Content = Canvas.GetLeft(rectangul);

            //CheckColision(kanvas);
        }

        private bool CheckColision (Canvas canvas)
        {
            /*UIElementCollection UIC = from child in canvas.Children
                                      where (num % 2) == 0
                                      select num;

            return false;*/
            /*

            foreach (UIElement child in canvas.Children)
            {
                if (rectangul.Height) {
                    Canvas.GetTop(child);
                }

            }*/
            /*
            List<UIElement> UIE = new List<UIElement>();
            UIE =
                (from canvas.Children in canvas
                where (num % 2) == 0
                select num).ToList();
            */
            //Console.WriteLine(canvas.Children[0].RenderSize);

            int recWidth = (int)(rectangul.Width / 2);
            int recHeight = (int)(rectangul.Height / 2);
            int recX = (int)Canvas.GetLeft(rectangul) + recWidth;
            int recY = (int)Canvas.GetTop(rectangul) + recHeight;

            var result = from item in canvas.Children.OfType<UIElement>() where (Canvas.GetTop(item) + (item.RenderSize.Height / 2)) > recY select item; 

            var betterResult = from item in result where (Canvas.GetLeft(item) + (item.RenderSize.Width / 2)) > recX select item;

            foreach (UIElement el in betterResult)
            {
                if (el != rectangul)
                {
                    deb1.Content = Canvas.GetTop(el) + el.RenderSize.Height;
                    deb2.Content = Canvas.GetTop(rectangul);
                    if ((Canvas.GetTop(el) + el.RenderSize.Height) >= Canvas.GetTop(rectangul) || Canvas.GetTop(el) <= (Canvas.GetTop(rectangul) + recHeight * 2))
                    {
                        return true;
                    } else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void mouseMove (object sender, MouseEventArgs e)
        {

            Point pointToWindow = Mouse.GetPosition(this);

            //CursorImage.Margin = new Thickness(pointToWindow.X, pointToWindow.Y, this.Width - pointToWindow.X, this.Height - pointToWindow.Y);
            
        }

        private void MovementTimer ()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(Movement);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dispatcherTimer.Start();
        }

        private void SpawnItem ()
        {
            /*
            rectangul;
            Rectangle rec = new RectangleGeometry();
            kanvas.Children.Add(rec);
            Canvas.SetLeft(rec, Canvas.GetLeft(rectangul));
            Canvas.SetTop(rec, Canvas.GetTop(rectangul));
            */
        }

    }
}
