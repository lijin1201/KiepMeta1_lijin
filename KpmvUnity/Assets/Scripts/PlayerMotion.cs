using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    public bool isGrounded = true;

    public Vector3 zeroVec;

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
        }
        else
        {
            anim.SetBool("Run", false);
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
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }
}
