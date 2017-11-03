using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace whitodo_csharp
{
    /// <summary>
    /// SettingDialog.xaml 的交互逻辑
    /// </summary>
    public partial class SettingDialog : Window
    {
        public delegate void DoSettings(SettingsPDU spdu);
        DoSettings doSettings;
        SettingsPDU spdu;

        public SettingDialog(DoSettings _doSettings, SettingsPDU _spdu)
        {
            InitializeComponent();
            doSettings = _doSettings;
            spdu = _spdu;
            OuterWidth.Value = spdu.outerWidth;
            OuterWidthValue.Content = spdu.outerWidth;
            InnerWidth.Value = spdu.innerWidth;
            InnerWidthValue.Content = spdu.innerWidth;
            InnerHeight.Value = spdu.innerHeight;
            InnerHeightValue.Content = spdu.innerHeight;
            OuterTransparency.Value = spdu.outerTransparency;
            OuterTransparencyValue.Content = spdu.outerTransparency;
            InnerTransparency.Value = spdu.innerTransparency;
            InnerTransparencyValue.Content = spdu.innerTransparency;
            OuterBrush.Background = spdu.outerBrush;
            InnerBrush.Background = spdu.innerBrush;
            Brush1.Background = spdu.brush1;
            Brush2.Background = spdu.brush2;
            Brush3.Background = spdu.brush3;
            Brush4.Background = spdu.brush4;
            Brush5.Background = spdu.brush5;
            Brush6.Background = spdu.brush6;
            InnerWidth.Minimum = 100;
            InnerHeight.Minimum = 100;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OuterWidth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            spdu.id = 1;
            OuterWidthValue.Content = OuterWidth.Value;
            spdu.outerWidth = OuterWidth.Value;
            doSettings.Invoke(spdu);
        }

        private void InnerWidth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            spdu.id = 2;
            InnerWidthValue.Content = InnerWidth.Value;
            spdu.innerWidth = InnerWidth.Value;
            doSettings.Invoke(spdu);
        }    

        private void InnerHeight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            spdu.id = 3;
            InnerHeightValue.Content = InnerHeight.Value;
            spdu.innerHeight = InnerHeight.Value;
            doSettings.Invoke(spdu);
        }

        private void OuterTransparency_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            spdu.id = 4;
            OuterTransparencyValue.Content = OuterTransparency.Value;
            spdu.outerTransparency = OuterTransparency.Value;
            doSettings.Invoke(spdu);
        }

        private void InnerTransparency_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            spdu.id = 5;
            InnerTransparencyValue.Content = InnerTransparency.Value;
            spdu.innerTransparency = InnerTransparency.Value;
            doSettings.Invoke(spdu);
        }
    }
}
