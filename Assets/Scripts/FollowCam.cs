using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target; //pozicija igraca
    private Vector3 offset; //poizicija kamere u odnosu na igraca

    
    void Awake()
    {
        offset=transform.position-target.position;
    }

    private void LateUpdate()
    {
        transform.position=target.position+offset;
    }

}