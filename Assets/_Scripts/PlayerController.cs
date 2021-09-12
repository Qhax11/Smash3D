using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PlayerController : MonoBehaviour
{

    #region Instance Method
    public static PlayerController Instance;
    private void InstanceMethod()
    {
        if (Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    #endregion

    public GameObject hitSmokeFx;
    public GameObject deathEffect2;
    public GameObject deathEffect3;
    public GameObject coinEffect;
    public GameObject hearthEffect;

    public GameObject tractorCase;
    public GameObject tractorSmoke;

    public GameObject tractorMesh;
    public GameObject cylinder;

    public float smokeGrowScale;
    public TrailRenderer tractorTrail;
    private TrailRenderer trailTemp;

    public int playerHealt;


    private void Awake()
    {
        #region Instance Method
        InstanceMethod();
        #endregion
    }


    private void Start()
    {

        playerHealt = (12 - LevelManager.Instance.levelIndex * 2);

        tractorCase.GetComponent<Shakeing>().shakeSpeed = (10 - playerHealt)* 1.5f + 10 ;
        tractorSmoke.transform.localScale = new Vector3((10 - playerHealt) * 0.1f + 0.1f, (10 - playerHealt) * 0.1f + 0.1f, (10 - playerHealt) * 0.1f + 0.1f);

    }


    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Coin"))
        {
            UIManager.Instance.ScoreEnhance();
          //  GameManager.Instance.speedForward -= 3;
            SoundManager.Instance.coin.Play();
            
            CoinEffectMaker(other);
            TrailMaker(other);
            CylinderColorChance(other);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("badcoin"))
        {
            UIManager.Instance.ProgressBarReduce();
            SoundManager.Instance.badCoin.Play();

            Camera.main.DOShakePosition(0.5f, 0.5f);
            transform.DOShakePosition(0.5f, 0.5f);

            Destroy(other.gameObject);
            Destroy(Instantiate(hitSmokeFx, transform.position, Quaternion.identity),1);

            trailTemp.minVertexDistance = 1;
            Invoke("TrailMinVertexDistanceReset", 1);

            playerHealt--;
            if (playerHealt == 0) Death();
            
            if (playerHealt < 7)
            {
                tractorCase.GetComponent<Shakeing>().shakeSpeed += 3;
                tractorSmoke.transform.localScale = new Vector3(tractorSmoke.transform.localScale.x + smokeGrowScale, tractorSmoke.transform.localScale.y + smokeGrowScale, tractorSmoke.transform.localScale.z + smokeGrowScale);
            }

        }
        if (other.CompareTag("heart"))
        {
            playerHealt++;
            UIManager.Instance.ProgressBarEnhance();
            SoundManager.Instance.heal.Play();
            Destroy(Instantiate(hearthEffect, new Vector3(transform.position.x,transform.position.y+1,transform.position.z), Quaternion.Euler(-90, 0, 0)),1);
            Destroy(other.gameObject);

            if (playerHealt < 7 || tractorSmoke.transform.localScale != Vector3.one/5)
            {
                tractorCase.GetComponent<Shakeing>().shakeSpeed -= 2;
                tractorSmoke.transform.localScale = new Vector3(tractorSmoke.transform.localScale.x - smokeGrowScale, tractorSmoke.transform.localScale.y - smokeGrowScale, tractorSmoke.transform.localScale.z - smokeGrowScale);
            }

        }

        if (other.CompareTag("finish"))
        {
            GameManager.Instance.gameState = GameManager.GameState.Win;
            UIManager.Instance._GameWin();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Coin"))
        {
            GameManager.Instance.speedForward += 3;
        }

    }

    void TrailMinVertexDistanceReset()
    {
        trailTemp.minVertexDistance = 0.1f;
    }

    void CoinEffectMaker(Collider other)
    {
        var color = other.GetComponent<MeshRenderer>().material.color;
        coinEffect.GetComponent<ParticleSystem>().startColor = color;
        coinEffect.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().startColor = color;
        coinEffect.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().startColor = color;
        Destroy(Instantiate(coinEffect, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z+1), Quaternion.identity), 1);
    }
    void CoinPress(Collider other)
    {
        other.transform.localScale = new Vector3(1, 0.1f, 1.2f);
        other.transform.position = new Vector3(other.transform.position.x, 0.5f, other.transform.position.z);
        other.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        other.GetComponent<RiseAndRotate>().enabled = false;
    }

    void TrailMaker(Collider other)
    {
        if (trailTemp!=null)
        {
            trailTemp.transform.parent = null;
            Destroy(trailTemp.gameObject, 2);
        }
        Color color = other.GetComponent<MeshRenderer>().material.color;
        trailTemp = Instantiate(tractorTrail, new Vector3(transform.position.x, transform.position.y + 0.9f, transform.position.z + 1.8f), Quaternion.identity,transform);
        trailTemp.transform.localPosition = new Vector3(0, transform.position.y + 0.7f, 1.8f);
        trailTemp.startColor = color;
        trailTemp.endColor = color;

    }

    void CylinderColorChance(Collider other)
    {
        Color color = other.GetComponent<MeshRenderer>().material.color;
        cylinder.GetComponent<MeshRenderer>().material.color = color;
    }

    void GameLoseUI()
    {
        UIManager.Instance._GameLose();
    }

    public void Death()
    {
        Invoke("GameLoseUI", 1);
        var v3 = new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
        Instantiate(deathEffect2, v3, Quaternion.identity);
        Instantiate(deathEffect3, v3, Quaternion.identity);

        Destroy(tractorMesh);
        SoundManager.Instance.death.Play();
        GameManager.Instance.GameLose();
    }

  
}
