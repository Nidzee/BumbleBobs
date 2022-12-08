public interface IEnemy
{
    public void InitEnemyStats();
    public void Attack();
    public DamageType damageType {get;}
}