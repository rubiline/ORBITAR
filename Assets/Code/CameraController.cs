using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float trackSpeed;
    public float trackSmoothing;
    public float lookAhead;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float smoothedSpeed = Mathf.Abs((this.transform.position - target.position).magnitude) * trackSmoothing;
        this.transform.position = Vector3.MoveTowards(this.transform.position, target.position, (trackSpeed + smoothedSpeed) * Time.deltaTime);
    }
}
