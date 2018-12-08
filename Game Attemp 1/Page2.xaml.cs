using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
            ib.ImageSource = new BitmapImage(new Uri(@"no-work.jpg", UriKind.Relative));
            kanvas.Background = ib;

            App.Current.MainWindow.KeyDown += new System.Windows.Input.KeyEventHandler(Page_KeyDown);
            App.Current.MainWindow.KeyUp += new System.Windows.Input.KeyEventHandler(Page_KeyUp);

            App.Current.MainWindow.MouseDown += new System.Windows.Input.MouseButtonEventHandler(Shoot);


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

            if (Keyboard.IsKeyDown(Key.Space))
            {
                SpawnItem();
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
                if (CheckColision(kanvas, "left")) {
                    Canvas.SetLeft(rectangul, Canvas.GetLeft(rectangul) - moveCoefficient);
                }
            }
            if (moveRight)
            {
                int moveCoefficient = 2;
                if (moveUp || moveDown)
                {
                    moveCoefficient = 1;
                }
                if (CheckColision(kanvas, "right"))
                {
                    Canvas.SetLeft(rectangul, Canvas.GetLeft(rectangul) + moveCoefficient);
                }

            }
            if (moveUp)
            {
                int moveCoefficient = 2;
                if (moveLeft || moveRight)
                {
                    moveCoefficient = 1;
                }
                if (CheckColision(kanvas, "up"))
                {
                    Canvas.SetTop(rectangul, Canvas.GetTop(rectangul) - moveCoefficient);
                }

            }
            if (moveDown)
            {
                int moveCoefficient = 2;
                if (moveLeft || moveRight)
                {
                    moveCoefficient = 1;
                }
                if (CheckColision(kanvas, "down"))
                {
                    Canvas.SetTop(rectangul, Canvas.GetTop(rectangul) + moveCoefficient);
                }

            }

            /*Point pointToWindow = Mouse.GetPosition(this);

            ypos.Content = Canvas.GetTop(rectangul);
            xpos.Content = Canvas.GetLeft(rectangul);
            */
        }

        private bool CheckColision (Canvas canvas, string direction)
        {

            int recWidth = (int)(rectangul.Width);
            int recHeight = (int)(rectangul.Height);
            int recX = (int)Canvas.GetLeft(rectangul);
            int recY = (int)Canvas.GetTop(rectangul);

            switch (direction)
            {
                case "up":

                    var resultUp = from item in canvas.Children.OfType<UIElement>() where Canvas.GetTop(item) + item.RenderSize.Height - 5 < recY + recHeight select item; 

                    foreach (UIElement el in resultUp)
                    {
                        if (el != rectangul)
                        {
                            if ((Canvas.GetLeft(rectangul) + recWidth) > Canvas.GetLeft(el) && Canvas.GetLeft(rectangul) < (Canvas.GetLeft(el) + el.RenderSize.Width))// && (Canvas.GetLeft(el) + el.RenderSize.Width) > Canvas.GetLeft(rectangul))) // (Canvas.GetTop(el) + el.RenderSize.Height) <= Canvas.GetTop(rectangul) || 
                            {
                                if ( (Canvas.GetTop(el) + el.RenderSize.Height) < Canvas.GetTop(rectangul))
                                {}
                                else
                                {
                                    return false;
                                }
                            }
                        }
                    }

                    return true;

                case "down":

                    var resultDown = from item in canvas.Children.OfType<UIElement>() where Canvas.GetTop(item) + 5 > recY select item;

                    foreach (UIElement el in resultDown)
                    {
                        if (el != rectangul)
                        {
                            if ((Canvas.GetLeft(rectangul) + recWidth) > Canvas.GetLeft(el) && Canvas.GetLeft(rectangul) < (Canvas.GetLeft(el) + el.RenderSize.Width))// && (Canvas.GetLeft(el) + el.RenderSize.Width) > Canvas.GetLeft(rectangul))) // (Canvas.GetTop(el) + el.RenderSize.Height) <= Canvas.GetTop(rectangul) || 
                            {
                                if (Canvas.GetTop(el) > (Canvas.GetTop(rectangul) + recHeight))
                                {}
                                else
                                {
                                    return false;
                                }
                            }
                        }
                    }

                    return true;

                case "left":

                    var resultLeft = from item in canvas.Children.OfType<UIElement>() where Canvas.GetLeft(item) + item.RenderSize.Width - 5 < recX select item;

                    foreach (UIElement el in resultLeft)
                    {
                        if (el != rectangul)
                        {
                            if ((Canvas.GetTop(rectangul) + recHeight) > Canvas.GetTop(el) && Canvas.GetTop(rectangul) < (Canvas.GetTop(el) + el.RenderSize.Height))// && (Canvas.GetLeft(el) + el.RenderSize.Width) > Canvas.GetLeft(rectangul))) // (Canvas.GetTop(el) + el.RenderSize.Height) <= Canvas.GetTop(rectangul) || 
                            {
                                if ((Canvas.GetLeft(el) + el.RenderSize.Width) < Canvas.GetLeft(rectangul))
                                {}
                                else
                                {
                                    return false;
                                }
                            }
                        }
                    }

                    return true;

                case "right":

                    var resultRight = from item in canvas.Children.OfType<UIElement>() where Canvas.GetLeft(item) + 5 > recX + recWidth select item;

                    foreach (UIElement el in resultRight)
                    {
                        if (el != rectangul)
                        {
                            if ((Canvas.GetTop(rectangul) + recHeight) > Canvas.GetTop(el) && Canvas.GetTop(rectangul) < (Canvas.GetTop(el) + el.RenderSize.Height))// && (Canvas.GetLeft(el) + el.RenderSize.Width) > Canvas.GetLeft(rectangul))) // (Canvas.GetTop(el) + el.RenderSize.Height) <= Canvas.GetTop(rectangul) || 
                            {
                                if (Canvas.GetLeft(el) > Canvas.GetLeft(rectangul) + recWidth)
                                {}
                                else
                                {
                                    return false;
                                }
                            }
                        }
                    }

                    return true;

                default:
                    return true;
            }

        }

        private void mouseMove (object sender, MouseEventArgs e)
        {

            Point pointToWindow = Mouse.GetPosition(this);

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
            
            Rectangle r = new Rectangle();
            
            r.Width = 20;
            r.Height = 20;
            
            r.Fill = Brushes.Black;

            r.Name = "kkk";
            
            Canvas.SetTop(r, Canvas.GetTop(rectangul));
            Canvas.SetLeft(r, Canvas.GetLeft(rectangul));
            
            kanvas.Children.Add(r);

        }

        private Dictionary<string ,int> GetPlayerPosData ()
        {
            Dictionary<string, int> ret = new Dictionary<string, int>();

            ret.Add("top", (int)Canvas.GetTop(rectangul));
            ret.Add("left", (int)Canvas.GetLeft(rectangul));
            ret.Add("right", (int)Canvas.GetRight(rectangul));
            ret.Add("bottom", (int)Canvas.GetBottom(rectangul));
            ret.Add("width", (int)rectangul.Width);
            ret.Add("height", (int)rectangul.Height);

            return ret;

        }
        private void Shoot (object sender, MouseButtonEventArgs e)
        {
            Point pointToWindow = Mouse.GetPosition(this);
            Dictionary<string, int> plrData = GetPlayerPosData();

            int mouseX = (int)pointToWindow.X;
            int mouseY = (int)pointToWindow.Y;
            int plrX = plrData["left"];
            int plrY = plrData["top"];

            /*if (mouseX < plrX)
            {
                plrX = plrX - mouseX;
                mouseX = 0;
            } else if (mouseX > plrX)
            {
                mouseX = mouseX - plrX;
                plrX = 0;
            }

            if (mouseY < plrY)
            {
                plrY = plrY - mouseY;
                mouseY = 0;
            } else if (mouseY > plrY)
            {
                mouseY = mouseY - plrY;
                plrY = 0;
            }*/

            if (mouseX < plrX)
            {
                plrX = plrX - mouseX;
                mouseX = plrX;
                plrX = 0;
            } else if (mouseX > plrX)
            {
                mouseX = mouseX - plrX;
                plrX = 0;
            }

            if (mouseY < plrY)
            {
                plrY = plrY - mouseY;
                mouseY = 0;
            } else if (mouseY > plrY)
            {
                mouseY = mouseY - plrY;
                plrY = mouseY;
                mouseY = 0;
            }

            int mouseToPlr = (int)Math.Sqrt(mouseX * mouseX + plrY * plrY);

            double ratio = (double)plrY / mouseToPlr;

            double radians = Math.Asin(ratio);

            double angle = (180 / Math.PI) * radians;

            int pop = 1;

        }
    }
}
