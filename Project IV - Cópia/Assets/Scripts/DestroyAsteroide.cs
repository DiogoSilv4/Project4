using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAsteroide : MonoBehaviour
{
    public string Tag;
    // Start is called before the first frame update
    void Awake()
    {
        DestroyAll(Tag);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DestroyAll(string tag)
     {
         GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
         for(int i=0; i< enemies.Length; i++)
         {
             Destroy(enemies[i]);
         }
     }
}
