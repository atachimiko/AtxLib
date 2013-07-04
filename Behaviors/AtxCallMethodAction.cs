using Livet.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtxLib.Behaviors
{
	public class AtxCallMethodAction : LivetCallMethodAction
	{
		private MethodBinderWithArgument _callbackMethod = new MethodBinderWithArgument();

		protected override void Invoke(object parameter)
		{
			_callbackMethod.Invoke(MethodTarget, MethodName, parameter);
		}
	}
}
