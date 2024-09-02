using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttackSpider : BaseStates
{
    Vector3 startDir;
    float timeToStart;
    float timeToEnd;
    public LaserAttackSpider(SpiderManager mySpider) : base(mySpider)
    {
        
    }

    public override void EnterState(SpiderManager spd)
    {
        startDir = spd.getDirToPlayer();
        timeToStart = 2;
        timeToEnd = 5;
    }

    public override void UpdateState(SpiderManager spd)
    {
        if (timeToStart > 0)
        {
            RotateStartLaser(startDir);
            timeToStart -= Time.deltaTime;
        }            
        else
        {
            RotateStartLaser(spd.getDirToPlayer());
            timeToEnd -= Time.deltaTime;

            //lazer HitBox
            RaycastHit hit;
            Vector3 hitPoint;
            if (Physics.SphereCast(spd.transform.position, 1, spd.transform.up, out hit))
            {
                hitPoint = hit.point;
                Debug.DrawLine(spd.transform.position, hitPoint, Color.red);
            }


            if (timeToEnd <= 0)
            {
                spd.SwitchState(spd.fireWebStateSpider);
            }
        }
    }
    void RotateStartLaser(Vector3 dir)
    {
        float angleToPlayer = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        Quaternion RotateAngles = Quaternion.AngleAxis(angleToPlayer, transform.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, RotateAngles, 0.75f * Time.deltaTime);
    }

}
