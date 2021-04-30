using UnityEngine;


namespace Neat.Tween
{
    [AddComponentMenu("Neat/Tweens/Quaternion Tween")]
    public class QuaternionTween : Tween<Quaternion>
    {
        protected override Quaternion Lerp(Quaternion a, Quaternion b, float t) => Quaternion.Slerp(a, b, t);

        protected override float Distance(Quaternion a, Quaternion b) => Quaternion.Angle(a, b);
    }
}
