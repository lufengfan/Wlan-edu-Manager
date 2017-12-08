using HtmlAgilityPack;
using SamLu.Native.Wifi;
using SamLu.Tools.Wlan_edu_Manager.Login.Implementation;
using SamLu.Tools.Wlan_edu_Manager.Logout;
using SamLu.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using static SamLu.Tools.Wlan_edu_Manager.Wlan_eduManager;

namespace SamLu.Tools.Wlan_edu_Manager.Login
{
    internal sealed partial class Program
    {
        public const string SSID_Wlan_edu = "Wlan-edu";
        
        public bool UserNameMask { get; set; } = true;

        public string MaskedUserName
        {
            get
            {
                if (this.UserNameMask)
                    return Regex.Replace(this.userName, @"(?<=^\d\d\d)(\d*?)(?=\d\d\d\d$)", (match => string.Empty.PadRight(match.Length, '*')));
                else return this.userName;
            }
        }

        private string userName, userPwd;
        private bool isAutoLogin, cancelAutoLogin;

        internal void Run(string userName, string userPwd, bool isAutoLogin, bool cancelAutoLogin)
        {
            this.userName = userName;
            this.userPwd = userPwd;
            this.isAutoLogin = isAutoLogin;
            this.cancelAutoLogin = cancelAutoLogin;

            WifiWatcher.GetNativeWifi(out WIFISSID currentSsid, out WIFISSID[] ssids);
            if (currentSsid == null || currentSsid.SSID != SSID_Wlan_edu)
                throw new Wlan_eduNotConnectedException();

            string url;
            Encoding encoding = Encoding.UTF8;
            HtmlDocument document;

            // 通过重定向获取登录地址。
            url = "http://1.1.1.1/";
            document = new HtmlDocument();
            document.Load(HttpRequestUtil.GetHtmlContentStreamReader(url, encoding));
            string refresh_content = document.DocumentNode.SelectSingleNode(@"html/head/meta[@http-equiv='refresh']")?.GetAttributeValue("content", null);
            ;

            // 获取登录地址。
            url = Regex.Match(refresh_content.Split(';')[1], @"url=(?<Url>\S*)").Groups["Url"].Value;
            var queryArgs =
                Regex.Matches(
                    new Uri(url, UriKind.Absolute).Query,
                    @"(?<=^\?|&)(?<Key>\w+?)=(?<Value>\S*?)(?=&|$)"
                )
                .OfType<Match>()
                .ToDictionary(
                    (match => match.Groups["Key"].Value.ToLower()),
                    (match => match.Groups["Value"].Value)
                );
            string wlanAcName = queryArgs["wlanacname"];
            string wlanUserIp = queryArgs["wlanuserip"];
            ;

            Wlan_eduManager manager = new Wlan_eduManager(wlanAcName, wlanUserIp, url, encoding);
            while (manager.NextPage(this.manager_ChangePage, this.manager_callback)) ;
        }

        private IManagerPage manager_ChangePage(IManagerPage page, Wlan_eduManager.CancelArgs e)
        {
            if (page is ILoginInfoPage)
            {
                return ((ILoginInfoPage)page).Login(this.userName, this.userPwd, this.isAutoLogin);
            }
            else if (page is ILoginForcedPage)
            {
                if (ConsoleBinaryQuestion("您好，您当前登录的用户已在线，是否继续操作？"))
                {
                    return ((ILoginForcedPage)page).ForceLogin();
                }
                else
                {
                    return ((ILoginForcedPage)page).Cancel();
                }
            }
            else if (page is ILoginingPage)
            {
                Console.WriteLine("登录成功！");
                return ((ILoginingPage)page).Success();
            }
            else if (page is ILoginFailedPage)
            {
                ConsoleForeColoredWriteLine(ConsoleColor.Red, $"登录认证失败，用户( {userName} )当前处于非正常状态！");
                return ((ILoginFailedPage)page).Ignore();
            }
            else if (page is ILoginSucceededPage)
            {
                using (var timer = new System.Timers.Timer() { Enabled = false, Interval = 1000 })
                {
                    string maskedUserName = this.MaskedUserName;
                    timer.Elapsed += (sender, _e) =>
                    {
                        Console.Clear();
                        ConsoleForeColoredWrite(ConsoleColor.White, "尊敬的用户：");
                        ConsoleForeColoredWrite(ConsoleColor.Blue, maskedUserName);
                        ConsoleForeColoredWriteLine(ConsoleColor.White, "，您已登录成功");
                        Console.WriteLine();
                        ConsoleForeColoredWriteLine(ConsoleColor.Gray, "上网过程中不要关闭该窗口。如需下线，请按 Esc 键。");
                        Console.WriteLine();
                        ConsoleForeColoredWrite(ConsoleColor.White, "本次登录时长：\t");
                        TimeSpan timeSpan = DateTime.Now - ((ILoginSucceededPage)page).SucceededTime;
                        ConsoleForeColoredWriteLine(ConsoleColor.Yellow, "{0:D2} : {1:D2} : {2:D2}",
                            (int)Math.Floor(timeSpan.TotalHours),
                            timeSpan.Minutes,
                            timeSpan.Seconds
                        );

                        foreach (var pair in ((ILoginSucceededPage)page).WlanInfos)
                        {
                            ConsoleForeColoredWrite(ConsoleColor.White, "{0}：\t", pair.Key);
                            ConsoleForeColoredWriteLine(ConsoleColor.Yellow, "{0}", pair.Value);
                        }

                        Console.WriteLine();
                        ConsoleForeColoredWrite(ConsoleColor.White, "按 Esc 键下线……");
                        ;
                    };
                    timer.Start();

                    while (true)
                    {
                        if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                        {
                            timer.Stop();

                            Thread.Sleep(1000); // 等待计时器停止。

                            Console.WriteLine();
                            if (ConsoleBinaryQuestion("请确认下线！"))
                            {
                                return ((ILoginSucceededPage)page).Logout(this.userName, this.cancelAutoLogin);
                            }
                            else
                            {
                                timer.Start();
                            }
                        }
                    }
                }
            }
            else if (page is ILogoutingPage)
            {
                return ((ILogoutingPage)page).Success();
            }
            else if (page is ILogoutSucceededPage)
            {
                if (ConsoleBinaryQuestion("下线成功！是否关闭窗口？"))
                {
                    e.Cancel = true;
                    ((ILogoutSucceededPage)page).Exit();
                    return null;
                }
                else
                {
                    return ((ILogoutSucceededPage)page).Login(this.userName, this.userPwd, this.isAutoLogin);
                }
            }
            else
            {
                IManagerPage mp = page;
                ;
                e.Cancel = true;
                return null;
            }
        }

        private void manager_callback(IManagerPage page, CancelArgs e)
        {
            if (page is ILogoutSucceededPage)
            {
                Console.Clear();
                e.Cancel = !ConsoleBinaryQuestion($"是否重新登录 {this.userName} ？");
            }
        }
        
        private static bool ConsoleBinaryQuestion(string question)
        {
            Console.WriteLine("{0} [Y|N]", question);
            while (true)
            {
                switch (Console.ReadLine().ToUpper())
                {
                    case "Y":
                        return true;
                    case "N":
                        return false;
                    default:
                        Console.WriteLine("应输入Y或N。");
                        break;
                }
            }
        }

        private static void ConsoleForeColoredWriteLine(ConsoleColor foreColor, string format, params object[] args)
        {
            ConsoleColoredWriteLine(foreColor, Console.BackgroundColor, format, args);
        }

        private static void ConsoleBackColoredWriteLine(ConsoleColor backColor, string format, params object[] args)
        {
            ConsoleColoredWriteLine(Console.ForegroundColor, backColor, format, args);
        }

        private static void ConsoleColoredWriteLine(ConsoleColor foreColor, ConsoleColor backColor, string format, params object[] args)
        {
            ConsoleColor fc = Console.ForegroundColor;
            ConsoleColor bc = Console.BackgroundColor;
            Console.ForegroundColor = foreColor;
            Console.BackgroundColor = backColor;

            Console.WriteLine(format, args);

            Console.ForegroundColor = fc;
            Console.BackgroundColor = bc;
        }

        private static void ConsoleForeColoredWrite(ConsoleColor foreColor, string format, params object[] args)
        {
            ConsoleColoredWrite(foreColor, Console.BackgroundColor, format, args);
        }

        private static void ConsoleBackColoredWrite(ConsoleColor backColor, string format, params object[] args)
        {
            ConsoleColoredWrite(Console.ForegroundColor, backColor, format, args);
        }

        private static void ConsoleColoredWrite(ConsoleColor foreColor, ConsoleColor backColor, string format, params object[] args)
        {
            ConsoleColor fc = Console.ForegroundColor;
            ConsoleColor bc = Console.BackgroundColor;
            Console.ForegroundColor = foreColor;
            Console.BackgroundColor = backColor;

            Console.Write(format, args);

            Console.ForegroundColor = fc;
            Console.BackgroundColor = bc;
        }
    }
}
