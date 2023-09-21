using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwitch : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite idle;
    [SerializeField] private Sprite locked;

    public void Lock()
    {
        spriteRenderer.sprite = locked;
    }

    public void Unlock()
    {
        spriteRenderer.sprite = idle;
    }
}
