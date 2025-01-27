using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
enum SkillName //Wave �� �ĵ�������, Rain �� �񳻸��°Ű�����, Shoot�� rotation�������� �ѹ� �߻�(������)
{
    Wave=6,Rain=11,Shoot=9
}
public class Wizard_Movement : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    Animator animator;
    private bool warpDistance=false;
    [SerializeField]
    private float warpCooltime = 0;
    [SerializeField]
    private float skillCooltime = 6;
    [SerializeField]
    private int skillnum;
    protected bool changeRot=false;
    protected float wizardHP = 30;
    [SerializeField]
    GameObject[] Skills;
    bool BFanimate = true;
    float upypos;
    float downypos;
    float ct = 0;
    [SerializeField]
    GameObject floor;
    [SerializeField]
    GameObject updown_Obj;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        upypos = transform.position.y;
        downypos = transform.position.y-11;
        skillnum = Random.Range(1, 4);
        skillCooltime = 0;
        Warp();
    }
    void Update()
    {
        ChangeRotation();
        CoolTime();
        SkillController();
        ct += Time.deltaTime;
    }   
    private void CoolTime() 
    {
        WarpDis();
        if (warpCooltime < 0 && (warpDistance))
        {
            Warp();
            warpCooltime = 12;
        }
        else if (warpCooltime > 0) { warpCooltime -= Time.deltaTime; }
        else warpCooltime -= Time.deltaTime;
    }
    private void ChangeRotation()  //�ٶ󺸴� ���� �ٲٱ�
    {
        if (player.transform.position.x > this.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            changeRot = true;
        }
        else if(player.transform.position.x < this.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0f,180f,0f);
            changeRot=false;
        }
        else
        {
            new WaitForSeconds(0.1f);
        }
    }
    private void WarpDis()//�Ÿ� üũ (��������) - x��ǥ �ֺ� 3 , 8 / -8 �̻�/����
    {
        if (((this.transform.position.x -3 > player.transform.position.x )) &&
            ((this.transform.position.x +3 < player.transform.position.x )))
        {
            warpDistance = true;
        }
        else
        {
            warpDistance = false;
        }
    }
    private void Warp() //����
    {
        if (this.transform.position.x < transform.position.x+20)
        {
            transform.position = new Vector2(Random.Range(transform.position.x + 20f, transform.position.x + 22f), Random.Range(0,2)<1?upypos:downypos);
        }
        else if(this.transform.position.x > transform.position.x-20)
        {
            transform.position = new Vector2(Random.Range(transform.position.x - 20f, transform.position.x - 22f), Random.Range(0, 2) < 1 ? upypos : downypos);
        }
        warpDistance = false;
    }
    int SkillCooltimeSet(int skill) //��ų ��Ÿ�� ���� + C#������ ���ٽ� �ذ�
    {
        return skill switch
        {
            1=>(int)SkillName.Wave,
            2=>(int)SkillName.Rain,
            3=>(int)SkillName.Shoot,
            _=> 0
        };
    }
    void SkillController() //��ų�� üũ �׽�ų ���
    {
        skillCooltime -= Time.deltaTime;
        if (0 >= skillCooltime)
        {
            skillCooltime = 2;
            StartCoroutine(animations("Attack1", true));
            if (skillnum == 2)
            {
                Invoke("Skill", 1.8f);
                if (BFanimate)
                {
                    BFanimate = false;
                }
            }
            else
            {
                Invoke("Skill", 1);
            }
        }
    }
    void Skill()
    {
            GameObject thisskill = Instantiate(Skills[skillnum - 1]);
            //thisskill.transform.SetParent(this.gameObject.transform);
            skillnum = Random.Range(1, 4);
            skillCooltime = SkillCooltimeSet(skillnum);
        BFanimate = true;
    }
    IEnumerator animations(string animation,bool setbool)
    {
        animator.SetBool(animation, setbool);
        yield return new WaitForSeconds(0f);
        animator.SetBool(animation, !setbool);
    }
    //void SkillUse(int skill) // ���� ������ ����
    //{
    //    GameObject thisskill = Instantiate(Skills[skill], this.transform);
    //    thisskill.transform.position = this.transform.position;
    //    thisskill.transform.SetParent(this.transform);
    //}
    private void OnCollisionEnter2D(Collision2D collision) //�÷��̾� ���� ����
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (ct > 1)
            {
                animator.SetBool("Take hit", true);

                if (Random.Range(1, 11) < 3)
                {
                    wizardHP -= 3;
                }
                else
                    wizardHP -= 2;
            }
            if (wizardHP <= 0)
            {
                animator.SetBool("Death", true);
                floor.SetActive(false);
                updown_Obj.SetActive(false);
                updown_Obj.transform.position = new Vector2(51.3f, -31.5f);
                updown_Obj.SetActive(true);
                Destroy(gameObject);
            }
            ct = 0;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")||ct>0.8f)
        {
            animator.SetBool("Take hit", false);
        }
        }
}
