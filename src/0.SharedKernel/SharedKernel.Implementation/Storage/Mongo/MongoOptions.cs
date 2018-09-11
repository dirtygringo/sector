namespace NM.SharedKernel.Implementation.Storage.Mongo
{
    internal class MongoOptions
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public bool Seed { get; set; }
    }
}
