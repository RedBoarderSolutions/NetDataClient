namespace RedBoarder.NetDataClient.Dtos
{
    public class ServerState
    {
        public string? Name { get; set; }
        public double Cpu { get; set; }
        public double Ram { get; set; }
        public double InBound { get; set; }
        public double OutBound { get; set; }

    }
}