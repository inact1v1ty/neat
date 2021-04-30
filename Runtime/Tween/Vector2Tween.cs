using UnityEngine;


namespace Neat.Tween
{
    [AddComponentMenu("Neat/Tweens/Vector2 Tween")]
    public class Vector2Tween : Tween<Vector2>
    {
        protected override Vector2 Lerp(Vector2 a, Vector2 b, float t) => Vector2.Lerp(a, b, t);

        protected override float Distance(Vector2 a, Vector2 b) => Vector2.Distance(a, b);
    }
}
