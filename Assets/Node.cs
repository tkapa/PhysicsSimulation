using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    public bool isAnchor = false;
    float speed = 0.25f;
    bool isPos = true;
    Vector3 moveTo = new Vector3(3, 0, 0);

    private void Update()
    {
        if (isAnchor)
        {
            if (isPos)
            {
                transform.position = Vector3.MoveTowards(this.transform.position, moveTo, speed);

                if (this.transform.position == moveTo)
                    isPos = false;
            }
            else if (!isPos)
            {
                transform.position = Vector3.MoveTowards(this.transform.position, -moveTo, speed);

                if (this.transform.position == -moveTo)
                    isPos = true;
            }
        }
    }
}
