using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField] private List<GameObject> endList;

    private List<float> lengthList;

    //public float springCoefficient;
    //public float friction;

    private LineRenderer lineRenderer;
    private void Start()
    {
        lengthList = new List<float>();
        for (int i = 0; i < endList.Count; i++)
        {
            var length = (endList[i].transform.position - transform.position).magnitude;
            length = Mathf.Abs(length);
            lengthList.Add(length);
        }

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.positionCount = 2 * endList.Count;
        
    }

    public void Perform(float mass, float springCoefficient, float friction, bool isVisible)
    {
        GetComponent<Rigidbody>().mass = mass;

        GetComponent<MeshRenderer>().enabled = isVisible;
        GetComponent<LineRenderer>().enabled = isVisible;

        for (int i = 0; i < endList.Count; i++)
        {
            lineRenderer.SetPosition(2 * i, transform.position);
            lineRenderer.SetPosition(2 * i + 1, endList[i].transform.position);

            PerformForce(i, springCoefficient, friction);
        }
    }

    private void PerformForce(int index, float springCoefficient, float friction)
    {
        GameObject end = endList[index];

        var f = (lengthList[index] - (end.transform.position - transform.position).magnitude) * springCoefficient;

        f = Mathf.Lerp(f, 0, friction);

        Vector3 direction = (end.transform.position - transform.position).normalized;

        Vector3 force = f * direction;

        GetComponent<Rigidbody>().AddForce(-force);
        end.GetComponent<Rigidbody>().AddForce(force);
    }

    public void AddEnd(GameObject end)
    {
        endList.Add(end);
    }
}
