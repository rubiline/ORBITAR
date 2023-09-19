using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePlayerFacing : MonoBehaviour
{
    public void LateUpdate()
    {
        Vector3 euler = transform.parent.rotation.eulerAngles;
        transform.localRotation = Quaternion.Euler(0, 0, -euler.z);
    }
}
