using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    private Transform target;
    public Vector3 offset;

    private void Start()
    {     
    }

    public void SetTarget(GameObject obj)
    {
        target = obj.transform;
    }
    void Update()
    {
        if (target) { 
            transform.position = target.position + offset;
        }
    }
}
