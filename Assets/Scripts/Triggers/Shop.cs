using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            if(EnvManager.Instance.getRock() <= 0){
                EnvManager.Instance.text("Come back when... you have more... Rockssss");
            }
            if (EnvManager.Instance.getRock() > 0){
                EnvManager.Instance.text("YESSSSsss... More ROcksss... more ROckssss.");
                EnvManager.Instance.addScore(EnvManager.Instance.getRock());
                EnvManager.Instance.setRock(0);
            }
        }
        
        
    }
}
