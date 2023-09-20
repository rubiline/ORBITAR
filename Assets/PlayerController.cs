using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField] private float angularSpeed;
    [SerializeField] private float linearSpeed;

    [SerializeField] private Transform sun;
    [SerializeField] private Transform moon;

    private Transform target;
    private Transform focus;

    private Vector3 movementVector;
    private float currentAngularVelocity;
    private int spin;
    // Start is called before the first frame update
    void Start()
    {
        StartRotating();
    }

    public void StartRotating()
    {
        SetRotation(true);
    }
    
    public void SetRotation(bool clockwise = true)
    {
        if (clockwise)
        {
            spin = 1;
        } else
        {
            spin = -1;
        }
    }

    public void StopRotation()
    {
        spin = 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="a">True for moon focus, false for sun focus</param>
    public void Lock(bool a)
    {
        target = a ? sun : moon;
        focus = a ? moon : sun;

        movementVector = Vector3.zero;

        Vector3 presentPosition = rb.position;
        Vector3 focusOffset = focus.position - presentPosition;
        Vector3 targetOffset = target.position - presentPosition;

        transform.position = focus.position;
        target.position += targetOffset;
        focus.position -= focusOffset;
    }

    public void Release(bool a)
    {
        target = a ? sun : moon;
        focus = a ? moon : sun;

        Vector3 offset = (target.position - focus.position) / 2;

        movementVector = (Quantize(Vector3.Cross((target.position - focus.position), Vector3.forward).normalized, 16) * -spin) * (Mathf.Deg2Rad * angularSpeed * (target.position - focus.position).magnitude);

        transform.position += offset;
        target.position -= offset;
        focus.position -= offset;

    }

    // Update is called once per frame
    void Update()
    {
        currentAngularVelocity = angularSpeed * Time.deltaTime;
        rb.SetRotation(rb.rotation + currentAngularVelocity * spin);
        rb.MovePosition(rb.position + (Vector2)movementVector * Time.deltaTime);
    }

    private Vector3 Quantize(Vector3 vector, int segments)
    {
        Vector3 comparator = Vector3.zero;

        for (int i = 0; i < segments; i++)
        {
            comparator = Quaternion.AngleAxis(i * (360f / segments), Vector3.forward) * Vector3.up;
            float diff = Vector3.SignedAngle(vector, comparator, Vector3.forward);

            if (Mathf.Abs(diff) < (180f / segments))
            {
                break;
            }
        }

        return comparator * vector.magnitude;
    }
}