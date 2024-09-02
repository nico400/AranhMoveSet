using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderManager : MonoBehaviour
{

    public enum TypeSpider
    {
        SpiderVariantOne,
        SpiderVariantTwo
    }
    public TypeSpider currentType;

    BaseStates currentState;

    public Transform PlayerTransform;
    public Rigidbody Rigidbody;
    public GameObject WebFire;
    public LayerMask floorlayer;

    //states

    public FireWebStateSpider fireWebStateSpider;
    public LaserAttackSpider laserAttackSpider;
    public BigWebFireState bigWebFireState;
    public void Start()
    {

        currentState = fireWebStateSpider;
        currentState.EnterState(this);
    }

    void Update()
    {       
        currentState.UpdateState(this);
    }

    public void SwitchState(BaseStates newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }

    public virtual void RotateToPlayer()
    {
        Vector3 dirToPlayer = PlayerTransform.position - transform.position;
        float angleToPlayer = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x) * Mathf.Rad2Deg - 90;
        Quaternion RotateAngles = Quaternion.AngleAxis(angleToPlayer, transform.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, RotateAngles, 5 * Time.deltaTime);
    }
    public Vector3 getDirToPlayer()
    {
        Vector3 dirToPlayer = PlayerTransform.position - transform.position;
        return dirToPlayer;
    }

}
