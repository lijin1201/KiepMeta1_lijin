using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nc1Ex1Server
{
    class Mdb1
    {
        public const string
            Dbn = "test",
            Clcn = "quiz",
            Clcn1 = "users",
            Clcn2 = "nftLists";

        public static IMongoDatabase DbCon1(string dbn = Dbn)
        {
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            var database = client.GetDatabase(dbn);
            return database;
        }

        public static List<BsonDocument> DbEx_FindAll()
        {
            List<BsonDocument> list = new List<BsonDocument>();
            var db1 = DbCon1();
            var clc1 = db1.GetCollection<BsonDocument>(Clcn);
            var docs1 = clc1.Find(new BsonDocument()).ToList();
            foreach (var d1 in docs1)
            {
                // DB에서 잘가져오는지 체크용
                // Console.WriteLine(d1.ToString() + "FindAll " + d1.GetValue("name"));
                list.Add(d1);
            }
            return list;
        }
        public static List<BsonDocument> DbEx_FindPlayer()
        {
            List<BsonDocument> list = new List<BsonDocument>();
            var db1 = DbCon1();
            var clc1 = db1.GetCollection<BsonDocument>(Clcn1);
            var docs1 = clc1.Find(new BsonDocument()).ToList();
            foreach (var d1 in docs1)
            {
                list.Add(d1);
            }
            return list;
        }


        public static void DbEx_InsertBson1()
        {
            var db1 = DbCon1();
            var clc1 = db1.GetCollection<MongoDB.Bson.BsonDocument>(Clcn);

            var rand = new Random(DateTime.Now.Millisecond);
            var bd1 = new MongoDB.Bson.BsonDocument();
            bd1.Set("Name", "bsonnn");
            bd1.Set("Age", rand.Next());
            clc1.InsertOne(bd1);
        }
        public static void DbEx_UpdateTest(string gameName, string winner, string nftAddr)
        {
            var db1 = DbCon1();
            var clc1 = db1.GetCollection<MongoDB.Bson.BsonDocument>(Clcn2);

            var rand = new Random(DateTime.Now.Millisecond);
            var ft1 = Builders<MongoDB.Bson.BsonDocument>.Filter.Eq("game", gameName);
            var us1 = Builders<MongoDB.Bson.BsonDocument>.Update.Set("winner", winner).Set("EOA", nftAddr);
            clc1.UpdateOne(ft1, us1);
        }

    }
}
