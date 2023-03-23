using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winner : MonoBehaviour
{
    public GameObject winner;

    public Transform target;
    public bool isFollowing;

    void Update()
    {
        if (isFollowing)
        {
            winner.transform.position = new Vector3(target.position.x, target.position.y + 2.5f, target.position.z);
        }
    }

}
