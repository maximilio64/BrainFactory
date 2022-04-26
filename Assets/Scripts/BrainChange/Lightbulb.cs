using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightbulb : Change
{
    // Start is called before the first frame update
    void Start()
    {
        if (!shouldBeActive())
            transform.Find("lightbulb/Light").gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
