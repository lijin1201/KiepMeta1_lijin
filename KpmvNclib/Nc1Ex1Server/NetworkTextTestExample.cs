using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nc1Ex1Server
{
    class NetworkTextTestExample
    {
        public List<BsonDocument> mList = new List<BsonDocument>();

        public List<BsonDocument> Db()
        {
            mList = Mdb1.DbEx_FindAll();
            foreach (var d1 in mList)
            {
                var s1 = d1.GetValue("content");
                Nc1Ex1ServerMainAm2.qv("Dbg mongodb content " + s1);
            }
            return mList;
        }

        public void QuizDataSend(Nc1Ex1ServerMainAm2.Sv sv, int cti)
        {
            using (var pkw = sv.mMm.allocNw1pk(0xff))
            {
                //Send(pkw, mList);
                pkw.setType(100);
                pkw.wInt32s(mList.Count);
                foreach (var d1 in mList)
                {
                    pkw.wStrToNclib1FromClr((string)d1.GetValue("answer"));
                    pkw.wStrToNclib1FromClr((string)d1.GetValue("content"));
                }
                sv.send(cti, pkw);
            }
        }
    }
}

