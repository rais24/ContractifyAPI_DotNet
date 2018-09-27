using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Contractify_API.Models
{
    public class MongoHelper<T> where T : class
    {
        public MongoCollection<T> Collection { get; private set; }

        public MongoHelper()
        {
            MongoConnectionStringBuilder con = new MongoConnectionStringBuilder(ConfigurationManager.ConnectionStrings["mongoDB"].ConnectionString);
            MongoClient client = new MongoClient(MongoClientSettings.FromConnectionStringBuilder(con));
            MongoServer server = new MongoServer(MongoServerSettings.FromClientSettings(client.Settings));
            MongoDatabase db = server.GetDatabase(con.DatabaseName);
            Collection = db.GetCollection<T>(typeof(T).Name.ToLower());
        }
    }
}