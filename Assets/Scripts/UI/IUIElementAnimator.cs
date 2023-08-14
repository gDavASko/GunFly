public interface IUIElementAnimator
{
    void Animate(string animationId, System.Action<string> onAnimationComplete);
}