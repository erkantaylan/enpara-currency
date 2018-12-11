using Newtonsoft.Json;
using Prism.Mvvm;

namespace EnparaUsdCurrency.Models
{

    internal class Currency : BindableBase
    {
        private double buying;
        private string code;
        private string fullName;
        private string name;
        private double selling;
        private string updateDate;

        [JsonProperty("selling")]
        public double Selling
        {
            get => selling;
            set => SetProperty(ref selling, value);
        }

        [JsonProperty("buying")]
        public double Buying
        {
            get => buying;
            set => SetProperty(ref buying, value);
        }

        [JsonProperty("name")]
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        [JsonProperty("full_name")]
        public string FullName
        {
            get => fullName;
            set => SetProperty(ref fullName, value);
        }

        [JsonProperty("code")]
        public string Code
        {
            get => code;
            set => SetProperty(ref code, value);
        }

        [JsonProperty("update_date")]
        public string UpdateDate
        {
            get => updateDate;
            set => SetProperty(ref updateDate, value);
        }
    }

}