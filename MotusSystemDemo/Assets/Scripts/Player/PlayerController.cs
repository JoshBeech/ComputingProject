using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameObject TopDownCamera;
    public GameObject ShoulderCamera;
    public bool CanInteract;

    public float MovementSpeed;
    public float TurnSpeed;

    private CharacterController m_Controller;

    // Use this for initialization
    void Start()
    {
        ShoulderCamera.SetActive(false);
        CanInteract = false;
        m_Controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(CanInteract && Input.GetButtonDown("Interact"))
        {
            SwapCamera();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float l_VerticalInput = Input.GetAxis("Vertical");
        float l_HorizontalInput = Input.GetAxis("Horizontal");

        m_Controller.SimpleMove(transform.forward * l_VerticalInput * MovementSpeed * Time.fixedDeltaTime);

        transform.Rotate(transform.up * l_HorizontalInput * TurnSpeed * Time.fixedDeltaTime);
    }

    public void SwapCamera()
    {
        if(TopDownCamera.activeInHierarchy)
        {
            TopDownCamera.SetActive(false);
            ShoulderCamera.SetActive(true);
        }
        else
        {
            ShoulderCamera.SetActive(false);
            TopDownCamera.SetActive(true);
        }
    }
}
