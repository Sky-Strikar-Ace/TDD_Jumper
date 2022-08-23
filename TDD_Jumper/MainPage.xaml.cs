using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace TDD_Jumper
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        BitmapImage[] images = new BitmapImage[8];
        DispatcherTimer timer;
        int counter = 0;
        int speed = 0;
        int boxX = 600;
        int boxY = 300;
        int stickY = 200;

        public MainPage()
        {
            this.InitializeComponent();

            for(int i = 0; i < 8; i++)
            {
                images[i] = new BitmapImage(new Uri("ms-appx:///Images/stickman" + i + ".png"));
            }

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(30);
            timer.Tick += Tick;
            timer.Start();

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;

        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            if (args.VirtualKey == Windows.System.VirtualKey.Space && stickY == 200)
            {
                speed = -15;
            }
        }

        private void Tick(object sender, object e)
        {
            counter = (counter + 1) % 8;
            Stickman.Source = images[counter];

            boxX = (boxX > -100) ? boxX - 10 : 600;
            
            Box.SetValue(Canvas.LeftProperty, boxX);
            Box.SetValue(Canvas.TopProperty, boxY);


            speed += 1;
            stickY += speed;
            if (stickY > 200)
            {
                stickY = 200;
                speed = 0;
            }

            Stickman.SetValue(Canvas.TopProperty,stickY);

        }
    }
}
