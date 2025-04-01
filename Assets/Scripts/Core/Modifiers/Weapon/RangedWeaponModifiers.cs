using UnityEngine;

namespace Core.Modifiers.Weapon
{
    [System.Serializable]
    public class RangedWeaponModifiers
    {
        public bool enabled = false;
        
        [Range(0f, 2f)]
        public float weaponDamageModifier = 1f;
        [Range(0f, 2f)]
        public float fireRateModifier = 1f;
        [Range(0f, 2f)]
        public float weaponRangeModifier = 1f;
    }
}
