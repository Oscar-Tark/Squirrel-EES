using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;

namespace Scorpion_MongoDB_Library
{
    public class Mongodb
    {
        private static string URL;
        private static int PORT;
        private static string connectionstring;

        public static void Mongodbstart(string URL_, string PORT_)
        {
            URL = URL_;
            PORT = Convert.ToInt32(PORT_);
            connectionstring = "mongodb://" + URL + ":" + PORT;
        }

        public static string Mongogetall(string db_, string collection_)
        {
            //Returns JSON
            Console.WriteLine("Connecting to {0}", connectionstring);
            var client = new MongoClient(connectionstring);
            var db = client.GetDatabase(db_);
            var collection = db.GetCollection<BsonDocument>(collection_);

            string JSON = "[";
            using (IAsyncCursor<BsonDocument> cursor = collection.FindSync(new BsonDocument()))
            {
                while (cursor.MoveNext())
                {
                    IEnumerable<BsonDocument> batch = cursor.Current;
                    foreach (BsonDocument document in batch)
                        JSON = JSON + document.ToJson();
                }
            }
            return JSON + ']';
        }

        public static string Mongogetspecific(string db_, string collection_, string filter_)
        {
            Console.WriteLine("Connecting to {0}", connectionstring);
            var client = new MongoClient(connectionstring);
            var db = client.GetDatabase(db_);
            var collection = db.GetCollection<BsonDocument>(collection_);

            string JSON = "[";
            using (IAsyncCursor<BsonDocument> cursor = collection.FindSync(filter_))
            {
                while (cursor.MoveNext())
                {
                    IEnumerable<BsonDocument> batch = cursor.Current;
                    foreach (BsonDocument document in batch)
                        JSON = JSON + document.ToJson();
                }
            }

            return JSON + ']';
        }

        public static void Mongosetone(string db_, string collection_, string JSON)
        {
            Console.WriteLine("Connecting to {0}", connectionstring);
            var client = new MongoClient(connectionstring);
            var db = client.GetDatabase(db_);
            var collection = db.GetCollection<BsonDocument>(collection_);
            var BsonArray = BsonSerializer.Deserialize<BsonArray>(JSON);
            var document = new BsonDocument();

            var JSON__ = BsonArray.ToJson();
            document.Add(collection_, BsonArray);
            collection.InsertOneAsync(document);
        }
    }
}
