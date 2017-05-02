using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ListViewApp
{
	/// <summary>
	/// App.xaml の相互作用ロジック
	/// </summary>
	public partial class App : Application
	{
		/// <summary>
		/// ！！これ重要！！
		/// RaiseCanExecuteChanged などのUIDispatcherを使用する処理で必要
		/// </summary>
		/// <param name="e"></param>
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			Livet.DispatcherHelper.UIDispatcher = this.Dispatcher;
		}
	}
}
