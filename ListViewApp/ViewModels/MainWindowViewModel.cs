using ListViewTestLib.ViewModels;
using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ListViewTestLib.Models;
using Livet.Commands;

namespace ListViewApp.ViewModels
{
	class MainWindowViewModel :ViewModel
	{
		private ListViewCtrlViewModel _listViewCtrl;
		private int i;

		public MainWindowViewModel(ListViewCtrlViewModel listViewCtrl)
		{
			_listViewCtrl = listViewCtrl;
			_listViewCtrl.FilterNoEventLogDelegate = FilterNoEventLog;
			_listViewCtrl.ControlCharToStringDelegate = ControlCharToString;
		}

		private void DummyAddLog()
		{
			_listViewCtrl.AddLog(new LogRowData { LogType = ListViewLogType.others, LogBytes = new byte [] { 1, 3, 4, 5}, });

			int max = i + 100;
			for (; i< max; i++)
			{
				Thread.Sleep(10);

				byte[] bytes = { (byte)i, (byte)'t', (byte)'e', (byte)'s', (byte)'t', };

				_listViewCtrl.AddLog(new LogRowData { LogType = (i % 2) == 0 ? ListViewLogType.recv : ListViewLogType.send, LogBytes = bytes, });
			}
		}

		private ViewModelCommand _addLogCommand;
		public ViewModelCommand AddLogCommand => (_addLogCommand = _addLogCommand ?? new ViewModelCommand(() => { Task.Factory.StartNew(DummyAddLog); }));

		/// <summary>
		/// とりあえず疑似的にrecvログのみ表示するようにしている
		/// </summary>
		/// <param name="rowData"></param>
		/// <returns></returns>
		private bool FilterNoEventLog(LogRowData rowData)
		{
			return rowData.LogType == ListViewLogType.recv;
		}

		/// <summary>
		/// 制御文字を文字列に変換する
		/// </summary>
		/// <param name="chr"></param>
		/// <returns></returns>
		private string ControlCharToString(char chr)
		{
			switch (chr)
			{
				case (char)0x02:
					return "[STX]";
				case (char)0x03:
					return "[ETX]";
				default:
					return String.Format("{0:X2}", (int)chr);
			}
		}
	}
}
