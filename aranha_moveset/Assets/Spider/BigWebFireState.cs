using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWebFireState : BaseStates
{
    GameObject BigWeb;
    float timesFollow;
    float timeToLoadSize;
    public BigWebFireState(SpiderManager mySpider) : base(mySpider)
    {
        
    }

    public override void EnterState(SpiderManager spd)
    {
        timesFollow = 4;
        timeToLoadSize = 6;

        BigWeb = Instantiate(spd.WebFire, spd.transform.up, Quaternion.identity);
        BigWeb.transform.localScale = Vector3.zero;
    }

    public override void UpdateState(SpiderManager spd)
    {
        if (timeToLoadSize > 0)
        {
            Vector3 dir = spd.getDirToPlayer();
            RotateToPlayer(dir);

            BigWeb.transform.position = spd.transform.position + spd.transform.up * 1.3f;
            BigWeb.transform.localScale = Vector3.Slerp(BigWeb.transform.localScale, Vector3.one * 2, 0.3f * Time.deltaTime);
            timeToLoadSize -= Time.deltaTime;
        }else
        {
            if (timesFollow > 0)
            {
                Vector3 dir = spd.getDirToPlayer();
                BigWeb.GetComponent<Rigidbody>().velocity = dir.normalized * 4f;

                timesFollow -= Time.deltaTime;
            }
            if (Physics.CheckSphere(BigWeb.transform.position, 0.4f, spd.floorlayer))
            {
                BigWeb.GetComponent<Rigidbody>().velocity = Vector3.zero;
                StartCoroutine(waitTime(spd));
            }
        }

    }
    void RotateToPlayer(Vector3 dir)
    {
        float angleToPlayer = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        Quaternion RotateAngles = Quaternion.AngleAxis(angleToPlayer, transform.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, RotateAngles, 0.75f * Time.deltaTime);
    }

    IEnumerator waitTime(SpiderManager spd)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(BigWeb, 2);
        spd.SwitchState(spd.fireWebStateSpider);
    }
}
