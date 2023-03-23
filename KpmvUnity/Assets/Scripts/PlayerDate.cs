using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDate : MonoBehaviour
{
    static public void qv(string s1) { Debug.Log(s1); }

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

    public class Client : JcCtUnity1.JcCtUnity1
    {

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

                            qv("ServerEnter ¼ö½Å s1: " + s1 + " s2 : " + s2);
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
    static public List<ObjP> pdbList;

    void Start()
    {
        mCt = new Client();
        mCt.connect("127.0.0.3", 7777);
        Debug.Log("Player Start 1111");
    }

    void Update()
    {
        mCt.framemove();
    }
}
