using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float fwd = Input.GetAxis("Vertical");
        anim.SetFloat("Forward", Mathf.Abs(fwd));
        anim.SetFloat("Sense", Mathf.Sign(fwd));
        anim.SetFloat("Turn", Input.GetAxis("Horizontal"));
    }
}
