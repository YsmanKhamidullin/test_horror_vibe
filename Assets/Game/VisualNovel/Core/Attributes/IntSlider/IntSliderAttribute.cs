using UnityEngine;

namespace Game.VisualNovel.Scripts.Attributes.IntSlider
{
    public class IntSliderAttribute : PropertyAttribute
    {
        public int min;
        public int max;

        public IntSliderAttribute(int min, int max)
        {
            this.min = min;
            this.max = max;
        }
    }
}