using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthStepStats
{
    public int HealthValue;
}


[System.Serializable]
public class HealthLevelDataConfig
{
    public List<HealthStepStats> HealthStepStatsCollection;
    Dictionary<int, int> _healthStepStatsCache;
    int _stepsAmount;


    public bool IsValid()
    {
        if (HealthStepStatsCollection == null || HealthStepStatsCollection.Count <= 0)
        {
            return false;
        }

        return true;
    }

    public void BuildCache()
    {
        // Steps in this level
        _stepsAmount = HealthStepStatsCollection.Count;


        // Build cache for steps->values
        _healthStepStatsCache = new Dictionary<int, int>();
        for (int i = 0; i < _healthStepStatsCache.Count; i++)
        {
            HealthStepStats item = HealthStepStatsCollection[i];
            _healthStepStatsCache[i] = item.HealthValue;
        }
    }

    public int GetStepsAmount()
    {
        return _stepsAmount;
    }
}




// Conrainer class for data
[System.Serializable]
public class HealthDataConfig
{
    public List<HealthLevelDataConfig> HealthLevelsConfigCollection;
}