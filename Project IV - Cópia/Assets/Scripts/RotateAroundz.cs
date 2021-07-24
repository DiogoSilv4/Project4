using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundz : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public bool look; 
   
    
    void Update()
    {
        this.transform.RotateAround(target.transform.position, Vector3.up, speed * Time.deltaTime);
        if(look){
            this.transform.LookAt(target.transform);
        }
    }
}
