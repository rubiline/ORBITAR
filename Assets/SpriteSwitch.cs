using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwitch : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite idle;
    [SerializeField] private Sprite locked;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject sparkle;

    public void Lock()
    {
        spriteRenderer.sprite = locked;
        Sparkle();
    }

    public void Unlock()
    {
        spriteRenderer.sprite = idle;
    }

    public void Explode()
    {
        spriteRenderer.enabled = false;
        explosion.SetActive(true);
    }

    public void Sparkle()
    {
        StartCoroutine(SparkleCoroutine());
    }

    private IEnumerator SparkleCoroutine()
    {
        sparkle.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        sparkle.SetActive(false);
    }
}
