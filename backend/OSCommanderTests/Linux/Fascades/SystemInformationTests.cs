using Xunit;
using OSCommander.Linux.Fascades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSCommander.Linux.Services;
using OSCommanderTests.Linux.Services;
// ReSharper disable ExceptionNotDocumented
// ReSharper disable StringLiteralTypo

namespace OSCommander.Linux.Fascades.Tests
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

        [Fact()]
        public void GetDistributionNameTest_Good()
        {
            var res = _good.GetDistributionName();
            Assert.Equal("Armbian 21.02.2 Focal", res);
        }

        [Fact()]
        public void GetDistributionNameTest_Bad_EmptyResult()
        {
            var res = _empty.GetDistributionName();
            Assert.Equal("Error", res);
        }

        [Fact()]
        public void GetDistributionNameTest_Bad_Exception()
        {
            var res = _exception.GetDistributionName();
            Assert.Equal("Error", res);
        }

        // ---

        [Fact()]
        public void GetKernelNameTest_Good()
        {
            var res = _good.GetKernelName();
            Assert.Equal("5.4.98-odroidxu4", res);
        }

        [Fact()]
        public void GetKernelNameTest_Bad_EmptyResult()
        {
            var res = _empty.GetKernelName();
            Assert.Equal("Error", res);
        }

        [Fact()]
        public void GetKernelNameTest_Bad_Exception()
        {
            var res = _exception.GetKernelName();
            Assert.Equal("Error", res);
        }

        // ---

        [Fact()]
        public void GetCPUInfoTest_Good()
        {
            var res = _good.GetCPUInfo();
            Assert.Equal("ARMv7", res.Name);
            Assert.Equal(14.92M, res.PercentageUsage);
            Assert.Equal(48.5M, res.Temperature);
        }

        [Fact()]
        public void GetCPUInfoTest_Bad_EmptyResult()
        {
            var res = _empty.GetCPUInfo();
            Assert.Equal("Error", res.Name);
            Assert.Equal(0, res.PercentageUsage);
            Assert.Equal(0, res.Temperature);
        }

        [Fact()]
        public void GetCPUInfoTest_Bad_Exception()
        {
            var res = _exception.GetCPUInfo();
            Assert.Equal("Error", res.Name);
            Assert.Equal(0, res.PercentageUsage);
            Assert.Equal(0, res.Temperature);
        }

        // ---

        [Fact()]
        public void GetRAMInfoTest_Good()
        {
            var res = _good.GetRAMInfo();
            Assert.Equal(1197428, res.FreeInKB);
            Assert.Equal(1642896, res.TotalInKB);
            Assert.Equal(2038220, res.AvailableInKB);
        }

        [Fact()]
        public void GetRAMInfoTest_Bad_EmptyResult()
        {
            var res = _empty.GetRAMInfo();
            Assert.Equal(0, res.FreeInKB);
            Assert.Equal(0, res.TotalInKB);
            Assert.Equal(0, res.AvailableInKB);
        }

        [Fact()]
        public void GetRAMInfoTest_Bad_Exception()
        {
            var res = _exception.GetRAMInfo();
            Assert.Equal(0, res.FreeInKB);
            Assert.Equal(0, res.TotalInKB);
            Assert.Equal(0, res.AvailableInKB);
        }

        // ---

        [Fact()]
        public void GetDisksInfoTest_Good()
        {
            var res = _good.GetDisksInfo();
            Assert.Equal(1, res.Count());
            var disk = res.First();
            Assert.Equal("/dev/mmcblk0p1", disk.Name);
            Assert.Equal(14767, disk.MemoryInMB);
            Assert.Equal(1658, disk.UsedMemoryInMB);
        }

        [Fact()]
        public void GetDisksInfoTest_Bad_EmptyResult()
        {
            var res = _empty.GetDisksInfo();
            Assert.Equal(0, res.Count());
        }

        [Fact()]
        public void GetDisksInfoTest_Bad_Exception()
        {
            var res = _exception.GetDisksInfo();
            Assert.Equal(0, res.Count());
        }

        // ---

        [Fact()]
        public void GetDiskInfoTest_Good()
        {
            var disk = _good.GetDiskInfo("/dev/mmcblk0p1");
            Assert.Equal("/dev/mmcblk0p1", disk.Name);
            Assert.Equal(14767, disk.MemoryInMB);
            Assert.Equal(1658, disk.UsedMemoryInMB);
            Assert.True(disk.IsMain);
        }

        [Fact()]
        public void GetDiskInfoTest_NotPresent()
        {
            var disk = _good.GetDiskInfo("/dev/mmcblk0p1337");
            Assert.Equal("Error", disk.Name);
        }

        [Fact()]
        public void GetDiskInfoTest_Bad_EmptyResult()
        {
            var disk = _empty.GetDiskInfo("/dev/mmcblk0p1");
            Assert.Equal("Error", disk.Name);
        }

        [Fact()]
        public void GetDiskInfoTest_Bad_Exception()
        {
            var disk = _exception.GetDiskInfo("/dev/mmcblk0p1");
            Assert.Equal("Error", disk.Name);
        }

        // ---

        [Fact()]
        public void GetIPTest_Good()
        {
            var res = _good.GetIP();
            Assert.Equal("192.168.0.155", res);
        }

        [Fact()]
        public void GetIPTest_Bad_EmptyResult()
        {
            var res = _empty.GetIP();
            Assert.Equal("Error", res);
        }

        [Fact()]
        public void GetIPTest_Bad_Exception()
        {
            var res = _exception.GetIP();
            Assert.Equal("Error", res);
        }
        // ---

        [Fact()]
        public void GetStartTimeTest_Good()
        {
            var res = _good.GetStartTime();
            var expect = DateTime.Parse("2021-02-21 00:31:38");
            Assert.Equal(expect.Ticks, ((DateTime)res).Ticks);
        }

        [Fact()]
        public void GetStartTimeTest_Bad_EmptyResult()
        {
            var res = _empty.GetStartTime();
            Assert.Null(res);
        }

        [Fact()]
        public void GetStartTimeTest_Bad_Exception()
        {
            var res = _exception.GetStartTime();
            Assert.Null(res);
        }

    }
}