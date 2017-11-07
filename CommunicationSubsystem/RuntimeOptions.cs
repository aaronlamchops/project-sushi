using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CommandLine;
using CommandLine.Text;

namespace CommunicationSubsystem
{
    public abstract class RuntimeOptions
    {
        [Option("registry", MetaValue = "STRING", Required = false, HelpText = "Registry's end point")]
        public string Registry { get; set; }

        [Option("label", MetaValue = "STRING", Required = false, HelpText = "Process label")]
        public string Label { get; set; }

        [Option("minport", MetaValue = "INT", Required = false, HelpText = "Min port in a range of possible ports for this process's communications")]
        public int? MinPortNullable { get; set; }
        public int MinPort => MinPortNullable ?? 0;

        [Option("maxport", MetaValue = "INT", Required = false, HelpText = "Max port in a range of possible ports for this process's communications")]
        public int? MaxPortNullable { get; set; }
        public int MaxPort => MaxPortNullable ?? 0;

        [Option("timeout", MetaValue = "INT", Required = false, HelpText = "Default timeout for request-reply communications")]
        public int? TimeoutNullable { get; set; }
        public int Timeout => TimeoutNullable ?? 0;

        [Option("retries", MetaValue = "INT", Required = false, HelpText = "Default max retries for request-reply communications")]
        public int? RetriesNullable { get; set; }
        public int Retries => RetriesNullable ?? 0;

        [Option('s', "autostart", Required = false, HelpText = "Autostart")]
        public bool AutoStart { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }

        public abstract void SetDefaults();
    }
}
