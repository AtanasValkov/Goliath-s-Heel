using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class MonsterController : MonoBehaviour
{
    public NavMeshAgent agent;
    private Vector3 coord;
    public List<GameObject> weakPoints;
    public Transform bone,camedra,tail;
    public GameObject target;
    public Animator anim;
    private IEnumerator fight,chase;
    public AudioSource boombox;
    public bool dontRotate = true;
    private float tailRot,saveTailRot,time,t = -200; 
    private bool betweenLegs = false,tailBack;
    public EndGame end;
    public LevelTimer timer;

    // Start is called before the first frame update
    void Start()
    {
        tailRot = tail.eulerAngles.z;
        saveTailRot = tailRot;
        fight = Fight();
        chase = Chase();
        coord = target.transform.position;
        agent.SetDestination(coord);
        StartCoroutine(chase);
    }

    void LateUpdate()
    {
        if (dontRotate == false)
        {
            BodyPartRotation(t);
        }

        if(betweenLegs)
        {
            TailSwipe();
        }

        //Debug.Log(t);
    }

    void BodyPartRotation(float newrotation)
    {
        var pos = new Vector3(bone.eulerAngles.x, bone.eulerAngles.y, newrotation);
        bone.eulerAngles = pos;
    }

    void TailSwipe()
    {
        if (tailRot < 140 && tailBack == true)
        {
            tailRot += 2f;
            tail.eulerAngles = new Vector3(tail.eulerAngles.x, tail.eulerAngles.y, tailRot);
        }
        else
        {
            tailBack = false;
            tailRot -= 4f;
            tail.eulerAngles = new Vector3(tail.eulerAngles.x, tail.eulerAngles.y, tailRot);
            if (tailRot <= saveTailRot)
                tailBack = true;
        }
    }



        // Update is called once per frame
    void Update()
    {
        if (weakPoints.Count <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        agent.enabled = false;
        //gameObject.layer = 9;  this is supposed to make the monster walkable after it dies
        //gameObject.GetComponentInChildren<GameObject>().layer = 9;
        StopCoroutine(fight);
        StopCoroutine(chase);
        StartCoroutine(Death());
        dontRotate = true;
        time = timer.StopTimer();
        anim.SetBool("isDead", true);
    }

    IEnumerator Death()
    {
        while (true)
        {
            if(boombox.volume > 0)
            {
                boombox.volume -= 0.0003f;
            }
            if(boombox.volume == 0)
            {
                end.Win(time);
            }

            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator Chase()
    {
        while(true)
        {
            agent.isStopped = false;
            dontRotate = true;
            //Debug.Log(agent.remainingDistance);
            coord = target.transform.position;
            agent.SetDestination(coord);
            if (agent.remainingDistance < 40 && agent.remainingDistance > 0)
            {
                anim.SetBool("isFighting", true);
                StartCoroutine(fight);
                StopCoroutine(chase);
            }
            
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator Fight()
    {
        while(true)
        {
            dontRotate = false;
            t = target.transform.position.y - 180;
            //agent.isStopped = true;
           // Debug.Log(agent.remainingDistance);
            coord = target.transform.position;
            agent.updateRotation = true;
            agent.SetDestination(coord);
            if(agent.remainingDistance < 20)
            {
                betweenLegs = true;
            }
            else
            {
                betweenLegs = false;
            }
            if (agent.remainingDistance >= 40)
            {
                anim.SetBool("isFighting", false);
                StartCoroutine(chase);
                StopCoroutine(fight);
            }
            
            yield return new WaitForSeconds(0.1f);
        }
    }
}
