using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerletManager : MonoBehaviour {

    public GameObject[] nodes;
    public Vector3[] previousPositions;
    Vector3 g = new Vector3(0, -20f, 0);
    bool switchDir;

    Vector3 orego1Temp;
    Vector3 orego2Temp;


    private void Start()
    {
        nodes = GameObject.FindGameObjectsWithTag("Player");
        previousPositions = new Vector3[nodes.Length];

        for (int i = 0; i < nodes.Length; ++i)
            previousPositions[i] = nodes[i].transform.position;

        switchDir = false;
        orego1Temp = nodes[0].transform.position;
    }

    private void FixedUpdate()
    {
        for(int i = 0; i < 10; ++i)
        {
            for (int ite = 0; ite < nodes.Length - 1; ++ite)
                ConstrainObjects(ref nodes[ite], ref nodes[ite + 1], 1.5f, 0.5f, 0.5f);
        }
            


       for(int i = 0; i< nodes.Length; ++i)
        {
            if (!nodes[i].GetComponent<Node>().isAnchor)
            {
                Vector3 temp = nodes[i].transform.position;
                nodes[i].transform.position = 1.99f * nodes[i].transform.position  - previousPositions[i]*0.99f + g * (Time.deltaTime * Time.deltaTime);
                previousPositions[i] = temp;
            }            
        }
    }

    void ConstrainObjects(ref GameObject p1, ref GameObject p2, float min, float comp1, float comp2)
    {
        Vector3 delta = p2.transform.position - p1.transform.position;
        
        float deltLeng = (float)Mathf.Sqrt(delta.x * delta.x + delta.y * delta.y + delta.z * delta.z);

        float diff = (deltLeng - min) / deltLeng;

        if (!p1.GetComponent<Node>().isAnchor)
            p1.transform.position += delta * comp1 * diff;
        if (!p2.GetComponent<Node>().isAnchor)
            p2.transform.position -= delta * comp1 * diff;                              
    }
}
