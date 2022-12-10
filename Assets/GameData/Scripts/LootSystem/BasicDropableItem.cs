using UnityEngine;

[RequireComponent (typeof(Rigidbody), typeof(SphereCollider))]
public class BasicDropableItem : MonoBehaviour
{
    [SerializeField] DropItemType _itemType;


    void Start()
    {
        // Ensure tag is correct
        this.tag = TagConstraintsConfig.COLLECTIBLE_ITEM_TAG;
    }

    void Update()
    {
        // Loop rotation forever
    }

    public DropItemType GetDropItemType()
    {
        return _itemType;
    }
}