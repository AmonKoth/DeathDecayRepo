using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skelly : MonoBehaviour
{
    private float decayTime = 0.0f;
    public Creep skeletonPrefab;
    private GameObject creepParent;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Decay()
    {
        decayTime += Time.deltaTime;
        if (decayTime >= 5)
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        Decay();
    }

    public void ReAnimate()
    {
        creepParent = GameObject.FindGameObjectWithTag("parent");
        Instantiate(skeletonPrefab, this.transform.position, Quaternion.identity, creepParent.transform);
        Destroy(gameObject);
    }


}
