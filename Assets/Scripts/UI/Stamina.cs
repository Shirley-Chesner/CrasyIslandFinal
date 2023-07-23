using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public GameObject[] stamina;

    public int currentStamina;
    public int maxStamina;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regan;

    public bool isMoving = false;

    private void Start()
    {
        maxStamina = stamina.Length;
        currentStamina = maxStamina;
        InvokeRepeating("movementStamina", 1f, 1f);
    }

    private void movementStamina()
    {
        if (isMoving)
        {
            useStamina();
            StartCoroutine(waitForMovementStamina());
        }
    }

    private void Update()
    {
        // Make sure it doesn't get negative values...
        if (currentStamina < 0)
        {
            currentStamina = 0;
        }
    }

    public void useStamina()
    {
        if (maxStamina - currentStamina >= 0)
        {
            currentStamina--;
            stamina[currentStamina].SetActive(false);

            if (regan != null)
            {
                StopCoroutine(regan);
            }

            regan = StartCoroutine(reganStamina());
        }
    }

    IEnumerator reganStamina()
    {
        yield return new WaitForSeconds(2);

        while (currentStamina < maxStamina)
        {
            stamina[currentStamina].SetActive(true);
            currentStamina++;
           
            yield return  regenTick;
        }
        regan = null;
    }

    IEnumerator waitForMovementStamina()
    {
        yield return new WaitForSeconds(20f);
    }
}
