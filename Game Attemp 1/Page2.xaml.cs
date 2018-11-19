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
    /// Interakční logika pro Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {

        private Frame frame;

        public Page2()
        {
            InitializeComponent();
        }

        public Page2(Frame frame) : this()
        {
            this.frame = frame;
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
        }
    }
}
