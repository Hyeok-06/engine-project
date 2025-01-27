using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TeleportCollider : MonoBehaviour
{
    public enum NextPositionType
    {
        Shop,
        Mine,
        Dungeon

    }

    public NextPositionType nextPositionType;
    public Transform DestinationPoint;
    public GameObject playerScaffolding;
    public GameObject CurrentCam;
    public GameObject ShopCam;
    public GameObject DungeonCam;
    public GameObject MineCam;
    //public GameObject currentBackGround;
    //public GameObject otherBackGround;
    public bool isEnterDungeon = false;
    public AudioSource currentBgm;
    public AudioSource miniBgM;
    public AudioSource DungeonBgM;
    public Image Panel;

    float time = 0f;
    float ftime = 0.5f;

    static public TeleportCollider instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        //currentBackGround.SetActive(true);
        //otherBackGround.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (nextPositionType == NextPositionType.Shop)
            {
                collision.transform.position = new Vector3(0, 3, 0);
            }
            else if (nextPositionType == NextPositionType.Mine)
            {
                collision.transform.position = DestinationPoint.position;
                DungeonCam.SetActive(false);
                playerScaffolding.SetActive(true);
                Fade(MineCam, miniBgM);
            }
            else if (nextPositionType == NextPositionType.Dungeon)
            {
                isEnterDungeon = true;
                collision.transform.position = DestinationPoint.position;
                MineCam.SetActive(false);
                Fade(DungeonCam, DungeonBgM);
            }
        }
    }
    public void Fade(GameObject otherCam, AudioSource bgm) // fade�� ų ķ�� �Ѱ������
    {
        StartCoroutine(FadeIn(otherCam, bgm));
    }

    IEnumerator FadeIn(GameObject otherCam, AudioSource bgm)
    {
        Panel.gameObject.SetActive(true);
        Color alpha = Panel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / ftime;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
        }
        time = 0f;
        CurrentCam.SetActive(false);
        currentBgm.Stop();
        yield return new WaitForSeconds(0.7f);
        playerScaffolding.SetActive(false);
        otherCam.SetActive(true);
        bgm.Play();

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / ftime;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha;
            yield return null;
        }
        Panel.gameObject.SetActive(false);
        yield return null;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

        }
    }
}
