using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public GameObject TopDownCamera;
    public GameObject ShoulderCamera;
    public bool CanInteract;


    public float WalkSpeed;
    public float SprintSpeed;
    public float MovementSpeed;
    public float TurnSpeed;
    public bool CanMove = true;
    public bool IsMoving = false;
    public bool IsSprinting = false;

    public GameObject InteractingNPC;

    public DialogueManager TheDialogueManager;

    private CharacterController m_Controller;
    private Animator m_Animator;

    // Use this for initialization
    void Start()
    {
        ShoulderCamera.SetActive(false);
        CanInteract = false;
        m_Controller = GetComponent<CharacterController>();
        m_Animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if(CanInteract && Input.GetButtonDown("Interact"))
        {
            Type NPCType = InteractingNPC.GetComponent<NPC>().GetType();
            Type Interactable = typeof(IInteractable);
            Debug.Log(Interactable);
            if(Interactable.IsAssignableFrom(NPCType))
            {
                IInteractable NPCInteraction = (IInteractable)InteractingNPC.GetComponent<NPC>();
                NPCInteraction.Interact(gameObject);
            }
            else
            {
                Debug.Log("Cannot Assign");
            }
            

        }

        if (Input.GetButtonDown("Sprint"))
            IsSprinting = IsSprinting == false ? true : false;

        if(IsMoving && IsSprinting && !m_Animator.GetBool("Run"))
        {
            MovementSpeed = SprintSpeed;
            m_Animator.SetBool("Walk", false);
            m_Animator.SetBool("Run", true);
        }
        else if (IsMoving && !IsSprinting  && !m_Animator.GetBool("Walk"))
        {
            MovementSpeed = WalkSpeed;
            m_Animator.SetBool("Run", false);
            m_Animator.SetBool("Walk", true);
        }
        else if (IsMoving == false)
        {
            m_Animator.SetBool("Run", false);
            m_Animator.SetBool("Walk", false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CanMove)
        {
            float l_VerticalInput = Input.GetAxis("Vertical");
            float l_HorizontalInput = Input.GetAxis("Horizontal");

            if (l_VerticalInput > 0.1f || l_VerticalInput <-0.1f)
                IsMoving = true;
            else
                IsMoving = false;


            m_Controller.SimpleMove(transform.forward * l_VerticalInput * MovementSpeed * Time.fixedDeltaTime);

            transform.Rotate(transform.up * l_HorizontalInput * TurnSpeed * Time.fixedDeltaTime);
        }
        else
        {
            m_Animator.SetBool("Walk", false);
        }
    }

    public IEnumerator SwapCamera()
    {
        // TODO: trigger event when camera turns off to move the player
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
