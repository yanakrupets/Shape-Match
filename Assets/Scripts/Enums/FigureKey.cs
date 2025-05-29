namespace Enums
{
    public record FigureKey
    {
        public readonly int ShapeIndex;
        public readonly int ColorIndex;
        public readonly int IconIndex;

        public FigureKey(int shapeIndex, int colorIndex, int iconIndex)
        {
            ShapeIndex = shapeIndex;
            ColorIndex = colorIndex;
            IconIndex = iconIndex;
        }
    }
}
