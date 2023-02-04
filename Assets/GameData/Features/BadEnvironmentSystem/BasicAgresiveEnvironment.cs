using UnityEngine;

public class BasicAgresiveEnvironment : MonoBehaviour
{
    [SerializeField] AgressiveEnvironmentType _environmentType;

    // Private stats
    DamageType _damageType;
    float _damagePoints;
    int _damageInterval_Miliseconds;


    // References
    public AgressiveEnvironmentType EnvironmnentType => _environmentType;
    public DamageType DamageType => _damageType;
    public int DamageInterval_Miliseconds => _damageInterval_Miliseconds;
    public float DamagePoints => _damagePoints;




    public void Start()
    {
        // Get stats from manager
        var stats = BadEnvironmentSystemManager.Instance.GetBadEnvironmentStats(_environmentType);


        // Set stats
        if (stats != null)
        {
            _damageType = stats.DamageType;
            _damagePoints = stats.DamagePoints;
            _damageInterval_Miliseconds = stats.DamageIntervas_Miliseconds;
        }
    }
}
