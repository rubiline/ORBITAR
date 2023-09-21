using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloorDetection : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private bool moon;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
        {
            if (moon)
            {
                player.MoonGrounded = true;
            } else
            {
                player.SunGrounded = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
        {
            if (moon)
            {
                player.MoonGrounded = false;
            }
            else
            {
                player.SunGrounded = false;
            }
        }
    }
}
