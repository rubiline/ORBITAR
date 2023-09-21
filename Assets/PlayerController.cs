using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
    public bool SunGrounded;
    public bool SunLocked;
    [SerializeField] private Transform moon;
    public bool MoonGrounded;
    public bool MoonLocked;

    [SerializeField] private SpriteSwitch sunSprite;
    [SerializeField] private SpriteSwitch moonSprite;

    private Transform target;
    private Transform focus;

    public Vector3 MovementVector;
    public Mover CurrentInfluence;

    private float currentAngularVelocity;
    private int spin;
    private bool offsetting;
    private float offsetDirection;
    private bool locked;


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

    public void ReverseRotation()
    {
        SetRotation(spin != 1);
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

        float max = maximumOffset * (locked ? 2f : 1f);

        if (sunx < -max) sunx = -max;
        if (moonx > max) moonx = max;

        sun.localPosition = new Vector3(sunx, sun.localPosition.y, sun.localPosition.z);
        moon.localPosition = new Vector3(moonx, moon.localPosition.y, moon.localPosition.z);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="a">True for moon focus, false for sun focus</param>
    public void Lock(bool a)
    {
        Transform target = a ? sun : moon;
        Transform focus = a ? moon : sun;

        if ((a && !MoonGrounded) || (!a && !SunGrounded))
        {
            return;
        }

        this.target = target;
        this.focus = focus;

        if (!locked)
        {
            locked = true;
            Lock(target, focus);
        } else
        {
            Swap(target, focus);
        }

        if (a)
        {
            MoonLocked = true;
            if (!locked) SunLocked = false;
        }
        else
        {
            SunLocked = true;
            if (!locked) MoonLocked = false;
        }
    }

    public void Release(bool a)
    {
        Transform target = a ? sun : moon;
        Transform focus = a ? moon : sun;

        if (target == this.target)
        {
            Release(target, focus);
            locked = false;
        }
    }

    public void Lock(Transform target, Transform focus)
    {
        if (focus == sun) sunSprite.Lock();
        if (focus == moon) moonSprite.Lock();

        MovementVector = Vector3.zero;

        Vector3 presentPosition = transform.position;
        Vector3 focusOffset = focus.position - presentPosition;
        Vector3 targetOffset = target.position - presentPosition;

        transform.position = focus.position;
        target.position += targetOffset;
        focus.position -= focusOffset;
    }



    public void Release(Transform target, Transform focus)
    {
        if (focus == sun)
        {
            if (!SunLocked) return;
            sunSprite.Unlock();
            SunLocked = false;
        }
        if (focus == moon)
        {
            if (!MoonLocked) return;
            moonSprite.Unlock();
            MoonLocked = false;
        }

        Vector3 offset = (target.position - focus.position) / 2;

        RecalculateSpeed();

        Vector3 direction = Vector3.Cross((target.position - focus.position), Vector3.forward).normalized;

        MovementVector = (Quantize(direction, 16) * -spin);

        transform.position += offset;
        target.position -= offset;
        focus.position -= offset;
    }

    public void Swap(Transform target, Transform focus)
    {
        Vector3 offset = focus.position - transform.position;

        transform.position = focus.position;
        focus.position -= offset;
        target.position -= offset;

        if (focus == sun)
        {
            sunSprite.Lock();
            moonSprite.Unlock();
            MoonLocked = false;
            SunLocked = true;
        }
        if (focus == moon)
        {
            moonSprite.Lock();
            sunSprite.Unlock();
            SunLocked = false;
            MoonLocked = true;
        }
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

        this.transform.position = this.transform.position + (((MovementVector + (CurrentInfluence != null ? CurrentInfluence.Vector : Vector3.zero)) * Time.deltaTime) * linearSpeed);
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

    private Vector3 SumInfluences(List<Mover> m)
    {
        Vector3 res = Vector3.zero;
        for (int i = 0; i < m.Count; i++) 
        {
            res += m[i].Vector;
        }
        return res;
    }
}
