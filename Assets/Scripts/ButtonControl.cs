using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{

    private int maxHealth = 100;
    public void LoadScene(int level)
    {
        SceneManager.LoadScene(level);
        
    }

    public void resetHealth()
    {
        EnvManager.Instance.setHealth(maxHealth);
    }

    public void resetScore()
    {
        EnvManager.Instance.setScore(0);
    }


}