using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creep : MonoBehaviour
{
    NavMeshAgent agent;
    private Animator anim;
    private CapsuleCollider capsCollider;
 
    [HideInInspector]
    public GameObject target;

    [Header("Health")]
    public float maxHealth = 100.0f;
    public float damageTaken = 40.0f;

    [Header("Death")]
    public Skelly corpse;
    public bool isSkeleton = false;
    
    [HideInInspector]
    public bool isDead ()
    {
        if (health >0)
        {
            return false;
        }
        else { return true; }
    }
    private float health = 0.0f;
    private float deadTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("target");
        capsCollider = GetComponent<CapsuleCollider>();

        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
        if (!agent)
        {
            gameObject.AddComponent<NavMeshAgent>();
            agent = this.GetComponent<NavMeshAgent>();
        }
        if(!capsCollider)
        {
            gameObject.AddComponent<CapsuleCollider>();
            capsCollider = this.GetComponent<CapsuleCollider>();
        }
        health = maxHealth;
        EnableComponents();
    }

    public void EnableComponents()
    {
        if(!agent.enabled)
        {
            agent.enabled = true;
        }
        if(!capsCollider.enabled)
        {
            capsCollider.enabled = true;
        }
    }

    private void TakeDamage()
    {
        this.health -= damageTaken ;
        isDead();
        if(this.health<=0)
        {
            DeathHandle();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        TakeDamage();
    }
    
    private void DeathHandle()
    {
        capsCollider.enabled = false;
        agent.enabled = false;
    }

    private void Decay()
    {
        if (!isSkeleton)
        {
            deadTime += Time.deltaTime;
            if (deadTime >= 5)
            {
                Instantiate(corpse, this.transform.position, Quaternion.identity);
                deadTime = 0;
                Destroy(gameObject);
            }
        }else
        {
            deadTime += Time.deltaTime;
            if (deadTime >= 5)
            {
                deadTime = 0;
                Destroy(gameObject);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void EnableMove()
    {
        agent.SetDestination(target.transform.position);

    }
    public void Ressurrect()
    {
        health = maxHealth;
    }

    private void LateUpdate()
    {
        if(health == maxHealth)
        {
            EnableComponents();
        }
        if (agent.enabled)
        {
            EnableMove();
        }

        if(isDead())
        {
            Decay();
        }
        else { deadTime = 0.0f; }
    }
}
