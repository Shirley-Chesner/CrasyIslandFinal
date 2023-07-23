using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public bool canAttack = true;
    public GameObject[] weapons;

    [SerializeField] private int currentWeaponIndex;

    public bool a = false;

    private Collider enemy;

    private void Start()
    {
        changeSword();
    }

    public void swordAttack()
    {
        if (canAttack && enemy)
        {
            canAttack = false;
            a = true;
            enemy.GetComponent<EnemyHealth>().takeDamage(20f);
            canAttack = true;
        }
    }

    public void changeSword()
    {
        weapons[currentWeaponIndex].SetActive(false);
        currentWeaponIndex = CurrentWeapon.weaponIndex;
        weapons[currentWeaponIndex].SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemy = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemy = null;
        }
    }
}
