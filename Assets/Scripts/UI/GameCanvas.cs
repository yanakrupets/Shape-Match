using System;
using Extensions;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameCanvas : MonoBehaviour, ICanvas
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private GraphicRaycaster graphicRaycaster;
        
        [SerializeField] private Bar bar;
        [SerializeField] private Button shuffleButton;
        
        private EventManager _eventManager;
        private UIManager _uiManager;
        
        public Canvas Canvas => canvas;

        public void Construct(EventManager eventManager, UIManager uiManager)
        {
            _eventManager = eventManager;
            _uiManager = uiManager;
            
            _eventManager.OnFigureClick += bar.Add;
            
            shuffleButton.onClick.AddListener(Shuffle);
        }

        private void OnDisable()
        {
            if (_eventManager != null)
            {
                _eventManager.OnFigureClick -= bar.Add;
            }
            
            shuffleButton.onClick.RemoveListener(Shuffle);
        }

        public void Initialize(object obj = null)
        {
            ArgumentTypeChecker.Check<int>(obj, out var count);

            bar.Initialize(count);
            bar.SetManagers(_uiManager);
        }

        public void Active(bool isOn)
        {
            graphicRaycaster.enabled = isOn;
        }

        private void Shuffle()
        {
            _eventManager.PublishShuffle(bar.FiguresInBar);
        }
    }
}
