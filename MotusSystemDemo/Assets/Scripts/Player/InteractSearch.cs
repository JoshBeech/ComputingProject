﻿using UnityEngine;
using System.Collections.Generic;

public class InteractSearch : MonoBehaviour
{
    public Dictionary<string, GameObject> m_InteractableObjects = new Dictionary<string, GameObject>();

    private SphereCollider m_SphereCollider;
    private PlayerController m_PlayerController;
    // Use this for initialization
    void Start()
    {
        m_SphereCollider = GetComponent<SphereCollider>();
        m_PlayerController = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_InteractableObjects.Count > 0)
        {
            RaycastHit l_Hit = new RaycastHit();
            foreach(KeyValuePair<string, GameObject> l_InteractableObject in m_InteractableObjects)
            {
                if(Physics.Raycast(transform.position, transform.forward, out l_Hit, m_SphereCollider.radius))
                {
                    // Prompt player for interact
                    Debug.Log("You can interact with this object");
                }
            }
        }
    }

    void OnTriggerEnter(Collider p_OtherCollider)
    {
        Debug.Log("Trigger Entered: " + p_OtherCollider.name);

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
        }
    }
}
