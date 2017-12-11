using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

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
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateDocumentFragment());
            XmlElement bugreport = doc.CreateElement("BugReport");
            doc.AppendChild(bugreport);

            #region environment
            XmlElement environment = doc.CreateElement("Environment");
            bugreport.AppendChild(environment);

            XmlElement osversion = doc.CreateElement("OSVersion");
            environment.AppendChild(osversion);
            osversion.AppendChild(doc.CreateTextNode(Environment.OSVersion.VersionString));

            #region currentdirectory
            XmlElement currentdirectory = doc.CreateElement("CurrentDirectory");
            environment.AppendChild(currentdirectory);
            currentdirectory.AppendChild(doc.CreateTextNode(Environment.CurrentDirectory));
            #endregion

            #region basedirectory
            XmlElement basedirectory = doc.CreateElement("BaseDirectory");
            environment.AppendChild(basedirectory);
            basedirectory.AppendChild(doc.CreateTextNode(AppDomain.CurrentDomain.BaseDirectory));
            #endregion

            #region version
            XmlElement version = doc.CreateElement("Version");
            environment.AppendChild(version);
            version.AppendChild(doc.CreateTextNode(Environment.Version.ToString()));
            #endregion

            #region machinename
            XmlElement machinename = doc.CreateElement("MachineVersion");
            environment.AppendChild(machinename);
            machinename.AppendChild(doc.CreateTextNode(Environment.MachineName));
            #endregion

            #region processorcount
            XmlElement processorcount = doc.CreateElement("ProcessorCount");
            environment.AppendChild(processorcount);
            processorcount.AppendChild(doc.CreateTextNode(Environment.ProcessorCount.ToString()));
            #endregion

            #region commandline
            XmlElement commandline = doc.CreateElement("CommandLine");
            environment.AppendChild(commandline);
            commandline.AppendChild(doc.CreateTextNode(Environment.CommandLine));
            #endregion

            #region workingset
            XmlElement workingset = doc.CreateElement("WorkingSet");
            environment.AppendChild(workingset);
            workingset.AppendChild(doc.CreateTextNode(Environment.WorkingSet.ToString()));
            #endregion
            #endregion

            XmlElement _exception = doc.CreateElement("Exception");
            bugreport.AppendChild(_exception);
            _exception.SetAttribute("type", exception.GetType().FullName);





            ErrorHandleHelper.SerializeObject(doc, _exception, exception);

            string bugreportfile = "__bugreport_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            doc.Save(bugreportfile);
            File.SetAttributes(bugreportfile, File.GetAttributes(bugreportfile) | (FileAttributes.Hidden | FileAttributes.ReadOnly));

            return new[] { bugreportfile };
        }

        private static void SerializeObject(XmlDocument doc, XmlElement element, object o, int stack = 0)
        {
            if (stack == 8) return;

            try
            {
                if (o == null)
                {
                    element.AppendChild(doc.CreateElement("null"));
                }
                else if (
                    o is sbyte ||
                    o is byte ||
                    o is int ||
                    o is uint ||
                    o is long ||
                    o is ulong ||
                    o is float ||
                    o is double ||
                    o is decimal ||
                    o is string
                )
                {
                    element.AppendChild(doc.CreateTextNode(o.ToString()));
                }
                else if (o is Enum)
                {
                    XmlElement enum_element = doc.CreateElement(o.GetType().FullName);
                    element.AppendChild(enum_element);
                    enum_element.AppendChild(doc.CreateTextNode(o.ToString()));
                }
                else if (o is IDictionary)
                {
                    foreach (DictionaryEntry item in (IDictionary)o)
                    {
                        XmlElement keyvaluepair_element = doc.CreateElement("DictionaryEntry");
                        element.AppendChild(keyvaluepair_element);

                        XmlElement key_element = doc.CreateElement("Key");
                        keyvaluepair_element.AppendChild(key_element);
                        ErrorHandleHelper.SerializeObject(doc, key_element, item.Key, stack + 1);

                        XmlElement value_element = doc.CreateElement("Value");
                        keyvaluepair_element.AppendChild(value_element);
                        ErrorHandleHelper.SerializeObject(doc, value_element, item.Value, stack + 1);
                    }
                }
                else if (o is IEnumerable)
                {
                    foreach (object item in (IEnumerable)o)
                    {
                        XmlElement item_element = doc.CreateElement("Item");
                        element.AppendChild(item_element);
                        ErrorHandleHelper.SerializeObject(doc, item_element, o, stack + 1);
                    }
                }
                else
                {
                    try
                    {
                        Type t = o.GetType();
                        PropertyInfo[] properties = t.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
                        foreach (PropertyInfo property in properties)
                        {
                            if (o is Exception && property.Name == "TargetSite") continue;

                            XmlElement property_element = doc.CreateElement(property.Name);
                            element.AppendChild(property_element);

                            object property_value = property.GetValue(o, null);
                            if (o is Exception && property.Name == "InnerException" && property_value != null)
                            {
                                property_element.SetAttribute("type", property_value.GetType().FullName);
                            }

                            ErrorHandleHelper.SerializeObject(doc, property_element, property_value, stack + 1);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (StackOverflowException)
            {
            }
        }

        [Obsolete("目前版本暂不支持。", true)]
        private static void SerializeException(XmlDocument doc, Exception e)
        {
            throw new NotSupportedException();
        }
    }
}
