using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{

    public bool clock;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (clock) this.transform.Rotate(Vector3.up, .15f);
        else this.transform.Rotate(Vector3.up, -.15f);
    }
}
