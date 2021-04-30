using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Neat.Components
{
    [AddComponentMenu("Neat/Lister")]
    public class Lister : MonoBehaviour
    {
        public delegate UINode RenderItem<T>(T item, int idx);
        public delegate (string key, UINode node) RenderItemWithKey<T>(T item, int idx);

        private Transform wrapper;
        private GameObject obj;

        private readonly HashSet<string> keys = new HashSet<string>();

        private void Awake()
        {
            if (transform.childCount != 1)
            {
                Debug.LogError("Lister component must be used on GameObject with only 1 child!");
                return;
            }

            wrapper = transform.GetChild(0);

            if (wrapper.childCount != 1)
            {
                Debug.LogError("Wrapper must have only 1 child!");
                return;
            }

            obj = wrapper.GetChild(0).gameObject;
            obj.SetActive(false);
        }

        public Node[] Render<T>(IEnumerable<T> items, RenderItem<T> render)
        {
            return this.Render(items, (t, idx) => (idx.ToString(), render(t, idx)));
        }

        public Node[] Render<T>(IEnumerable<T> items, RenderItemWithKey<T> render)
        {
            var toRender = new List<Node>();

            int itemIdx = 0;

            foreach (var item in items)
            {
                var (key, rendered) = render(item, itemIdx);
                rendered.Name = obj.name + $"({key})";
                toRender.Add(rendered);
                itemIdx++;

                if (!keys.Contains(key))
                {
                    var go = Instantiate(obj, wrapper);
                    go.SetActive(true);
                    go.name = obj.name + $"({key})";
                    keys.Add(key);
                }
            }

            return new Node[] {
                toRender.Count > 0 ? DSL.Draw(wrapper.name, toRender.ToArray()) : DSL.Draw(wrapper.name)
            };
        }
    }
}