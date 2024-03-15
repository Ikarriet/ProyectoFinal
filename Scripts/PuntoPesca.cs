using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntoPesca : MonoBehaviour
{
    public bool posiblePesca=false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Agua"))
        {
            posiblePesca = true;
        }
        else
        {
            posiblePesca=false;
        }

    }
}
