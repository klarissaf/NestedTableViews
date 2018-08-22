using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace TestnestedTableViews.UI.Transactions
{
    public partial class MyTransactionTableViewCell : UITableViewCell
    {
        private const int _initalExpandedSection = 0;
        public static readonly NSString Key = new NSString("MyTransactionTableViewCell");
        public static readonly UINib Nib = UINib.FromName("MyTransactionTableViewCell", NSBundle.MainBundle);

        protected MyTransactionTableViewCell(IntPtr handle) : base(handle){}

        public void SetTableViewDelegate(UIViewController transactionsViewController, ref bool[] isMonthSelectedArray, int selectedMonthCount)
        {
            MonthTableView.WeakDataSource = transactionsViewController;
            MonthTableView.WeakDelegate = transactionsViewController;
            MonthTableView.RegisterNibForCellReuse(MyTransactionCell.Nib, MyTransactionCell.KeyOne);
            MonthTableView.RegisterNibForHeaderFooterViewReuse(MyTransactionHeaderCell.Nib, MyTransactionHeaderCell.KeyTwo);
            MonthTableView.EstimatedRowHeight = 200.0f;
            MonthTableView.RowHeight = UITableView.AutomaticDimension;
            isMonthSelectedArray = new bool[selectedMonthCount];

            //if (_initalExpandedSection < isMonthSelectedArray.Length)
            //{
            //    isMonthSelectedArray[_initalExpandedSection] = true;
            //}


            MonthTableView.ReloadData();
        }

        //public static CGRect InsetRect(CGRect rect, UIEdgeInsets insets)
        //{
        //    return new CGRect(rect.X + insets.Left,
        //                      rect.Y + insets.Top,
        //                      rect.Width - insets.Left - insets.Right,
        //                      rect.Height - insets.Top - insets.Bottom);
        //}

        //public override void LayoutSubviews()
        //{
        //    base.LayoutSubviews();
        //    //ContentView.Frame = InsetRect(ContentView.Frame, new UIEdgeInsets(1, 0, 0, 0));
        //}
    }
}
