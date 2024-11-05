using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 5.0f;
    private float horizontalInput;
    private float forwardInput;

    public AudioSource pickaxeSound;
    public GameObject pickaxe;
    public ParticleSystem pickaxeParticle;
    private bool swinging = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);

        if(Input.GetMouseButtonDown(0))
        {
            if(swinging == false){
                StartCoroutine(SwingPickaxe());
                swinging = true;
            
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition-new Vector3(0, Screen.height, 0));
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit, 5f)) // added a max distance of 3f
                {
                    Vector3 point = hit.point;
                    point.y += 0.1f;
                    pickaxeSound.Play();
                    Instantiate(pickaxeParticle, point, Quaternion.identity);
                    if(hit.transform.tag == "Rock"){
                        Destroy(hit.transform.gameObject);
                        Instantiate(pickaxeParticle, point, Quaternion.identity);
                        
                    }
                    if(hit.transform.tag == "Enemy"){
                        Destroy(hit.transform.gameObject);
                        Instantiate(pickaxeParticle, point, Quaternion.identity);
                    }
                

                }
            
            }
        
        
        }
    }

    IEnumerator SwingPickaxe()
    {
        float swingDuration = 0.2f; // duration of one swing cycle
        float elapsedTime = 0f;
        Vector3 startPosition = pickaxe.transform.localPosition;
        Vector3 forwardPosition = startPosition + new Vector3(0, 0, 0.5f); // adjust the Z value for desired swing distance

        while (elapsedTime < swingDuration)
        {
            pickaxe.transform.localPosition = Vector3.Lerp(startPosition, forwardPosition, (elapsedTime / swingDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < swingDuration)
        {
            pickaxe.transform.localPosition = Vector3.Lerp(forwardPosition, startPosition, (elapsedTime / swingDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        pickaxe.transform.localPosition = startPosition; // ensure it returns to the original position
        swinging = false;
    }
}
