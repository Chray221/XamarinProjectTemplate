using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Timers;
using Prism.Commands;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamProjectTemplate.Extension;

namespace XamProjectTemplate.Views
{
    public partial class CalculatorPage 
    {
        public ObservableCollection<string> CalculationHistory { get; } = new ObservableCollection<string>();

        public DelegateCommand<object> ButtonCommand { get { return new DelegateCommand<object>(Execute_ButtonCommand); } }

        public CalculatorPage()
        {
            InitializeComponent();
        }

        private void Execute_ButtonCommand(object obj)
        {
            Vibration.Vibrate(250);
            if (obj is string inputKey)
            {
                switch (inputKey)
                {

                    case "arrow-left":
                        EntryLabel.Text = string.IsNullOrEmpty(EntryLabel.Text) ? "" : EntryLabel.Text.Substring(0, EntryLabel.Text.Length - 1);
                        break;
                    case "C":
                        EntryLabel.Text = "";
                        break;
                    case "=":
                        if (!string.IsNullOrEmpty(EntryLabel.Text))
                        {
                            string calculation = EntryLabel.Text.Compute<string>();
                            if (Regex.IsMatch(calculation, "\\d"))
                            {
                                CalculationHistory.Add($"{ EntryLabel.Text}");
                                CalcuationHistoryCollectionView.ScrollTo(CalculationHistory.Count - 1);
                                EntryLabel.Text = calculation;
                            }
                        }
                        break;
                    default:
                        EntryLabel.Text += inputKey;
                        break;
                }

            }
        }

        void CalcuationHistoryCollectionView_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            if (CalcuationHistoryCollectionView.SelectedItem != null)
            {
                EntryLabel.Text = CalcuationHistoryCollectionView.SelectedItem.ToString();
                CalcuationHistoryCollectionView.SelectedItem = null;
            }
        }
    }
}
