using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyer : MonoBehaviour
{

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            (collision.gameObject.GetComponent<PlayerController>() as PlayerController).conveyerDirection = transform.right * 1f;
        }
        else
            collision.gameObject.transform.position += transform.right * Time.deltaTime * 1f;
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            (collision.gameObject.GetComponent<PlayerController>() as PlayerController).conveyerDirection = new Vector3(0, 0, 0);
        }
    }
}
