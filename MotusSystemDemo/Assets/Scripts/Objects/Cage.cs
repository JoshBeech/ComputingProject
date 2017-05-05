using UnityEngine;
using System.Collections;

public class Cage : MonoBehaviour, IInteractable
{
    public Transform PrisonerWarpLocation;
    public GameObject Prisoner;

    void Start()
    {
        PrisonerWarpLocation = transform.Find("PrisonerWarpPoint");
    }

    public void Interact(GameObject p_Player)
    {
        // Release prisoner
        Prisoner.transform.position = PrisonerWarpLocation.transform.position;
    }
}
