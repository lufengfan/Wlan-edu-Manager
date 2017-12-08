using CommandLineInfo.Core.ComponentModel1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BugReport.CommandLine
{
    public class FileOption : StringOption
    {
        public FileOption(string name, string description) : base(name, description) { }

        public FileOption(string name, string description, string template) : base(name, description, template) { }

        protected override ParseResult ParseArgument(string argument)
        {
            ParseResult result = base.ParseArgument(argument);
            if (result == ParseResult.Success)
            {
                if (File.Exists((string)this.Value))
                    return ParseResult.Success;
                else
                    return ParseResult.ArgumentNotAllowed;
            }
            else return result;
        }
    }
}
