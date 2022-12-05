[System.Serializable]
public class LootItem
{
    public BasicDropableItem lootItem;
    public float probability;

    public bool IsItemValid()
    {
        if (probability != 0 && lootItem != null)
        {
            return true;
        }

        return false;
    }
}