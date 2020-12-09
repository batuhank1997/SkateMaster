using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public GameObject target2;
    private Vector3 velocity = Vector3.one;

    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target2.transform.position, ref velocity, 0.3f);

        direction = (target.transform.position - transform.position).normalized;

        //transform.rotation = Quaternion.Euler(direction);
        transform.LookAt(target.transform);
    }
}
