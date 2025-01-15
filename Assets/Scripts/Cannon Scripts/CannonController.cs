using UnityEngine;

public class CannonController : MonoBehaviour
{
    GameManager gameManager;

    [Header("Cannon Settings")]
    [SerializeField] Transform firePoint;
    [SerializeField] float ballPower;
    private int ballIndex;

    [Header("Cannon Animations")]
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem fireEffect;

    [SerializeField] Quaternion setxrotation;

    void Start()
    {
        gameManager = GameManager.Instance;
        EventManager.Instance.onClick += BallController;
    }

    private void OnDisable()
    {
        EventManager.Instance.onClick -= BallController; // Sahne deðiþtiðinde bir önceki sahnedeki metod'un bellek adresi hala tutulmuþ oluyor.Bu yüzden abonelikten çýkarmak gerekir.
    }
    private void BallController()
    {
        GetCannonAnimations();
        VibrationManager.Vibrate(55);

        gameManager.balls[ballIndex].gameObject.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);  
        gameManager.balls[ballIndex].gameObject.SetActive(true);
        gameManager.balls[ballIndex].GetComponent<Rigidbody>().AddForce(gameManager.balls[ballIndex].transform.TransformDirection(90, 90, 0) * ballPower, ForceMode.Force);
        if (gameManager.balls.Length - 1 == ballIndex)
            ballIndex = 0;
        else
            ballIndex++;
    }
    private void GetCannonAnimations()
    {
        animator.SetTrigger("fire");
        fireEffect.Play();
    }
}
