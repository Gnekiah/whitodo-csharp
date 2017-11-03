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
using Microsoft.Win32;

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

        private void OuterTransparency_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            spdu.id = 2;
            OuterTransparencyValue.Content = OuterTransparency.Value;
            spdu.outerTransparency = OuterTransparency.Value;
            doSettings.Invoke(spdu);
        }

        private void InnerTransparency_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            spdu.id = 3;
            InnerTransparencyValue.Content = InnerTransparency.Value;
            spdu.innerTransparency = InnerTransparency.Value;
            doSettings.Invoke(spdu);
        }

        private Brush ChooseColor()
        {
            System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SolidColorBrush b = new SolidColorBrush();
                Color c = new Color();
                c.A = colorDialog.Color.A;
                c.R = colorDialog.Color.R;
                c.G = colorDialog.Color.G;
                c.B = colorDialog.Color.B;
                b.Color = c;
                return b;
            }
            return null;
        }

        private void OuterBrush_Click(object sender, RoutedEventArgs e)
        {
            spdu.id = 4;
            Brush b = ChooseColor();
            if (b == null) return;
            OuterBrush.Background = b;
            spdu.outerBrush = b;
            doSettings.Invoke(spdu);
        }

        private void InnerBrush_Click(object sender, RoutedEventArgs e)
        {
            spdu.id = 5;
            Brush b = ChooseColor();
            if (b == null) return;
            InnerBrush.Background = b;
            spdu.innerBrush = b;
            doSettings.Invoke(spdu);
        }

        private void Brush1_Click(object sender, RoutedEventArgs e)
        {
            spdu.id = 6;
            Brush b = ChooseColor();
            if (b == null) return;
            Brush1.Background = b;
            spdu.brush1 = b;
            doSettings.Invoke(spdu);
        }

        private void Brush2_Click(object sender, RoutedEventArgs e)
        {
            spdu.id = 7;
            Brush b = ChooseColor();
            if (b == null) return;
            Brush2.Background = b;
            spdu.brush2 = b;
            doSettings.Invoke(spdu);
        }

        private void Brush3_Click(object sender, RoutedEventArgs e)
        {
            spdu.id = 8;
            Brush b = ChooseColor();
            if (b == null) return;
            Brush3.Background = b;
            spdu.brush3 = b;
            doSettings.Invoke(spdu);
        }

        private void Brush4_Click(object sender, RoutedEventArgs e)
        {
            spdu.id = 9;
            Brush b = ChooseColor();
            if (b == null) return;
            Brush4.Background = b;
            spdu.brush4 = b;
            doSettings.Invoke(spdu);
        }

        private void Brush5_Click(object sender, RoutedEventArgs e)
        {
            spdu.id = 10;
            Brush b = ChooseColor();
            if (b == null) return;
            Brush5.Background = b;
            spdu.brush5 = b;
            doSettings.Invoke(spdu);
        }

        private void Brush6_Click(object sender, RoutedEventArgs e)
        {
            spdu.id = 11;
            Brush b = ChooseColor();
            if (b == null) return;
            Brush6.Background = b;
            spdu.brush6 = b;
            doSettings.Invoke(spdu);
        }
    }
}
