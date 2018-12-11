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

namespace Game_Attemp_1
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Player player;

        public MainWindow()
        {
            InitializeComponent();

            player = new Player();

            myFrame.Navigate(new Page2(myFrame));
        }

        public void ChangePlayerLocation (Page page)
        {

        }


    }
}
