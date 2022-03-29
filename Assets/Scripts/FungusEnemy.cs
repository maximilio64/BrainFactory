using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusEnemy : MonoBehaviour
{
    Control control;

    private void Start()
    {
        control = (Control)GameObject.FindObjectOfType<Control>();
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //Debug.Log("hit");
            //(collision.gameObject.GetComponent<PlayerController>() as PlayerController).conveyerDirection = PlayerController.ConveyerDirection.right;

            Vector3 playerPos = collision.gameObject.transform.position;
            Vector3 myPos = transform.position;

            Vector3 launchDirection = playerPos - myPos;

            (collision.gameObject.GetComponent<PlayerController>() as PlayerController).enemyLaunch = launchDirection.normalized * 15f;
            (collision.gameObject.GetComponent<PlayerController>() as PlayerController).invincibleTimer = 100f;
            control.ChangeLives(-1);

        }

        if (collision.gameObject.tag == "kill_enemy")
        {
            Destroy(transform.parent.gameObject);
        }
    }

}
