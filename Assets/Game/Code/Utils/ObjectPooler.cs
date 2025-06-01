using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Code.Utils
{
    public class ObjectPooler<T> where T : MonoBehaviour
    {
        private readonly List<T> PoolList;
        private readonly Transform _container;
        private readonly T _prefab;

        public ObjectPooler(T prefab, Transform container, int count)
        {
            _prefab = prefab;
            _container = container;
            PoolList = new List<T>();

            for (int i = 0; i < count; i++)
                CreateObject();
        }

        public T GetObject()
        {
            if (HasFreeObject(out T prefab))
                return prefab;
            else return CreateObject(true);

            throw new System.Exception("There is no objects in pool");
        }

        private T CreateObject(bool isActiveByDefault = false)
        {
            var prefab = Object.Instantiate(_prefab, _container);
            prefab.gameObject.SetActive(isActiveByDefault);
            PoolList.Add(prefab);

            return prefab;
        }

        private bool HasFreeObject(out T prefab)
        {
            foreach (var obj in PoolList)
                if (!obj.gameObject.activeInHierarchy)
                {
                    prefab = obj;
                    prefab.gameObject.SetActive(true);
                    return true;
                }
            prefab = null;

            return false;
        }
    }
}