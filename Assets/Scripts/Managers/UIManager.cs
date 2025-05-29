using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameCanvas gameCanvas;
        [SerializeField] private ResultCanvas resultCanvas;

        private Dictionary<Type, ICanvas> _canvases;
        private ICanvas _currentCanvas;

        public void Construct()
        {
            _canvases = new Dictionary<Type, ICanvas>
            {
                { typeof(GameCanvas), gameCanvas },
                { typeof(ResultCanvas), resultCanvas }
            };
        }

        public void Open<T>(object obj = null) where T : ICanvas
        {
            _currentCanvas?.Hide();
            _currentCanvas = _canvases[typeof(T)];
            _currentCanvas.Initialize(obj);
            _currentCanvas.Show();
        }

        public void Hide()
        {
            _currentCanvas?.Hide();
        }
    }
}
