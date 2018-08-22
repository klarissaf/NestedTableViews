using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using TestnestedTableViews.UI.Transactions;
using UIKit;

namespace TestnestedTableViews
{
    public partial class ViewController : UIViewController
    {
        Dictionary<string, Dictionary<string, List<string>>> _transactions = new Dictionary<string, Dictionary<string, List<string>>>();
        int selectedYear;
        int selectedMonth;
        bool[] isYearSelectedArray;
        bool[] isMonthSelectedArray;

        private const int _initalExpandedSection = 0; // 1st section is initally expanded

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            MainTableView.RegisterNibForCellReuse(MyTransactionTableViewCell.Nib, MyTransactionTableViewCell.Key);
            MainTableView.RegisterNibForHeaderFooterViewReuse(MyTransactionHeaderCell.Nib, MyTransactionHeaderCell.KeyOne);
            MainTableView.EstimatedRowHeight = 200.0f;
            MainTableView.RowHeight = UITableView.AutomaticDimension;
            GetData();
        }

        private void GetData()
        {
            selectedYear = 0;
            selectedMonth = -1;
            MainTableView.WeakDataSource = this;
            MainTableView.WeakDelegate = this;
            MockData();
            isYearSelectedArray = new bool[_transactions.ElementAt(selectedYear).Value.Count];
            if (_initalExpandedSection < isYearSelectedArray.Length)
            {
                isYearSelectedArray[_initalExpandedSection] = true;
            }
            MainTableView.ReloadData();
        }

        private void MockData()
        {
            for (int g = 0; g < 2; g++)
            {
                Dictionary<string, List<string>> monthItems = new Dictionary<string, List<string>>();
                for (int h = 0; h < 3; h++)
                {
                    List<string> transactionItems = new List<string>();
                    for (int i = 0; i < 4; i++)
                    {
                        if(h == 2)
                        {
                            transactionItems.Add("Item a" + i);
                            transactionItems.Add("Item b" + i);
                        }
                        else
                            transactionItems.Add("Item" + i);
                    }
                    monthItems.Add("Month" + h, transactionItems);
                }
                _transactions.Add("Year" + g, monthItems);
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }

    public partial class ViewController : IUITableViewDataSource, IUITableViewDelegate
    {
        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            if (tableView.Tag == 1)
            {
                var cell = tableView.DequeueReusableCell(MyTransactionTableViewCell.Key) as MyTransactionTableViewCell;
                cell.SetTableViewDelegate(this, ref isMonthSelectedArray, _transactions.ElementAt(selectedYear).Value.Count);
                return cell;
            }
            else
            {
                var cell = tableView.DequeueReusableCell(MyTransactionCell.KeyOne) as MyTransactionCell;
                cell.UpdateRow(_transactions.ElementAt(selectedYear).Value.ElementAt(indexPath.Section).Value[indexPath.Row], indexPath.Row);
                cell.BackgroundColor = UIColor.White;
                cell.SelectionStyle = UITableViewCellSelectionStyle.None;
                return cell;
            }
        }

        [Export("numberOfSectionsInTableView:")]
        public nint NumberOfSections(UITableView tableView)
        {
            if (tableView.Tag == 1)
                return _transactions.Count;
            else
                return _transactions.ElementAt(selectedYear).Value.Count;
        }

        public nint RowsInSection(UITableView tableView, nint section)
        {
            var rowsInSection = 0;
            if (tableView.Tag == 1)
                rowsInSection = isYearSelectedArray[(int)section] ? 1 : 0;
            else
                rowsInSection = isMonthSelectedArray[(int)section] ? _transactions.ElementAt(selectedYear).Value.ElementAt((int)section).Value.Count : 0;
            return rowsInSection;
        }


        [Export("tableView:didSelectRowAtIndexPath:")]
        public void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            
        }

        [Export("tableView:heightForHeaderInSection:")]
        public nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            //return 36f;
            if (tableView.Tag == 1)
                return 38f;
            else
                return 36f;
        }

        [Export("tableView:heightForRowAtIndexPath:")]
        public nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            if (tableView.Tag == 1)
                return CalculateRowHeight();
            else
                return 36f;
                
            //return UITableView.AutomaticDimension;
        }

        private nfloat CalculateRowHeight()
        {
            var monthsInSection = _transactions.ElementAt(selectedYear).Value.Count;
            var rowsInSection = selectedMonth > -1 ? (isMonthSelectedArray[selectedMonth] ? _transactions.ElementAt(selectedYear).Value.ElementAt((int)selectedMonth).Value.Count : 0) : 0;
                

            var rowHeight = rowsInSection * 36f;
            var monthHeight = monthsInSection * 36f;
            var height = (rowHeight + monthHeight);//+44f for year header

            return height;
        }

        [Export("tableView:viewForHeaderInSection:")]
        public UIView GetViewForHeader(UITableView tableView, nint section)
        {
            if (tableView.Tag == 1)
            {
                selectedYear = (int)section;
                var header = tableView.DequeueReusableHeaderFooterView(MyTransactionHeaderCell.KeyOne) as MyTransactionHeaderCell;

                header.UpdateHeader(_transactions.ElementAt((int)section).Key, tableView.Frame.Width, GetHeightForHeader(tableView, section));
                header.SetHeaderCellColor(UIColor.DarkGray);
                //header.AnimateDropDownIcon(animateOpen: isSectionOpenArray[(int)section]);
                if (header.GestureRecognizers != null)
                {
                    foreach (var gesture in header.GestureRecognizers)
                        header.RemoveGestureRecognizer(gesture);
                }
                header.AddGestureRecognizer(new UITapGestureRecognizer((recognizer) =>
                {
                    header.AddClickHandler(tableView, section, ref isYearSelectedArray, RowsInSection(tableView, section), _transactions.Count);
                }));
                return header;
            }
            else
            {
                selectedMonth = (int)section;
                var header = tableView.DequeueReusableHeaderFooterView(MyTransactionHeaderCell.KeyTwo) as MyTransactionHeaderCell;

                header.UpdateHeader(_transactions.ElementAt(selectedYear).Value.ElementAt((int)section).Key, tableView.Frame.Width, GetHeightForHeader(tableView, section));
                header.SetHeaderCellColor(UIColor.LightGray);
                //header.AnimateDropDownIcon(animateOpen: isMonthSelectedArray[(int)section]);
                if (header.GestureRecognizers != null)
                {
                    foreach (var gesture in header.GestureRecognizers)
                        header.RemoveGestureRecognizer(gesture);
                }
                header.AddGestureRecognizer(new UITapGestureRecognizer((recognizer) =>
                {
                    header.AddClickHandler(tableView, section, ref isMonthSelectedArray, RowsInSection(tableView, section), _transactions.ElementAt(selectedYear).Value.Count);
                }));
                return header;
            }
        }
    }

}
