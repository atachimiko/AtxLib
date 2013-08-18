using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace AtxLib.Behaviors
{
	/// <summary>
	/// ScrollViewerとMVVMの親和性を高めるためのユーティリティクラス
	/// </summary>
	/// <remarks>
	/// ScrollViewer.ScrollToHorizontalOffsetメソッドなどへの関数でしか提供されていないScrollViewerのメンバーに
	/// 依存関係プロパティを使ってアクセスするためのユーティリティを含みます
	/// 
	/// </remarks>
	public class ScrollViewerBehavior : Behavior<ScrollViewer>
	{
		protected override void OnAttached()
		{
			base.OnAttached();

			// ListViewにはDependencyPropertyであるItemsSourceプロパティの変更通知が実装されていない
			// 下記のコードで、ItemsSource依存関係プロパティの変更通知を通知するように変更する。
			var dpd = DependencyPropertyDescriptor.FromProperty(ScrollViewer.VerticalOffsetProperty, AssociatedType);
			if (dpd != null)
			{
				dpd.AddValueChanged(AssociatedObject, VerticalOffsetPropertyPropertyIsChanged);
			}

			var dpd2 = DependencyPropertyDescriptor.FromProperty(ScrollViewer.HorizontalOffsetProperty, AssociatedType);
			if (dpd2 != null)
			{
				dpd2.AddValueChanged(AssociatedObject, HorizontalOffsetPropertyPropertyIsChanged);
			}
		}

		private void VerticalOffsetPropertyPropertyIsChanged(object sender, EventArgs e)
		{
			VerticalOffset = AssociatedObject.VerticalOffset;
		}

		private void HorizontalOffsetPropertyPropertyIsChanged(object sender, EventArgs e)
		{
			HorizontalOffset = AssociatedObject.HorizontalOffset;
		}


		// http://marlongrech.wordpress.com/2009/09/14/how-to-set-wpf-scrollviewer-verticaloffset-and-horizontal-offset/
		#region 依存関係プロパティ

		#region HorizontalOffset : ScrollViewer.ScrollToHorizontalOffsetとバインドするための依存関係プロパティ
		public double HorizontalOffset
		{
			get { return (double)GetValue(HorizontalOffsetProperty); }
			set { SetValue(HorizontalOffsetProperty, value); }
		}

		/// <summary>
		/// HorizontalOffset Attached Dependency Property
		/// </summary>
		public static readonly DependencyProperty HorizontalOffsetProperty =
			DependencyProperty.RegisterAttached("HorizontalOffset", typeof(double), typeof(ScrollViewerBehavior),
			   new FrameworkPropertyMetadata((double)0.0,
				   FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
				   new PropertyChangedCallback(OnHorizontalOffsetChanged)));

		/// <summary>
		/// Handles changes to the HorizontalOffset property.
		/// </summary>
		/// <param name="d"></param>
		/// <param name="e"></param>
		private static void OnHorizontalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var viewer = (ScrollViewerBehavior)d;
			viewer.AssociatedObject.ScrollToHorizontalOffset((double)e.NewValue);
		}
		#endregion



		#region VerticalOffset
		public double VerticalOffset
		{
			get { return (double)GetValue(VerticalOffsetProperty); }
			set { SetValue(VerticalOffsetProperty, value); }
		}
		public static readonly DependencyProperty VerticalOffsetProperty =
			DependencyProperty.RegisterAttached("VerticalOffset", typeof(double), typeof(ScrollViewerBehavior),
			 new FrameworkPropertyMetadata(
				 (double)0.0,
				 FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
				 new PropertyChangedCallback(OnVerticalOffsetChanged)
				 )
			);

		private static void OnVerticalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var viewer = (ScrollViewerBehavior)d;
			viewer.AssociatedObject.ScrollToVerticalOffset((double)e.NewValue);
		}
		
		#endregion


		#endregion
	}
}
