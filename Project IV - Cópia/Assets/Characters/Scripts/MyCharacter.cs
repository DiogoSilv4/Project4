using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacter : MonoBehaviour
{
    public Transform targetPosition;
    public float moveSpeed = 4;
    public float maxSpeed = 4;
    private CharacterController characterController;
    private Animator animator;
    private float danceTimer = 0;
    private bool isDancing = false;
    private int danceCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(new Vector3(targetPosition.position.x, 0, targetPosition.position.z),
                                          new Vector3(transform.position.x, 0, transform.position.z));
        if (distance / 2.5f < 1)
        {
            animator.SetFloat("Speed", distance / 2.5f);
        }
        else
        {
            animator.SetFloat("Speed", 1);
        }
        if (distance / 2.5f < 0.06f)
        {
            animator.SetFloat("Speed", 0);
        }

        if ((distance / 2.5f < 0.1f) && (isDancing == false) && (danceCount == 0))
        {
            animator.SetBool("CanDance", true);
            isDancing = true;
        }

        if (isDancing)
        {
            danceTimer = danceTimer + Time.deltaTime;
            if (danceTimer >= 10)
            {
                isDancing = false;
                danceTimer = 0;
                danceCount++;
                animator.SetBool("CanDance", false);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = Vector3.Distance(new Vector3(targetPosition.position.x, 0, targetPosition.position.z),
                                          new Vector3(transform.position.x, 0, transform.position.z));
        if (distance > 0.1f)
        {
            Vector3 direction = Vector3.Normalize(targetPosition.position - transform.position);
            if (distance < 2.5f)
            { 
                if (characterController.velocity.magnitude < maxSpeed)
                {
                    characterController.SimpleMove(direction * (distance / 2));
                }
            }
            else
            {
                if (characterController.velocity.magnitude < maxSpeed)
                {
                    characterController.SimpleMove(direction * moveSpeed);
                }
            }
            Vector3 lookAtPosition = new Vector3(targetPosition.position.x, transform.position.y, targetPosition.position.z);
            transform.LookAt(lookAtPosition);
        }
        
    }
}
