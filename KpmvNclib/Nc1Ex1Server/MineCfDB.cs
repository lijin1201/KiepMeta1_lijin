using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;


namespace Nc1Ex1Server
{
    class MineCfDB
    {
        public const string
            Dbn = "CsharpDb1", Clcn = "POS230208";

        public static object UpdateFlags { get; private set; }

        public static IMongoDatabase DbCon1(string dbn = Dbn)
        {
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            var database = client.GetDatabase(dbn);
            return database;
        }

        public static float[] DbEx_findObjPos(string objName)
        {
            var db1 = DbCon1();
            var clc1 = db1.GetCollection<BsonDocument>(Clcn);
            var ft1 = Builders<BsonDocument>.Filter.Eq("Name", objName);
            var docs1 = clc1.Find(ft1).ToList();
            foreach (var d1 in docs1)
            {
                Console.WriteLine(d1.ToString() + " GetValue Name " + d1.GetValue("Name"));
                return new float[] { (float)d1.GetValue("PosX"), (float)d1.GetValue("PosZ") };
            }
            return new float[] { 0, 0 };
        }

        public static UpdateResult DbEx_UpdateObjPos(string objName, float[] pos)
        {
            var db1 = DbCon1();
            var clc1 = db1.GetCollection<BsonDocument>(Clcn);
            var ft1 = Builders<BsonDocument>.Filter.Eq("Name", objName);
            var update = Builders<BsonDocument>.Update.Set("PosX", pos[0]).Set("PosZ", pos[1]);
            var upsert = new UpdateOptions { IsUpsert = true };
            return clc1.UpdateOne(ft1, update, upsert);
        }

        public static UpdateResult DbEx_UpdateObjFound(string objName)
        {
            var db1 = DbCon1();
            var clc1 = db1.GetCollection<BsonDocument>(Clcn);
            var ft1 = Builders<BsonDocument>.Filter.Eq("Name", objName);
            var update = Builders<BsonDocument>.Update.Inc("nFound", 1);
            var upsert = new UpdateOptions { IsUpsert = true };
            return clc1.UpdateOne(ft1, update, upsert);
        }

        public static void DbEx_Insert1(string objName, float[] pos)
        {
            var db1 = DbCon1();
            var clc1 = db1.GetCollection<BsonDocument>(Clcn);

            //var o = new { Name = "Obj1", Posx = 20 };
            BsonDocument b = new BsonDocument();
            b.Add("Name", objName);
            b.Add("PosX", pos[0]);
            b.Add("PosZ", pos[1]);
            clc1.InsertOne(b);

            // Builders<BsonDocument>.
        }
    }
}
