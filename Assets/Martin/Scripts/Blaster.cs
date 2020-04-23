using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    public Transform blasterEnd, player, mainCamera;

    public MonsterController monster;
    public walldestruct wall;

    public BoolEvent shotFired;

    public float blastRate;
    public float blastingRange;

    private RaycastHit hit;

    private AudioSource gunAudio;
    private float nextBlast;
    private bool hitWeakSpot;

    void Awake()
    {
        gunAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        bool hitSomething = Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, blastingRange);

        if (Input.GetButtonDown("UseWeapon") && Time.time > nextBlast)
        {
            Fire(hitSomething);
        }
    }

    void Fire(bool hitSomething)
    {
        hitWeakSpot = false;

        nextBlast = Time.time + blastRate;

        if (hitSomething)
        {
            Debug.Log("You shot (a) " + hit.transform.gameObject.name);

            if(hit.transform.gameObject.layer == 11)
            {
                hitWeakSpot = true;
                hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                monster.weakPoints.Remove(hit.transform.gameObject);
            }
            if(hit.transform.gameObject.layer == 12)
            {
                hitWeakSpot = true;
                hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                wall.blocks.Remove(hit.transform.gameObject);
            }
        }
        else
        {
            Debug.Log("You missed everything");
        }

        StartCoroutine(BlastEffects());
    }

    private IEnumerator BlastEffects()
    {
        gunAudio.Play();

        // do the fancy animation(s)
        shotFired.Invoke(hitWeakSpot);

        yield return null;
    }
}
