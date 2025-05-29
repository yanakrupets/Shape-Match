using Enums;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIFigureFrame : MonoBehaviour
    {
        [SerializeField] private GameObject backgroundObject;
        
        [SerializeField] private Image figureImage;
        [SerializeField] private Image iconImage;

        public bool IsFree { get; private set; } = true;
        public FigureKey Key { get; private set; }

        public void ShowFigure(Figure figure)
        {
            Key = figure.Key;
            figureImage.sprite = figure.Shape;
            figureImage.color = figure.Color;
            iconImage.sprite = figure.Icon;
            
            IsFree = false;
            backgroundObject.SetActive(false);
            figureImage.gameObject.SetActive(true);
        }

        public void RemoveFigure()
        {
            IsFree = true;
            backgroundObject.SetActive(true);
            figureImage.gameObject.SetActive(false);
        }
    }
}
