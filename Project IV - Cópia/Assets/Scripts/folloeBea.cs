using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class folloeBea : MonoBehaviour
{
    public Transform bea;
    public Transform point;
    private float number;
    // Start is called before the first frame update
    void Start()
    {
          number = bea.position.y - point.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        point.position = new Vector3(bea.position.x,bea.position.y - number ,bea.position.z);
    }
}
