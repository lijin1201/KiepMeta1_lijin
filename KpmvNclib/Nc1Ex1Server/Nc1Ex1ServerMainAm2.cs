using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nc1Ex1Server
{
    class Nc1Ex1ServerMainAm2
    {
        public class Sv : NccpcDll.NccpcNw1Sv
        {
            public List<int> mCs = new List<int>();
            public NetworkTextTestExample mNtte = new NetworkTextTestExample();
            public NetworkTextTestExample mNttep = new NetworkTextTestExample();
            public NccpcDll.NccpcMemmgr2Mgr mMm;

            public Sv()
                : base()
            {
                mMm = new NccpcDll.NccpcMemmgr2Mgr();
            }

            public bool create()
            {
                mNtte.Db();
                mNttep.Dbp();


                if (!mMm.create()) { return false; }

                var co = new NccpcDll.NccpcNw1Sv.CreateOptions(mMm, "7777");

                if (!base.create(co)) { return false; }

                return true;
            }

            new public void release()
            {
                base.release();
                mMm.release();
            }

            public void qv(string s1) { System.Console.WriteLine(s1); }
            public override void onNccpcNwLog(string s1) { qv(s1); }
            //public override void onNccpcNwErr(string s1) { qv("Err " + s1); }
            public override void onNccpcNwEnter(int cti, string peer)
            {
                qv("Dbg NwEnter ct:" + cti + " Peer:" + peer);
                mCs.Add(cti);
                mNtte.QuizDataSend(this, cti);
                mNttep.PlayerDataSend(this, cti);
                using (var pkw = mMm.allocNw1pk(0xff))
                {
                    pkw.setType(2);
                    pkw.wInt32s(mCs.Count);
                    pkw.wInt32s((int)cti);
                    send(cti, pkw);  //send to one
                }
            }

            public override void onNccpcNwRecv(int cti, NccpcDll.NccpcNw1Pk2 ncpk)
            {
                qv("Dbg NwRecv Type:" + ncpk.getType() + " Len:" + ncpk.getDataLen());
                if (ncpk.getType() == 111)
                {
                    string s1 = ncpk.rStrFromNclib1ToClr();
                    string s2 = ncpk.rStrFromNclib1ToClr();
                    string s3 = "제6회 메타버스 퀴즈 대회";
                    Mdb1.DbEx_UpdateTest(s3, s1, s2);
                    qv("Dbg NwRecv Type:" + ncpk.getType() + "name : " + s1 + " addr : " + s2);

                    return;
                }
                using (var pkw = ncpk.copyDeep())
                {
                    send(mCs, pkw);
                }
            }
            public override void onNccpcNwLeave(int cti) { qv("Dbg NwLeave ct:" + cti + " remain:" + (mCs.Count - 1)); mCs.Remove(cti); }

        }

        static public void qv(string s1) { System.Console.WriteLine(s1); }

        static void Main(string[] args)
        {
            NccpcDll.NccpcNw1Cmn.stWsaStartup();

            qv("Dbg mongodb rst");

            var sv = new Sv();

            qv("Dbg server starting");

            if (!sv.create()) { return; }

            qv("Dbg server started");

            bool bWhile = true;

            while (bWhile)
            {
                sv.framemove();

                System.Threading.Thread.Sleep(100);
            }

            sv.release();

            NccpcDll.NccpcNw1Cmn.stWsaCleanup();
        }
    }
}