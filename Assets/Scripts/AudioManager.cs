using UnityEngine;

public class AudioManager : GenericSingleton<AudioManager>
{
    
    [Header("Game Music")]
    [SerializeField] private AudioSource audioSource;

    [Header("Other Sounds")]
    [SerializeField] private AudioSource otherSoundsSource;
    [SerializeField] private AudioClip cannonFireSound;
    [SerializeField] private AudioClip springSound;
    [SerializeField] private AudioClip targetSound;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip diamondSound;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private AudioClip winSound;

    private GameManager gameManager;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
       
        gameManager = GameManager.Instance; ;
        audioSource = GetComponent<AudioSource>();
        EventManager.Instance.onClick += GetCannonFireSound;
        EventManager.Instance.onScored += TargetSound;
        EventManager.Instance.onCollectable += DiamondCollectSound;
    }

    private void GetCannonFireSound()
    {
        otherSoundsSource.PlayOneShot(cannonFireSound);
    }

    public void GetSpringSound()
    {
       // otherSoundsSource.PlayOneShot(springSound);
    }
    public void GetExplosionSound()
    {
        otherSoundsSource.PlayOneShot(explosionSound);
    }
    private void TargetSound()
    {
        otherSoundsSource.PlayOneShot(targetSound);

    }
    private void DiamondCollectSound(object param)
    {
        otherSoundsSource.PlayOneShot(diamondSound);
    }
    public void ClikSound()
    {
        otherSoundsSource.PlayOneShot(clickSound);
    }
    public void WinSound()
    {
        otherSoundsSource.PlayOneShot(winSound);

    }
}
