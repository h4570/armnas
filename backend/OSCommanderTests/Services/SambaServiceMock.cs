using System.IO;
using Moq;
using OSCommander.Repositories;
using OSCommander.Services;

// ReSharper disable StringLiteralTypo

namespace OSCommanderTests.Services
{
    public static class SambaServiceMock
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
        internal static ISambaService GetGood()
        {
            var mockRepo = new Mock<ISambaService>();

            mockRepo
                .Setup(obj => obj.Get())
                .Returns(File.ReadAllText("MockData/smb-conf.txt"));

            return mockRepo.Object;
        }

        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        internal static ISambaService GetWithCommandException()
        {
            var mockRepo = new Mock<ISambaService>();

            mockRepo
                .Setup(obj => obj.Get())
                .Throws(new CommandFailException("err"));


            return mockRepo.Object;
        }

        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        internal static ISambaService GetWithEmptyResponses()
        {
            var mockRepo = new Mock<ISambaService>();

            mockRepo
                .Setup(obj => obj.Get())
                .Returns(string.Empty);

            return mockRepo.Object;
        }

    }
}
