using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;

namespace OSCommander
{
    public static class CurrentOS
    {

        public static ISystemInformation GetSystemInformation(ILogger logger)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return new Linux.SystemInformation(logger);
            return new Windows.SystemInformation();
        }

    }
}
