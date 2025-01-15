using UnityEditor;
using UnityEngine;

public class DragSystem : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] GameObject _object;
    private Vector2 offset;
    private bool isDragging;

    [SerializeField] float distance;
    float minX,maxX;
    float minY,maxY;
    float fixedXPosition;
    float fixedYPosition;

    enum MovedDirection
    {
        Horizontal,
        Vertical,
        Diagonal
    }
    [SerializeField] MovedDirection movedDirection;
    private void Start()
    {
        mainCamera = Camera.main;

        minY = _object.transform.position.y - distance;
        maxY = _object.transform.position.y + distance;
        minX = _object.transform.position.x - distance;
        maxX = _object.transform.position.x + distance;
        fixedXPosition = _object.transform.position.x;
        fixedYPosition = _object.transform.position.y;
    }

    private void OnMouseDown()
    {
        isDragging = true;
        Vector2 objectPosition = new Vector2(_object.transform.position.x, _object.transform.position.y);
        offset = objectPosition - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector2 targetPosition = GetMouseWorldPosition() + offset;
            switch (movedDirection)
            {
                case MovedDirection.Horizontal:
                    targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
                    targetPosition.y = fixedYPosition;
                    _object.transform.position = targetPosition;
                    break;
                case MovedDirection.Vertical:
                    targetPosition.y = Mathf.Clamp(targetPosition.y ,minY, maxY);
                    targetPosition.x = fixedXPosition;
                    _object.transform.position = targetPosition;
                    break;
                case MovedDirection.Diagonal:
                    targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
                    targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
                    _object.transform.position = Vector2.Lerp(_object.transform.position, targetPosition, 0.5f);
                    break;

            }

        }
    }
    private void OnMouseUp()
    {
        isDragging = false;
    }
    private Vector2 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.WorldToScreenPoint(_object.transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePosition);
    }
}
