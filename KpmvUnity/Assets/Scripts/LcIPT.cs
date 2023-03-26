using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LcIPT : MonoBehaviour
{
    public static LcIPT Instance;
    const bool mbOnline = true;
    public float mJumpHeight = 2f;

    private MainClient mCf;
    public GameObject playerPF;
    public GameObject go;
    public GameObject Camera;

    public const int maxP = 2;
    Vector3[] positions = { new Vector3(10, 40, 10), new Vector3(-10, 40, 10) };
    Color[] colors = { Color.blue, Color.red };
    public List<GameObject> mPlayers = new List<GameObject>();
    private int pIndex=0;

    public void SetIndex(int index) { 
        pIndex = index;
        for (int i = 0; i <= pIndex; i++)
        {
            InstantiatePlayer(i);
        }
        go = mPlayers[pIndex];
        Camera.GetComponent<camera>().SetTarget(go);
    }

    public int GetIndex () { return pIndex; }

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

  
        
    }

    public void InstantiatePlayer(int i)
    {    
        GameObject player = Instantiate(playerPF, positions[i], Quaternion.identity);
        Transform head = player.transform.Find("Bone/Bone.001/Bone.002/Cube");
        head.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Diffuse"));
        head.GetComponent<MeshRenderer>().material.SetColor("_Color", colors[i]);
        mPlayers.Add(player);
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