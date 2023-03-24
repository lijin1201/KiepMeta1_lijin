using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainClient : MonoBehaviour
{
        public class Obj1
        {
            public string mContent, mAnswer;
        }

        public class ObjP
        {
            public string mName;
            public string mNftAddr;


            public void posiSend(Client ct, bool saveDB = false)
            {
                using (JcCtUnity1.PkWriter1Nm pkw = new JcCtUnity1.PkWriter1Nm(111))
                {
                    pkw.wStr1(mName);
                    pkw.wStr1(mNftAddr);
                    ct.send(pkw);
                }
            }
        }
        public List<Obj1> mlist()
        {
            return dbList;
        }

    public class Client : JcCtUnity1.JcCtUnity1
    {
        static public void qv(string s1) { Debug.Log(s1); }

        public Client() : base(System.Text.Encoding.Unicode) { }

        protected override void innLogOutput(string s1) { Debug.Log(s1); }
        protected override void onConnect(JcCtUnity1.NwRst1 rst1, System.Exception ex = null)
        {
            qv("Dbg on connect: " + rst1);
            int pkt = 1111;
            using (JcCtUnity1.PkWriter1Nm pkw = new JcCtUnity1.PkWriter1Nm(pkt))
            {
                pkw.wInt32u(2222);
            }
            qv("Dbg send packet Type:" + pkt);
        }
        protected override void onDisconnect()
        { qv("Dbg on disconnect"); }

        protected override bool onRecvTake(Jc1Dn2_0.PkReader1 pkrd)
        {
            switch (pkrd.getPkt())
            {
                case 100:
                    {

                        dbList = new List<Obj1>();
                        var count = pkrd.rInt32s();

                        var s1 = "";
                        var s2 = "";

                        for (int i = 0; i < count; i++)
                        {
                            Obj1 mObj1 = new Obj1();
                            s1 = pkrd.rStr1def();
                            s2 = pkrd.rStr1def();

                            qv("ServerEnter 수신 s1: " + s1 + " s2 : " + s2);
                            mObj1.mContent = s1;
                            mObj1.mAnswer = s2;
                            dbList.Add(mObj1);
                            qv("dbList : " + dbList.Count);
                        }
                    }
                    break;
                case 101:
                    {
                        pdbList = new List<ObjP>();
                        var count = pkrd.rInt32s();

                        var s1 = "";
                        var s2 = "";

                        for (int i = 0; i < count; i++)
                        {
                            ObjP mObjP = new ObjP();
                            s1 = pkrd.rStr1def();
                            s2 = pkrd.rStr1def();

                            qv("ServerEnter 수신 s1: " + s1 + " s2 : " + s2);
                            mObjP.mName = s1;
                            mObjP.mNftAddr = s2;
                            pdbList.Add(mObjP);
                            qv("Player dbList : " + pdbList.Count);
                        }
                    }
                    break;
            }
            return true;
        }
    }

    public Client mCt;

    static public List<MainClient.ObjP> pdbList;
    static public List<MainClient.Obj1> dbList;

    void Start()
    {
        mCt = new Client();
        mCt.connect("127.0.0.3", 7777);
        Debug.Log("Client Start 1111");
    }

    // Update is called once per frame
    void Update()
    {
        mCt.framemove();
    }
}
