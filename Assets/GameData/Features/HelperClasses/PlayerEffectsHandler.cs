using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;


[System.Serializable]
public class AgressiveEnvironmentParticlesData
{
    public AgressiveEnvironmentType EnvironmnentType;
    public GameObject ParticleObject;
    public Image Icon;
}


public class PlayerEffectsHandler : MonoBehaviour
{
    [SerializeField] List<AgressiveEnvironmentParticlesData> _availableEffects;
    [SerializeField] PlayerController _player;

    List<BasicAgresiveEnvironment> _effectsOnPlayer;


    public void Reset()
    {
        // Clear list of current effects
        _effectsOnPlayer = new List<BasicAgresiveEnvironment>();
        
        // Deactivate all particles
        DeactivateAllParticles();
    }



    public void RemoveEffect(BasicAgresiveEnvironment effectData)
    {
        var effetOnPlayerData = _effectsOnPlayer.FirstOrDefault(i => i.EnvironmnentType == effectData.EnvironmnentType);
        
        if (effetOnPlayerData != null)
        {
            Debug.Log("Remove effect: " + effetOnPlayerData.EnvironmnentType);
            UpdateEffectParticle(effetOnPlayerData.EnvironmnentType, false);
            _effectsOnPlayer.Remove(effetOnPlayerData);
        }
        else
        {
            Debug.Log("Trying to remove effect: " + effectData.EnvironmnentType + " which is not on player.");
        }
    }

    public void ApplyEffect(BasicAgresiveEnvironment effectData)
    {
        // Check if effect is already on player
        var effectDataOnPlayer = _effectsOnPlayer.FirstOrDefault(i => i.EnvironmnentType == effectData.EnvironmnentType);
        if (effectDataOnPlayer != null)
        {
            // _effectsOnPlayer.Add(effectData); // if multiple objects intersects -> uncomment
            Debug.Log("Effect: " + effectData.EnvironmnentType + " is already on player.");
            return;
        }

        _effectsOnPlayer.Add(effectData);

        // Try to activate particles
        UpdateEffectParticle(effectData.EnvironmnentType, true);

        // Start apply damage logic
        ApplyEffectDamageLogic(effectData);
    }



    bool IsEffectOnPlayer(AgressiveEnvironmentType type)
    {
        var effectOnPlayerData = _effectsOnPlayer.FirstOrDefault(i => i.EnvironmnentType == type);
        
        if (effectOnPlayerData != null)
        {
            return true;
        }

        return false;
    }

    async void ApplyEffectDamageLogic(BasicAgresiveEnvironment effectData)
    {
        while(IsEffectOnPlayer(effectData.EnvironmnentType))
        {
            Debug.Log("ADD DAMAGE: " + effectData.EnvironmnentType + "   POINTS: " + effectData.DamagePoints);
            await Task.Delay(effectData.DamageInterval_Miliseconds);
        }
    }







    // Effects particles logic
    void UpdateEffectParticle(AgressiveEnvironmentType type, bool status)
    {
        foreach (var data in _availableEffects)
        {
            if (data.EnvironmnentType == type)
            {
                data.ParticleObject.SetActive(status);
                data.Icon.gameObject.SetActive(status);
                return;
            }
        }

        Debug.LogError("Missing particle object for: " + type);
    }

    void DeactivateAllParticles()
    {
        foreach (var data in _availableEffects)
        {
            data.ParticleObject.SetActive(false);
            data.Icon.gameObject.SetActive(false);
        }
    }
}