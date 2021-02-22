using System.IO;
using Moq;
using OSCommander.Repositories;
using OSCommander.Services;

// ReSharper disable StringLiteralTypo

namespace OSCommanderTests.Services
{
    public static class SystemServiceMock
    {
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// <exception cref="T:System.UnauthorizedAccessException">specified a file that is read-only.</exception>
        /// <exception cref="T:System.IO.FileNotFoundException">The file specified in was not found.</exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="T:System.ArgumentException"> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
        /// <exception cref="T:System.ArgumentNullException"> is <see langword="null" />.</exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="T:System.NotSupportedException"> is in an invalid format.</exception>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        internal static ISystemService GetGood()
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
                .Setup(obj => obj.GetTop())
                .Returns(File.ReadAllText("MockData/top.txt"));

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

        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        internal static ISystemService GetWithCommandException()
        {
            var mockRepo = new Mock<ISystemService>();

            mockRepo
                .Setup(obj => obj.GetCpuInfo())
                .Throws(new CommandFailException("err"));

            mockRepo
                .Setup(obj => obj.GetDf())
                .Throws(new CommandFailException("err"));

            mockRepo
                .Setup(obj => obj.GetFdisk())
                .Throws(new CommandFailException("err"));

            mockRepo
                .Setup(obj => obj.GetIpAddresses())
                .Throws(new CommandFailException("err"));

            mockRepo
                .Setup(obj => obj.GetMemInfo())
                .Throws(new CommandFailException("err"));

            mockRepo
                .Setup(obj => obj.GetOsRelease())
                .Throws(new CommandFailException("err"));

            mockRepo
                .Setup(obj => obj.GetTop())
                .Throws(new CommandFailException("err"));

            mockRepo
                .Setup(obj => obj.GetTempInfo())
                .Throws(new CommandFailException("err"));

            mockRepo
                .Setup(obj => obj.GetUname())
                .Throws(new CommandFailException("err"));

            mockRepo
                .Setup(obj => obj.GetUpTime())
                .Throws(new CommandFailException("err"));

            return mockRepo.Object;
        }

        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        internal static ISystemService GetWithEmptyResponses()
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
                .Setup(obj => obj.GetTop())
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
