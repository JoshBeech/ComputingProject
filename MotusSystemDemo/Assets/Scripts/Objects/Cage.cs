using UnityEngine;
using System.Collections;

public class Cage : MonoBehaviour, IInteractable
{
    public Transform PrisonerWarpLocation;
    public GameObject Prisoner;

    void Start()
    {
        PrisonerWarpLocation = transform.Find("PrisonerWarpPoint");
        Prisoner.GetComponent<King>().enabled = false;
    }

    public void Interact(GameObject p_Player)
    {
        // Release prisoner
        Prisoner.transform.position = PrisonerWarpLocation.transform.position;
        Prisoner.GetComponent<King>().enabled = true;
    }
}
