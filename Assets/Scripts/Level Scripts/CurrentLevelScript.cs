using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentLevelScript : MonoBehaviour
{
    public List<Vector3> diamondPositions = new List<Vector3>();
    [SerializeField] int targetScore;
    [SerializeField] int ballCount;
    [SerializeField] float waitTime;
    [SerializeField] ParticleSystem confettiEffect;
    [SerializeField] float ballXRotation;
    int i;
    void Start()
    {
        //SaveSystem.Instance.SaveData();
        GameManager.Instance.SetTargetScore(targetScore);
        GameManager.Instance.SetBallCount(ballCount);
        LevelManager.Instance.SetCurrentLevel();
        EventManager.Instance.OnChangedLevel();
        // GameManager.Instance.GetDiamond();
        ActiveDiamond(0);
        EventManager.Instance.onCollectable += ActiveDiamond;

        UiManager.Instance.UpdateLevelText();

        EventManager.Instance.onFinish += GetConfettiEffect;

        //GameManager.Instance.SetBallsRightRotation(ballXRotation);
    }
    private void ActiveDiamond(object param) => StartCoroutine(DiamondCoroutine());
    private IEnumerator DiamondCoroutine()
    {
        yield return new WaitForSeconds(waitTime);

        if(i < diamondPositions.Count)
            GameManager.Instance.GetDiamond(diamondPositions[i++]);
    }
    private void GetConfettiEffect()
    {
        confettiEffect.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        EventManager.Instance.onCollectable -= ActiveDiamond;
        EventManager.Instance.onFinish -= GetConfettiEffect;
    }
    
}
