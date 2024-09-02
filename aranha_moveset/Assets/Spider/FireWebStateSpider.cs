using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWebStateSpider : BaseStates
{
    int countWeb = 4;
    float FireRate = 1f;
    public FireWebStateSpider(SpiderManager mySpider) : base(mySpider)
    {
    }

    public override void EnterState(SpiderManager spd)
    {
        countWeb = 4;
        FireRate = 3f; //start with more time to fire
    }
    public override void UpdateState(SpiderManager spd)
    {
        spd.RotateToPlayer();

        
        if (FireRate <= 0 && countWeb > 0)
        {
            fireWeb(spd);
            FireRate = 0.85f;
        }else
            FireRate -= Time.deltaTime;

        if (countWeb <= 0)
        {
            //verify tha variant of spider
            if (spd.currentType == SpiderManager.TypeSpider.SpiderVariantOne)
            {
                spd.SwitchState(spd.laserAttackSpider);
            }
            else if (spd.currentType == SpiderManager.TypeSpider.SpiderVariantTwo)
            {
                spd.SwitchState(spd.bigWebFireState);
            }
        }     
    }
    void fireWeb(SpiderManager spd)
    {
        GameObject web = Instantiate(spd.WebFire, spd.transform.position, Quaternion.identity);
        web.GetComponent<Rigidbody>().velocity = spd.getDirToPlayer().normalized * 10;
        Destroy(web, 5);
        countWeb--;
    }
}
