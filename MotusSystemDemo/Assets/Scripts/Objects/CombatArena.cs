using UnityEngine;
using System.Collections.Generic;

public class CombatArena : MonoBehaviour
{
    public List<GameObject> EnemyCombatants = new List<GameObject>();
    public List<GameObject> FriendlyCombatants = new List<GameObject>();

    // If player enters the area and there are enemies
    // Add player to friendly list, give lists to opposing sides
    // Tell each combatant to pick a target
    void OnTriggerEnter(Collider p_Collider)
    {
        if(p_Collider.gameObject.name == "Player" && EnemyCombatants.Count > 0)
        {
            PlayerController l_Player = p_Collider.gameObject.GetComponent<PlayerController>();

            if (!FriendlyCombatants.Contains(p_Collider.gameObject))
            {
                FriendlyCombatants.Add(p_Collider.gameObject);

                if (l_Player.Companion != null)
                    FriendlyCombatants.Add(l_Player.Companion);
            }

            foreach(GameObject l_Friendly in FriendlyCombatants)
            {
                ICombat CombatNPC = l_Friendly.GetComponent<ICombat>();

                if(CombatNPC != null)
                {
                    CombatNPC.IOpponents = EnemyCombatants;
                    CombatNPC.SetTarget();
                }               
            }

            foreach (GameObject l_Enemy in EnemyCombatants)
            {
                ICombat CombatNPC = l_Enemy.GetComponent<ICombat>();

                if (CombatNPC != null)
                {
                    CombatNPC.IOpponents = FriendlyCombatants;
                    CombatNPC.SetTarget();
                }
            }
        }
    }
}
