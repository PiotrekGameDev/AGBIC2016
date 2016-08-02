using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon", order = 0)]
public class Weapon : ScriptableObject
{
    public string weaponName="New Weapon";
    public TypeOfWeaopn type;
    public float damagePerShot = 10f;
    public bool isAutomatic = true;
    public float rateOfFire = 2f;
}
public enum TypeOfWeaopn { Melee, Gun };