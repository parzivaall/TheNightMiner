using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] public float mouseSensitivity = 3f;
    [SerializeField] private float maxLookAngle = 90f;

    [Header("References")]
    [SerializeField] private Transform playerBody;

    private float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            mouseSensitivity = PlayerPrefs.GetFloat("Sensitivity");
        }
    }

    private void Update()
    {
        if (!ExitMenu.GameIsPaused){
            // Get mouse input
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            // Rotate the player body horizontally
            playerBody.Rotate(Vector3.up * mouseX);

            // Rotate the camera vertically
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }
}
