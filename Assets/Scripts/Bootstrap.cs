using Managers;
using Scriptable_Objects;
using UI;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private FigureData figureData;
    
    [Header("Managers")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uiManager;
    
    [Header("Canvases")]
    [SerializeField] private GameCanvas gameCanvas;
    [SerializeField] private ResultCanvas resultCanvas;

    private void Awake()
    {
        var eventManager = new EventManager();

        uiManager.Construct();
        gameManager.Construct(figureData, eventManager, uiManager, gameCanvas);
        gameCanvas.Construct(eventManager, uiManager);
        resultCanvas.Construct();
        
        gameManager.StartGame();
    }
}
