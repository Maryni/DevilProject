using UnityEngine;

namespace Project.Game
{
    public enum ReflectType
    {
        None,
        Reflect,
        NonReflect
    }
    public class ReflectObject : MonoBehaviour
    {
        public ReflectType ReflectType;
        public bool AddScore;
    }
}
