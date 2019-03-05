using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform target;
    Vector3 pos;

    // Update is called once per frame
    void Update()
    {
        pos = new Vector3(target.position.x, target.position.y, target.position.z);
        transform.position = Vector3.MoveTowards(transform.position, pos, 25f);
    }
}
