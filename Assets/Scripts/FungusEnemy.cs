using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusEnemy : MonoBehaviour
{
    Control control;
    Renderer renderer;
    bool destroying = false;
    MeshCollider playerCollider;
    MeshCollider playerCollider2;

    private void Start()
    {
        control = (Control)GameObject.FindObjectOfType<Control>();
        renderer = transform.parent.GetComponent<Renderer>();
        playerCollider = GetComponent<MeshCollider>();
        playerCollider2 = transform.parent.GetComponent<MeshCollider>();
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.name == "Player" && !destroying && (collision.gameObject.GetComponent<PlayerController>() as PlayerController).invincibleTimer == 0)
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
            if (! destroying)
            {
                destroying = true;
                StartCoroutine(destroy());
            }
        }
    }

    private IEnumerator destroy()
    {
        playerCollider.enabled = false;
        playerCollider2.enabled = false;
        float alpha = 1;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * 2f;
            if (alpha < 0) alpha = 0;
            renderer.material.color = new Color(1,1,1,alpha);
            yield return null;
        }
        Destroy(transform.parent.gameObject);
    }

}
