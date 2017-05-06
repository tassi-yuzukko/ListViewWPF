using System;
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

	public class LogRowData
	{
		public DateTime LogDate { get; set; }
		public ListViewLogType LogType { get; set; }
		public byte [] LogBytes { get; set; }

		public LogRowData()
		{
			LogDate = DateTime.Now;
		}
	}
}
