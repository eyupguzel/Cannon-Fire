using System;
using System.Collections;
using UnityEngine;

public class BallControll : MonoBehaviour
{
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Diamond"))
        {
            other.gameObject.SetActive(false);
            EventManager.Instance.OnCollectable(transform.position);
           //GameManager.Instance.GetDiamondVfx(transform.position);
        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
            gameObject.SetActive(false);
            Color color = gameObject.GetComponent<Renderer>().material.color;
            GameManager.Instance.GetParticalEffect(transform.position,color);
            AudioManager.Instance.GetExplosionSound();
        }
        if (other.gameObject.CompareTag("BucketTrigger"))
        {
            EventManager.Instance.OnScored();
            ResetValues();
            GameManager.Instance.CheckFinish();
        }
        if (other.gameObject.CompareTag("Boundary"))
        {
            //...
            ResetValues();
            GameManager.Instance.CheckFinish();

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spring"))
        {
            VibrationManager.Vibrate(28);
            AudioManager.Instance.GetSpringSound();
            //collision.gameObject.GetComponent<Animator>().SetTrigger("active");
        }
    }
    
    private void ResetValues()
    {
        gameObject.SetActive(false);
        rb.angularVelocity = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
    }
    private void OnEnable()
    {
        StartCoroutine(DeactiveBall());
    }
    
    private void OnDisable()
    {
        ResetValues();
    }

    private IEnumerator DeactiveBall()
    {
        yield return new WaitForSeconds(7f);
        gameObject.SetActive(false);
    }
}
