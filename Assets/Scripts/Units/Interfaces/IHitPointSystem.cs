public interface IHitPointSystem
{
    float HitPoints { get; }
    System.Action OnDeath { get; set; }

    void ReduceHitPoints(float value);
    void Kill();
}