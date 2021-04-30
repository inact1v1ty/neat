using UnityEngine;


namespace Neat.Tween
{
    [AddComponentMenu("Neat/Tweens/Float Tween")]
    public class FloatTween : Tween<float>
    {
        protected override float Lerp(float a, float b, float t) => Mathf.Lerp(a, b, t);

        protected override float Distance(float a, float b) => Mathf.Abs(a - b);
    }
}
