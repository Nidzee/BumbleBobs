using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : AliveUnit, IEnemy, ICanDropItem
{
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] LayerMask _whatIsPlayer;
    [SerializeField] float _attackRange;
    [SerializeField] float _attackCoolDown;

    PlayerController _player;
    bool _isPlayerReached;
    float _currentAttackCoolDown;


    public void Update()
    {
        // Reduce attack cooldown
        if (_currentAttackCoolDown > 0)
        {
            _currentAttackCoolDown -= Time.deltaTime;
        }


        // Check if we reach player
        _isPlayerReached = Physics.CheckSphere(transform.position, _attackRange, _whatIsPlayer);


        if (_isPlayerReached)
        {
            Attack();
        } 
        else
        {
            ChasePlayer();
        }
    }

    public void ChasePlayer()
    {
        Vector3 chasePoint = _player.transform.position;
        chasePoint.y = transform.position.y;
        _agent.SetDestination(chasePoint);
    }

    public void Attack()
    {
        _agent.SetDestination(transform.position);

        Vector3 lookVector = _player.transform.position;
        lookVector.y = transform.position.y;
        transform.LookAt(lookVector);

        if (_currentAttackCoolDown > 0)
        {
            return;
        }
        
        Debug.Log("[Enemy] Attack player: " + _damagePoints);
        _currentAttackCoolDown = _attackCoolDown;
    }






    [Header("Enemy config")]
    [SerializeField] EnemyType _enemyType;
    [SerializeField] DamageType _damageType;
    
    [Header("Info config")]    
    [SerializeField] InfoBar _healthInfoBar;
    [SerializeField] InfoBar _armourInfoBar;

    [Header("Loot drop config")]
    [SerializeField] LootBag _lootBag;

    [Header("Death sound")]
    [SerializeField] AudioClip _deathSound;

    float _damagePoints;
    float _moveSpeed;
    public DamageType damageType { get => _damageType; }
    public LootBag lootBag { get => _lootBag; }



    public void Start()
    {
        InitEnemyStats();
        _player = GameSceneManager.Instance.Player;
    }

    public void InitEnemyStats()
    {
        EnemyStats stats = EnemySystemManager.Instance.GetEnemyStats(_enemyType);
        SetEnemyStats(stats);
    }

    void SetEnemyStats(EnemyStats stats)
    {
        // Check if stats is provided
        if (stats == null)
        {
            Debug.LogException(new System.Exception("Missing stats for enemy initialization."));
            return;
        }

        _moveSpeed = stats.moveSpeed;
        _damagePoints = stats.damagePoints;

        Health = stats.maxHealthPoints;
        Armour = stats.maxArmourPoints;

        _healthInfoBar.InitInfoBar(Health, Health);
        _armourInfoBar.InitInfoBar(Armour, Armour);

        _agent.speed = _moveSpeed;
    }








    public override void TakeDamage(int damagePoints)
    {
        Health -= damagePoints;
        _healthInfoBar.ReduceValue(damagePoints);
        
        if (Health <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        PlayDeathSound();
        DropLoot();
        Destroy(gameObject);
    }

    public void DropLoot()
    {
        lootBag?.DropLoot();
    }
    
    void PlayDeathSound()
    {
        AudioSource.PlayClipAtPoint(_deathSound, transform.position);
    }
}