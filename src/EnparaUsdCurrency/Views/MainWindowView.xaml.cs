using Prism.Regions;

namespace EnparaUsdCurrency.Views
{

    internal partial class MainWindow
    {
        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();
            regionManager.RegisterViewWithRegion("CalculationsRegion", typeof(EasyCalculationsView));
            regionManager.RegisterViewWithRegion("AlarmRegion", typeof(DifferenceAlarmView));
        }
    }

}