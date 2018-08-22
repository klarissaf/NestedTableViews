using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace TestnestedTableViews.UI.Transactions
{
    public partial class MyTransactionHeaderCell : UITableViewHeaderFooterView
    {
        public static readonly NSString KeyOne = new NSString("YearOneTransactionHeaderCell");
        public static readonly NSString KeyTwo = new NSString("YearTwoTransactionHeaderCell");
        public static readonly UINib Nib = UINib.FromName("MyTransactionHeaderCell", NSBundle.MainBundle);

        protected MyTransactionHeaderCell(IntPtr handle) : base(handle) { }



        public void UpdateHeader(String title, nfloat width, nfloat height)
        {
            HeaderLabel.Text = title;
            // There is a bug on iOS 9 not setting content width correct
            ContentViewRef.Frame = new CGRect(0, 0, width, height);
        }

        public void SetHeaderCellColor(UIColor color)
        {
            ContentView.BackgroundColor = color;
        }

        public void AddClickHandler(UITableView tableView, nint section, ref bool[] isSectionOpenArray, nint rowsInSection, int numberOfSections)
        {
            bool isSelectedItemOpen = isSectionOpenArray[(int)section];
            isSectionOpenArray = new bool[numberOfSections];  // Clear array (collapse all)
            isSectionOpenArray[(int)section] = !isSectionOpenArray[(int)section] && !isSelectedItemOpen;
            //this.AnimateDropDownIcon(animateOpen: isSectionOpenArray[(int)section]);

            // Animate the section cells
            var paths = new NSIndexPath[rowsInSection];
            for (int i = 0; i < paths.Length; i++)
            {
                paths[i] = NSIndexPath.FromItemSection(i, section);
            }

            tableView.ReloadData();
        }

        public override void RemoveGestureRecognizer(UIGestureRecognizer gestureRecognizer)
        {
            base.RemoveGestureRecognizer(gestureRecognizer);
        }

        //public void AnimateDropDownIcon(bool animateOpen)
        //{
        //    var rotationAngle = animateOpen ? (nfloat)Math.PI : 180 * (nfloat)Math.PI;
        //    HeaderDropDownImageIcon.Transform = CGAffineTransform.MakeRotation(rotationAngle);
        //}
    }
}
