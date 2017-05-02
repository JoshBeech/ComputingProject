using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour, IInteractable
{
    public Transform PlayerWarpLocation;
    public Transform CompanionWarpLocation;
    public bool WarpToOutside;

    void Start()
    {
        PlayerWarpLocation = transform.Find("PlayerWarpPoint");
        CompanionWarpLocation = transform.Find("CompanionWarpPoint");
    }

    public void Interact(GameObject p_Player)
    {
        if (p_Player.GetComponent<PlayerController>().Companion != null)
        {
            p_Player.GetComponent<PlayerController>().Warp(PlayerWarpLocation, CompanionWarpLocation);
            p_Player.GetComponent<PlayerController>().Companion.GetComponent<Companion>().InsideWall = !WarpToOutside;
        }
        else
            p_Player.GetComponent<PlayerController>().Warp(PlayerWarpLocation);
    }
}
