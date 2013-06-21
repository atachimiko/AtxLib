using AtxLib.Messaging.Windows;
using Livet.Behaviors.Messaging;
using Livet.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AtxLib.Behaviors.Actions.Windows
{
	/// <summary>
	/// Windowのサイズや位置の設定を行うアクションです。
	/// WindowScreenActionMessageに対応します。
	/// </summary>
	public class WindowScreenMessageAction : InteractionMessageAction<FrameworkElement>
	{
		protected override void InvokeAction(InteractionMessage message)
		{
			var windowScreenMessage = message as WindowScreenMessage;
			if (windowScreenMessage != null)
			{
				var window = Window.GetWindow(AssociatedObject);

				if (windowScreenMessage.WindowWidth > 0.0)
					window.Width = windowScreenMessage.WindowWidth;
			}
		}
	}
}
