using System.Collections.Generic;
using UnityEngine;

public class DropItemSystemManager : MonoBehaviour
{
    [SerializeField] List<DropItemValueConfig> _dropItemValueConfig;
    Dictionary<DropItemType, float> _dropItemTypeValueCache = new Dictionary<DropItemType, float>(); 
}

[System.Serializable]
public class DropItemValueConfig
{
    public DropItemType itemType;
    public float value;
}

public enum DropItemType{
    HealthPack,
    ArmourPack,
}