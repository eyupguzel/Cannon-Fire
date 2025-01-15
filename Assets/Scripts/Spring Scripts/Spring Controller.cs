using UnityEngine;

public class SpringController : MonoBehaviour
{
    [SerializeField] float forcePower;

    [SerializeField]
    enum SpringBehavior
    {
        None,
        ApplyForce
    }
    [SerializeField] SpringBehavior behavior;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            switch (behavior)
            {
                case SpringBehavior.None:
                    break;
                case SpringBehavior.ApplyForce:
                    collision.gameObject.GetComponent<Rigidbody>().AddForce(-transform.forward * (Time.deltaTime * forcePower), ForceMode.Impulse);
                    break;
            }

        }
    }
}
