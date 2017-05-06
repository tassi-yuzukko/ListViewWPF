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
		public DispatcherCollection<ListViewConverter> ViewItems => _viewItems;

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
				_origDataList?.Add(rowData);
				AddLogRowDataOnViewItems(new List<LogRowData> { rowData });
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

			// 追加しなおし
			AddLogRowDataOnViewItems(_origDataList);
		}

		/// <summary>
		/// 表示用ログリストにデータを追加
		/// </summary>
		/// <param name="rowData"></param>
		private void AddLogRowDataOnViewItems(List<LogRowData> rowDataList)
		{
			// 無理やりLINQ使ってみる
			rowDataList
				.Where(data => !_isSelected ? true : FilterNoEventLog(data))  // 状態問い合わせとイベントなしの状態通知は表示しない
				.ToList().ForEach(rowData =>
				{
					var item = new ListViewConverter(rowData);
					item.LogBytesToString = _isHex ? (Func<byte[], string>)BytesToStringHex : BytesToStringAscii;
					_viewItems?.Add(item);
				});
		}

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
		/// ログを１６進数で出力する
		/// </summary>
		/// <param name="bytes"></param>
		/// <returns></returns>
		private string BytesToStringHex(byte[] bytes)
		{
			string ret = "";

			var strs = bytes
				.Select(val => Convert.ToInt32(val))            // データを数値に変換
				.Select(val => String.Format("{0:X2} ", val));	// 数値を文字列に変換

			foreach (var str in strs)
			{
				ret += str;
			}

			return ret;
		}

		/// <summary>
		/// ログをASCII文字で出力する
		/// </summary>
		/// <param name="bytes"></param>
		/// <returns></returns>
		private string BytesToStringAscii(byte[] bytes)
		{
			string ret = "";

			var strs = bytes
				.Select(chr => Convert.ToChar(chr))				// データを文字に変換
				.Select(chr => !Char.IsControl(chr) ? chr + " " : String.Format("{0:X2} ", (int)chr) + " ");  // 文字を文字列に変換

			foreach (var str in strs)
			{
				ret += str;
			}

			return ret;
		}
	}
}
