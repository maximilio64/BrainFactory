using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    Transform playerTrans;
    // Start is called before the first frame update
    void Start()
    {
        playerTrans = GameObject.Find("Player").transform.Find("WalkModel").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerTrans.position);
    }
}
