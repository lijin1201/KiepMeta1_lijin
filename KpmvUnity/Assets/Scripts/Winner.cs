using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winner : MonoBehaviour
{
    public GameObject winner;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("충돌한 오브젝트의 이름: " + collision.collider.gameObject.name);
        }
    }

}
