using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTypeGameData
{
    public WeaponType weaponType;
    public Dictionary<int, ProgressDataCollection> progressDataLevelCollection;

    public WeaponTypeGameData(WeaponType type, List<ProgressDataCollection> dataConfiguration)
    {
        weaponType = type;

        progressDataLevelCollection = new Dictionary<int, ProgressDataCollection>();

        foreach (var config in dataConfiguration)
        {
            progressDataLevelCollection[config.levelNumber] = config;
        }
    }
}