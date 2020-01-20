#region copyright
// Copyright (c) 2015 Wm. Barrett Simms wbsimms.com
//
// Permission is hereby granted, free of charge, to any person 
// obtaining a copy of this software and associated documentation 
// files (the "Software"), to deal in the Software without restriction, including 
// without limitation the rights to use, copy, modify, merge, publish, 
// distribute, sublicense, and/or sell copies of the Software, 
// and to permit persons to whom the Software is furnished to do so, 
// subject to the following conditions:
//
// The above copyright notice and this permission notice shall be 
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR 
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER 
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
#endregion
using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Forms;
using Application = System.Windows.Application;
using Microsoft.Extensions.Configuration;
using WaveSim;

namespace MyScreensaver_wpf
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private HwndSource winWPFContent;

		private void ApplicationStartup(object sender, StartupEventArgs e)
		{
            IConfigurationRoot configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true)
    .Build();
            if (e.Args.Length == 0 || e.Args[0].ToLower().StartsWith("/s"))
			{

                //foreach (Screen s in Screen.AllScreens)
                //{
                     Screen s = Screen.PrimaryScreen;
                    if (s != Screen.PrimaryScreen)
					{
                    Blackout window = new Blackout
                    {
                        Left = s.WorkingArea.Left,
                        Top = s.WorkingArea.Top,
                        Width = s.WorkingArea.Width,
                        Height = s.WorkingArea.Height
                    };
                    window.Show();
					}
					else
					{

                    MainWindow window = new MainWindow(configuration)
                    {
                        Left = s.WorkingArea.Left,
                        Top = s.WorkingArea.Top,
                        Width = s.WorkingArea.Width,
                        Height = s.WorkingArea.Height
                    };
                    window.Show();
					}
				//}
			}
			else if (e.Args[0].ToLower().StartsWith("/p"))
			{
				MainWindow window = new MainWindow();
				Int32 previewHandle = Convert.ToInt32(e.Args[1]);
				IntPtr pPreviewHnd = new IntPtr(previewHandle);
                WaveSim.Rect lpRect = new WaveSim.Rect();

                HwndSourceParameters sourceParams = new HwndSourceParameters("sourceParams")
                {
                    PositionX = 0,
                    PositionY = 0,
                    Height = lpRect.Bottom - lpRect.Top,
                    Width = lpRect.Right - lpRect.Left,
                    ParentWindow = pPreviewHnd,
                    WindowStyle = (int)(WindowStyles.WS_VISIBLE | WindowStyles.WS_CHILD | WindowStyles.WS_CLIPCHILDREN)
                };

                winWPFContent = new HwndSource(sourceParams);
				winWPFContent.Disposed += (o, args) => window.Close();
				winWPFContent.RootVisual = window.MainGrid;

			}
		}
	}
}
