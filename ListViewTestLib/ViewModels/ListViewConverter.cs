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
		private bool _isHex;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="logRowData"></param>
		public ListViewConverter(LogRowData logRowData, bool isHex)
		{
			_logRowData = logRowData;
			_isHex = isHex;

			_logRowData.LogDate = DateTime.Now;
		}

		public string LogType => _logRowData.LogType.ToString();
		public string LogDate => _logRowData.LogDate.ToString("MM/dd HH:mm:ss.fff");
		public string LogStr => (_isHex ? BytesToStringHex(_logRowData.LogBytes) : BytesToStringAscii(_logRowData.LogBytes));

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
				default:
					return Brushes.White;
			}
		}

		/// <summary>
		/// ログを１６進数で出力する
		/// </summary>
		/// <param name="bytes"></param>
		/// <returns></returns>
		private string BytesToStringHex(byte [] bytes)
		{
			string ret ="";

			foreach(var val in bytes)
			{
				int integral = Convert.ToInt32(val);
				ret += String.Format("{0:X2} ", integral);
			}

			return ret;
		}

		/// <summary>
		/// ログをASCII文字で出力する
		/// </summary>
		/// <param name="bytes"></param>
		/// <returns></returns>
		private string BytesToStringAscii(byte [] bytes)
		{
			string ret = "";

			foreach(var val in bytes)
			{
				char charactor = Convert.ToChar(val);
				if (!Char.IsControl(charactor)){
					ret += charactor + " ";
				}
				else
				{
					ret += String.Format("{0:X2} ", (int)charactor) + " ";
				}
			}

			return ret;
		}
	}
}
