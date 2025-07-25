using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GG_Character : MonoBehaviour
{
    private float Health;
    private Characters type;
    private float movementSpeed;
    private float stoppingDistance;
    private GameObject Target;
   
    private bool ismove
    {
        get
        {
            if(Target == null)
            return false;
            return Vector3.Distance(Target.transform.position, transform.position) > stoppingDistance;
        }
    }
    private bool isClosetoTarget
    {
        get=> Vector3.Distance(Target.transform.position, transform.position) < stoppingDistance;
    }
    private Coroutine _updaterCoroutine;
    //private SO_Characters Data;
    private GameObject CurrentModel;
    private Animator animator;
    public GameObject WarriorModel, ArcherModel;
    public void Init(SO_Characters Data)
    {
        Health= Data.Health;
        movementSpeed=Data.movementSpeed;
        stoppingDistance= Data.stoppingDistance;
        type = Data.type;
        WarriorModel.SetActive(false);
        ArcherModel.SetActive(false);
        CurrentModel=type==Characters.Warrior?WarriorModel:ArcherModel;
        CurrentModel.SetActive(true);
        this.gameObject.SetActive(true);
        _updaterCoroutine=StartCoroutine(Updater());
        //Set Animation to idle
        animator=CurrentModel.GetComponent<Animator>();
        animator.Play("Idle");
    }
    public void SetTarget(GameObject _traget)
    {
        Target = _traget;
    }
    public void Move()
    {
        //Move to a position
            var dir = Target.transform.position-gameObject.transform.position;
        if (ismove)
        {
            gameObject.transform.position += dir.normalized * movementSpeed * Time.deltaTime;
            gameObject.transform.rotation = Quaternion.LookRotation(dir,Vector3.up);
            //Set Animation To Moving
            animator.Play(type==Characters.Warrior?"Running_B":"Running_A");
            //return;
        }
        else
        {
            animator.Play("Idle");
            gameObject.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        }
    }
    public void Attack()
    {
        if (isClosetoTarget) { 
        //Attack animation
        }

    }
    public void GetDamage(int damage)
    {
        Health-=damage;
        if (Health <= 0) { 
        //Call Object Pooling here
        Deactivate();
        }
    }
    public void Deactivate()
    {
        if (_updaterCoroutine != null)
            StopCoroutine(_updaterCoroutine);
        this.gameObject.SetActive(false);
        GameManager.Instance.MySummoner.AddCharacterTotheQueue(this);
    }
    
    IEnumerator Updater()
    {
        while (true)
        {
            yield return null;
            Move();
        }
    }

}
