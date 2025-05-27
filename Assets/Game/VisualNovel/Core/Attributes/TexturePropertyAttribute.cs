using System;
using UnityEngine;

namespace Game.VisualNovel.Scripts.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class TexturePropertyAttribute : PropertyAttribute
    {
        public TexturePropertyAttribute()
        {
        }
    }
}