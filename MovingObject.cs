using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public Vector3[] posList;

    public Vector3[] rotList;

    public int[] speeds;

    public Vector3 basePos;

    public float lerp;

    public int step;

    public bool run;

    public bool stopAtPoint;

    public bool baseAtStart;

    public bool teleport;

    // Start is called before the first frame update
    void Start()
    {
        if(baseAtStart)
        {
            basePos = transform.position;
        }
        for(int i=0; i < posList.Length; i++)
        {
            posList[i] = posList[i]+basePos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (run)
        {
            //modifies lerp value relative to distance between points a and b
            if (step == 0)
            {
                lerp += (speeds[speeds.Length-1] / Vector3.Distance(posList[posList.Length - 1], posList[step])) * Time.deltaTime;
            }
            else
            {
                lerp += (speeds[step - 1] / Vector3.Distance(posList[step - 1], posList[step])) * Time.deltaTime;
            }

            //triggers when object gets to point b
            if (lerp > 1)
            {
                if (stopAtPoint)
                {
                    run = false;
                }
                transform.position = posList[step];
                transform.eulerAngles = rotList[step];
                lerp -= 1;
                step += 1;
                if (step > posList.Length - 1)
                {
                    step = 0;
                }
            }

            //sets position based on lerp value
            if (step == 0 && run)
            {
                var a = posList[posList.Length - 1];
                var b = posList[step];
                var rota = rotList[posList.Length - 1];
                var rotb = rotList[step];
                transform.position = Vector3.Lerp(a,b,lerp);
                transform.eulerAngles = Vector3.Lerp(rota,rotb,lerp);
            }
            else if (run)
            {
                var a = posList[step - 1];
                var b = posList[step];
                var rota = rotList[step - 1];
                var rotb = rotList[step];
                transform.position = Vector3.Lerp(a,b,lerp);
                transform.eulerAngles = Vector3.Lerp(rota,rotb,lerp);
            }
        }
    }
}
