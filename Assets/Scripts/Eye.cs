using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    public GameObject[] objectsToMakeInvisible;
    public float minTime = 3f;
    public float maxTime = 10f;

    private float nextTimeToMakeInvisible;
    private bool isCurrentlyInvisible = false;

    // Start is called before the first frame update
    void Start()
    {
        nextTimeToMakeInvisible = Time.time + Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextTimeToMakeInvisible && !isCurrentlyInvisible)
        {
            MakeObjectsInvisible();
        }
        else if (Time.time > nextTimeToMakeInvisible && isCurrentlyInvisible)
        {
            MakeObjectsVisible();
        }
    }

    private void MakeObjectsInvisible()
    {
        foreach (GameObject obj in objectsToMakeInvisible)
        {
            obj.SetActive(false);
        }
        isCurrentlyInvisible = true;
        nextTimeToMakeInvisible = Time.time + 0.1f;
    }

    private void MakeObjectsVisible()
    {
        foreach (GameObject obj in objectsToMakeInvisible)
        {
            obj.SetActive(true);
        }
        isCurrentlyInvisible = false;
        nextTimeToMakeInvisible = Time.time + Random.Range(minTime, maxTime);
    }
}


