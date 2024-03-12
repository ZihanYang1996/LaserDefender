using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int _damage = 10;
    public int damage
    {
        get {return _damage;}  // get the damage value
    }

    // An alternative way to get the damage value
    // public int GetDamage()
    // {
    //     return _damage;
    // }

    public void Hit()
    {
        Destroy(gameObject);  // destroy the game object when hitting other game object
    }
}
