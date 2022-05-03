using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatUpAndDown : MonoBehaviour
{

    Vector3 centralPos;

    private void Start()
    {
        centralPos = transform.position;
    }

    private void Update()
    {
        Random.seed = GetInstanceID();
        transform.position = centralPos + new Vector3(0, Mathf.Sin((Time.realtimeSinceStartup + Random.Range(0, 2f))) / 2f + 1f, 0);
    }
}
