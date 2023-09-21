using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerFloorDetection : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private bool moon;
    private bool stuckBody;

    private void Update()
    {
        if (stuckBody && player.transform.parent != null)
        {
            if ((!player.MoonLocked && moon) || (!player.SunLocked && !moon))
            {
                player.transform.parent = null;
                stuckBody = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
        {
            GroundAlter(true);
        }

        if (collision.CompareTag("MovingPlatform"))
        {
            GroundAlter(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("MovingPlatform"))
        {
            if (player.SunLocked && !moon || player.MoonLocked && moon)
            {
                player.transform.parent = collision.transform;
                stuckBody = true;
            } 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
        {
            GroundAlter(false);
        }

        if (collision.CompareTag("MovingPlatform"))
        {
            GroundAlter(false);
        }
    }

    private void GroundAlter(bool ground, int moonOnly = 0)
    {
        if (moon && moonOnly < 2)
        {
            player.MoonGrounded = ground;
        }
        else if (!moon && moonOnly != 1)
        {
            player.SunGrounded = ground;
        }
    }
}
