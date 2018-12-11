using Prism.Events;

// ReSharper disable once CheckNamespace
namespace EnparaUsdCurrency
{

    internal class Events
    {
        public class SellingChangedEvent : PubSubEvent<double>
        { }

        public class BuyingChangedEvent : PubSubEvent<double>
        { }
    }

}