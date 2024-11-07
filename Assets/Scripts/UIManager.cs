using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager Instance;
    public EnvManager Environment;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        Instance = GetComponent<UIManager>();
        Environment = FindObjectOfType<EnvManager>();
        
        //setDialogue("Hello Friend");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDialogue(string textToAppear)
    {
        StartCoroutine(AppearAndDissapearText(textToAppear));
    }

    public IEnumerator AppearAndDissapearText(string textToAppear)
    {
        text.text = textToAppear;
        Color initialColor = text.color;
        text.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0);
        while(text.color.a < 1)
        {
            text.color = new Color(initialColor.r, initialColor.g, initialColor.b, text.color.a + Time.deltaTime * 5);
            yield return null;
        }
        yield return new WaitForSeconds(3);
        while(text.color.a > 0)
        {
            text.color = new Color(initialColor.r, initialColor.g, initialColor.b, text.color.a - Time.deltaTime * 5);
            yield return null;
        }
    }


}
