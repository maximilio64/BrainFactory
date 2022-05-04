using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPower : MonoBehaviour
{
    MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime;
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(4f);

        StartCoroutine(Blink());

        yield return new WaitForSeconds(1f);

        Destroy(this.gameObject);
    }

    IEnumerator Blink()
    {
        while (true)
        {
            meshRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
            meshRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }
}
