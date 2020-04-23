using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterAttack : MonoBehaviour
{
    public GameObject player;
    public Transform monster;

    private void OnCollisionEnter(Collision collision)
    {
        if(monster.gameObject.GetComponent<MonsterController>().dontRotate == false)
        {
            if (collision.gameObject == player)
            {
                player.GetComponent<Health>().HitByMonster(monster.position);
            }
        }

    }
}
