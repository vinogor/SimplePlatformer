using UnityEngine;

public class DisplacePressedButtonText : MonoBehaviour
{
    public int offsetX = 2;
    public int offsetY = 2;
    public RectTransform textRect;
    
    private Vector3 _position;

    void Start()
    {
        _position = textRect.localPosition;
    }

    public void Down()
    {
        textRect.localPosition = new Vector3(_position.x + offsetX, _position.y - offsetY, _position.z);
    }

    public void Up()
    {
        textRect.localPosition = _position;
    }
}