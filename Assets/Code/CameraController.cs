using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float trackSpeed;
    public float trackSmoothing;
    public float lookAhead;
    public float spinDistance;
    public static Vector3 lastMovement;
    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        target = player.transform;
        this.transform.position = target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!player) return;
        float smoothedSpeed = Mathf.Abs((this.transform.position - target.position).magnitude) * trackSmoothing;
        Vector3 aheadTarget = target.position + (player.MovementVector * player.MovementSpeed * lookAhead);
        if(player.SunLocked) 
        {
            aheadTarget = target.position + ((Quaternion.AngleAxis(target.eulerAngles.z, Vector3.forward) * Vector3.right) * spinDistance);
        }
        else if(player.MoonLocked)
        {
            aheadTarget = target.position + ((Quaternion.AngleAxis(target.eulerAngles.z, Vector3.forward) * Vector3.left) * spinDistance);
        }
        Vector3 oldVector = this.transform.position;
        this.transform.position = Vector3.MoveTowards(this.transform.position, aheadTarget, (trackSpeed + smoothedSpeed) * Time.deltaTime);
        lastMovement = this.transform.position - oldVector;
    }
}
