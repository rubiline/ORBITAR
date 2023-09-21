using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class MovingPlatform : Mover
{
    [SerializeField] private SplineAnimate SplineAnimate;

    private Vector3 last;
    // Start is called before the first frame update
    private void Start()
    {
        SplineAnimate.Updated += UpdateVector;
        last = transform.position;
    }

    void UpdateVector(Vector3 position, Quaternion rotation)
    {
        Vector = (position - last).normalized / SplineAnimate.MaxSpeed; 
        last = position;
    }
}
