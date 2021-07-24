using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorShower : MonoBehaviour
{
    public GameObject[] fractured;


    public Transform meteorTarget;
    public Rigidbody meteorRigidbody;

    public float turn;
    public float meteorVelocity;


    private void FixedUpdate()
    {
        meteorRigidbody.velocity = transform.forward * meteorVelocity;
        var meteorTargetRotation = Quaternion.LookRotation(meteorTarget.position - transform.position);
        meteorRigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, meteorTargetRotation, turn));
        
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("star"))
        {
        for (int i = 0; i < fractured.Length; i++)
        {
            Instantiate(fractured[i], transform.position, transform.rotation); //Spawn in the broken version

        }
        Destroy(gameObject); //Destroy the object to stop it getting in the way

        }

    }
}
