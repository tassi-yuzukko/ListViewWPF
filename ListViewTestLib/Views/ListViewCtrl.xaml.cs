using ListViewTestLib.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
			//LVC.DataContext = (this.DataContext as ListViewCtrlViewModel).Items;
		}

		private void ListBox_TargetUpdated(object sender, DataTransferEventArgs e)
		{
			(LVC.ItemsSource as INotifyCollectionChanged).CollectionChanged += new NotifyCollectionChangedEventHandler(listBox_CollectionChanged);
		}
		void listBox_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			this.LVC.ScrollIntoView(this.LVC.Items[this.LVC.Items.Count - 1]); 
		}
	}
}
