using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;

namespace OSCommander
{
    public static class CurrentOS
    {

        public static ISystemInformation GetSystemInformation(ILogger logger)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return new Linux.Fascades.SystemInformation(logger);
            return new Mockup.SystemInformation();
        }

    }
}
