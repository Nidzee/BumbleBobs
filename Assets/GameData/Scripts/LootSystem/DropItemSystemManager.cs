using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemSystemManager : MonoBehaviour
{
    [SerializeField] List<DropItemValueConfig> _dropItemValueConfig;

    
}

[System.Serializable]
public class DropItemValueConfig
{
    public DropItemType itemType;
    public float value;
}

public enum DropItemType{
    Gel1 = 1,
    Gel2 = 2,
    Gel3 = 3,
    Gel4 = 4,

    Crystal1 = 5,
    Crystal2 = 6,
    Crystal3 = 7,
    Crystal4 = 8,

    HealthPack1 = 9,
    HealthPack2 = 10,
    HealthPack3 = 11,

    ArmourPack1 = 12,
    ArmourPack2 = 13,
    ArmourPack3 = 14,
}