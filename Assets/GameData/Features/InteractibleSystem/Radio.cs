using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour, IInteractible
{
    public void Interact()
    {
        Debug.Log("[Interact] : " + gameObject.name);
    }
}