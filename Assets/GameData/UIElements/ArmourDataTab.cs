using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourDataTab : MonoBehaviour
{
    [SerializeField] ElementDataContainer _dataContainer;


    public void InitTab()
    {
        _dataContainer.Init(null);
    }
}