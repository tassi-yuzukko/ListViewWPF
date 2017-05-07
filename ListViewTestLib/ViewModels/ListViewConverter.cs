using ListViewTestLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace ListViewTestLib.ViewModels
{
	/// <summary>
	/// LogRowData型をListViewに描画するように変換するためのクラス
	/// </summary>
	public class ListViewConverter : IMyListViewItems
	{
		private LogRowData _logRowData;

		public Func<byte[], string> LogBytesToString { get; set; } = (x) => x.ToString();

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="logRowData"></param>
		public ListViewConverter(LogRowData logRowData)
		{
			_logRowData = logRowData;
		}

		public string LogType => _logRowData.LogType.ToString();
		public string LogDate => _logRowData.LogDate.ToString("MM/dd HH:mm:ss.fff");
		public string LogStr => LogBytesToString(_logRowData.LogBytes);

		/// <summary>
		/// 行の表示色を返す
		/// </summary>
		/// <returns></returns>
		public SolidColorBrush GetListViewRowColor()
		{
			switch (_logRowData.LogType)
			{
				case ListViewLogType.recv:
					return Brushes.LightPink;
				case ListViewLogType.send:
					return Brushes.LightBlue;
				case ListViewLogType.others:
					return Brushes.LightGreen;
				default:
					return Brushes.White;
			}
		}
	}
}
