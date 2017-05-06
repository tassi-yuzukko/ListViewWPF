using ListViewTestLib.Models;
using Livet;
using Livet.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace ListViewTestLib.ViewModels
{
	public class ListViewCtrlViewModel : ViewModel
	{
		// 表示用のリストではなく、オリジナルデータのリスト
		List<LogRowData> _origDataList = new List<LogRowData>();

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ListViewCtrlViewModel()
		{
			IsAutoScroll = true;
		}

		readonly DispatcherCollection<ListViewConverter> _viewItems = new DispatcherCollection<ListViewConverter>(DispatcherHelper.UIDispatcher);
		public DispatcherCollection<ListViewConverter> ViewItems { get { return _viewItems; } }

		public bool IsAutoScroll { get; set; }

		private ViewModelCommand _clearLogCommand;
		public ViewModelCommand ClearLogCommand => (_clearLogCommand = _clearLogCommand ?? new ViewModelCommand(()=> { _viewItems.Clear(); _origDataList.Clear(); }));

		private bool _isHex;
		public bool IsHex
		{
			get { return _isHex; }
			set
			{
				_isHex = value;
				// 表示しなおし
				RedrawListView();
			}
		}

		private bool _isSelected;
		public bool IsSelected
		{
			get { return _isSelected; }
			set
			{
				_isSelected = value;
				// 表示しなおし
				RedrawListView();
			}
		}

		/// <summary>
		/// ログ追加
		/// </summary>
		/// <param name="rowData"></param>
		public void AddLog(LogRowData rowData)
		{
			if(rowData != null)
			{
				_viewItems?.Add(new ListViewConverter(rowData, IsHex));
				_origDataList?.Add(rowData);
			}
		}

		/// <summary>
		/// 最新行表示
		/// </summary>
		/// <param name="lv"></param>
		public void AutoScroll(ListView lv)
		{
			if (IsAutoScroll)
			{
				if(lv?.Items.Count > 0)
				{
					lv?.ScrollIntoView(lv.Items[lv.Items.Count - 1]);
				}
			}
		}

		private void RedrawListView()
		{
			// 表示上のログをいったんすべて削除
			_viewItems.Clear();

			// 状態問い合わせとイベントなしの状態通知は表示しない
			if (_isSelected)
			{
				foreach(var data in _origDataList)
				{
					// 簡易的にrecvのみ表示としておく
					if(data.LogType == ListViewLogType.recv)
					{
						_viewItems.Add(new ListViewConverter(data, _isHex));
					}
				}
			}
			// 全てのログを表示する
			else
			{
				foreach (var data in _origDataList)
				{
					_viewItems.Add(new ListViewConverter(data, _isHex));
				}
			}
		}
	}
}
