using UnityEngine;
using System.Collections.Generic;

public class CombatArena : MonoBehaviour
{
    public List<GameObject> EnemyCombatants = new List<GameObject>();
    public List<GameObject> FriendlyCombatants = new List<GameObject>();
    public bool AllEnemiesDefeated = false;

    private bool CombatInprogress = false;


    void Update()
    {
        if(CombatInprogress)
        {
            int l_DefeatedEnemyCount = 0;

            foreach(GameObject l_Enemy in EnemyCombatants)
            {
                Combat NPCCombat = l_Enemy.GetComponent<Combat>();
                if (NPCCombat.Dead || NPCCombat.Fleeing)
                    l_DefeatedEnemyCount++;
            }

            if (l_DefeatedEnemyCount == EnemyCombatants.Count)
            {
                AllEnemiesDefeated = true;
                CombatInprogress = false;
                
                foreach (GameObject l_Friendly in FriendlyCombatants)
                {
                    Combat CombatNPC = l_Friendly.GetComponent<Combat>();

                    if (CombatNPC != null)
                    {
                        CombatNPC.Disengage();
                    }
                }

            }
        }
    }


    // If player enters the area and there are enemies
    // Add player to friendly list, give lists to opposing sides
    // Tell each combatant to pick a target
    void OnTriggerEnter(Collider p_Collider)
    {
        if(p_Collider.gameObject.name == "Player" && !CombatInprogress)
        {
            PlayerController l_Player = p_Collider.gameObject.GetComponent<PlayerController>();

            if (!FriendlyCombatants.Contains(p_Collider.gameObject))
            {
                if (l_Player.Companion != null)
                    FriendlyCombatants.Add(l_Player.Companion);
            }

            foreach(GameObject l_Friendly in FriendlyCombatants)
            {
                if (l_Friendly == l_Player.Companion)
                    l_Player.Companion.GetComponent<Companion>().EnterCombat();

                Combat CombatNPC = l_Friendly.GetComponent<Combat>();
                
                if(CombatNPC != null)
                {
                    CombatNPC.Opponents = EnemyCombatants;
                    CombatNPC.SetTarget();
                }               
            }

            foreach (GameObject l_Enemy in EnemyCombatants)
            {
                Combat CombatNPC = l_Enemy.GetComponent<Combat>();

                if (CombatNPC != null)
                {
                    CombatNPC.Opponents = FriendlyCombatants;
                    CombatNPC.SetTarget();

                    for (int i = 0; i < EnemyCombatants.Count; i++)
                    {
                        if(l_Enemy != EnemyCombatants[i])
                        {
                            EnemyCombatants[i].GetComponent<Combat>().NPCDied += CombatNPC.f_NPCDied;
                        }
                    }
                }
            }

            CombatInprogress = true;
        }
    }
}
