using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorLink : MonoBehaviour
{
    SpriteRenderer renderer;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        renderer.enabled = target.activeInHierarchy;
    }
}
