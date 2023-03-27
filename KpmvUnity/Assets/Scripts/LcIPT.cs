using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LcIPT : MonoBehaviour
{
    public static LcIPT Instance;
    private bool mbOnline = true;
    public float mJumpHeight = 2f;

    private MainClient mCf;
    public GameObject splayer;
    public GameObject playerPF;
    public GameObject go;
    public GameObject Camera;

    public const int maxP = 2;
    Vector3[] positions = { new Vector3(10, 40, 10), new Vector3(-10, 40, 10) };
    Color[] colors = { Color.blue, Color.red };
    //public List<GameObject> mPlayers = new List<GameObject>();
    public GameObject[] mPlayers;
    public int?[] mCtis;
    private int pIndex = -1;

  
    //public void setOffline() { 
    //    mbOnline = false;
    //    if (mCf.mCt.isConnected()) { DisConnect(); }
    //    go = GameObject.Find("Player");
    //}
    public bool isOnline() { return mbOnline; }
    public void SetIndex() {
        StartCoroutine(waiter());
        IEnumerator waiter()
        {

            yield return new WaitForSeconds(2);
            pIndex = -1;
            for (int i = 0; i < maxP; i++)
            {
                Debug.Log("mPlayers " + i + " : " + (mPlayers[i]==null? null: mPlayers[i]) );
                Debug.Log("mCtis " + i + " : " + (mCtis[i] == null ? null : mCtis[i]) );
            }
                for (int i = 0; i < maxP; i++)
            {
                Debug.Log("index: " + i);
                // if (!mPlayers[i]) { pIndex = i; break; }
                if (mCtis[i]==null ) { pIndex = i; break; }
            }

            if (pIndex >= 0) {
                InstantiatePlayer(pIndex);
                go = mPlayers[pIndex];
                Camera.GetComponent<camera>().SetTarget(go);
                currentSend(mCf.mCt);
                ctiSend(mCf.mCt);
            } else
            {
                Debug.Log("인원수 초과2"); mCf.mCt.disconnect();
            }
        }

    }

    public int GetIndex() { return pIndex; }

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
    void Start()
    {
        mCf = GetComponent<MainClient>();
        mPlayers = new GameObject[maxP];
        mCtis = new int?[maxP];


    }

    public void Connect() {
        if (!mCf.mCt.isConnected()) {
            go = null;
            splayer.SetActive(false);
            mCf.mCt.connect("127.0.0.3", 7777);
            Debug.Log("Client Start 1111");
        }
        mbOnline = true;
        
    }

    public void DisConnect()
    {
        if(mCf.mCt.isConnected()) { 
            for (int i =0; i<maxP; i++) {
                if (mPlayers[i])
                {
                    Destroy(mPlayers[i]);
                    Debug.Log("Destroy pidx: " + i);
                }
                mCtis[i] = null;
            }
            pIndex = -1;
            mCf.mCt.disconnect();
            mbOnline = false;
            go = splayer;
            go.SetActive(true);
        }
    }
    public void InstantiatePlayer(int i)
    {    
        GameObject player = Instantiate(playerPF, positions[i], Quaternion.identity);
        Transform head = player.transform.Find("Bone/Bone.001/Bone.002/Cube");
        head.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Diffuse"));
        head.GetComponent<MeshRenderer>().material.SetColor("_Color", colors[i]);
        mPlayers[i]=player;
    }

    public void moveSend(JcCtUnity1.JcCtUnity1 ct, GameObject obj, int code, float plusx, float plusy, float plusz)
    {
        using (JcCtUnity1.PkWriter1Nm pkw = new JcCtUnity1.PkWriter1Nm(3))
        {
            pkw.wInt32s(LcIPT.Instance.pIndex);
            pkw.wInt32s(code);
            pkw.wReal32(obj.transform.position.x + plusx);
            pkw.wReal32(obj.transform.position.y + plusy);
            pkw.wReal32(obj.transform.position.z + plusz);
            ct.send(pkw);
        }
    }

    public void ctiSend(MainClient.Client ct)
    {
        using (JcCtUnity1.PkWriter1Nm pkw = new JcCtUnity1.PkWriter1Nm(30))
        {
            pkw.wInt32s(LcIPT.Instance.pIndex);
            pkw.wInt32s(ct.cti);
            ct.send(pkw);
        }
    }

    public void currentSend(JcCtUnity1.JcCtUnity1 ct)
    {
        using (JcCtUnity1.PkWriter1Nm pkw = new JcCtUnity1.PkWriter1Nm(3))
        {
            pkw.wInt32s(LcIPT.Instance.pIndex);
            pkw.wInt32s(0);
            pkw.wReal32(go.transform.position.x );
            pkw.wReal32(go.transform.position.y );
            pkw.wReal32(go.transform.position.z );
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
      

        if (go)
        {

            if (Input.GetKey(KeyCode.W))
            {
                if (mbOnline)
                {
                    moveSend(mCf.mCt, go, (int)KeyCode.W,  0, 0, +spd);
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
                    moveSend(mCf.mCt, go, (int)KeyCode.S, 0, 0, -spd);
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
                    moveSend(mCf.mCt, go, (int)KeyCode.A, -spd, 0, 0);
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
                    moveSend(mCf.mCt, go, (int)KeyCode.D, +spd, 0, 0);
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
                        moveSend(mCf.mCt, go, (int)KeyCode.Space, 0, mJumpHeight, 0);
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