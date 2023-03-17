using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    public bool isGrounded = true;

    public Vector3 zeroVec;

    public GameObject terrain; // Generator;

    private Rigidbody rigid;
    private Animator anim;

    private void Start()
    {
        //zeroVec = transform.position;
        zeroVec = new Vector3(0, 0, 1);
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        terrain = GameObject.Find("Terrain");

    }

    private void Update()
    {
        Move();
        Turn();
        Motion();
    }

    private void Move()
    {


        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            anim.SetBool("Run", true);
            anim.SetBool("Hello", false);
            anim.SetBool("Win", false);
            anim.SetBool("Lose", false);
            rigid.velocity += new Vector3(0, 0f, 0);
        }
        else
        {
            anim.SetBool("Run", false);
            rigid.velocity += new Vector3(0, -0.2f, 0);
        }
    }

    private void Turn()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            movement.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement.x = 1;
        }

        if (Input.GetKey(KeyCode.W))
        {
            movement.z = 1;
        }
        else if (Input.GetKey(KeyCode.S))
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
        if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
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
        if (collision.gameObject.CompareTag("Floor")|| collision.gameObject.CompareTag("Quiz") || collision.gameObject.CompareTag("Die")) //바닥 태그 추가 (태그 추가시 || 사용)
        {
            isGrounded = true;
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Quiz"))
        {
            Debug.Log("선택");
        }
        if (collision.gameObject.CompareTag("Die"))
        {
            Debug.Log("충돌중");
            transform.position = new Vector3(8, 30, -12);
        }
        if (collision.gameObject.CompareTag("Respawn"))
        {
            transform.position = new Vector3(8, 30, -12);
        }
    }
}
