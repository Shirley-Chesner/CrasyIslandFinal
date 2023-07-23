using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChoosing : MonoBehaviour
{
    public Image[] weapons;

    [SerializeField] private int weapon;
    public void setOutlineOfImage(int index)
    {
        if (index != weapon)
        {
            weapons[weapon].GetComponent<Outline>().enabled = false;
            weapon = index;
            weapons[weapon].GetComponent<Outline>().enabled = true;
            CurrentWeapon.weaponIndex = weapon;
        } 
    }
}
