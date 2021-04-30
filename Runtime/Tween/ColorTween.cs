using UnityEngine;


namespace Neat.Tween
{
    [AddComponentMenu("Neat/Tweens/Color Tween")]
    public class ColorTween : Tween<Color>
    {
        protected override Color Lerp(Color a, Color b, float t)
        {
            //Debug.Log(new { a, b, t });
            return Color.Lerp(a, b, t);
        }

        protected override float Distance(Color a, Color b) => Mathf.Max(
                Mathf.Abs(a.r - b.r),
                Mathf.Abs(a.g - b.g),
                Mathf.Abs(a.b - b.b),
                Mathf.Abs(a.a - b.a)
            );
    }
}

