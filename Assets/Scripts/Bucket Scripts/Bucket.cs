using System;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Bucket : MonoBehaviour
{
    [SerializeField] ParticleSystem effect;

    [SerializeField] Renderer _renderer;
    private float bucketValue;
    private float bucketStepValue;
    
   
    private void Start()
    {
        bucketValue = 0.5f;
        bucketStepValue = 0.25f / GameManager.Instance.targetScore;
    }
    private void OnTriggerEnter(Collider other)
    {
        effect.gameObject.GetComponent<ParticleSystem>().startColor = other.gameObject.GetComponent<Renderer>().material.color;
        effect.Play();

        if (other.gameObject.CompareTag("Ball"))
        {
            if (bucketValue > 0.25f)
            {
                VibrationManager.Vibrate(30);
                bucketValue -= bucketStepValue;
                _renderer.material.SetTextureScale("_MainTex", new Vector2(1, bucketValue));
            }
        }
    }
}
