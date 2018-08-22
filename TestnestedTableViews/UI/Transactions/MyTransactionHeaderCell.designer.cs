// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace TestnestedTableViews.UI.Transactions
{
	[Register ("MyTransactionHeaderCell")]
	partial class MyTransactionHeaderCell
	{
		[Outlet]
		UIKit.UIView ContentViewRef { get; set; }

		[Outlet]
		UIKit.UILabel HeaderLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ContentViewRef != null) {
				ContentViewRef.Dispose ();
				ContentViewRef = null;
			}

			if (HeaderLabel != null) {
				HeaderLabel.Dispose ();
				HeaderLabel = null;
			}
		}
	}
}
