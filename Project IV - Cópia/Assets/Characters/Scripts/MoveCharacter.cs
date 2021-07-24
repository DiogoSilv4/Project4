using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{    
    public GameObject[] points;
    public float[] waitTime;
    public float moveSpeed = 0.5f;
    public float maxSpeed = 0.5f;
    public float stopDistance = 0.3f;
    private Transform targetPosition;
    private Transform lookPosition;
    private CharacterController characterController;
    private Animator animator;
    private int currentPoint = 0;
    bool[] reached = new bool[3];

    void Awake(){
        for(int i = 0; i < 3 ; i++){
            reached[i] = false;
        }
    }
    void Start(){
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        targetPosition = points[currentPoint].transform;

        
    }

    private IEnumerator StartMovementx(){

        
                    targetPosition = points[currentPoint].transform;

            
            float distance = Vector3.Distance(new Vector3(targetPosition.position.x, 0, targetPosition.position.z),
                                              new Vector3(transform.position.x, 0, transform.position.z));

            if (distance <= stopDistance)
            {
                reached[currentPoint] = true;
                animator.SetFloat("Speed", 0);
                yield return new WaitForSeconds(waitTime[currentPoint]);
                if(currentPoint < points.Length - 1){
                    currentPoint = currentPoint + 1;

                }
            }

        
    }

    private void Update()
    {
        StartCoroutine("StartMovementx");


        if ( reached[currentPoint] == false)
        {
            float distance = Vector3.Distance(new Vector3(targetPosition.position.x, 0, targetPosition.position.z),
                                          new Vector3(transform.position.x, 0, transform.position.z));
            if (distance  / 2.5f < 1)
            {
                animator.SetFloat("Speed", distance / 2.5f);
            }
            else
            {
                animator.SetFloat("Speed", 1);
            }
            if (distance  / 2.5f < 0.06f)
            {
                animator.SetFloat("Speed", 0);
            }
        }
    }

    private void FixedUpdate()
    {

        if ( reached[currentPoint] == false)
        {
            float distance = Vector3.Distance(new Vector3(targetPosition.position.x, 0, targetPosition.position.z),
                                              new Vector3(this.transform.position.x, 0, this.transform.position.z));
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
                if(lookPosition != null){
                     lookAtPosition = new Vector3(lookPosition.position.x, transform.position.y, lookPosition.position.z);
                }
                
                transform.LookAt(lookAtPosition);
            }
        }
    }

}
