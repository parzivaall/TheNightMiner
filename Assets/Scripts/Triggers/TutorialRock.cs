using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(EnvManager.Instance.getScore() > 0){
            gameObject.transform.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
