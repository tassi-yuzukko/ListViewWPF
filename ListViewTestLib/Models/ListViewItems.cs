﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace ListViewTestLib.Models
{
	/// <summary>
	/// ログの表示タイプ
	/// </summary>
	public enum ListViewLogType
	{
		send,
		recv,
	}

	public class LogRowData : IMyListViewItems
	{
		private DateTime _logDate;
		public string LogDate
		{
			get
			{
				return _logDate.ToString("MM/dd HH:mm:ss.fff");
			}
		}
		public ListViewLogType LogType { get; set; }
		public string LogStr { get; set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public LogRowData()
		{
			// 現在の時刻を設定
			_logDate = DateTime.Now;
		}

		/// <summary>
		/// リストビュー上で表示する際の、行の色を返す
		/// </summary>
		/// <returns></returns>
		public SolidColorBrush GetListViewRowColor()
		{
			switch (LogType)
			{
				case ListViewLogType.recv:
					return Brushes.LightPink;
				case ListViewLogType.send:
					return Brushes.LightBlue;
				default:
					return Brushes.White;
			}
		}
	}
}
