using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    public bool isDetecting = false;
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
            isDetecting = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
            isDetecting = false;
    }
}
