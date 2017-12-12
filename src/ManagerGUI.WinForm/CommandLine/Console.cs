using CommandLineInfo.Core.ComponentModel1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager.GUI.CommandLine
{
    internal static class Console
    {
        public sealed class _accounts
        {
            public _account[] accounts;

            public sealed class _account
            {
                public string username;
                public string userpwd;
                public bool rememberme;
                public bool autologin;
            }
        }

        public static readonly _accounts Accounts;
        public static readonly IDictionary<string, _accounts._account> AccountDictionary;
        static Console()
        {
            //Console.LoadConfig();
            string configFilePath = Path.Combine(Path.GetDirectoryName(typeof(Console).Assembly.Location), "accounts.config");
            Accounts = File.Exists(configFilePath) ? Newtonsoft.Json.JsonConvert.DeserializeObject<_accounts>(File.ReadAllText(configFilePath)) : new _accounts() { accounts = new _accounts._account[0] };
            AccountDictionary = CommandLine.Console.Accounts.accounts.ToDictionary(account => account.username);
        }

        public static void LoadConfig()
        {
            //string configFilePath = Path.Combine(Path.GetDirectoryName(typeof(Console).Assembly.Location), "accounts.config");
            //Accounts = Newtonsoft.Json.JsonConvert.DeserializeObject<_accounts>(File.ReadAllText(configFilePath));
            //AccountDictionary = CommandLine.Console.Accounts.accounts.ToDictionary(account => account.username);
        }

        public static void SaveConfig()
        {
            var accounts = new _accounts()
            {
                accounts = AccountDictionary.Select(pair => pair.Value).ToArray()
            };
            string configFilePath = Path.Combine(Path.GetDirectoryName(typeof(Console).Assembly.Location), "accounts.config");
            string serializedStr = Newtonsoft.Json.JsonConvert.SerializeObject(accounts);
            File.WriteAllText(configFilePath, serializedStr);
        }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
#if true
            var account = Accounts.accounts.Where(_account => _account.autologin == true).DefaultIfEmpty(new _accounts._account() { username = null, userpwd = null, rememberme = false, autologin = false }).First();
            new Program().Run(account.username, account.userpwd, account.autologin, false);
            return 0;
#else
            OptionCollection options = new OptionCollection();
            options.Add(new StringOption("username", "指定登录的用户名。", "{用户名}"));
            options.Add(new StringOption("userpwd", "指定登录的用户密码。", "{用户密码}"));
            options.Add(new BooleanOption("autologin", "是否在此次登录时记住用户名和密码，使得下一次登录时无用户名和密码。"));
            options.Add(new BooleanOption("cancelautologin", "是否在此次登出时取消自动登录。"));
            options.Add(new SwitchOption("?", "显示此帮助信息。"));

            var result = options.ParseArguments(args);

            if (result.Options["?"].IsPresent)
            {
                System.Console.WriteLine("{0} [options]", Path.GetFileNameWithoutExtension(typeof(Program).Assembly.Location));
                options.WriteOptionSummary(System.Console.Out);
                return 1;
            }

            if (!result.Success)
            {
                result.WriteParseErrors(System.Console.Out);
                return 1;
            }
            if (result.UnusedArguments.Count != 0)
            {
                System.Console.WriteLine("无效的命令行参数。");
                return 1;
            }

            if (result.Options["username"].IsPresent && result.Options["userpwd"].IsPresent)
            {
                string username, userpwd;
                bool autologin, cancelautologin;
                username = (string)result.Options["username"].Value;
                userpwd = (string)result.Options["userpwd"].Value;
                autologin = result.Options["autologin"].IsPresent && (bool)result.Options["autologin"].Value;
                cancelautologin = result.Options["cancelautologin"].IsPresent && (bool)result.Options["cancelautologin"].Value;
                if (!autologin && cancelautologin) cancelautologin = false;
                else if (autologin && !cancelautologin) autologin = false;

                new Program().Run(username, userpwd, autologin, cancelautologin);
                return 0;
            }
            else
            {
                System.Console.WriteLine("未输入用户名或密码。");
                return 1;
            }
#endif
        }
    }
}
