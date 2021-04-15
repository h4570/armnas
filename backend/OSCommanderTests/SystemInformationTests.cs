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

        /// <exception cref="T:System.ArgumentException">is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
        /// <exception cref="T:System.ArgumentNullException">is <see langword="null" />.</exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="T:System.NotSupportedException">is in an invalid format.</exception>
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
        /// <exception cref="T:Xunit.Sdk.ThrowsException">Thrown when an exception was not thrown, or when an exception of the incorrect type is thrown</exception>
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
            Assert.Equal(2.7M, res.PercentageUsage);
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
            Assert.Equal(274.6M, res.UsedInMB);
            Assert.Equal(1990.4M, res.TotalInMB);
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
        public void GetMountedPartitionsInfoTest_Good()
        {
            var res = _good.GetMountedPartitionsInfo().ToList();
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
        public void GetMountedPartitionsInfoTest_Bad_EmptyResult()
        {
            Assert.Throws<CommandResponseParsingException>(() => _empty.GetMountedPartitionsInfo());
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [Fact]
        public void GetMountedPartitionsInfoTest_Bad_Exception()
        {
            Assert.Throws<CommandFailException>(() => _exception.GetMountedPartitionsInfo());
        }

        // ---

        [Fact]
        public void GetMountedPartitionInfoTest_Good()
        {
            var disk = _good.GetMountedPartitionInfo("/dev/mmcblk0p1");
            Assert.Equal("/dev/mmcblk0p1", disk.Name);
            Assert.Equal(14767, disk.MemoryInMB);
            Assert.Equal(1658, disk.UsedMemoryInMB);
            Assert.True(disk.IsMain);
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [Fact]
        public void GetMountedPartitionInfoTest_NotPresent()
        {
            Assert.Throws<CommandResponseParsingException>(() => _good.GetMountedPartitionInfo("/dev/mmcblk0p1337"));
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [Fact]
        public void GetMountedPartitionInfoTest_Bad_EmptyResult()
        {
            Assert.Throws<CommandResponseParsingException>(() => _empty.GetMountedPartitionInfo("/dev/mmcblk0p1"));
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [Fact]
        public void GetMountedPartitionInfoTest_Bad_Exception()
        {
            Assert.Throws<CommandFailException>(() => _exception.GetMountedPartitionInfo("/dev/mmcblk0p1"));
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

        // ---

        /// <exception cref="T:System.ArgumentNullException">is <see langword="null" />.</exception>
        /// <exception cref="T:System.InvalidOperationException">No element satisfies the condition in.  
        ///  -or-  
        ///  More than one element satisfies the condition in  
        ///  -or-  
        ///  The source sequence is empty.</exception>
        [Fact]
        public void GetDisksInfoTest_Good()
        {
            var res = _good.GetDisksInfo().ToList();
            Assert.Equal(5, res.Count);

            var disk1 = res.Single(c => c.Name.Equals("sda"));
            Assert.Single(disk1.Partitions);
            Assert.Equal(1800000, disk1.MemoryInMB);
            var disk1P1 = disk1.Partitions.Single(c => c.Name == "sda1");
            Assert.Equal(1800000, disk1P1.MemoryInMB);
            Assert.Null(disk1P1.MountingPoint);

            var disk2 = res.Single(c => c.Name.Equals("mmcblk1"));
            Assert.Equal(2, disk2.Partitions.Count);
            Assert.Equal(29000, disk2.MemoryInMB);
            var disk2P1 = disk2.Partitions.Single(c => c.Name == "mmcblk1p1");
            Assert.Equal(128, disk2P1.MemoryInMB);
            Assert.Null(disk1P1.MountingPoint);
            var disk2P2 = disk2.Partitions.Single(c => c.Name == "mmcblk1p2");
            Assert.Equal(28900, disk2P2.MemoryInMB);
            Assert.Null(disk2P2.MountingPoint);

            var disk3 = res.Single(c => c.Name.Equals("mmcblk0"));
            Assert.Single(disk3.Partitions);
            Assert.Equal(14700, disk3.MemoryInMB);
            var disk3P1 = disk3.Partitions.Single(c => c.Name == "mmcblk0p1");
            Assert.Equal(14700, disk3P1.MemoryInMB);
            Assert.Equal("/", disk3P1.MountingPoint);
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        /// <exception cref="T:OSCommander.Services.SambaUpdateException">When JSON parsing fail.</exception>
        [Fact]
        public void GetDisksInfoTest_Bad_EmptyResult()
        {
            Assert.Throws<CommandResponseParsingException>(() => _empty.GetDisksInfo());
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        /// <exception cref="T:OSCommander.Services.SambaUpdateException">When JSON parsing fail.</exception>
        [Fact]
        public void GetDisksInfoTest_Bad_Exception()
        {
            Assert.Throws<CommandFailException>(() => _exception.GetDisksInfo());
        }

    }
}