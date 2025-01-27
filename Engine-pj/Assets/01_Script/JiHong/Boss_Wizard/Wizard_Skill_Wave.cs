using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_Skill_Wave : MonoBehaviour
{
    Transform wizard_Transform;
    GameObject player;
    float rotate=90;
    int rotation_y;
    int setxpos;
    float continuoustime = 0.5f; // ���ӽð�
    private void Awake()
    {
        wizard_Transform = FindObjectOfType<Wizard_Movement>().transform;
        player = GameObject.FindGameObjectWithTag("Player");
        

    }
    private void Start()
    {
        SetDirection();
        transform.position = new Vector3(player.transform.position.x + setxpos, player.transform.position.y - 1);
    }
    private void Update()
    {
        StartCoroutine(Rotating());
    }
    void SetDirection()
    {
        if(wizard_Transform.position.x < player.transform.position.x)
        {
            rotation_y = 0;
            setxpos = -5;
        }
        else
        {
            rotation_y = 180;
            setxpos = 5;
        }
    }
    IEnumerator Rotating()
    {
        if (rotate <= 180)
        {  
            this.transform.rotation = Quaternion.Euler(0, rotation_y, rotate);
            rotate+=1.5f;
        }
        yield return new WaitForSeconds(Time.deltaTime);
        if(rotate >= 180)
        {
            yield return new WaitForSeconds(continuoustime);
            Destroy(this.gameObject);
        }
    }
}