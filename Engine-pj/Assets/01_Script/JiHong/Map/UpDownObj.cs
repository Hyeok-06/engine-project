using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownObj : MonoBehaviour
{
    float waitSeconds=-25.5f;
    float high_ypos=-18.5f;
    float low_ypos=2f;
    bool goup=false;
    bool godown=false;
    float moveSpeed=5;
    [SerializeField]
    GameObject floor;
    [SerializeField]
    GameObject Boss;
    bool repeat=true;
    private void Update()
    {
        if (goup&&transform.position.y<high_ypos)
        {
            transform.position += Vector3.up * Time.deltaTime*moveSpeed;
        }
        else if (godown&&transform.position.y>low_ypos)
        {
            transform.position += Vector3.down * Time.deltaTime * moveSpeed;
        }
        if (repeat)
        {
            if (transform.position.y >= low_ypos && (floor.activeInHierarchy == false || Boss.activeInHierarchy == false))
            {
                floor.SetActive(true);
                Boss.SetActive(true);
                Boss = null;
                repeat=false;
            }
        }
    }
    IEnumerator GoUpandDown()
    {
        goup=true;
        yield return new WaitForSeconds(waitSeconds);
        goup=false;
        godown = true;
        yield return new WaitForSeconds(waitSeconds);
        godown=false;
        StartCoroutine(GoUpandDown());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.position.y>-21.5f)
        collision.transform.SetParent(this.transform);
        if (Input.GetKeyDown(KeyCode.F) && collision.gameObject.CompareTag("Player"))
        {
            Boss.SetActive(true);
            StartCoroutine(GoUpandDown());
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Input.GetKeyDown(KeyCode.F) && collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(GoUpandDown());
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
