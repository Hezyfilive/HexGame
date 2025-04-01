using Core.Modifiers.Weapon;
using Editor.Attributes;
using UnityEngine;
namespace Core.TileModifiers
{
    [CreateAssetMenu(fileName = "TileModifierPreset", menuName = "Scriptable Objects/TileModifierPreset")]
    public class TileModifierPreset : ScriptableObject
    {
        public string tileType;
        
        
        [Header("Move cost modifier")]
        public float moveCostModifier = 1.0f;


        [ModifierCategory("Weapon Modifiers")] 
        public RangedWeaponModifiers rangedWeaponModifiers = new RangedWeaponModifiers();

    }
}
