using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using OSCommander.Linux.Repositories;
using OSCommander.Linux.Services;
// ReSharper disable StringLiteralTypo

namespace OSCommanderTests.Linux.Services
{
    public static class SystemServiceMock
    {
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found.</exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><paramref name="path" /> specified a file that is read-only.  
        ///  -or-  
        ///  This operation is not supported on the current platform.  
        ///  -or-  
        ///  <paramref name="path" /> specified a directory.  
        ///  -or-  
        ///  The caller does not have the required permission.</exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
        public static ISystemService GetGood()
        {
            var mockRepo = new Mock<ISystemService>();

            mockRepo
                .Setup(obj => obj.GetCpuInfo())
                .Returns(File.ReadAllText("MockData/proc-cpuinfo.txt"));

            mockRepo
                .Setup(obj => obj.GetDf())
                .Returns(File.ReadAllText("MockData/df.txt"));

            mockRepo
                .Setup(obj => obj.GetFdisk())
                .Returns(File.ReadAllText("MockData/fdisk.txt"));

            mockRepo
                .Setup(obj => obj.GetIpAddresses())
                .Returns(File.ReadAllText("MockData/hostname.txt"));

            mockRepo
                .Setup(obj => obj.GetMemInfo())
                .Returns(File.ReadAllText("MockData/proc-meminfo.txt"));

            mockRepo
                .Setup(obj => obj.GetOsRelease())
                .Returns(File.ReadAllText("MockData/etc-os-release.txt"));

            mockRepo
                .Setup(obj => obj.GetProcStat())
                .Returns(File.ReadAllText("MockData/proc-stat.txt"));

            mockRepo
                .Setup(obj => obj.GetTempInfo())
                .Returns(File.ReadAllText("MockData/temp-info.txt"));

            mockRepo
                .Setup(obj => obj.GetUname())
                .Returns(File.ReadAllText("MockData/uname.txt"));

            mockRepo
                .Setup(obj => obj.GetUpTime())
                .Returns(File.ReadAllText("MockData/uptime.txt"));

            return mockRepo.Object;
        }

        public static ISystemService GetWithCommandException()
        {
            var mockRepo = new Mock<ISystemService>();

            mockRepo
                .Setup(obj => obj.GetCpuInfo())
                .Throws(new Command.CommandFailException("err"));

            mockRepo
                .Setup(obj => obj.GetDf())
                .Throws(new Command.CommandFailException("err"));

            mockRepo
                .Setup(obj => obj.GetFdisk())
                .Throws(new Command.CommandFailException("err"));

            mockRepo
                .Setup(obj => obj.GetIpAddresses())
                .Throws(new Command.CommandFailException("err"));

            mockRepo
                .Setup(obj => obj.GetMemInfo())
                .Throws(new Command.CommandFailException("err"));

            mockRepo
                .Setup(obj => obj.GetOsRelease())
                .Throws(new Command.CommandFailException("err"));

            mockRepo
                .Setup(obj => obj.GetProcStat())
                .Throws(new Command.CommandFailException("err"));

            mockRepo
                .Setup(obj => obj.GetTempInfo())
                .Throws(new Command.CommandFailException("err"));

            mockRepo
                .Setup(obj => obj.GetUname())
                .Throws(new Command.CommandFailException("err"));

            mockRepo
                .Setup(obj => obj.GetUpTime())
                .Returns(string.Empty);

            return mockRepo.Object;
        }

        public static ISystemService GetWithEmptyResponses()
        {
            var mockRepo = new Mock<ISystemService>();

            mockRepo
                .Setup(obj => obj.GetCpuInfo())
                .Returns(string.Empty);

            mockRepo
                .Setup(obj => obj.GetDf())
                .Returns(string.Empty);

            mockRepo
                .Setup(obj => obj.GetFdisk())
                .Returns(string.Empty);

            mockRepo
                .Setup(obj => obj.GetIpAddresses())
                .Returns(string.Empty);

            mockRepo
                .Setup(obj => obj.GetMemInfo())
                .Returns(string.Empty);

            mockRepo
                .Setup(obj => obj.GetOsRelease())
                .Returns(string.Empty);

            mockRepo
                .Setup(obj => obj.GetProcStat())
                .Returns(string.Empty);

            mockRepo
                .Setup(obj => obj.GetTempInfo())
                .Returns(string.Empty);

            mockRepo
                .Setup(obj => obj.GetUname())
                .Returns(string.Empty);

            mockRepo
                .Setup(obj => obj.GetUpTime())
                .Returns(string.Empty);

            return mockRepo.Object;
        }

    }
}
