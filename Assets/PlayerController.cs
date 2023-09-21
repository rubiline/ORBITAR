using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField] private float angularSpeed;
    [SerializeField] private float linearSpeed;


    [SerializeField] private float offsetSpeed;
    [SerializeField] private float minimumOffset;
    [SerializeField] private float maximumOffset;

    [SerializeField] private Transform sun;
    [SerializeField] private Transform moon;

    [SerializeField] private SpriteSwitch sunSprite;
    [SerializeField] private SpriteSwitch moonSprite;

    private Transform target;
    private Transform focus;

    private Vector3 movementVector;
    private float currentAngularVelocity;
    private int spin;
    private bool offsetting;
    private float offsetDirection;
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

    public void TriggerOffset(float dir, bool trig)
    {
        offsetting = trig;
        offsetDirection = dir;
    }

    public void AlterOffset(float dir, float delta)
    {
        float sunx = sun.localPosition.x * (1f + (dir * offsetSpeed * delta));
        float moonx = moon.localPosition.x * (1f + (dir * offsetSpeed * delta));

        if (Mathf.Abs(sunx) > 0.05f && sunx > -minimumOffset) sunx = -minimumOffset;
        if (Mathf.Abs(moonx) > 0.05f && moonx < minimumOffset) moonx = minimumOffset;

        if (sunx < -maximumOffset) sunx = -maximumOffset;
        if (moonx > maximumOffset) moonx = maximumOffset;

        sun.localPosition = new Vector3(sunx, sun.localPosition.y, sun.localPosition.z);
        moon.localPosition = new Vector3(moonx, moon.localPosition.y, moon.localPosition.z);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="a">True for moon focus, false for sun focus</param>
    public void Lock(bool a)
    {
        target = a ? sun : moon;
        focus = a ? moon : sun;

        if (focus == sun) sunSprite.Lock();
        if (focus == moon) moonSprite.Lock();

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

        if (focus == sun) sunSprite.Unlock();
        if (focus == moon) moonSprite.Unlock();

        Vector3 offset = (target.position - focus.position) / 2;

        RecalculateSpeed();
        movementVector = (Quantize(Vector3.Cross((target.position - focus.position), Vector3.forward).normalized, 16) * -spin);

        transform.position += offset;
        target.position -= offset;
        focus.position -= offset;

    }

    private void RecalculateSpeed()
    {
        linearSpeed = (Mathf.Deg2Rad * angularSpeed * (sun.position - moon.position).magnitude);
    }

    // Update is called once per frame
    void Update()
    {
        if (offsetting) AlterOffset(offsetDirection, Time.deltaTime);
        currentAngularVelocity = angularSpeed * Time.deltaTime;
        //rb.SetRotation(rb.rotation + currentAngularVelocity * spin);
        //rb.MovePosition(rb.position + (Vector2)movementVector * Time.deltaTime);
        this.transform.position = this.transform.position + (movementVector * Time.deltaTime) * linearSpeed;
        this.transform.eulerAngles = this.transform.eulerAngles + new Vector3(0, 0, currentAngularVelocity * spin);
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
