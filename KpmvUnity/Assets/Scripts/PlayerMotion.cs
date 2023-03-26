using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    public bool isGrounded = true;

    public Vector3 zeroVec;

    public GameObject terrain; // Generator;

    private Rigidbody rigid;
    private Animator anim;

    public static List<MainClient.ObjP> pdbList;

    public MainClient mClient;

    public Winner win;

    public string a;
    public string b;

    private KeyCode currentKeycode;
    
    private bool GetKey (KeyCode code) { return currentKeycode==code; }
    public void SetKey(KeyCode code) {  currentKeycode = code; }

    public bool save = false;
    private void Start()
    {

        //zeroVec = transform.position;
        zeroVec = new Vector3(0, 0, 1);
    }

    private void Awake()
    {
        mClient = GameObject.FindObjectOfType<MainClient>();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        terrain = GameObject.Find("Terrain");

    }

    public void ThisUpdate()
    {
        Move();
        Turn();
        Motion();
    }

    private void Move()
    {


        if (GetKey(KeyCode.W) || GetKey(KeyCode.S) || GetKey(KeyCode.A) || GetKey(KeyCode.D))
        {
            anim.SetBool("Run", true);
            anim.SetBool("Hello", false);
            anim.SetBool("Win", false);
            anim.SetBool("Lose", false);
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }

    private void Turn()
    {
        Vector3 movement = Vector3.zero;

        if (GetKey(KeyCode.A))
        {
            movement.x = -1;
        }
        else if (GetKey(KeyCode.D))
        {
            movement.x = 1;
        }

        if (GetKey(KeyCode.W))
        {
            movement.z = 1;
        }
        else if (GetKey(KeyCode.S))
        {
            movement.z = -1;
        }

        if (movement != Vector3.zero)
        {
            zeroVec = movement.normalized;
        }

        transform.LookAt(transform.position + zeroVec);
    }

    private void Motion()
    {
        if (!(GetKey(KeyCode.W) || GetKey(KeyCode.S) || GetKey(KeyCode.A) || GetKey(KeyCode.D)))
        {

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                anim.SetBool("Hello", true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                anim.SetBool("Win", true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                anim.SetBool("Lose", true);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Quiz") || collision.gameObject.CompareTag("Die")) //바닥 태그 추가 (태그 추가시 || 사용)
        {
            isGrounded = true;
        }
    }
    void OnCollisionStay(Collision collision)
    {
        pdbList = MainClient.pdbList;

        if (collision.gameObject.CompareTag("Die"))
        {
            transform.position = new Vector3(8, 30, -12);
        }
        if (collision.gameObject.CompareTag("Respawn"))
        {
            transform.position = new Vector3(8, 30, -12);
        }
        if (collision.gameObject.CompareTag("Win"))
        {
            if (save == false)
            {

                win.isFollowing = true;
                a = pdbList[0].mName;
                b = pdbList[0].mNftAddr;
                Save();
            }
        }
    }

    public void Save()
    {
        pdbList[0].mName = a;
        pdbList[0].mNftAddr = b;
        pdbList[0].posiSend(mClient.mCt, true);
        save = true;
    }
}
