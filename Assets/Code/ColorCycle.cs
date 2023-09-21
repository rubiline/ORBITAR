using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ColorCycle : MonoBehaviour
{
    public Color color1;
    public Color color2;
    public float speed;
    private float interp;
    private bool dir;
    private Tilemap sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        if(dir)
        {
            interp += speed * Time.deltaTime;
            if(interp >= 1)
            {
                interp = 1;
                dir = false;
            }
        }
        else
        {
            interp -= speed * Time.deltaTime;
            if(interp <= 0) 
            {
                interp = 0;
                dir = true;
            }
        }
        sprite.color = Color.Lerp(color1, color2, interp);
    }
}
