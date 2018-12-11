using System.Windows;
using EnparaUsdCurrency.Models;
using MahApps.Metro.Controls.Dialogs;
using Prism.Mvvm;

namespace EnparaUsdCurrency.ViewModels
{

    internal class DifferenceAlarmViewModel : BindableBase
    {
        private readonly Currency currency;
        private readonly IDialogCoordinator dialogCoordinator;

        private bool alarmStarted;
        private double currentProfit;
        private double desiredProfit;

        private double startTry;
        private double startUsd;

        public DifferenceAlarmViewModel(Currency currency, IDialogCoordinator dialogCoordinator)
        {
            this.currency = currency;
            this.dialogCoordinator = dialogCoordinator;
            currency.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(currency.Selling))
                {
                    SellingPriceChanged();
                }
            };
        }

        public double StartUsd
        {
            get => startUsd;
            set => SetProperty(ref startUsd, value);
        }

        public double StartTry
        {
            get => startTry;
            set => SetProperty(ref startTry, value);
        }

        public double DesiredProfit
        {
            get => desiredProfit;
            set => SetProperty(ref desiredProfit, value);
        }

        public double CurrentProfit
        {
            get => currentProfit;
            set => SetProperty(ref currentProfit, value);
        }

        public bool AlarmStarted
        {
            get => alarmStarted;
            set
            {
                SetProperty(ref alarmStarted, value);
                SetCurrentProfit();
            }
        }


        private void SellingPriceChanged()
        {
            SetCurrentProfit();
        }

        /// <summary>
        ///     Profit = Usd Selling Ratio * Starting Amount of Usd - Starting TRY Price
        /// </summary>
        /// <param name="selling"></param>
        /// <param name="usd"></param>
        /// <param name="try"></param>
        /// <returns></returns>
        private static double CalculateCurrentProfit(double selling, double usd, double @try)
        {
            return selling * usd - @try;
        }

        private void SetCurrentProfit()
        {
            if (!AlarmStarted)
            {
                return;
            }

            CurrentProfit = CalculateCurrentProfit(currency.Selling, StartUsd, StartTry);

            if (CurrentProfit >= DesiredProfit)
            {
                AlarmUser();
            }
        }

        private void AlarmUser()
        {
            if (Application.Current.MainWindow == null)
            {
                return;
            }

            Application.Current.MainWindow.WindowState = WindowState.Minimized;
            Application.Current.MainWindow.WindowState = WindowState.Normal;
            Application.Current.MainWindow.Focus();
            Application.Current.MainWindow.Activate();

            dialogCoordinator.ShowMessageAsync(this, "ALARM", $"REACHED TO DESIRED PROFIT: {CurrentProfit}");
        }
    }

}