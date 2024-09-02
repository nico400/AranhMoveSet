using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStates : MonoBehaviour
{
    protected SpiderManager spiderManager;

    public BaseStates(SpiderManager mySpider)
    {
        this.spiderManager = mySpider;
    }

    public abstract void EnterState(SpiderManager _spiderManager);
    public abstract void UpdateState(SpiderManager _spiderManager);
}
