using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject thirdCam;
    public GameObject firstCam;
    public GameObject firstCamSneaking;
    public int camMode;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (camMode == 1) camMode = 0;
            else camMode = 1;
            StartCoroutine(camChange());
        }
            StartCoroutine(sneakingFirstCamChange());
    }

    IEnumerator camChange()
    {
        yield return new WaitForSeconds(0.01f);
        if (camMode == 0)
        {
            thirdCam.SetActive(true);
            firstCam.SetActive(false);
        } else
        {
            thirdCam.SetActive(false);
            if (Input.GetButton("Sneaking")) {
                firstCam.SetActive(false);
                firstCamSneaking.SetActive(true);
            } else
            {
                firstCamSneaking.SetActive(false);
                firstCam.SetActive(true);
            }
        }
    }

    IEnumerator sneakingFirstCamChange()
    {
        yield return new WaitForSeconds(0.01f);
        if (camMode == 1)
        {
            if (Input.GetButton("Sneaking"))
            {
                firstCam.SetActive(false);
                firstCamSneaking.SetActive(true);
            }
            else
            {
                firstCamSneaking.SetActive(false);
                firstCam.SetActive(true);
            }
        }
    }
}
