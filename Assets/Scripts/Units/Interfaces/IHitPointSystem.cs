public interface IHitPointSystem
{
    float HitPoints { get; }
    float HitPointPercent { get; }

    System.Action<float> OnHPChanged { get; set; }
    System.Action OnDeath { get; set; }

    void ReduceHitPoints(float value);
    void Kill();
}