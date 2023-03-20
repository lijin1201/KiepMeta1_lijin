using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LcIPT : MonoBehaviour
{
    public static LcIPT Instance;
    const bool mbOnline = false;
    public float mJumpHeight = 2f;

    public GameObject playerPF;
    //Instantiate(terrainChunk, new Vector3(xPos, 0, zPos), Quaternion.identity);
    public List<GameObject> mPlayers = new List<GameObject>();
    public int pIndex;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public class Client : JcCtUnity1.JcCtUnity1
    {

        public int cti;
        public Client() : base(System.Text.Encoding.Unicode) { }
        public void qv(string s1) { innLogOutput(s1); }

        // JcCtUnity1.JcCtUnity1
        protected override void innLogOutput(string s1) { Debug.Log(s1); }
        protected override void onConnect(JcCtUnity1.NwRst1 rst1, System.Exception ex = null)
        {
            qv("Dbg on connect: " + rst1);
            int pkt = 1111;
            using (JcCtUnity1.PkWriter1Nm pkw = new JcCtUnity1.PkWriter1Nm(pkt))
            {
                pkw.wInt32u(2222);
                //this.send(pkw);
            }
            qv("Dbg send packet Type:" + pkt);
        }
        protected override void onDisconnect()
        { qv("Dbg on disconnect"); }
        //protected override bool onRecvTake(Jc1Dn2_0.PkReader1 pkrd)
        //{ qv("Dbg on recv: " + pkrd.getPkt()/* + pkrd.ReadString()*/ ); return true; }
        protected override bool onRecvTake(Jc1Dn2_0.PkReader1 pkrd)
        {
            switch (pkrd.getPkt())
            {

                case 2:
                    {
                        int id = pkrd.rInt32s();
                        LcIPT.Instance.pIndex = id - 1;
                        cti = pkrd.rInt32s();
                        qv("server 수신 cti: " + cti);
                        if (id>=3)
                        {
                            qv("3번제 이상!");
                        }
                    }
                    break;
                case 101:
                    {
                        var pidx = pkrd.rInt32s();
                        var xx = pkrd.rReal32();
                        var yy = pkrd.rReal32();
                        var zz = pkrd.rReal32();

                        if (pidx == LcIPT.Instance.pIndex)
                        {
                            LcIPT.Instance.mPlayers[pidx].transform.position = new Vector3(xx, yy, zz);
                        }

                        {
                            //var go1 = GameObject.Find("Cube");
                            //var pos = go1.transform.position;
                            //pos.x = x1; pos.y = y1;
                            //go1.transform.position = pos;
                        }

                        //Debug.Log("server 수신 s1: " + s1
                        //    + " X: " + x1 + "Y: " + y1);
                        //Debug.Log("server 수신 Treasure: " + s1
                        //    + " X: " + x2 + "Y: " + y2);
                    }
                    break;
            }
            return true;
        }
    }




    void Start()
    {
        const int maxP = 2;
        Vector3[] positions = { new Vector3(10, 0, 10), new Vector3(-10, 0, 10) };
        Color[] colors = { Color.blue, Color.red };

        for (int i = 0; i < maxP; i++) {
            GameObject player = Instantiate(playerPF, positions[i], Quaternion.identity);
            Transform head = player.transform.Find("Bone/Bone.001/Bone.002/Cube");
            head.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Diffuse"));
            head.GetComponent<MeshRenderer>().material.SetColor("_Color", colors[i]);
            mPlayers.Add(player);
        }

    }

    public void moveSend(JcCtUnity1.JcCtUnity1 ct, GameObject obj, float plusx, float plusy, float plusz)
    {
        using (JcCtUnity1.PkWriter1Nm pkw = new JcCtUnity1.PkWriter1Nm(101))
        {
            pkw.wInt32s(LcIPT.Instance.pIndex);
            pkw.wReal32(obj.transform.position.x + plusx);
            pkw.wReal32(obj.transform.position.x + plusy);
            pkw.wReal32(obj.transform.position.x + plusz);
            ct.send(pkw);
        }
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float spd = 10.0f * Time.deltaTime;
        var go = GameObject.Find("Player");

        if (go)
        {

            if (Input.GetKey(KeyCode.W))
            {
                if (mbOnline)
                {
                    moveSend(null, go, 0, 0, +spd);
                }
                else
                {
                    go.GetComponent<PlayerMotion>().zeroVec = new Vector3(0, 0, +spd);
                    go.transform.position += new Vector3(0, 0, +spd);
                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                if (mbOnline)
                {
                    moveSend(null, go, 0, 0, -spd);
                }
                else
                {
                    go.GetComponent<PlayerMotion>().zeroVec = new Vector3(0, 0, -spd);
                    go.transform.position += new Vector3(0, 0, -spd);
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (mbOnline)
                {
                    moveSend(null, go, -spd, 0, 0);
                }
                else
                {
                    go.GetComponent<PlayerMotion>().zeroVec = new Vector3(-spd, 0, 0);
                    go.transform.position += new Vector3(-spd, 0, 0);
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (mbOnline)
                {
                    moveSend(null, go, +spd, 0, 0);
                }
                else
                {
                    go.GetComponent<PlayerMotion>().zeroVec = new Vector3(+spd, 0, 0);
                    go.transform.position += new Vector3(+spd, 0, 0);
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (go.GetComponent<PlayerMotion>().isGrounded)
                {
                    if (mbOnline)
                    {
                        moveSend(null, go, 0, mJumpHeight, 0);
                    }
                    else
                    {
                        Rigidbody rb = go.GetComponent<Rigidbody>();
                        rb.AddForce(Vector3.up * Mathf.Sqrt(mJumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
                    }
                    go.GetComponent<PlayerMotion>().isGrounded = false;
                }
            }
        }
    }
}