using ListViewTestLib.ViewModels;
using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ListViewTestLib.Models;

namespace ListViewApp.ViewModels
{
	class MainWindowViewModel :ViewModel
	{
		private ListViewCtrlViewModel _listViewCtrl;

		public MainWindowViewModel(ListViewCtrlViewModel listViewCtrl)
		{
			_listViewCtrl = listViewCtrl;

			// 疑似処理スレッド作成
			Task.Factory.StartNew(DummyAddLog);
		}

		private void DummyAddLog()
		{
			for (int i=0; i<1000; i++)
			{
				Thread.Sleep(1000);

				ListViewItems item = new ListViewItems{ LogDate = DateTime.Now, LogType = ListViewLogType.recv, LogStr = i.ToString(), };
				_listViewCtrl.AddLog(item);
			}
		}
	}
}
