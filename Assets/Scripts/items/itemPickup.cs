using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPickup : MonoBehaviour
{
    private bool pickUpAllowed;
    public item currentItem;
    private void Update()
    {
        if (pickUpAllowed && Input.GetKeyDown(KeyCode.E))
        {
            pickUp();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
            pickUpAllowed = true;
    }

    private void OnTriggerExit(Collider other)
    {
            pickUpAllowed = false;
    }


    //public void startPickUpAnimation()
    //{

    //}

    //public void stopPickUpAnimation()
    //{

    //}

    void pickUp()
    {
        playerInventory.instance.add(currentItem);
        Destroy(gameObject);
    }
}
