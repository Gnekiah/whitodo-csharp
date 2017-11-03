using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Forms;

namespace whitodo_csharp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool canmove = true;
        private Point mouseOffset;
        private Point startControlArea;
        private Point endControlArea;
        private Point startTextArea;
        private Point endTextArea;
        public SettingsPDU spdu = new SettingsPDU();
        
        public int InitWhitodo()
        {
            int ret = 0x00;
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Global.mainDirPath = Path.Combine(appDataPath, Global.mainDirName);
            if (!Directory.Exists(Global.mainDirPath))
            {
                Directory.CreateDirectory(Global.mainDirPath);
            }

            Global.cacheDirPath = Path.Combine(Global.mainDirPath, Global.cacheDirName);
            if (!Directory.Exists(Global.cacheDirPath))
            {
                Directory.CreateDirectory(Global.cacheDirPath);
            }

            Global.cfgFilePath = Path.Combine(Global.mainDirPath, Global.cfgFileName);
            Global.txtFilePath = Path.Combine(Global.mainDirPath, Global.txtFileName);

            if (!File.Exists(Global.cfgFilePath))
            {
                ret = 0x01;
                goto LOADTEXT;
            }
            string[] cfglines = File.ReadAllLines(@Global.cfgFilePath, Encoding.UTF8);
            BrushConverter brushConverter = new BrushConverter();
            foreach (string i in cfglines)
            {
                string[] ii = i.Split(new char[] { '=' });
                if (ii.Length != 2)
                    continue;
                switch (ii[0])
                {
                    case "window_width":
                        this.Width = double.Parse(ii[1]);
                        break;
                    case "window_height":
                        this.Height = double.Parse(ii[1]);
                        break;
                    case "outer_width":
                        spdu.outerWidth = double.Parse(ii[1]);
                        break;
                    case "outer_opacity":
                        spdu.outerTransparency = double.Parse(ii[1]);
                        break;
                    case "inner_opacity":
                        spdu.innerTransparency = double.Parse(ii[1]);
                        break;
                    case "outer_brush":
                        
                        spdu.outerBrush = (Brush)brushConverter.ConvertFromString(ii[1]);
                        break;
                    case "inner_brush":
                        spdu.innerBrush = (Brush)brushConverter.ConvertFromString(ii[1]);
                        break;
                    case "brush1":
                        spdu.brush1 = (Brush)brushConverter.ConvertFromString(ii[1]);
                        break;
                    case "brush2":
                        spdu.brush2 = (Brush)brushConverter.ConvertFromString(ii[1]);
                        break;
                    case "brush3":
                        spdu.brush3 = (Brush)brushConverter.ConvertFromString(ii[1]);
                        break;
                    case "brush4":
                        spdu.brush4 = (Brush)brushConverter.ConvertFromString(ii[1]);
                        break;
                    case "brush5":
                        spdu.brush5 = (Brush)brushConverter.ConvertFromString(ii[1]);
                        break;
                    case "brush6":
                        spdu.brush6 = (Brush)brushConverter.ConvertFromString(ii[1]);
                        break;
                    default:
                        break;
                }
            }
LOADTEXT:
            if (!File.Exists(Global.txtFilePath))
            {
                ret = ret & 0x10;
                goto EXRET;
            }
            using (FileStream stream = File.OpenRead(Global.txtFilePath))
            {
                TextRange docTextRange = new TextRange(WhitodoText.Document.ContentStart, WhitodoText.Document.ContentEnd);
                docTextRange.Load(stream, System.Windows.DataFormats.Rtf);
            }
EXRET:
            return ret;
        }

        public void HideControlPanel()
        {
            ControlPanelBackground.Visibility = Visibility.Hidden;
            ControlPanel.Visibility = Visibility.Hidden;
            WhitodoText.IsReadOnly = true;
            WhitodoText.SelectionBrush = null;
            this.ResizeMode = ResizeMode.NoResize;
            canmove = false;
        }

        public void ShowControlPanel()
        {
            ControlPanelBackground.Visibility = Visibility.Visible;
            ControlPanel.Visibility = Visibility.Visible;
            WhitodoText.IsReadOnly = false;
            WhitodoText.SelectionBrush = Brushes.LightPink;
            this.ResizeMode = ResizeMode.CanResizeWithGrip;
            canmove = true;
        }

        public void AddNotifyMenu(NotifyIcon notifyIcon)
        {
            System.Windows.Forms.ContextMenu notifyMenu = new System.Windows.Forms.ContextMenu();
            System.Windows.Forms.MenuItem openMainWindow = new System.Windows.Forms.MenuItem();
            System.Windows.Forms.MenuItem close = new System.Windows.Forms.MenuItem();

            openMainWindow.Text = "打开控制栏";
            close.Text = "退出";

            openMainWindow.Click += new EventHandler(delegate { ShowControlPanel(); });
            close.Click += new EventHandler(delegate { this.Close(); });

            notifyMenu.MenuItems.Add(openMainWindow);
            notifyMenu.MenuItems.Add(close);
            notifyIcon.ContextMenu = notifyMenu;
        }

        public void CalcRectArea()
        {
            startTextArea = WhitodoText.TranslatePoint(new Point(0, 0), this);
            endTextArea.X = startTextArea.X + WhitodoText.Width;
            endTextArea.Y = startTextArea.Y + WhitodoText.Height;
            startControlArea = RedButton.TranslatePoint(new Point(0, 0), this);
            endControlArea = WhitodoButton.TranslatePoint(new Point(0, 0), this);
            endControlArea.X += WhitodoButton.Width;
            endControlArea.Y += WhitodoButton.Height;
            /* DEBUG INFO
            string x = "startTextArea=" + startTextArea.X.ToString() + "," + startTextArea.Y.ToString();
            x += "\nendTextArea=" + endTextArea.X.ToString() + "," + endTextArea.Y.ToString();
            x += "\nstartControlArea=" + startControlArea.X.ToString() + "," + startControlArea.Y.ToString();
            x += "\nendControlArea=" + endControlArea.X.ToString() + "," + endControlArea.Y.ToString();
            System.Windows.MessageBox.Show(x);
            */
        }

        
        public void SetSPDU()
        {
            spdu.outerWidth = spdu.outerWidth == -1 ? TextPanelInnerBackground.Margin.Left - TextPanelOuterBackground.Margin.Left : spdu.outerWidth;
            spdu.outerTransparency = spdu.outerTransparency == -1 ? TextPanelOuterBackground.Opacity : spdu.outerTransparency;
            spdu.innerTransparency = spdu.innerTransparency == -1 ? TextPanelInnerBackground.Opacity : spdu.innerTransparency;
            spdu.outerBrush = spdu.outerBrush == null ? TextPanelOuterBackground.Background : spdu.outerBrush;
            spdu.innerBrush = spdu.innerBrush == null ? TextPanelInnerBackground.Background : spdu.innerBrush;
            spdu.brush1 = spdu.brush1 == null ? RedButton.Background : spdu.brush1;
            spdu.brush2 = spdu.brush2 == null ? BlueButton.Background : spdu.brush2;
            spdu.brush3 = spdu.brush3 == null ? GreenButton.Background : spdu.brush3;
            spdu.brush4 = spdu.brush4 == null ? YellowButton.Background : spdu.brush4;
            spdu.brush5 = spdu.brush5 == null ? WhiteButton.Background : spdu.brush5;
            spdu.brush6 = spdu.brush6 == null ? BlackButton.Background : spdu.brush6;

            TextPanelOuterBackground.Opacity = spdu.outerTransparency;
            TextPanelInnerBackground.Opacity = spdu.innerTransparency;
            TextPanelOuterBackground.Background = spdu.outerBrush;
            TextPanelInnerBackground.Background = spdu.innerBrush;
            RedButton.Background = spdu.brush1;
            BlueButton.Background = spdu.brush2;
            GreenButton.Background = spdu.brush3;
            YellowButton.Background = spdu.brush4;
            WhiteButton.Background = spdu.brush5;
            BlackButton.Background = spdu.brush6;

            spdu.id = 1;
            DoSettings(spdu);
        }
        /*
        public void InitPanelBySPDU()
        {
            for (int i = 1; i < 14; i++)
            {
                spdu.id = i;
                DoSettings(spdu);
            }
        }
        */

        public MainWindow()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            FuckNotifyIcon FuckNotify = new FuckNotifyIcon();
            AddNotifyMenu(FuckNotify.GetNotifyIcon());
            int ret = InitWhitodo();
            SetSPDU();
        }

        private void RedButton_Click(object sender, RoutedEventArgs e)
        {
            TextSelection text = this.WhitodoText.Selection;
            if (!text.IsEmpty)
            {
                text.ApplyPropertyValue(System.Windows.Controls.RichTextBox.ForegroundProperty, RedButton.Background);
            }
            this.WhitodoText.Focus();
        }

        private void BlueButton_Click(object sender, RoutedEventArgs e)
        {
            TextSelection text = this.WhitodoText.Selection;
            if (!text.IsEmpty)
            {
                text.ApplyPropertyValue(System.Windows.Controls.RichTextBox.ForegroundProperty, BlueButton.Background);
            }
            this.WhitodoText.Focus();
        }

        private void GreenButton_Click(object sender, RoutedEventArgs e)
        {
            TextSelection text = this.WhitodoText.Selection;
            if (!text.IsEmpty)
            {
                text.ApplyPropertyValue(System.Windows.Controls.RichTextBox.ForegroundProperty, GreenButton.Background);
            }
            this.WhitodoText.Focus();
        }

        private void YellowButton_Click(object sender, RoutedEventArgs e)
        {
            TextSelection text = this.WhitodoText.Selection;
            if (!text.IsEmpty)
            {
                text.ApplyPropertyValue(System.Windows.Controls.RichTextBox.ForegroundProperty, YellowButton.Background);
            }
            this.WhitodoText.Focus();
        }

        private void WhiteButton_Click(object sender, RoutedEventArgs e)
        {
            TextSelection text = this.WhitodoText.Selection;
            if (!text.IsEmpty)
            {
                text.ApplyPropertyValue(System.Windows.Controls.RichTextBox.ForegroundProperty, WhiteButton.Background);
            }
            this.WhitodoText.Focus();
        }

        private void BlackButton_Click(object sender, RoutedEventArgs e)
        {
            TextSelection text = this.WhitodoText.Selection;
            if (!text.IsEmpty)
            {
                text.ApplyPropertyValue(System.Windows.Controls.RichTextBox.ForegroundProperty, BlackButton.Background);
            }
            this.WhitodoText.Focus();
        }

        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            TextSelection text = this.WhitodoText.Selection;
            if (!text.IsEmpty)
            {
                bool isBold = false;
                try
                {
                    FontWeight fw = (FontWeight)text.GetPropertyValue(System.Windows.Controls.RichTextBox.FontWeightProperty);
                    if (fw == FontWeights.Bold)
                    {
                        isBold = true;
                    }
                }
                catch (Exception) { }
                if (isBold)
                {
                    text.ApplyPropertyValue(System.Windows.Controls.RichTextBox.FontWeightProperty, FontWeights.Normal);
                    this.BoldButton.Background = Brushes.White;
                }
                else
                {
                    text.ApplyPropertyValue(System.Windows.Controls.RichTextBox.FontWeightProperty, FontWeights.Bold);
                    this.BoldButton.Background = Brushes.LightPink;
                }
            }
            this.WhitodoText.Focus();
        }

        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            TextSelection text = this.WhitodoText.Selection;
            if (!text.IsEmpty)
            {
                bool isItalic = false;
                try
                {
                    FontStyle fs = (FontStyle)text.GetPropertyValue(System.Windows.Controls.RichTextBox.FontStyleProperty);
                    if (fs == FontStyles.Italic)
                    {
                        isItalic = true;
                    }
                }
                catch (Exception) { }
                if (isItalic)
                {
                    text.ApplyPropertyValue(System.Windows.Controls.RichTextBox.FontStyleProperty, FontStyles.Normal);
                    this.ItalicButton.Background = Brushes.White;
                }
                else
                {
                    text.ApplyPropertyValue(System.Windows.Controls.RichTextBox.FontStyleProperty, FontStyles.Italic);
                    this.ItalicButton.Background = Brushes.LightPink;
                }
            }
            this.WhitodoText.Focus();
        }

        private void UnderLineButton_Click(object sender, RoutedEventArgs e)
        {
            TextSelection text = this.WhitodoText.Selection;
            if (!text.IsEmpty)
            {
                bool isUnderline = false;
                try
                {
                    TextDecorationCollection ul = (TextDecorationCollection)text.GetPropertyValue(TextBlock.TextDecorationsProperty);
                    if (ul == TextDecorations.Underline)
                    {
                        isUnderline = true;
                    }
                }
                catch (Exception) { }
                if (isUnderline)
                {
                    text.ApplyPropertyValue(TextBlock.TextDecorationsProperty, null);
                    this.UnderLineButton.Background = Brushes.White;
                }
                else
                {
                    text.ApplyPropertyValue(TextBlock.TextDecorationsProperty, TextDecorations.Underline);
                    this.UnderLineButton.Background = Brushes.LightPink;
                }
            }
            this.WhitodoText.Focus();
        }

        private void WhitodoText_SelectionChanged(object sender, RoutedEventArgs e)
        {
            TextSelection text = this.WhitodoText.Selection;
            if (!text.IsEmpty)
            {
                bool isBold = false;
                bool isItalic = false;
                bool isUnderline = false;
                try
                {
                    TextDecorationCollection ul = (TextDecorationCollection)text.GetPropertyValue(TextBlock.TextDecorationsProperty);
                    if (ul == TextDecorations.Underline)
                    {
                        isUnderline = true;
                    }
                }
                catch (Exception) { }
                try
                {
                    FontWeight fw = (FontWeight)text.GetPropertyValue(System.Windows.Controls.RichTextBox.FontWeightProperty);
                    if (fw == FontWeights.Bold)
                    {
                        isBold = true;
                    }
                }
                catch (Exception) { }
                try
                {
                    FontStyle fs = (FontStyle)text.GetPropertyValue(System.Windows.Controls.RichTextBox.FontStyleProperty);
                    if (fs == FontStyles.Italic)
                    {
                        isItalic = true;
                    }
                }
                catch (Exception) { }
                this.BoldButton.Background = isBold ? Brushes.LightPink : Brushes.White;
                this.ItalicButton.Background = isItalic ? Brushes.LightPink : Brushes.White;
                this.UnderLineButton.Background = isUnderline ? Brushes.LightPink : Brushes.White;
            }
            this.WhitodoText.Focus();
        }

        private void FontSizeButton_Loaded(object sender, RoutedEventArgs e)
        {
            int[] fontsize = { 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 32, 36, 40, 48, 56, 64, 72 };
            foreach (int i in fontsize)
            {
                FontSizeButton.Items.Add(i.ToString());
            }
        }

        private void FontTypeButton_Loaded(object sender, RoutedEventArgs e)
        {
            LinkedList<string> fontnames = new LinkedList<string>();
            foreach (FontFamily font in Fonts.SystemFontFamilies)
            {
                LanguageSpecificStringDictionary fontname = font.FamilyNames;
                string _fontname = null;
                if (fontname.ContainsKey(System.Windows.Markup.XmlLanguage.GetLanguage("zh-cn")) &&
                    fontname.TryGetValue(System.Windows.Markup.XmlLanguage.GetLanguage("zh-cn"), out _fontname))
                {
                    fontnames.AddLast(_fontname);
                }
                else
                {
                    FontTypeButton.Items.Add(font.Source);
                }
            }
            foreach (string fontname in fontnames)
            {
                FontTypeButton.Items.Add(fontname);
            }
        }

        private void FontTypeButton_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextSelection text = this.WhitodoText.Selection;
            if (!text.IsEmpty)
            {
                try
                {
                    FontFamily font = new FontFamily(FontTypeButton.SelectedItem.ToString());
                    text.ApplyPropertyValue(TextBlock.FontFamilyProperty, font);
                }
                catch (Exception) { }
            }
            this.WhitodoText.Focus();
        }

        private void FontSizeButton_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextSelection text = this.WhitodoText.Selection;
            if (!text.IsEmpty)
            {
                try
                {
                    text.ApplyPropertyValue(TextBlock.FontSizeProperty, FontSizeButton.SelectedItem);
                }
                catch (Exception) { }
            }
            this.WhitodoText.Focus();
        }

        private void SettingButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ScaleTransform stf = new ScaleTransform();
            stf.ScaleX = 2;
            stf.ScaleY = 2;
            SettingButton.RenderTransformOrigin = new Point(0.5, 0.5);
            SettingButton.RenderTransform = stf;
        }

        private void SettingButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ScaleTransform stf = new ScaleTransform();
            stf.ScaleX = 1.0;
            stf.ScaleY = 1.0;
            SettingButton.RenderTransformOrigin = new Point(0.5, 0.5);
            SettingButton.RenderTransform = stf;
        }

        private void WhitodoButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ScaleTransform stf = new ScaleTransform();
            stf.ScaleX = 2;
            stf.ScaleY = 2;
            WhitodoButton.RenderTransformOrigin = new Point(0.5, 0.5);
            WhitodoButton.RenderTransform = stf;
        }

        private void WhitodoButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ScaleTransform stf = new ScaleTransform();
            stf.ScaleX = 1.0;
            stf.ScaleY = 1.0;
            WhitodoButton.RenderTransformOrigin = new Point(0.5, 0.5);
            WhitodoButton.RenderTransform = stf;
        }

        private void BigerButton_Click(object sender, RoutedEventArgs e)
        {
            /* 设置增大行距 */
            /*
            TextPointer tp = this.WhitodoText.CaretPosition.GetLineStartPosition(0);
            TextBlock tb = new TextBlock();
            if (!text.IsEmpty)
            {
                try
                {
                    text.ApplyPropertyValue(TextBlock.LineHeightProperty, 12);
                }
                catch (Exception) { }
            }
            this.WhitodoText.Focus();
            */
        }

        private void LowerButton_Click(object sender, RoutedEventArgs e)
        {
            /* 设置缩小行距 */
            /*
            TextSelection text = this.WhitodoText.Selection;
            if (!text.IsEmpty)
            {
                try
                {
                    text.ApplyPropertyValue(TextBlock.LineHeightProperty, 12);
                }
                catch (Exception) { }
            }
            this.WhitodoText.Focus();
            */
        }
        
        public void DoSettings(SettingsPDU spdu)
        {
            double width = 0;
            Thickness margin;

            switch (spdu.id)
            {
                case 1:
                    if (TextPanelOuterBackground.Width - 2 * spdu.outerWidth - 40 < 100 ||
                        TextPanelOuterBackground.Height - 2 * spdu.outerWidth - 40 < 100)
                        return;

                    margin = TextPanelInnerBackground.Margin;
                    margin.Top = TextPanelOuterBackground.Margin.Top + spdu.outerWidth;
                    margin.Left = TextPanelOuterBackground.Margin.Left + spdu.outerWidth;
                    TextPanelInnerBackground.Margin = margin;

                    width = TextPanelOuterBackground.Width - 2 * spdu.outerWidth;
                    width = width < 0 ? 0 : width;
                    TextPanelInnerBackground.Width = width;

                    width = TextPanelOuterBackground.Height - 2 * spdu.outerWidth;
                    width = width < 0 ? 0 : width;
                    TextPanelInnerBackground.Height = width;

                    margin = WhitodoText.Margin;
                    margin.Top = TextPanelInnerBackground.Margin.Top + 20;
                    margin.Left = TextPanelInnerBackground.Margin.Left + 20;
                    WhitodoText.Margin = margin;

                    width = TextPanelInnerBackground.Width - 40;
                    width = width < 0 ? 0 : width;
                    WhitodoText.Width = width;

                    width = TextPanelInnerBackground.Height - 40;
                    width = width < 0 ? 0 : width;
                    WhitodoText.Height = width;
                    break;
                case 2:
                    TextPanelOuterBackground.Opacity = spdu.outerTransparency;
                    break;
                case 3:
                    TextPanelInnerBackground.Opacity = spdu.innerTransparency;
                    break;
                case 4:
                    TextPanelOuterBackground.Background = spdu.outerBrush;
                    break;
                case 5:
                    TextPanelInnerBackground.Background = spdu.innerBrush;
                    break;
                case 6:
                    RedButton.Background = spdu.brush1;
                    break;
                case 7:
                    BlueButton.Background = spdu.brush2;
                    break;
                case 8:
                    GreenButton.Background = spdu.brush3;
                    break;
                case 9:
                    YellowButton.Background = spdu.brush4;
                    break;
                case 10:
                    WhiteButton.Background = spdu.brush5;
                    break;
                case 11:
                    BlackButton.Background = spdu.brush6;
                    break;
                default:
                    break;
            }
        }

        private void SettingButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SettingDialog settingDialog = new SettingDialog(DoSettings, spdu);
            HideControlPanel();
            settingDialog.ShowDialog();
            ShowControlPanel();
            CalcRectArea();
        }

        private void WhitodoButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HideControlPanel();
            string[] cfglines = new string[13];
            cfglines[0] = "window_width=" + this.Width;
            cfglines[1] = "window_height=" + this.Height;
            cfglines[2] = "outer_width=" + spdu.outerWidth;
            cfglines[3] = "outer_opacity=" + spdu.outerTransparency;
            cfglines[4] = "inner_opacity=" + spdu.innerTransparency;
            cfglines[5] = "outer_brush=" + spdu.outerBrush.ToString();
            cfglines[6] = "inner_brush=" + spdu.innerBrush.ToString();
            cfglines[7] = "brush1=" + spdu.brush1.ToString();
            cfglines[8] = "brush2=" + spdu.brush2.ToString();
            cfglines[9] = "brush3=" + spdu.brush3.ToString();
            cfglines[10] = "brush4=" + spdu.brush4.ToString();
            cfglines[11] = "brush5=" + spdu.brush5.ToString();
            cfglines[12] = "brush6=" + spdu.brush6.ToString();
            File.WriteAllLines(@Global.cfgFilePath, cfglines, Encoding.UTF8);
            using (FileStream stream = File.OpenWrite(Global.txtFilePath))
            {
                TextRange docTextRange = new TextRange(WhitodoText.Document.ContentStart, WhitodoText.Document.ContentEnd);
                docTextRange.Save(stream, System.Windows.DataFormats.Rtf);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseOffset = e.GetPosition(this);
            CalcRectArea();
        }

        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!canmove) return;
            Point mousePos = e.GetPosition(this);
            if ((mousePos.X > startTextArea.X && mousePos.Y > startTextArea.Y &&
                mousePos.X < endTextArea.X && mousePos.Y < endTextArea.Y) || 
                (mousePos.X > startControlArea.X && mousePos.Y > startControlArea.Y &&
                mousePos.X < endControlArea.X && mousePos.Y < endControlArea.Y))
            {
                return;
            }
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.Left += (mousePos.X - mouseOffset.X);
                this.Top += (mousePos.Y - mouseOffset.Y);
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Thickness margin;
            
            margin = WindowGrid.Margin;
            margin.Left = 0;
            margin.Top = 0;
            WindowGrid.Margin = margin;
            WindowGrid.Width = this.Width;
            WindowGrid.Height = this.Height;

            margin = TextPanelOuterBackground.Margin;
            margin.Left = 0;
            margin.Top = 104;
            TextPanelOuterBackground.Margin = margin;
            TextPanelOuterBackground.Width = WindowGrid.Width;
            TextPanelOuterBackground.Height = WindowGrid.Height - 104;

            margin = TextPanelInnerBackground.Margin;
            margin.Left = TextPanelOuterBackground.Margin.Left + spdu.outerWidth;
            margin.Top = 122;
            TextPanelInnerBackground.Margin = margin;
            TextPanelInnerBackground.Width = TextPanelOuterBackground.Width - 2 * spdu.outerWidth;
            TextPanelInnerBackground.Height = TextPanelOuterBackground.Height - 2 * spdu.outerWidth;

            margin = WhitodoText.Margin;
            margin.Left = TextPanelInnerBackground.Margin.Left + 20;
            margin.Top = 142;
            WhitodoText.Margin = margin;
            WhitodoText.Width = TextPanelInnerBackground.Width - 40;
            WhitodoText.Height = TextPanelInnerBackground.Height - 40;
        }
    }
}
