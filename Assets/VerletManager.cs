using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerletManager : MonoBehaviour {

    public GameObject[] nodes;
    Vector3 g = new Vector3(0, 5.8f, 0);
    bool switchDir;

    Vector3 orego1Temp;
    Vector3 orego2Temp;


    private void Start()
    {
        nodes = GameObject.FindGameObjectsWithTag("Player");
        switchDir = false;
        orego1Temp = nodes[0].transform.position;
    }

    private void Update()
    {
        orego1Temp = nodes[0].transform.position;

       if (!switchDir)
       {
            for (int ite = 0; ite < nodes.Length - 1; ++ite)            
                ConstrainObjects(ref nodes[ite], ref nodes[ite + 1], 1.5f, 0.99f, 0.99f);
                         

            switchDir = true;
        } else if (switchDir)
        {
            for ( int ite = nodes.Length; ite < 1; --ite)            
                ConstrainObjects(ref nodes[ite], ref nodes[ite - 1], 1.5f, 0.99f, 0.99f);               

            switchDir = false;
        }
    }

    void ConstrainObjects(ref GameObject p1, ref GameObject p2, float min, float comp1, float comp2)
    {
        Vector3 delta = p2.transform.position - p1.transform.position;
        
        float deltLeng = (float)Mathf.Sqrt(delta.x * delta.x + delta.y * delta.y + delta.z * delta.z);
        delta -= g;

        float diff = (deltLeng - min) / deltLeng;

        if (!p1.GetComponent<Node>().isAnchor)
            p1.transform.position += delta * comp1 * diff;
        if (!p2.GetComponent<Node>().isAnchor)
            p2.transform.position -= delta * comp1 * diff;                              
    }
}
