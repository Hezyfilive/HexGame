using System.Collections.Generic;
using System.Reflection;
using Core.TileModifiers;
using Editor.Attributes;
using UnityEditor;

namespace Editor
{
    [CustomEditor(typeof(TileModifierPreset))]
    public class TileModifierPresetEditor  : UnityEditor.Editor
    {
        private SerializedProperty _moveCostModifier;
        private SerializedProperty _tileType;
        
        
        private readonly Dictionary<string, bool> _categoryVisibility = new();

        private void OnEnable()
        {
            _moveCostModifier = serializedObject.FindProperty("moveCostModifier");
            _tileType = serializedObject.FindProperty("tileType");
            
            _categoryVisibility.Clear();
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            EditorGUILayout.PropertyField(_tileType);
            EditorGUILayout.PropertyField(_moveCostModifier);
            
            var fields = target.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (var field in fields)
            {
                var categoryAttribute = field.GetCustomAttribute<ModifierCategoryAttribute>();
                if (categoryAttribute != null)
                {
                    _categoryVisibility.TryAdd(categoryAttribute.CategoryName, true);
                    
                    _categoryVisibility[categoryAttribute.CategoryName] = EditorGUILayout.Foldout(_categoryVisibility[categoryAttribute.CategoryName], categoryAttribute.CategoryName);

                    if (_categoryVisibility[categoryAttribute.CategoryName])
                    {
                        EditorGUI.indentLevel++;
                        
                        SerializedProperty property = serializedObject.FindProperty(field.Name);
                        EditorGUILayout.PropertyField(property, true);
                        
                        EditorGUI.indentLevel--;
                    }
                }
            }
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}
