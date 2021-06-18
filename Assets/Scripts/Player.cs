using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Revive")]
    public float reviveRadius = 4.0f;
    
    [Header("Movement")]
    public float moveSpeed = 12.0f;
    public float turnSpeed = 250.0f;

    [Header("Particle Systems")]
    public ParticleSystem revive;
    public ParticleSystem reanimate;
    private void InputHandle()
    {
        float translation = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float rotation = Input.GetAxisRaw("Horizontal") * turnSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        //simple movement
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);

        if (Input.GetMouseButtonDown(0))
        {
            revive.Play();
            Creep[] creeps;
            creeps = FindObjectsOfType<Creep>();
            foreach (Creep creep in creeps)
            {
                if (Vector3.Distance(this.transform.position, creep.transform.position) <= reviveRadius)
                {
                    creep.Ressurrect();
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            revive.Stop();
        }
        if (Input.GetMouseButtonDown(1))
        {
            reanimate.Play();
            Skelly[] bonePiles;
            bonePiles = FindObjectsOfType<Skelly>();
            foreach (Skelly pile in bonePiles)
            {
                if (Vector3.Distance(this.transform.position, pile.transform.position) <= reviveRadius)
                {
                    pile.ReAnimate();
                }
            }

            Creep[] creeps;
            creeps = FindObjectsOfType<Creep>();
            foreach (Creep creep in creeps)
            {
                if (Vector3.Distance(this.transform.position, creep.transform.position) <= reviveRadius&&
                    creep.isSkeleton)
                {
                    creep.Ressurrect();
                }
            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            reanimate.Stop();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        InputHandle();
    }
}
