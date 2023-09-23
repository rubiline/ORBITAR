using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            player.Die();
        }
        
        if (collision.collider.CompareTag("Bouncy"))
        {
            Vector3 normal = collision.contacts[0].normal;
            player.ReverseRotation();
            player.MovementVector = player.MovementVector - (2 * (Vector3.Dot(player.MovementVector, normal) * normal));
        }
    }
}
