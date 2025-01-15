using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private IAnimationsStrategy animationsStrategy;

    public void SetAnimationStrategy(IAnimationsStrategy strategy)
    {
        animationsStrategy = strategy;
    }

    private void Update()
    {
        if(animationsStrategy != null)
        {
            animationsStrategy.Animate(transform);
        }
    }
}
