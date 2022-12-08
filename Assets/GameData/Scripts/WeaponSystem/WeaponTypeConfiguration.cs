using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponTypeConfiguration 
{
    public WeaponType weaponType;
    public List<ProgressDataCollection> dataConfiguration;

    public bool IsConfigValid()
    {
        if (weaponType != WeaponType.None && dataConfiguration?.Count > 0)
        {
            return true;
        }

        return false;
    }
}