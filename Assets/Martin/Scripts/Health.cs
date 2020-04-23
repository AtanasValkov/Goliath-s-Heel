using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    int health;
    int maxHealth = 100;

    private bool invincible;
    private IEnumerator death;

    public int monsterDamagePercent;
    
    public UnityEvent InvincibleStart;
    public DoubleIntEvent healthChanged;
    
    public bool useFallDamage = true;

    private Rigidbody rb;
    private PlayerMovement mv;

    public int monsterForce;
    public Grappler gr;
    public EndGame end;

    private AudioSource punchedAudio;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        mv = GetComponent<PlayerMovement>();

        punchedAudio = GetComponent<AudioSource>();

        invincible = false;

        death = Death();
    }

    void SetHealth(int newHealth)
    {
        if (this.enabled)
        {
            health = newHealth;
            healthChanged.Invoke(health, maxHealth);
            if (health == 0)
            {
                mv.enabled = false;
                StartCoroutine(death);
            }
        }
    }

    void ChangeHealth(int change)
    {
        int newHealth = health + change;
        if(newHealth < 0) newHealth = 0;
        if(newHealth > maxHealth) newHealth = maxHealth;
        SetHealth(newHealth);
    }

    void Start()
    {
        SetHealth(maxHealth);
    }

    public void HeavyFall(int speed)
    {
        if(useFallDamage)
        {
            if(speed < 15) ChangeHealth(-maxHealth / 10); // 10% damage
            else if(speed < 20) ChangeHealth(-maxHealth / 4); // 25% damage
            else if(speed >= 20) SetHealth(0); // 100% damage
        }
    }

    public void HitByMonster(Vector3 monsterLoc)
    {
        if (!invincible)
        {
            punchedAudio.Play();

            int change = (int) (-maxHealth * ((float) monsterDamagePercent / 100));
            ChangeHealth(change);

            invincible = true;
            InvincibleStart.Invoke();

            gr.StopGrapple();

            BelieveYouCanFly(monsterLoc);
        }
    }

    void BelieveYouCanFly(Vector3 monsterLoc)
    {
        //invincibilityTimerStart.Invoke(damageTimer);  // for later maybe?

        Vector3 launchDirection = GetLaunchDirection(monsterLoc);
        
        rb.velocity = Vector3.zero;
        rb.AddForce(launchDirection * monsterForce);
    }

    Vector3 GetLaunchDirection(Vector3 monsterLoc)
    {
        Vector3 playerHorizontal = RemoveHeight(transform.position);
        Vector3 monsterHorizontal = RemoveHeight(monsterLoc);

        Vector3 dirHorizontal = (playerHorizontal - monsterHorizontal).normalized;

        return (dirHorizontal + Vector3.up).normalized;
    }

    Vector3 RemoveHeight(Vector3 original)
    {
        return new Vector3(original.x, 0, original.z);
    }

    private IEnumerator Death()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(1f); 
        }
        end.Lose();
    }

    public void InvincibleEnd()
    {
        invincible = false;
    }
}
