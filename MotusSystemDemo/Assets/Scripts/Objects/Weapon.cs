using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Weapon : MonoBehaviour
{
    public string WeaponType;
    public int Damage;

    void OnTriggerEnter(Collider p_Collider)
    {
        Combat CombatNPC = p_Collider.GetComponent<Combat>();

        if (CombatNPC != null)        
            CombatNPC.Health -= Damage;
        
    }
}
