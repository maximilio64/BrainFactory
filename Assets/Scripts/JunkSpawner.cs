using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkSpawner : MonoBehaviour
{
    public List<GameObject> trash;
    public List<GameObject> good;

    float ratio;

    // Start is called before the first frame update
    void Start()
    {
        ratio = SaveData.FactoryScore() / SaveData.TotalPossibleScore();
        StartCoroutine(spawningTrash());
    }

    IEnumerator spawningTrash()
    {
        while (true)
        {
            yield return new WaitForSeconds(.7f);

            GameObject newObject;

            //if (Random.value < ratio)
            if (Random.value < .5f)
            {
                newObject = Instantiate(good[Random.Range(0, good.Count)]);
            } else
            {
                newObject = Instantiate(trash[Random.Range(0, trash.Count)]);
            }

            newObject.GetComponent<Rigidbody>().velocity = new Vector3((Random.value - .5f), (Random.value - .5f), (Random.value - .5f));

            newObject.transform.localPosition = new Vector3(0, 0, 0);
            newObject.transform.position = transform.position;

            newObject.transform.localRotation = Random.rotation;

            
        }
    }
}
