using Livet.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AtxLib.Messaging.Windows
{
	/// <summary>
	/// Windowのサイズや位置を変更するための相互作用メッセージです。
	/// </summary>
	public class WindowScreenMessage : InteractionMessage
	{
		public WindowScreenMessage() { }

		/// <summary>
		/// メッセージキーを指定して新しい相互作用メッセージのインスタンスを生成します。
		/// </summary>
		/// <param name="messageKey">メッセージキー</param>
		public WindowScreenMessage(string messageKey)
			: base(messageKey) { }

		/// <summary>
		/// 派生クラスでは必ずオーバーライドしてください。Freezableオブジェクトとして必要な実装です。<br/>
		/// 通常このメソッドは、自身の新しいインスタンスを返すように実装します。
		/// </summary>
		/// <returns>自身の新しいインスタンス</returns>
		protected override Freezable CreateInstanceCore()
		{
			return new WindowScreenMessage(MessageKey);
		}

		public Double WindowWidth
		{
			get { return (Double)GetValue(WindowWidthProperty); }
			set { SetValue(WindowWidthProperty, value); }
		}

		public static readonly DependencyProperty WindowWidthProperty =
			DependencyProperty.Register("WindowWidth", typeof(Double), typeof(WindowScreenMessage), new PropertyMetadata(0.0));

	}
}
