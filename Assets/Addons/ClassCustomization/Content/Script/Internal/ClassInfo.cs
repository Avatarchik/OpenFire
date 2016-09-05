using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class ClassInfo {

    public string WeaponName = "Weapon";
    public Sprite WeaponIcon;
    public int ID;
    public bl_Gun.weaponType Type = bl_Gun.weaponType.Machinegun;
    [Space(5)]
    [Header("Gun Info")]
    [Range(0,100)]
    public int Damage = 50;
    [Range(0, 100)]
    public int Accuracy = 50;
    [Range(0, 100)]
    public int Rate = 50;
    
}