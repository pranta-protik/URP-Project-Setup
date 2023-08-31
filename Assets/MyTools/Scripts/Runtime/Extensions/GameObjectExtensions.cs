using UnityEngine;

namespace MyTools
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Returns component if found otherwise adds it to the gameobject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject"></param>
        /// <returns>Component</returns> 
        public static T GetOrAdd<T>(this GameObject gameObject) where T : Component
        {
            var component = gameObject.GetComponent<T>();
            return component != null ? component : gameObject.AddComponent<T>();
        }
    }
}