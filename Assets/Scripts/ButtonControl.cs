using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    private EnvManager _env;

    private void start(){
        _env = EnvManager.Instance;
    }

    private int maxHealth = 100;
    public void LoadScene(int level)
    {
        EnvManager.Instance.LoadScene(level);
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