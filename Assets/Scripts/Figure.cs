using Enums;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;

public class Figure : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SpriteRenderer shapeSpriteRenderer;
    [SerializeField] private SpriteRenderer iconSpriteRenderer;
    
    [SerializeField] private Rigidbody2D figureRigidbody2D;
    
    private EventManager _eventManager;

    public FigureKey Key { get; private set; }
    public Sprite Shape { get; private set; }
    public Color Color { get; private set; }
    public Sprite Icon { get; private set; }
    public Rigidbody2D Rigidbody2D => figureRigidbody2D;
    public bool IsEnable { get; set; }
    
    public void Initialize(FigureKey key, Sprite shapeSprite, Color color, Sprite iconSprite)
    {
        Key = key;
        Shape = shapeSprite;
        Color = color;
        Icon = iconSprite;
        
        shapeSpriteRenderer.sprite = shapeSprite;
        shapeSpriteRenderer.color = color;
        iconSpriteRenderer.sprite = iconSprite;
    }
    
    public void SetEventManager(EventManager eventManager)
    {
        _eventManager = eventManager;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsEnable)
            _eventManager.PublishFigureClick(this);
    }
}
