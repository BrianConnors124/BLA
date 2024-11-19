using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Weapons/NewWeapon", fileName = "NewWeapon")]
public class WeaponInfo : ScriptableObject
{
    [Header("Info")] 
    public string weaponName;
    public string description;

    public Sprite artWork;
    
    public float baseDamage;
    public float baseReach;
    public float primaryCD;
    public float secondaryCD;
}
