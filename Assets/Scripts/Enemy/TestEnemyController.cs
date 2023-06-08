public class TestEnemyController : EnemyController
{
    public override float MaxHealth => 100;
    public override string Name => "Test Enemy";

    public override void OnDeath()
    {
        print("DEADDDDDDDDDDDDDDD!");
        Destroy(gameObject);
    }
}