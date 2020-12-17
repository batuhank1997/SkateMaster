using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayCamera : MonoBehaviour
{
    public GameObject target;
    public GameObject target2;
    private Vector3 offset;
    private Vector3 velocity = Vector3.one;

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.SmoothDamp(transform.position, target2.transform.position, ref velocity, 0.1f);

        transform.LookAt(target.transform);
    }
}
