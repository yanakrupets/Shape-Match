using System.Collections.Generic;
using Enums;

namespace Managers
{
    public class EventManager
    {
        public delegate void FigureClickEvent(Figure figure);
        public event FigureClickEvent OnFigureClick;
    
        public delegate void ShuffleEvent(IReadOnlyDictionary<FigureKey, int> figures);
        public event ShuffleEvent OnShuffle;
    
        public void PublishFigureClick(Figure figure)
        {
            OnFigureClick?.Invoke(figure);
        }

        public void PublishShuffle(IReadOnlyDictionary<FigureKey, int> figures)
        {
            OnShuffle?.Invoke(figures);
        }
    }
}
