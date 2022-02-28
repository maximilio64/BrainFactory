using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyer : MonoBehaviour
{

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<CharacterController>().enabled = false;
            collision.gameObject.transform.localPosition += new Vector3(.1f, 0, 0);
            collision.gameObject.GetComponent<CharacterController>().enabled = true;
        }
        else
            collision.gameObject.transform.position += new Vector3(.1f,0,0);
    }
}
