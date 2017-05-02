using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

	public class ListViewItems
	{
		public DateTime LogDate { get; set; }
		public ListViewLogType LogType { get; set; }
		public string LogStr { get; set; }
	}
}
