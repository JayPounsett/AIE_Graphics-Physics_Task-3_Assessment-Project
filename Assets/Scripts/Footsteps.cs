using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public Transform[] feet;
    public GameObject prefab;

    public void Step(int foot)
    {
        // foot has to be >0 to register in the animation event
        // so values of 1 and 2 correspond to 
        // the first and second foot, hence the -1 here
        GameObject go = Instantiate(prefab, feet[foot - 1].position, Quaternion.identity);
        // kill the particles after 5 seconds. Make sure they don't loop,
        // and have a lifetie <5 seconds
        Destroy(go, 5);
    }
}
