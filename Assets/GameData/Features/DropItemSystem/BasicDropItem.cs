using UnityEngine;

[RequireComponent (typeof(Rigidbody), typeof(SphereCollider))]
public class BasicDropItem : MonoBehaviour
{
    [SerializeField] DropItemType _itemType;


    void Start()
    {
        // Ensure tag is correct
        this.tag = TagConstraintsConfig.COLLECTIBLE_ITEM_TAG;
    }

    public DropItemType GetDropItemType()
    {
        return _itemType;
    }
}