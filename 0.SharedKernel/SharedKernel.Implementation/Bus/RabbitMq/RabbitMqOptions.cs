namespace NM.SharedKernel.Implementation.Bus.RabbitMq
{
    internal class RabbitMqOptions
    {
        public string Hostname { get; set; }
        public ushort Port { get; set; }
        public string VirtualHost { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ushort RequestedHeartbeat { get; set; }
    }
}
