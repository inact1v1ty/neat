using UnityEngine;


namespace Neat.Tween
{
    [AddComponentMenu("Neat/Tweens/Vector3 Tween")]
    public class Vector3Tween : Tween<Vector3>
    {
        protected override Vector3 Lerp(Vector3 a, Vector3 b, float t) => Vector3.Lerp(a, b, t);

        protected override float Distance(Vector3 a, Vector3 b) => Vector3.Distance(a, b);
    }
}
