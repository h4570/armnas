using System.Linq;
using Xunit;
using OSCommander;
using OSCommander.Repositories;
using OSCommanderTests.Services;
using Assert = Xunit.Assert;

// ReSharper disable ExceptionNotDocumented
// ReSharper disable StringLiteralTypo

namespace OSCommanderTests
{
    public class SambaTests
    {

        private readonly Samba _good;
        private readonly Samba _exception;
        private readonly Samba _empty;

        /// <exception cref="T:System.ArgumentException">is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
        /// <exception cref="T:System.ArgumentNullException">is <see langword="null" />.</exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="T:System.NotSupportedException">is in an invalid format.</exception>
        public SambaTests()
        {
            _good = new Samba(SambaServiceMock.GetGood());
            _exception = new Samba(SambaServiceMock.GetWithCommandException());
            _empty = new Samba(SambaServiceMock.GetWithEmptyResponses());
        }

        /// <exception cref="T:System.ArgumentNullException"></exception>
        /// <exception cref="T:System.OverflowException"></exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"></exception>
        [Fact]
        public void GetTest_Good()
        {
            var res = _good.Get().ToList();
            Assert.Equal(3, res.Count());
            Assert.Equal("global", res[0].Name);
            Assert.Equal(14, res[0].Params.Count);
            Assert.Equal("printers", res[1].Name);
            Assert.Equal(7, res[1].Params.Count);
            Assert.Equal("print$", res[2].Name);
            Assert.Equal(5, res[2].Params.Count);
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        [Fact]
        public void GetTest_Bad_EmptyResult()
        {
            Assert.Throws<CommandResponseParsingException>(() => _empty.Get());
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        [Fact]
        public void GetTest_Bad_Exception()
        {
            Assert.Throws<CommandFailException>(() => _exception.Get());
        }

        // ---

    }
}