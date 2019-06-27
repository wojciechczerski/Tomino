using UnityEngine;

namespace Tomino
{
    public class GameObjectPool<T> where T : MonoBehaviour
    {
        public T[] Items { get; private set; }

        public GameObjectPool(GameObject prefab, int size, GameObject parent)
        {
            Items = new T[size];
            for (int i = 0; i < size; ++i)
            {
                var newItem = GameObject.Instantiate(prefab);
                newItem.transform.parent = parent.transform;
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
            foreach (var item in Items) item.gameObject.SetActive(false);
        }
    }
}