using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [HideInInspector]
    public ParticleSystem towerFire;

    private Transform target;

    [Header ("Attack")]
    public float maxDistance = 5.0f;
    public float attackSpeed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        towerFire = gameObject.GetComponentInChildren<ParticleSystem>();
        target = null;
        var emission = towerFire.emission;
        emission.rateOverTime = attackSpeed;
    }

    private void FindClosestTarget()
    {
        Creep[] creeps = FindObjectsOfType<Creep>();
        Transform checkEnemy = null;
        float checkDistance = Mathf.Infinity;
        foreach(Creep creep in creeps )
        {
            float targetDistance = Vector3.Distance(this.transform.position, creep.transform.position);
            if(targetDistance <= checkDistance && !creep.isDead())
            {

                checkDistance = targetDistance;
                checkEnemy = creep.transform;
            }

        }
        if (checkEnemy)
        {
            target = checkEnemy;
        }
        else
        {
            target = this.transform;
        }
    }

    private void RotateToTarget()
    {
        Vector3 lookAtTarget = new Vector3(target.transform.position.x,
                                           (target.transform.position.y + 1), 
                                           target.transform.position.z);
        towerFire.transform.LookAt(lookAtTarget);
    }

    private void AccuireTarget()
    {
        FindClosestTarget();
        RotateToTarget();
    }

    // Update is called once per frame
    void Update()
    {
        AccuireTarget();
    }
}
