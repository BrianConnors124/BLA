using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTutorialSequence : MonoBehaviour
{
    public GameObject activate;
    public GameObject deactivate;

    public void ActivateTutorial()
    {
        activate.SetActive(true);
        deactivate.SetActive(false);
    }
}
