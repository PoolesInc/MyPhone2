﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace GoodTimeStudio.MyPhone
{
    /// <summary>
    /// 提供特定于应用程序的行为，以补充默认的应用程序类。
    /// </summary>
    sealed partial class App : Application
    {
        private Frame rootFrame;
        
        private static App Instance;

        // Protocol communication
        private static string _result;
        private static SemaphoreSlim _signal = new SemaphoreSlim(0, 1);
        private static SemaphoreSlim _locker = new SemaphoreSlim(1, 1);

        public static bool Navigate(Type sourcePageType)
        {
            return Instance.rootFrame.Navigate(sourcePageType);
        }

        /// <summary>
        /// 初始化单一实例应用程序对象。这是执行的创作代码的第一行，
        /// 已执行，逻辑上等同于 main() 或 WinMain()。
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            Instance = this;
        }

        /// <summary>
        /// 在应用程序由最终用户正常启动时进行调用。
        /// 将在启动应用程序以打开特定文件等情况下使用。
        /// </summary>
        /// <param name="e">有关启动请求和过程的详细信息。</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            await InitApp(e);
        }

        protected override async void OnActivated(IActivatedEventArgs e)
        {
            if (e.Kind == ActivationKind.Protocol)
            {
                ProtocolActivatedEventArgs protocolArgs = (ProtocolActivatedEventArgs)e;
                Uri uri = protocolArgs.Uri;
                if (uri.Scheme == "goodtimestudio.myphone")
                {
                    if (uri.Host == "exit")
                    {
                        Exit();
                    }
                    if (!string.IsNullOrEmpty(uri.Host))
                    {
                        _result = uri.OriginalString;
                        _signal.Release();
                        return;
                    }

                    await InitApp(e);
                }
            }
        }

        private async Task InitApp(IActivatedEventArgs e)
        {
            InitAppPage(e);
            await DeviceManager.Init();
            await EnsureTrayAppIsRunning();
        }

        private async Task EnsureTrayAppIsRunning()
        {
            await Launcher.LaunchUriAsync(new Uri("goodtimestudio.myphone.trayapp://"));
        }

        private void InitAppPage(IActivatedEventArgs e)
        {
            rootFrame = Window.Current.Content as Frame;

            // 不要在窗口已包含内容时重复应用程序初始化，
            // 只需确保窗口处于活动状态
            if (rootFrame == null)
            {
                // 创建要充当导航上下文的框架，并导航到第一页
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: 从之前挂起的应用程序加载状态
                }

                // 将框架放在当前窗口中
                Window.Current.Content = rootFrame;
            }

            // Ignore Prelaunch event since this app does not support prelaunch 

            if (rootFrame.Content == null)
            {
                // 当导航堆栈尚未还原时，导航到第一页，
                // 并通过将所需信息作为导航参数传入来配置
                // 参数
                var settings = ApplicationData.Current.LocalSettings.Values;
                bool oobe = true;
                if (settings.TryGetValue("OOBE", out object obj))
                {
                    if (obj is bool && !(bool)obj)
                    {
                        oobe = false;
                    }
                }
                    
                if (oobe)
                {
                    //rootFrame.Navigate(typeof(TestPage), e.Arguments);
                    rootFrame.Navigate(typeof(OOBEPage), null);
                }
                else
                {
                    rootFrame.Navigate(typeof(MainPage), null);
                }
            }

            // 确保当前窗口处于活动状态
            Window.Current.Activate();
        }

        /// <summary>
        /// 导航到特定页失败时调用
        /// </summary>
        ///<param name="sender">导航失败的框架</param>
        ///<param name="e">有关导航失败的详细信息</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// 在将要挂起应用程序执行时调用。  在不知道应用程序
        /// 无需知道应用程序会被终止还是会恢复，
        /// 并让内存内容保持不变。
        /// </summary>
        /// <param name="sender">挂起的请求的源。</param>
        /// <param name="e">有关挂起请求的详细信息。</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: 保存应用程序状态并停止任何后台活动
            deferral.Complete();
        }

        public static async Task<string> SendRequest(string protocolCommand)
        {
            await _locker.WaitAsync();

            await Launcher.LaunchUriAsync(new Uri(protocolCommand));
            await _signal.WaitAsync();

            if (string.IsNullOrEmpty(_result))
            {
                return string.Empty;
            }

            string ret = string.Copy(_result);
            _result = string.Empty;

            _locker.Release();
            return ret;
        }
    }
}
