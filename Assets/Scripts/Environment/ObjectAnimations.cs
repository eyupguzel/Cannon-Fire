using UnityEngine;

public class ObjectAnimations : MonoBehaviour
{
    private Vector3 startPosition;
    [SerializeField] float speed = 1.5f;
    [SerializeField] float spinSpeed;
    [SerializeField] float distance = 2f;

    [SerializeField]
    enum ObjectAnimation
    {
        None,
        Right,
        Left,
        Up,
        Down,
        Forward
    }
    [SerializeField] enum RotationState
    {
        None,
        Static,
        Rotatable
    }
    [SerializeField] enum ObjectType
    {
        Sun,
        Saw_Platform,
        Axis_Y
    }
    [SerializeField] RotationState rotationState;
    [SerializeField] ObjectAnimation direction;
    [SerializeField] ObjectType objectType;
    void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        float offset = Mathf.PingPong(Time.time * speed, distance);


        switch (direction)
        {
            case ObjectAnimation.Right:
                transform.position = startPosition + new Vector3(offset, 0, 0);
                break;
            case ObjectAnimation.Left:
                transform.position = startPosition - new Vector3(offset, 0, 0);
                break;
            case ObjectAnimation.Up:
                transform.position = startPosition + new Vector3(0, offset, 0);
                break;
            case ObjectAnimation.Down:
                transform.position = startPosition - new Vector3(0, offset, 0);
                break;
            case ObjectAnimation.Forward:
                transform.localPosition = transform.up * offset;
                break;

        }

        switch (rotationState)
        {
            case RotationState.Static:
                break;
            case RotationState.Rotatable:
                switch (objectType)
                {
                    case ObjectType.Sun:
                        transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
                        break;
                    case ObjectType.Saw_Platform:
                        transform.Rotate(spinSpeed * Time.deltaTime, 0, 0);
                        break;
                    case ObjectType.Axis_Y:
                        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
                        break;
                }
                break;

        }
    }
}
