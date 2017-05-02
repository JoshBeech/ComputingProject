using UnityEngine;
using System.Collections.Generic;
using MotusSystem;

public interface ICombat
{
    Weapon IWeaponType { get; set; }
    Dictionary<string, int> IAnimations { get; set; }
    List<GameObject> IOpponents { get; set; }
    GameObject ITarget { get; set; }
    int IHealth { get; set; }
    bool IDead { get; set; }
    bool IEngaged { get; set; }
    e_EmotionsState IDeathFeeling { get; set; }

    void SetTarget();
    void Die();
    void Enrage();
    void Flee();
}
