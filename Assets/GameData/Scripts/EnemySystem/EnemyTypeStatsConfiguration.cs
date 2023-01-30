[System.Serializable]
public class EnemyTypeStatsConfiguration
{
    public EnemyType enemyType;
    public EnemyStats stats;

    public bool IsConfigValid()
    {
        if (stats != null)
        {
            return true;
        }

        return false;
    }
}