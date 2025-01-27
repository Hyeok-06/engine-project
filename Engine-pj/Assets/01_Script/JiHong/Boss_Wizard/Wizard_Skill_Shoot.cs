using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_Skill_Shoot : MonoBehaviour
{
    Transform   Boss;
    GameObject player;
            float scale = 0.1f;
                    float rotz=0;
    Vector3 dir;
    float speed=10;
    bool Moving=false;
    float destroytime = 6;
    private void Awake()
    {
        Boss = FindObjectOfType<Wizard_Movement>().transform;
        player = GameObject.FindWithTag("Player");
    }
    private void Start()
    {
        if (Boss.transform.position.x <= player.transform.position.x)
        {
            dir = Vector3.right;
        }
        else if(Boss.transform.position.x > player.transform.position.x)
        {
            dir = Vector3.left;
        }
        transform.position = new Vector2( Boss.transform.position.x, player.transform.position.y+Random.Range(0.5f,2.5f));
        transform.position = Boss.transform.position;
        StartCoroutine(Destroytime());
    }
    void Update()
    {
        StartCoroutine(Starting());
        if (Moving) transform.position += dir * Time.deltaTime * speed;
        rotz -= 1;
    }

    IEnumerator Starting()
    {
            StartCoroutine(StartSpin());
            yield return new WaitForSeconds(2.5f);
        Moving = true;
    }
    IEnumerator StartSpin()
    {
        rotz -= 1.25f;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotz);
        if (scale < 1)
        {
            transform.localScale = new(scale*1.5f, scale*1.5f, 1);
            scale += 0.01f;
        }
        yield return new WaitForSeconds(Time.deltaTime);
        rotz -= 1.25f;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotz);
    }
    IEnumerator Destroytime()
    {
        yield return new WaitForSeconds(destroytime);
        Destroy(gameObject);
    }
}
