using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Managers;
using UnityEngine;

namespace UI
{
    public class Bar : MonoBehaviour
    {
        [SerializeField] private UIFigureFrame framePrefab;

        private UIManager _uiManager;
        
        private List<UIFigureFrame> _frames;
        
        public Dictionary<FigureKey, int> FiguresInBar { get; private set; }

        public void Initialize(int count)
        {
            FiguresInBar = new Dictionary<FigureKey, int>();
            _frames = new List<UIFigureFrame>();
            for (var i = 0; i < count; i++)
            {
                var uiFigure = Instantiate(framePrefab, transform);
                _frames.Add(uiFigure);
            }
        }

        public void SetManagers(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        public void Add(Figure figure)
        {
            var freeFrame = _frames.First(f => f.IsFree);
            freeFrame.ShowFigure(figure);

            if (!FiguresInBar.TryAdd(figure.Key, 1))
                FiguresInBar[figure.Key]++;

            StartCoroutine(CheckCoroutine(figure.Key));
        }

        private IEnumerator CheckCoroutine(FigureKey key)
        {
            yield return new WaitForSeconds(0.1f);
            Check(key);
        }

        private void Check(FigureKey key)
        {
            if (FiguresInBar[key] == 3)
            {
                foreach (var frame in _frames.Where(f => f.Key == key))
                {
                    frame.RemoveFigure();
                }

                FiguresInBar.Remove(key);
            }
            else if (!_frames.Any(frame => frame.IsFree))
            {
                _uiManager.Open<ResultCanvas>(GameResult.Lose);
            }
        }
    }
}
