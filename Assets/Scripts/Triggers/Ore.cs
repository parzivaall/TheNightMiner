using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour
{
    public GameObject rockBody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnDestroy()
    {
        Destroy(rockBody);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
