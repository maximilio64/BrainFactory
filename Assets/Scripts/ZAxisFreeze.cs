using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZAxisFreeze : MonoBehaviour
{

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
