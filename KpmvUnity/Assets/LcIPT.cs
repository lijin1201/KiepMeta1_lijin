using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LcIPT : MonoBehaviour
{
    public float jumpHeight = 2f;

    void Start()
    {

    }

    public void moveSend(JcCtUnity1.JcCtUnity1 ct, GameObject obj, float plusx, float plusy, float plusz)
    {
        using (JcCtUnity1.PkWriter1Nm pkw = new JcCtUnity1.PkWriter1Nm(101))
        {
            pkw.wStr1(obj.name);
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

        bool bOnline = false;
        var go = GameObject.Find("Player");

        if (go)
        {

            if (Input.GetKey(KeyCode.W))
            {
                if (bOnline)
                {
                    moveSend(null, go, 0, 0, +0.1f);
                }
                else
                {
                    go.GetComponent<PlayerMotion>().zeroVec = new Vector3(0, 0, +0.1f);
                    go.transform.position += new Vector3(0, 0, +0.1f);
                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                if (bOnline)
                {
                    moveSend(null, go, 0, 0, -0.1f);
                }
                else
                {
                    go.GetComponent<PlayerMotion>().zeroVec = new Vector3(0, 0, -0.1f);
                    go.transform.position += new Vector3(0, 0, -0.1f);
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (bOnline)
                {
                    moveSend(null, go, -0.1f, 0, 0);
                }
                else
                {
                    go.GetComponent<PlayerMotion>().zeroVec = new Vector3(-0.1f, 0, 0);
                    go.transform.position += new Vector3(-0.1f, 0, 0);
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (bOnline)
                {
                    moveSend(null, go, +0.1f, 0, 0);
                }
                else
                {
                    go.GetComponent<PlayerMotion>().zeroVec = new Vector3(+0.1f, 0, 0);
                    go.transform.position += new Vector3(+0.1f, 0, 0);
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (go.GetComponent<PlayerMotion>().isGrounded)
                {
                    if (bOnline)
                    {
                        moveSend(null, go, 0, jumpHeight, 0);
                    }
                    else
                    {
                        Rigidbody rb = go.GetComponent<Rigidbody>();
                        rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
                    }
                    go.GetComponent<PlayerMotion>().isGrounded = false;
                }
            }
        }
    }
}