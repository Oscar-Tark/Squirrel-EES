using System;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Scorpion
{
    public class MongoInterface
    {
        const string database = "scorpion";

        //MONGO DB
        public void setfromJSON(ref string JSON, ref string collection)
        {
            //*database, *collection, *ref, *value, *ref, *value
            //How to add values:
            /*
            *ref, *val, *ref, *val
            */
            var client = new MongoClient();
            var db = client.GetDatabase(database);
            var coll = db.GetCollection<BsonDocument>(collection);
            var BsonArray = BsonSerializer.Deserialize<BsonArray>(JSON);
            var document = new BsonDocument();

            Console.ForegroundColor = ConsoleColor.Red;
            var JSON__ = BsonArray.ToJson();
            Console.WriteLine(JSON__);
            document.Add("user", BsonArray);
            coll.InsertOneAsync(document);

            Console.WriteLine("Successfully inserted retrieved JSON into " + database + "." + collection);
            return;
        }

        public void get(ref string filter_, string collection)
        {
            //*database, *collection, *filter, *limit
            var client = new MongoClient();
            var db = client.GetDatabase(database);
            var coll = db.GetCollection<BsonDocument>(collection);
            var filter = Builders<BsonDocument>.Filter.Eq(filter_, 1);
            var document = coll.Find(filter).FirstOrDefault();
            Console.WriteLine(document);

            /*if (document.ElementCount == 0)
            {
                Console.WriteLine("No rows found");
            }
            else
                Console.WriteLine("Rows found");
                */
            return;
        }

        public void mongolist(ref string[] command)
        {
            //::
            MongoClient dbClient = new MongoClient();
            var dbList = dbClient.ListDatabases().ToList();

            Console.WriteLine("The list of databases on this server is: ");
            foreach (var db in dbList)
                Console.WriteLine(db.ToJson());

            return;
        }
    }
}
