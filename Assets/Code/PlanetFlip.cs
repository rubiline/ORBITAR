using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFlip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.localScale.x > 0 && this.transform.position.x < this.transform.parent.position.x)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        if (this.transform.localScale.x < 0 && this.transform.position.x > this.transform.parent.position.x)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
