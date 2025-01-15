using UnityEngine;

public class Cloud : MonoBehaviour
{
    private Vector3 startPosition;
    private float speed = .5f;
    private float distance = 2f;

    [SerializeField] enum CloudDirection
    {
        right,
        left
    }
    [SerializeField] CloudDirection direction;
    void Start()
    {
        startPosition = transform.position;   
    }
    void Update()
    {
                float offset = Mathf.PingPong(Time.time * speed, distance);


        switch (direction)
        {

            case CloudDirection.right:
                transform.position = startPosition + new Vector3(offset, 0, 0);
                break;
            case CloudDirection.left:
                transform.position = startPosition - new Vector3(offset, 0, 0);
                break;
        }
    }
}
