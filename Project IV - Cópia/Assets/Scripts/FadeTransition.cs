using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeTransition : MonoBehaviour
{
    Image itiz;
    public float velocity;
    float speed;
    private float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        itiz = this.GetComponent<Image>();
        speed = 0;
        itiz.color  = new Color(itiz.color.r, itiz.color.g, itiz.color.b, speed);
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += ((1 * Time.deltaTime) + velocity)/100;
        itiz.color = new Color(itiz.color.r, itiz.color.g, itiz.color.b, currentTime);

    }
    void PanelFade() {
        itiz.color  = new Color(itiz.color.r, itiz.color.g, itiz.color.b, 0);
    }
}
