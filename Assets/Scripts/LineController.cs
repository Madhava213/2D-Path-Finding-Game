using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] private LineRenderer lr;
    public GameObject[] points;
    public Gradient nonPathGradient;
    public Gradient pathGradient;

    public void addPoints(GameObject[] pts){
        lr.positionCount = pts.Length;
        this.points = pts;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i].transform.position);
        }
        if(points != null){
            if(points[0] != null && points[1] != null){
                if(points[0].GetComponent<Node>().path && points[1].GetComponent<Node>().path){
                    lr.colorGradient = pathGradient;
                }
                else{
                    lr.colorGradient = nonPathGradient;
                }
            }
            else{
                lr.colorGradient = nonPathGradient;
            }
        }
        else
        {
            lr.colorGradient = nonPathGradient;
        }
    }
}
