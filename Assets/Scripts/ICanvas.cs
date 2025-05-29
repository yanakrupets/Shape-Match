using UnityEngine;

public interface ICanvas
{
    public Canvas Canvas { get; }

    public void Initialize(object obj = null);

    public void Show()
    {
        Canvas.enabled = true;
    }

    public void Hide()
    {
        Canvas.enabled = false;
    }
}
