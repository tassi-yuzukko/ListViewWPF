using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace ListViewTestLib.Views.Behaviours
{
	public class ListViewHeaderAutoResizeBehavior : Behavior<ListView>
	{
		public int AutoResizeColumnsNumber { get; set; }

		protected sealed override void OnAttached()
		{
			base.OnAttached();
			ListView hostControl = (ListView)this.AssociatedObject;

			hostControl.Loaded += Loaded;
			hostControl.SizeChanged += SizeChanged;
		}

		protected sealed override void OnDetaching()
		{
			base.OnDetaching();
		}

		private void Loaded(object sender, RoutedEventArgs e)
		{
			ListView view = sender as ListView;
			this.AutoColumnsSizeChanged(view);
		}

		private void SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
		{
			ListView view = sender as ListView;
			this.AutoColumnsSizeChanged(view);
		}

		private void AutoColumnsSizeChanged(ListView view)
		{
			GridView gridView = view.View as GridView;

			double size = 0;

			for (int i = 0; i < gridView.Columns.Count; i++)
			{
				if (this.AutoResizeColumnsNumber != i)
				{
					size += gridView.Columns[i].ActualWidth;
				}
			}
			if (view.ActualWidth - size > 0)
			{
				gridView.Columns[this.AutoResizeColumnsNumber].Width = view.ActualWidth - size;
			}
		}
	}
}
