using System.Collections.Generic;
using UnityEngine;

namespace Scriptable_Objects
{
    [CreateAssetMenu(fileName = "FigureData", menuName = "Data/Figure Data")]
    public class FigureData : ScriptableObject
    {
        [SerializeField] private Sprite[] shapes;
        [SerializeField] private Color[] colors;
        [SerializeField] private Sprite[] icons;

        public IReadOnlyList<Sprite> Shapes => shapes;
        public IReadOnlyList<Color> Colors => colors;
        public IReadOnlyList<Sprite> Icons => icons;
    }
}
