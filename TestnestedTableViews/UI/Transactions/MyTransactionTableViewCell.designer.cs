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
	[Register ("MyTransactionTableViewCell")]
	partial class MyTransactionTableViewCell
	{
		[Outlet]
		UIKit.UIView ContentViewRef { get; set; }

		[Outlet]
		UIKit.UITableView MonthTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ContentViewRef != null) {
				ContentViewRef.Dispose ();
				ContentViewRef = null;
			}

			if (MonthTableView != null) {
				MonthTableView.Dispose ();
				MonthTableView = null;
			}
		}
	}
}
