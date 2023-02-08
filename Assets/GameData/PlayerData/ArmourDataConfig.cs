using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ArmourStepStats
{
    public int ArmourValue;
}


[System.Serializable]
public class ArmourLevelDataConfig
{
    public List<ArmourStepStats> ArmourStepStatsCollection;
    Dictionary<int, int> _armourStepStatsCache;
    int _stepsAmount;


    public bool IsValid()
    {
        if (ArmourStepStatsCollection == null || ArmourStepStatsCollection.Count <= 0)
        {
            return false;
        }

        return true;
    }

    public void BuildCache()
    {
        // Steps in this level
        _stepsAmount = ArmourStepStatsCollection.Count;


        // Build cache for steps->values
        _armourStepStatsCache = new Dictionary<int, int>();
        for (int i = 0; i < ArmourStepStatsCollection.Count; i++)
        {
            ArmourStepStats item = ArmourStepStatsCollection[i];
            _armourStepStatsCache[i] = item.ArmourValue;
        }
    }

    public int GetStepsAmount()
    {
        return _stepsAmount;
    }
}




// Conrainer class for data
[System.Serializable]
public class ArmourDataConfig
{
    public List<ArmourLevelDataConfig> ArmourLevelsConfigCollection;
}