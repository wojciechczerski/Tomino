using UnityEngine;

namespace Tomino.Shared
{
    public class GameObjectPool<T> where T : MonoBehaviour
    {
        public T[] Items { get; }

        public GameObjectPool(GameObject prefab, int size, GameObject parent)
        {
            Items = new T[size];
            for (var i = 0; i < size; ++i)
            {
                var newItem = Object.Instantiate(prefab, parent.transform, true);
                Items[i] = newItem.GetComponent<T>();
            }
        }

        public T GetAndActivate()
        {
            var result = Items.FindFirst(item => !item.gameObject.activeInHierarchy);
            result.gameObject.SetActive(true);
            return result;
        }

        public void DeactivateAll()
        {
            foreach (var item in Items)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}
