using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameObject TopDownCamera;
    public GameObject ShoulderCamera;
    public bool CanInteract;

    public float MovementSpeed;
    public float TurnSpeed;
    public bool CanMove = true;

    public GameObject InteractingNPC;

    public DialogueManager TheDialogueManager;

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
            StartCoroutine(SwapCamera());
            TheDialogueManager.StartDialogue(InteractingNPC);
            transform.position = InteractingNPC.GetComponent<NPCController>().DialoguePosition;
            transform.LookAt(InteractingNPC.transform.position);
            CanMove = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CanMove)
        {
            float l_VerticalInput = Input.GetAxis("Vertical");
            float l_HorizontalInput = Input.GetAxis("Horizontal");

            m_Controller.SimpleMove(transform.forward * l_VerticalInput * MovementSpeed * Time.fixedDeltaTime);

            transform.Rotate(transform.up * l_HorizontalInput * TurnSpeed * Time.fixedDeltaTime);
        }
    }

    public IEnumerator SwapCamera()
    {
        if(TopDownCamera.activeInHierarchy)
        {
            TopDownCamera.GetComponent<CameraController>().FadeOut();
            yield return new WaitForSeconds(1.0f);
            TopDownCamera.SetActive(false);
            ShoulderCamera.SetActive(true);
            ShoulderCamera.GetComponent<CameraController>().FadeIn();
        }
        else
        {
            ShoulderCamera.GetComponent<CameraController>().FadeOut();
            yield return new WaitForSeconds(1.0f);
            ShoulderCamera.SetActive(false);
            TopDownCamera.SetActive(true);
            TopDownCamera.GetComponent<CameraController>().FadeIn();
        }
    }
}
