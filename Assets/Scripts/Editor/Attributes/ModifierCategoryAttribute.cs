using System;

namespace Editor.Attributes
{
    
    
    [AttributeUsage(AttributeTargets.Field)]
    public class ModifierCategoryAttribute : Attribute
    {
        public string CategoryName;

        public ModifierCategoryAttribute(string categoryName)
        {
            this.CategoryName = categoryName;
        }
    }
}
