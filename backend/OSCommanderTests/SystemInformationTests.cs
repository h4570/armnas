using Xunit;
using System;
using System.Linq;
using OSCommander;
using OSCommander.Repositories;
using OSCommanderTests.Services;
using Assert = Xunit.Assert;

// ReSharper disable ExceptionNotDocumented
// ReSharper disable StringLiteralTypo

namespace OSCommanderTests
{
    public class SystemInformationTests
    {

        private readonly SystemInformation _good;
        private readonly SystemInformation _exception;
        private readonly SystemInformation _empty;

        public SystemInformationTests()
        {
            _good = new SystemInformation(SystemServiceMock.GetGood());
            _exception = new SystemInformation(SystemServiceMock.GetWithCommandException());
            _empty = new SystemInformation(SystemServiceMock.GetWithEmptyResponses());
        }

        [Fact]
        public void GetDistributionNameTest_Good()
        {
            var res = _good.GetDistributionName();
            Assert.Equal("Armbian 21.02.2 Focal", res);
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [Fact]
        public void GetDistributionNameTest_Bad_EmptyResult()
        {
            Assert.Throws<CommandResponseParsingException>(() => _empty.GetDistributionName());
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [Fact]
        public void GetDistributionNameTest_Bad_Exception()
        {
            Assert.Throws<CommandFailException>(() => _exception.GetDistributionName());
        }

        // ---

        [Fact]
        public void GetKernelNameTest_Good()
        {
            var res = _good.GetKernelName();
            Assert.Equal("5.4.98-odroidxu4", res);
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [Fact]
        public void GetKernelNameTest_Bad_EmptyResult()
        {

            Assert.Throws<CommandResponseParsingException>(() => _empty.GetKernelName());
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [Fact]
        public void GetKernelNameTest_Bad_Exception()
        {
            Assert.Throws<CommandFailException>(() => _exception.GetKernelName());
        }

        // ---

        [Fact]
        public void GetCPUInfoTest_Good()
        {
            var res = _good.GetCPUInfo();
            Assert.Equal("ARMv7", res.Name);
            Assert.Equal(14.92M, res.PercentageUsage);
            Assert.Equal(48.5M, res.Temperature);
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [Fact]
        public void GetCPUInfoTest_Bad_EmptyResult()
        {
            Assert.Throws<CommandResponseParsingException>(() => _empty.GetCPUInfo());
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [Fact]
        public void GetCPUInfoTest_Bad_Exception()
        {
            Assert.Throws<CommandFailException>(() => _exception.GetCPUInfo());
        }

        // ---

        [Fact]
        public void GetRAMInfoTest_Good()
        {
            var res = _good.GetRAMInfo();
            Assert.Equal(1197428, res.FreeInKB);
            Assert.Equal(1642896, res.TotalInKB);
            Assert.Equal(2038220, res.AvailableInKB);
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [Fact]
        public void GetRAMInfoTest_Bad_EmptyResult()
        {
            Assert.Throws<CommandResponseParsingException>(() => _empty.GetRAMInfo());
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [Fact]
        public void GetRAMInfoTest_Bad_Exception()
        {
            Assert.Throws<CommandFailException>(() => _exception.GetRAMInfo());
        }

        // ---

        /// <exception cref="T:System.ArgumentNullException">is <see langword="null" />.</exception>
        /// <exception cref="T:System.InvalidOperationException">The source sequence is empty.</exception>
        [Fact]
        public void GetDisksInfoTest_Good()
        {
            var res = _good.GetDisksInfo().ToList();
            Assert.Single(res);
            var disk = res.First();
            Assert.Equal("/dev/mmcblk0p1", disk.Name);
            Assert.Equal(14767, disk.MemoryInMB);
            Assert.Equal(1658, disk.UsedMemoryInMB);
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [Fact]
        public void GetDisksInfoTest_Bad_EmptyResult()
        {
            Assert.Throws<CommandResponseParsingException>(() => _empty.GetDisksInfo());
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [Fact]
        public void GetDisksInfoTest_Bad_Exception()
        {
            Assert.Throws<CommandFailException>(() => _exception.GetDisksInfo());
        }

        // ---

        [Fact]
        public void GetDiskInfoTest_Good()
        {
            var disk = _good.GetDiskInfo("/dev/mmcblk0p1");
            Assert.Equal("/dev/mmcblk0p1", disk.Name);
            Assert.Equal(14767, disk.MemoryInMB);
            Assert.Equal(1658, disk.UsedMemoryInMB);
            Assert.True(disk.IsMain);
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [Fact]
        public void GetDiskInfoTest_NotPresent()
        {
            Assert.Throws<CommandResponseParsingException>(() => _good.GetDiskInfo("/dev/mmcblk0p1337"));
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [Fact]
        public void GetDiskInfoTest_Bad_EmptyResult()
        {
            Assert.Throws<CommandResponseParsingException>(() => _empty.GetDiskInfo("/dev/mmcblk0p1"));
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [Fact]
        public void GetDiskInfoTest_Bad_Exception()
        {
            Assert.Throws<CommandFailException>(() => _exception.GetDiskInfo("/dev/mmcblk0p1"));
        }

        // ---

        [Fact]
        public void GetIPTest_Good()
        {
            var res = _good.GetIP();
            Assert.Equal("192.168.0.155", res);
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [Fact]
        public void GetIPTest_Bad_EmptyResult()
        {
            Assert.Throws<CommandResponseParsingException>(() => _empty.GetIP());
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [Fact]
        public void GetIPTest_Bad_Exception()
        {
            Assert.Throws<CommandFailException>(() => _exception.GetIP());
        }

        // ---

        /// <exception cref="T:System.ArgumentNullException">is <see langword="null" />.</exception>
        /// <exception cref="T:System.FormatException"> does not contain a valid string representation of a date and time.</exception>
        [Fact]
        public void GetStartTimeTest_Good()
        {
            var res = _good.GetStartTime();
            var expect = DateTime.Parse("2021-02-21 00:31:38");
            Assert.Equal(expect.Ticks, res.Ticks);
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [Fact]
        public void GetStartTimeTest_Bad_EmptyResult()
        {
            Assert.Throws<CommandResponseParsingException>(() => _empty.GetStartTime());
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [Fact]
        public void GetStartTimeTest_Bad_Exception()
        {
            Assert.Throws<CommandFailException>(() => _exception.GetStartTime());
        }

    }
}