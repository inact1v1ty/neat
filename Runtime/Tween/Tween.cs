using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using System.Linq.Expressions;

namespace Neat.Tween
{
    internal interface ITween
    {
        string AnimationName { get; }
        void SetValue(object newValue, bool instant = false, float delay = 0f);
    }

    public abstract class Tween<T> : MonoBehaviour, ITween
        where T: IEquatable<T>
    {
        [SerializeField]
        private string animationName;

        public float time = 1;

        public AnimationCurve animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        [SerializeField]
        [HideInInspector]
        private Component target;

        [SerializeField]
        [HideInInspector]
        private string fieldName;

#if NEAT_FAST_INVOKE
        private Func<object, T> getter = null;
        private Action<object, T> setter = null;
#else
        private FieldInfo fieldInfo = null;
        private PropertyInfo propertyInfo = null;
        private bool isField;
#endif

        private T startValue;
        private T endValue;
        private float startTime = -100f;
        private float endTime = -100f;
        private bool done = true;

        private void Awake()
        {
            if (target != null && !string.IsNullOrEmpty(fieldName))
            {
                BindingFlags bindingFlags = BindingFlags.Public |
                                            BindingFlags.NonPublic |
                                            BindingFlags.Static |
                                            BindingFlags.Instance |
                                            BindingFlags.DeclaredOnly;

                var maybeFieldInfo = GetFieldRecursive(target.GetType(), fieldName, bindingFlags);

                if (maybeFieldInfo != null)
                {
#if NEAT_FAST_INVOKE
                    getter = FastInvoke.BuildTypedGetter<T>(maybeFieldInfo);
                    setter = FastInvoke.BuildTypedSetter<T>(maybeFieldInfo);
                    return;
#else
                    isField = true;
                    fieldInfo = maybeFieldInfo;
                    return;
#endif
                }

                var maybePropertyInfo = GetPropertyRecursive(target.GetType(), fieldName, bindingFlags);
                if (maybePropertyInfo != null)
                {
#if NEAT_FAST_INVOKE
                    getter = FastInvoke.BuildTypedGetter<T>(maybePropertyInfo);
                    setter = FastInvoke.BuildTypedSetter<T>(maybePropertyInfo);
                    return;
#else
                    isField = false;
                    propertyInfo = maybePropertyInfo;
                    return;
#endif
                }

                Debug.LogError($"Field or property with name {fieldName} not found on component {target.GetType().Name}");
            }
        }

        private FieldInfo GetFieldRecursive(Type type, string name, BindingFlags bindingFlags)
        {
            return type.GetField(name, bindingFlags) ?? (type.BaseType != null ? GetFieldRecursive(type.BaseType, name, bindingFlags) : null);
        }

        private PropertyInfo GetPropertyRecursive(Type type, string name, BindingFlags bindingFlags)
        {
            return type.GetProperty(name, bindingFlags) ?? (type.BaseType != null ? GetPropertyRecursive(type.BaseType, name, bindingFlags) : null);
        }

        private void Update()
        {
            bool accessValid = false;

#if NEAT_FAST_INVOKE
            accessValid = getter != null && setter != null;
#else
            accessValid = fieldInfo != null || propertyInfo != null;
#endif

            if (target != null && accessValid && !done)
            {
                if (endTime - startTime < float.Epsilon)
                {
                    done = true;
                    return;
                }

                var t = (Time.time - startTime) / (endTime - startTime);
                if (t >= 1)
                {
                    done = true;
                }
                var clamped = Mathf.Clamp01(t);
                var evaluated = animationCurve.Evaluate(clamped);
                var current = this.Lerp(startValue, endValue, evaluated);
                Value = current;
            }
        }

        public void SetValue(T newValue, bool instant = false, float delay = 0f)
        {
            if (instant)
            {
                Value = newValue;
                endValue = newValue;
                done = true;
            }
            else
            {
                var distance = this.Distance((endTime == -100f || done) ? Value : endValue, newValue);

                if (distance < 1e-3)
                {
                    return;
                }

                startValue = Value;
                startTime = Time.time + delay;

                endValue = newValue;

                endTime = startTime + time;
                done = false;
            }
        }


        protected T Value
        {
            get
            {
#if NEAT_FAST_INVOKE
                if (getter == null)
                {
                    throw new Exception();
                }
                return getter(target);
#else
                if (isField)
                {
                    if (target != null && fieldInfo != null)
                    {
                        return (T)fieldInfo.GetValue(target);
                    }
                }
                else
                {
                    if (target != null && propertyInfo != null)
                    {
                        return (T)propertyInfo.GetValue(target);
                    }
                }
                throw new System.Exception();
#endif
            }
            set
            {
#if NEAT_FAST_INVOKE
                if (setter == null)
                {
                    throw new Exception();
                }
                setter(target, value);
#else
                if (isField)
                {
                    if (target != null && fieldInfo != null)
                    {
                        fieldInfo.SetValue(target, value);
                        return;
                    }
                }
                else
                {
                    if (target != null && propertyInfo != null)
                    {
                        propertyInfo.SetValue(target, value);
                        return;
                    }
                }
                throw new System.Exception();
#endif
            }
        }

        protected abstract T Lerp(T a, T b, float t);
        protected abstract float Distance(T a, T b);

        string ITween.AnimationName
        {
            get { return animationName; }
        }

        void ITween.SetValue(object newValue, bool instant, float delay)
        {
            SetValue((T)newValue, instant, delay);
        }
    }

#if NEAT_FAST_INVOKE
    public static class FastInvoke
    {
        public static Func<object, T> BuildTypedGetter<T>(MemberInfo memberInfo)
        {
            var targetType = memberInfo.DeclaringType;
            var exInstance = Expression.Parameter(typeof(object), "t");

            var exConvertFromObject = Expression.Convert(exInstance, targetType);     // Convert(t, Type)

            var exMemberAccess = Expression.MakeMemberAccess(exConvertFromObject, memberInfo);       // Convert(t, Type).PropertyName
            var lambda = Expression.Lambda<Func<object, T>>(exMemberAccess, exInstance);

            var action = lambda.Compile();
            return action;
        }

        public static Action<object, T> BuildTypedSetter<T>(MemberInfo memberInfo)
        {
            var targetType = memberInfo.DeclaringType;
            var exInstance = Expression.Parameter(typeof(object), "t");

            var exConvertFromObject = Expression.Convert(exInstance, targetType);     // Convert(t, Type)
            var exMemberAccess = Expression.MakeMemberAccess(exConvertFromObject, memberInfo);

            // (Convert(t, Type)).PropertyValue(p)
            var exValue = Expression.Parameter(typeof(T), "p");
            var exBody = Expression.Assign(exMemberAccess, exValue);

            var lambda = Expression.Lambda<Action<object, T>>(exBody, exInstance, exValue);
            var action = lambda.Compile();
            return action;
        }
    }
#endif
}
