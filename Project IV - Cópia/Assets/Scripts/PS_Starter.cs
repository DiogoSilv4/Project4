using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_Starter : MonoBehaviour
{
    public float initTime;
    public ParticleSystem PE;

    void Start () {
        //PE = this;
        PE.Simulate(initTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
