using UnityEngine;

public class Sun : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, 0, -.15f);       
    }
}
