using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backnforth : MonoBehaviour
{

    float timer;
    public float mtimer;
    public bool direct;
    public float speed;
    Vector3 mov;

    // Start is called before the first frame update
    void Start()
    {
        if (direct) timer = mtimer;
        else timer = 0f;
        mov = new Vector3(0, 0, -1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= 0f)
        {
            timer = mtimer;
            mov = -mov;
        }
        transform.Translate(mov * speed * Time.deltaTime);
        timer -= .1f * Time.deltaTime;
    }
}
