using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public int Damage;

    void OnTriggerEnter(Collider p_Collider)
    {
        ICombat CombatNPC = p_Collider.GetComponent<ICombat>();

        if (CombatNPC != null)        
            CombatNPC.IHealth -= Damage;
        
    }
}
