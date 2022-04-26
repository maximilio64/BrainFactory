using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Enemy : MonoBehaviour
{
    Control control;
    public List<Renderer> renderers;
    public List<MeshCollider> meshColliders;

    private List<int> pastExplosions = new List<int>();

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

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "kill_enemy")
        {
            int explodeId = collision.gameObject.GetComponent<Explosion>().id;
            Debug.Log("id: " + explodeId);
            if (!pastExplosions.Contains(explodeId))
            {
                pastExplosions.Add(explodeId);
                StopAllCoroutines();
                StartCoroutine(destroy());
            }
        }
    }

    public int lives = 2;
    bool destroying = false;

    private IEnumerator destroy()
    {
        lives--;

        if (lives > 0)
        {
            float alpha = 1;
            while (alpha > .5f)
            {
                alpha -= Time.deltaTime * 2f;
                if (alpha < .5f) alpha = .5f;
                foreach (Renderer r in renderers)
                    r.material.color = new Color(1, 1, 1, alpha);
                yield return null;
            }

        } else
        {
            destroying = true;

            foreach (MeshCollider m in meshColliders)
            {
                m.enabled = false;
            }
            float alpha = .5f;
            while (alpha > 0)
            {
                alpha -= Time.deltaTime * 2f;
                if (alpha < 0) alpha = 0;
                foreach (Renderer r in renderers)
                    r.material.color = new Color(1, 1, 1, alpha);
                yield return null;
            }

            if (IsFungusEnemy())
                Destroy(transform.parent.parent.gameObject);
            else
                Destroy(transform.gameObject);
        }
    }

}
