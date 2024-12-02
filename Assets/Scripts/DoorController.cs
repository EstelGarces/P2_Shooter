using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool isActive = false;

    private Animator animator;
    private float timeActive = 0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RestartAnimation();
    }

    private void OnTriggerEnter2D()
    {
        isActive = true;
        animator.SetBool("isUsed", true);
    }

    private void RestartAnimation()
    {
        if (isActive) { 
            timeActive = timeActive + Time.deltaTime;
            if (timeActive > 1.2) {
                isActive = false;
                animator.SetBool("isUsed", false);
                timeActive = 0f;
            }
        }
    }
}
