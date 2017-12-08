using CommandLineInfo.Core.ComponentModel1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BugReport.CommandLine
{
    internal static class Console
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            OptionCollection options = new OptionCollection();
            options.Add(new StringOption("product", "发起调用的产品名", "{产品名}"));
            options.Add(new StringOption("component", "发起调用的组件名。", "{组件}"));
            options.Add(new FileOption("executive", "重新启动需要提供的程序可执行文件路径。"));
            options.Add(new StringOption("cmdline", "重新启动需要提供的命令行参数。"));
            options.Add(new SwitchOption("?", "显示此帮助信息。"));
            
            var result = options.ParseArguments(args);

            if (result.Options["?"].IsPresent)
            {
                System.Console.WriteLine("{0} [options] reportFiles", Path.GetFileNameWithoutExtension(typeof(Program).Assembly.Location));
                options.WriteOptionSummary(System.Console.Out);
                return 1;
            }

            if (!result.Success)
            {
                result.WriteParseErrors(System.Console.Out);
                return 1;
            }

            if (options["product"].IsPresent)
                Program.ProductName = (string)options["product"].Value;
            if (options["component"].IsPresent)
                Program.ComName = (string)options["component"].Value;
            if (options["executive"].IsPresent)
                Program.ExecutiveName = (string)options["executive"].Value;
            if (options["cmdline"].IsPresent)
                Program.RestartCmdLine = (string)options["cmdline"].Value;
            Program.ReportFiles = result.UnusedArguments.ToArray();

            Program.Run();
            return 0;
        }
    }
}
