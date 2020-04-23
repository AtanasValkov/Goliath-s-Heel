using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappler : MonoBehaviour
{
    public Transform grapplerEnd, player;
    private Transform mainCamera;
    public LayerMask whatIsGrappleable;
    public LayerMask blockingGrapple;
    public BoolEvent canHitSomething;
    public float grappleRate;
    public float grapplingRange;

    public bool mouseHoldMode;

    private RaycastHit hit;
    private Vector3 grapplePoint;
    private SpringJoint joint;
    private float nextGrapple;
    private LineRenderer lr;
    private Vector3 currentGrapplePosition;    

    private Rigidbody rb;
    public bool boostEnabled;
    public float boostRate;
    public float thrust;
    private bool ReadyBoost;

    public FloatEvent boostTimerStart;

    void Awake()
    {
        lr = GetComponent<CreateLineRenderer>().Create();

        rb = player.gameObject.GetComponent<Rigidbody>();

        mainCamera = transform.parent.transform;

        ReadyBoost = true;
    }

    void Update()
    {
        bool hitSomething = Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, grapplingRange, whatIsGrappleable | blockingGrapple);
        if (hitSomething)
        {
            hitSomething = !(hit.transform.gameObject.layer == LayerMask.NameToLayer("Monster"));
        }
            
        canHitSomething.Invoke(hitSomething && !IsGrappling());

        if (mouseHoldMode)
        {
            if (IsGrappling())
            {
                if (Input.GetButtonUp("UseGrappler")) StopGrapple();
            }
            else
            {
                if (Input.GetButtonDown("UseGrappler") && Time.time > nextGrapple)
                {
                    if (hitSomething)
                    {
                        //Debug.Log("Grapple Distance: " + hit.distance);
                        StartGrapple();
                    }

                    else {} // you missed.
                }
            }
        }
        else
        {
            if (Input.GetButtonDown("UseGrappler") && Time.time > nextGrapple)
            {
                if (IsGrappling()) StopGrapple();

                else if (hitSomething)
                {
                    //Debug.Log("Grapple Distance: " + hit.distance);
                    StartGrapple();
                }

                else {} // you missed.
            } 
        }

        if (Input.GetButtonDown("Boost") && ReadyBoost && boostEnabled)
        {
            if(IsGrappling()) Boost();
        }

        if (Input.GetKeyDown("0")) ChangeGrappleMode();
    }

    void LateUpdate()
    {
        if (IsGrappling()) //If not grappling, don't draw rope
        {
            DrawRope();
        }
    }

    void StartGrapple()
    {
        // the grappleRate is only there to prevent immediate disconnect, not immediate connection
        nextGrapple = Time.time + grappleRate;

        grapplePoint = hit.point;
        joint = player.gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = grapplePoint;

        float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

        // The distance grapple will try to keep from grapple point. 
        joint.maxDistance = distanceFromPoint * 0.8f;
        joint.minDistance = distanceFromPoint * 0.25f;

        // Adjust these values to whatever fits our game.
        joint.spring = 4.5f;
        joint.damper = 7f;
        joint.massScale = 4.5f;

        lr.positionCount = 2;
        currentGrapplePosition = grapplerEnd.position;
    }

    public void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }

    void Boost()
    {
        nextGrapple = Time.time;

        ReadyBoost = false;
        boostTimerStart.Invoke(boostRate);

        Vector3 desDirection = (grapplePoint - player.position).normalized;

        rb.velocity = Vector3.zero;
        rb.AddForce(desDirection * thrust);

        StopGrapple();
    }

    public void boostTimerEnd()
    {
        ReadyBoost = true;
    }

    void DrawRope()
    {
        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);
    
        lr.SetPosition(0, grapplerEnd.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }

    void ChangeGrappleMode()
    {
        mouseHoldMode = !mouseHoldMode;
    }
}
