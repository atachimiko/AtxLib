using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace AtxLib.Behaviors
{
	/// <summary>
	/// WPF ListViewで使用するビヘイビア
	/// </summary>
	public class ListViewBehavior : Behavior<ListView>
	{
		protected override void OnAttached()
		{
			base.OnAttached();

			// ListViewにはDependencyPropertyであるItemsSourceプロパティの変更通知が実装されていない
			// 下記のコードで、ItemsSource依存関係プロパティの変更通知を通知するように変更する。
			var dpd = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, AssociatedType);
			if (dpd != null)
			{
				dpd.AddValueChanged(AssociatedObject, ThisIsCalledWhenPropertyIsChanged);
			}

		}

		private void ThisIsCalledWhenPropertyIsChanged(object sender, EventArgs e)
		{
			// ItemsSourceプロパティの値が変更された場合、ListViewの垂直方向のスクロール位置を先頭に移動する
			// (ItemsSourceプロパティのコレクションへの操作では、この処理は行われないので注意してください)

			// ListViewからAutomationPeerを取得
			var peer = ItemsControlAutomationPeer.CreatePeerForElement(AssociatedObject);
			// GetPatternでIScrollProviderを取得
			var scrollProvider = peer.GetPattern(PatternInterface.Scroll) as IScrollProvider;

			if (scrollProvider.VerticallyScrollable)
			{
				scrollProvider.SetScrollPercent(
					// 水平スクロールは今の位置
					scrollProvider.HorizontalScrollPercent,
					// 垂直方向は先頭！（０％）
					0.0);
			}
		}
	}
}
