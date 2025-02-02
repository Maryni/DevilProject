using UnityEngine;

namespace Project.Game
{
    public enum BonusType
    {
        None,
        Gravity,
        BoxSize
    }
    public class Bonus : MonoBehaviour
    {
        public BonusType BonusType;
    }
}
