using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace TestnestedTableViews.UI.Transactions
{
    public partial class MyTransactionCell : UITableViewCell
    {
        public static readonly NSString KeyOne = new NSString("MyTransactionCellOne");
        public static readonly NSString KeyTwo = new NSString("MyTransactionCellTwo");
        public static readonly UINib Nib = UINib.FromName("MyTransactionCell", NSBundle.MainBundle);

        protected MyTransactionCell(IntPtr handle) : base(handle) { }

        public void UpdateRow(string labelText, int position)
        {
            TransactionLabel.Text = labelText;
        }

        public static CGRect InsetRect(CGRect rect, UIEdgeInsets insets)
        {
            return new CGRect(rect.X + insets.Left,
                              rect.Y + insets.Top,
                              rect.Width - insets.Left - insets.Right,
                              rect.Height - insets.Top - insets.Bottom);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            ContentView.Frame = InsetRect(ContentView.Frame, new UIEdgeInsets(1, 0, 0, 0));
        }
    }

}
