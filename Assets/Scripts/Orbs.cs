using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbs : MonoBehaviour
{


    Control control;

    Vector3 centralPos;

    private void Start()
    {
        control = (Control)GameObject.FindObjectOfType<Control>();
        centralPos = transform.position;

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            control.ChangeOrbs(1);
            Destroy(this.gameObject);

        }
    }

    private void Update()
    {
        Random.seed = GetInstanceID();
        transform.position = centralPos + new Vector3(0, Mathf.Sin((Time.realtimeSinceStartup + Random.Range(0, 2f))) / 2f + 1f, 0);
    }
}
