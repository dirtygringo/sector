using System;
using System.Collections.Generic;
using System.Text;

namespace NM.SharedKernel.Implementation.Storages.Mongo
{
    internal class MongoOptions
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
}
