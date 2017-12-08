using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager
{
    public static class ErrorHandleHelper
    {
        /// <summary>
        /// 搜集错误信息并启动 BugReport 。
        /// </summary>
        /// <param name="productName">产品名。</param>
        /// <param name="comName">组件名。</param>
        /// <param name="executiveName">重新启动时必须提供的可执行文件路径。</param>
        /// <param name="restartCmdLine">重新启动时必须提供的命令行参数。</param>
        /// <param name="exception">具体错误信息。默认值为 null 时，表示无错误信息。</param>
        /// <param name="autoRestart">一个值，指示是否自动重新启动。</param>
        public static void SetupBugReport(
            string productName, string comName, string executiveName, string restartCmdLine = "",
            Exception exception = null,
            bool autoRestart = true
        )
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo()
            {
                FileName = "BugReport.exe",
                Arguments = $"/product:\"{productName}\" /component:\"{comName}\" /executive:\"{executiveName}\" {string.Join(" ", ErrorHandleHelper.GenerateBugReports(exception))}"
            };
            process.Start();
        }

        internal static string[] GenerateBugReports(Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}
