using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Ticker : MonoBehaviour
{
    IEnumerable<Tick> ticks;
    int everyOther = 0;

    // Start is called before the first frame update
    void Start()
    {
        ticks = FindObjectsOfType<MonoBehaviour>().OfType<Tick>();
        StartCoroutine(doTick());
    }

    IEnumerator doTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            everyOther = (everyOther + 1) % 3;
            foreach (Tick t in ticks)
            {
                t.tick(everyOther);
            }
        }
    }
}
