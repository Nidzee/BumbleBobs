using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemCollectDisplayData
{
    public DropItemType ItemType;
    public AudioClip ItemCollectSound;
    public ParticleSystem ItemCollectParticles;
}

public class PlayerItemCollectorHandler : MonoBehaviour
{
    [SerializeField] List<ItemCollectDisplayData> _itemCollectData;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] PlayerController _player;


    public void Reset()
    {
        
    }

    public void CollectItem(BasicDropItem dropItem)
    {
        var dropItemType = dropItem.GetDropItemType();
        PlayCollectItemVisuals(dropItemType);
        AddPlayerPointsFromItem(dropItemType);
        Destroy(dropItem.gameObject);
    }

    void AddPlayerPointsFromItem(DropItemType itemType)
    {
        float itemValue = DropItemSystemManager.Instance.GetDropItemValue(itemType);

        switch(itemType)
        {
            case (DropItemType.HealthPack):
            Debug.Log("ADD PLAYER HEALTH: " + itemValue);
            break;

            case (DropItemType.ArmourPack):
            Debug.Log("ADD PLAYER ARMOUR: " + itemValue);
            break;
        }
    }

    void PlayCollectItemVisuals(DropItemType itemType)
    {
        foreach (var data in _itemCollectData)
        {
            if (data.ItemType == itemType)
            {
                _audioSource.clip = data.ItemCollectSound;
                _audioSource.Play();
                
                // Play particles
                // data.ItemCollectParticles

                return;
            }
        }
    }
}