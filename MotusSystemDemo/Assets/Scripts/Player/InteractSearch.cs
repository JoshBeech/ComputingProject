using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InteractSearch : MonoBehaviour
{
    public Dictionary<string, GameObject> m_InteractableObjects = new Dictionary<string, GameObject>();
    public Text InteractPrompt;

    private SphereCollider m_SphereCollider;
    private PlayerController m_PlayerController;
    private int InteractionLayerMask;
    // Use this for initialization
    void Start()
    {
        m_SphereCollider = GetComponent<SphereCollider>();
        m_PlayerController = GetComponentInParent<PlayerController>();
        InteractPrompt.enabled = false;
        InteractionLayerMask = LayerMask.GetMask("Interactable");
    }

    // Update is called once per frame
    void Update()
    {
        if (m_InteractableObjects.Count > 0)
        {

            RaycastHit l_Hit = new RaycastHit();

            if (Physics.Raycast(transform.position, transform.forward, out l_Hit, m_SphereCollider.radius, InteractionLayerMask))
            {
                if (m_InteractableObjects.ContainsKey(l_Hit.transform.name))
                {
                    if (!InteractPrompt.enabled)
                        InteractPrompt.enabled = true;

                    if (!m_PlayerController.CanInteract)
                    {
                        m_PlayerController.CanInteract = true;
                        m_PlayerController.InteractableObject = m_InteractableObjects[l_Hit.transform.name];
                    }

                    InteractPrompt.text = "Press E to interact with " + l_Hit.transform.name;
                }
            }
            else
            {
                if (InteractPrompt.enabled)
                    InteractPrompt.enabled = false;

                if (m_PlayerController.CanInteract)
                {
                    m_PlayerController.CanInteract = false;
                    m_PlayerController.InteractableObject = null;
                }
            }

        }
    }

    void OnTriggerEnter(Collider p_OtherCollider)
    {
        if (p_OtherCollider.tag.Contains("Interact"))
        {
            m_InteractableObjects.Add(p_OtherCollider.name, p_OtherCollider.gameObject);
        }
    }

    void OnTriggerExit(Collider p_OtherCollider)
    {
        if (m_InteractableObjects.ContainsKey(p_OtherCollider.name))
        {
            m_InteractableObjects.Remove(p_OtherCollider.name);
            m_PlayerController.InteractableObject = null;
        }
    }
}
