using HtmlAgilityPack;
using SamLu.Tools.Wlan_edu_Manager.Login.Implementation;
using SamLu.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace SamLu.Tools.Wlan_edu_Manager.Login
{
    class Program
    {
        const string Form_Force = "formForce";
        const string Form_Submit = "submitForm";

        static void Main(string[] args)
        {
            string userName = "13735536357", userPwd = "yh89e8w9";
            //string userName = "15957182175", userPwd = "avtPEbFD";

            LoginImplementation login = new LoginImplementation();
            login.Initialize();

            HtmlDocument document = new HtmlDocument();
            // 获取登录界面
            document.LoadHtml(login.Login(userName, userPwd, false));
            HtmlNode form = document.DocumentNode.SelectSingleNode("//form");
            switch (form.GetAttributeValue("name", null))
            {
                case Program.Form_Submit:
                    Console.WriteLine("登陆成功！");
                    document.LoadHtml(form.Submit());

                    DateTime loginSuccessTime = DateTime.Now;
                    using (var timer = new System.Timers.Timer() { Enabled = false, Interval = 1000 })
                    {
                        timer.Elapsed += (sender, e) =>
                        {
                            Console.Clear();
                            ConsoleForeColoredWrite(ConsoleColor.White, "尊敬的用户：");
                            ConsoleForeColoredWrite(ConsoleColor.Blue, userName);
                            ConsoleForeColoredWriteLine(ConsoleColor.White, "，您已登陆成功");
                            Console.WriteLine();
                            ConsoleForeColoredWriteLine(ConsoleColor.Gray, "上网过程中不要关闭该窗口。如需下线，请按 Esc 键。");
                            Console.WriteLine();
                            ConsoleForeColoredWrite(ConsoleColor.White, "本次登陆时长：\t");
                            TimeSpan timeSpan = DateTime.Now - loginSuccessTime;
                            ConsoleForeColoredWriteLine(ConsoleColor.Yellow, "{0:D2} : {1:D2} : {2:D2}",
                                (int)Math.Floor(timeSpan.TotalHours),
                                timeSpan.Minutes,
                                timeSpan.Seconds
                            );

                            document.DocumentNode.SelectNodes(@"//p[@class='tc_js']")
                                .Select(node => node.InnerText.Trim())
                                .ToList()
                                .ForEach(text =>
                                {
                                    Match match = Regex.Match(text, @"(?<Name>^\S*?)：(?<Value>(\s|\S)*$)");
                                    ConsoleForeColoredWrite(ConsoleColor.White, "{0}：\t", match.Groups["Name"].Value);
                                    ConsoleForeColoredWriteLine(ConsoleColor.Yellow, "{0}", match.Groups["Value"].Value);
                                });

                            Console.WriteLine();
                            ConsoleForeColoredWrite(ConsoleColor.White, "按 Esc 键下线……");
                            ;
                        };
                        timer.Start();

                        while (true)
                        {
                            if (Console.ReadKey(false).Key == ConsoleKey.Escape)
                            {
                                timer.Stop();

                                Thread.Sleep(1000); // 等待计时器停止。

                                Console.WriteLine();
                                if (ConsoleBinaryQuestion("请确认下线！"))
                                {
                                    //throw new NotSupportedException();
                                    ConsoleForeColoredWriteLine(ConsoleColor.Red, "暂不支持下线。");

                                    break;
                                }
                                else
                                {
                                    timer.Start();
                                }
                            }
                        }
                    }
                    break;
                case Program.Form_Force:
                    if (ConsoleBinaryQuestion("您好，您当前登录的用户已在线，是否继续操作？"))
                    {
                        document.LoadHtml(form.Submit());
                    }
                    break;
            }
            ;
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
