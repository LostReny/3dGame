using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LostWordls.Singleton;

public class CheckPointManager : Singleton<CheckPointManager>
{
    public int lastCheckPoint = 0;

    public List<CheckPointBase> checkPoints;

    public bool HasCheckPoint()
    {
        return lastCheckPoint > 0;
    }

    public void SaveCheckPoint(int i)
    {
        if(i > lastCheckPoint)
        {
            lastCheckPoint  = i;
        }
    }

    public Vector3 GetPositionFromLastCheckPoint()
    {
        var checkPoint = checkPoints.Find( i => i.Key == lastCheckPoint);
        return checkPoint.transform.position;
    }
}
