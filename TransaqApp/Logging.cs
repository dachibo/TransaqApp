using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace TransaqApp
{
    public static class Logging
    {
        public static ILogger logger = Logger.None;
    }

}