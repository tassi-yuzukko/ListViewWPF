using ListViewTestLib.ViewModels;
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

namespace ListViewTestLib.Views
{
    /// <summary>
    /// ListViewCtrl.xaml の相互作用ロジック
    /// </summary>
    public partial class ListViewCtrl : UserControl
    {
        public ListViewCtrl()
        {
            InitializeComponent();

			this.DataContext = new ListViewCtrlViewModel();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			LVC.DataContext = (this.DataContext as ListViewCtrlViewModel).Items;
		}
	}
}
