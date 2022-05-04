using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : Change
{
    bool shouldBeActive;
    public bool otherAxis = false;
    // Start is called before the first frame update
    void Start()
    {
        shouldBeActive = shouldBeActive();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldBeActive)
        {
            if (! otherAxis)
                transform.RotateAround(transform.position, Vector3.up, 30 * Time.deltaTime);
            else
                transform.RotateAround(transform.position, Vector3.right, 30 * Time.deltaTime);
        }
    }
}
