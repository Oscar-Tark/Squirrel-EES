/*  <Scorpion IEE(Intelligent Execution Environment). Server To Run Scorpion Built Applications Using the Scorpion Language>
    Copyright (C) <2020+>  <Oscar Arjun Singh Tark>

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
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

            string JSON = "[";
            Console.WriteLine("Connecting to {0}", connectionstring);
            var client = new MongoClient(connectionstring);
            var db = client.GetDatabase(db_);
            var collection = db.GetCollection<BsonDocument>(collection_);

            using (IAsyncCursor<BsonDocument> cursor = collection.FindSync(new BsonDocument()))
            {
                while (cursor.MoveNext())
                {
                    IEnumerable<BsonDocument> batch = cursor.Current;
                    foreach (BsonDocument document in batch)
                        JSON = JSON + document.ToJson();
                }
            }
            return JSON.Replace("ObjectId(", "").Replace(")", "") + "]";
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

        public void Mongodbs()
        { 
            
        }
    }
}
