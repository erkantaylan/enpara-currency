using System.ComponentModel;
using EnparaUsdCurrency.Models;
using Prism.Mvvm;

namespace EnparaUsdCurrency.ViewModels
{

    internal class EasyCalculationsViewModel : BindableBase
    {
        private readonly Currency currency;
        private double buyingAmount;
        private double buyingResult;
        private double sellingAmount;
        private double sellingResult;

        public EasyCalculationsViewModel(Currency currency)
        {
            this.currency = currency;
            currency.PropertyChanged += CurrencyOnPropertyChanged;
        }

        public double SellingAmount
        {
            get => sellingAmount;
            set
            {
                SetProperty(ref sellingAmount, value);
                SellingResult = CalculateTotal(currency.Selling, SellingAmount);
            }
        }

        public double BuyingAmount
        {
            get => buyingAmount;
            set
            {
                SetProperty(ref buyingAmount, value);
                BuyingResult = CalculateTotal(currency.Selling, BuyingAmount);
            }
        }

        public double SellingResult
        {
            get => sellingResult;
            set => SetProperty(ref sellingResult, value);
        }

        public double BuyingResult
        {
            get => buyingResult;
            set => SetProperty(ref buyingResult, value);
        }

        private void CurrencyOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Currency.Selling))
            {
                SellingResult = CalculateTotal(currency.Selling, SellingAmount);
            }
            else if (e.PropertyName == nameof(Currency.Buying))
            {
                BuyingResult = CalculateTotal(currency.Buying, BuyingAmount);
            }
        }

        private static double CalculateTotal(double ratio, double amount)
        {
            return ratio * amount;
        }
    }

}