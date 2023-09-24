using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroller : MonoBehaviour
{
    public Vector2 maxDistance;
    public Vector2 speed;
    private Vector3 start;
    private Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        start = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        offset += speed * Time.deltaTime;
        if (offset.x > maxDistance.x) offset.x -= maxDistance.x;
        else if (offset.x < 0) offset.x += maxDistance.x;
        if (offset.y > maxDistance.y) offset.y -= maxDistance.y;
        else if (offset.y < 0) offset.y += maxDistance.y;
        this.transform.position = (Vector3)start + (Vector3)offset;
    }
}
