namespace RedBoarder.NetDataClient.Dtos
{
    public class CPUPercentageDetailsDto
    {
        //"guest_nice",
        public double GuestNice { get; set; }
        //"guest",
        public double Guest { get; set; }
        //"steal",
        public double Steal { get; set; }
        //"softirq",
        public double SoftIRQ { get; set; }
        //"irq",
        public double IRQ { get; set; }
        //"user",
        public double User { get; set; }
        //"system",
        public double System { get; set; }
        //"nice",
        public double Nice { get; set; }
        //"iowait"
        public double IOWait { get; set; }


        public double Total { get; set; } 


    }
}