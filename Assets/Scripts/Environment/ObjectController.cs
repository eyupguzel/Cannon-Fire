using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class ObjectController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] float speed;
    [SerializeField] GameObject _object;
    [SerializeField] GameObject cycle;
    [SerializeField]
    public enum Direction
    {
        Right,
        Left
    }
    [SerializeField]
    enum ObjectType
    {
        Empty,
        Spring,
        Wood,
        Platform
    }
    [SerializeField] ObjectType objectType;
    public Direction direction;
    private bool isHolding;
    private void ApplyRotation()
    {
        cycle.gameObject.SetActive(false);
        switch (objectType)
        {
            case ObjectType.Spring:
                switch (direction)
                {
                    case Direction.Right: _object.transform.Rotate(0, speed, 0, Space.Self); break;
                    case Direction.Left: _object.transform.Rotate(0, -speed, 0, Space.Self); break;
                }
                break;
            case ObjectType.Wood:
                switch (direction)
                {
                    case Direction.Right:_object.transform.Rotate(0, 0, -speed, Space.Self); break;
                    case Direction.Left: _object.transform.Rotate(0, 0, speed, Space.Self); break;
                }
                break;
            case ObjectType.Platform:
                switch (direction)
                {
                    case Direction.Right: _object.transform.Rotate(0, speed, 0, Space.Self); break;
                    case Direction.Left: _object.transform.Rotate(0, -speed, 0, Space.Self); break;
                }
                break;
        }
    }
   

    private IEnumerator PerformAction()
    {
        while (isHolding)
        {
            ApplyRotation();
            yield return null;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
        StartCoroutine(PerformAction());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;
    }
}
