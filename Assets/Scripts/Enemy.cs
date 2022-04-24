using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Enemy : MonoBehaviour
{
    Control control;
    bool destroying = false;
    public List<Renderer> renderers;
    public List<MeshCollider> meshColliders;

    private void Start()
    {
        control = (Control)GameObject.FindObjectOfType<Control>();

        if (IsFungusEnemy()) //fix for all those fungus enemies that need to be reimported since their references are broken
        {
            renderers = new List<Renderer>();
            renderers.Add(transform.parent.GetComponent<MeshRenderer>());
            meshColliders = new List<MeshCollider>();
            meshColliders.Add(transform.GetComponent<MeshCollider>());
            meshColliders.Add(transform.parent.GetComponent<MeshCollider>());
        }
    }

    private bool IsFungusEnemy()
    {
        return transform.parent != null && transform.parent.parent != null && transform.parent.parent.name.Substring(0, 12) == "Fungus Enemy";
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
        Debug.Log(meshColliders.Count);

        foreach (MeshCollider m in meshColliders) {
            m.enabled = false;
        }
        float alpha = 1;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * 2f;
            if (alpha < 0) alpha = 0;
            foreach (Renderer r in renderers)
                r.material.color = new Color(1,1,1,alpha);
            yield return null;
        }

        if (IsFungusEnemy())
            Destroy(transform.parent.gameObject);
        else
            Destroy(transform.gameObject);
    }

}
