using System;
using System.Linq;
using System.Threading.Tasks;
using ByteSizeLib;
using Microsoft.Extensions.Logging;
using RedBoarder.NetDataClient.Clients;
using RedBoarder.NetDataClient.Dtos;

namespace RedBoarder.NetDataClient.Services
{
    public class NetDataStatusService
    {
        private readonly NetDataChartClient _netDataChart;
        private readonly ILogger<NetDataStatusService> _logger;

        public NetDataStatusService(NetDataChartClient netDataChart, ILogger<NetDataStatusService> logger)
        {
            _netDataChart = netDataChart;
            _logger = logger;
        }

        //public async Task<ServerState> GetServerStatusAsync()
        //{

        //    var cpuResult = await _netDataChart.GetChartData(NetDataChart.CPU, -5, 0, "average", 1);
        //    var cpuPercentage = cpuResult?.LatestValues?.Sum();
        //    var ramResult = await _netDataChart.GetChartData(NetDataChart.RAM, -5, 0, "average", 1);

        //    var free = ramResult?.Result?.Data?[0][1];
        //    var totalUsed = ramResult?.Result?.Data?[0][2] + ramResult?.Result?.Data?[0][3] + ramResult?.Result?.Data?[0][4];
        //    var used = ramResult?.Result?.Data?[0][2];
        //    var ramPercentage = 100 * (used / (free + totalUsed));


        //    var netResult = await _netDataChart.GetChartData(NetDataChart.NET, -5, 0, "average", 5);
        //    var inBound = netResult!.Result!.Data!.Average(m => m[1]);
        //    var outBound = Math.Abs(netResult!.Result.Data.Average(m => m[2]));


        //    var serverStatus = new ServerState
        //    {
               
        //        OutBound = ByteSize.FromBytes(outBound).KiloBytes,
        //        InBound = ByteSize.FromBytes(inBound).KiloBytes,
        //        Ram = ramPercentage ?? default,
        //        Cpu = cpuPercentage ?? default,
        //    };
        //    return serverStatus;
        //}

        public async Task<BandwidthSpeedDto> GetCurrentBandwidth()
        {
            // 5 points in last 5 Seconds
            var netResult = await _netDataChart.GetChartData(NetDataChart.NET, -5, 0, "average", 5);
            if (netResult == null)
            {
                throw new Exception("Null Net Response");
            }
            var inboundResult = netResult!.Result!.Data!.Average(m => m[1]);
            var inBound = inboundResult;
            var outboundResult = netResult!.Result!.Data!.Average(m => m[2]);
            var outBound = Math.Abs(outboundResult);

            return new BandwidthSpeedDto()
            {
                Inbound = ByteSize.FromBytes(inBound).KiloBytes,
                OutBound = ByteSize.FromBytes(outBound).KiloBytes,
            };

        }

        public async Task<double> GetRAMPercentage()
        {
            var ramResult = await _netDataChart.GetChartData(NetDataChart.RAM, -5, 0, "average", 1);
            if (ramResult == null)
            {
                throw new Exception("Null RAM Response");
            }
            // var free = ramResult!.Result!.Data![0][1];
            var free = ramResult!.Result!.Data!.Average(m => m[1]);
            //var totalUsed = ramResult!.Result.Data[0][2] + ramResult!.Result.Data[0][3] + ramResult!.Result.Data[0][4];
            var totalUsed = ramResult!.Result!.Data!.Average(m => m[2]) + ramResult!.Result!.Data!.Average(m => m[3]) + ramResult!.Result!.Data!.Average(m => m[4]);
            //var used = ramResult!.Result.Data[0][2];
            var used = ramResult!.Result!.Data!.Average(m => m[2]);
            var ramPercentage = used / (free + totalUsed) * 100;

            return ramPercentage;
        }

        public async Task<CPUPercentageDetailsDto> GetCPUPercentage()
        {
            var cpuResult = await _netDataChart.GetChartData(NetDataChart.CPU, -5, 0, "average", 1);
            if (cpuResult == null)
            {
                throw new Exception("Null CPU Response");
            }

            var cpuPercentage = new CPUPercentageDetailsDto();

            cpuPercentage.GuestNice = cpuResult!.Result!.Data!.Average(m => m[1]);
            cpuPercentage.Guest = cpuResult!.Result!.Data!.Average(m => m[2]);
            cpuPercentage.Steal = cpuResult!.Result!.Data!.Average(m => m[3]);
            cpuPercentage.SoftIRQ = cpuResult!.Result!.Data!.Average(m => m[4]);
            cpuPercentage.IRQ = cpuResult!.Result!.Data!.Average(m => m[5]);
            cpuPercentage.User = cpuResult!.Result!.Data!.Average(m => m[6]);
            cpuPercentage.System = cpuResult!.Result!.Data!.Average(m => m[7]);
            cpuPercentage.Nice = cpuResult!.Result!.Data!.Average(m => m[8]);
            cpuPercentage.IOWait = cpuResult!.Result!.Data!.Average(m => m[9]);
            //var cpuPercentage = cpuResult!.LatestValues!.Sum();
            cpuPercentage.Total
                = cpuPercentage.GuestNice + cpuPercentage.Guest +
                  cpuPercentage.Steal + cpuPercentage.SoftIRQ +
                  cpuPercentage.User + cpuPercentage.IRQ +
                  cpuPercentage.System + cpuPercentage.Nice +
                  cpuPercentage.IOWait; 
            return cpuPercentage;

        }


        



        public async Task<int> GetSocketNumber()
        {
            var socketResult = await _netDataChart.GetChartData(NetDataChart.IPV4_SOCKETS, -5, 0, "average", 5);
            if (socketResult == null)
            {
                throw new Exception("Null Socket Response");
            }
            var sockets = Convert.ToInt32(socketResult!.Result!.Data!.Average(m => m[1]));
            return sockets;
        }
    }
}
