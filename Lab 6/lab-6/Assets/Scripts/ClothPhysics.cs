using System.Collections.Generic;
using UnityEngine;

public class ClothPhysics : MonoBehaviour
{
    [SerializeField] private GameObject targetMesh;

    [SerializeField] private bool freezeEdges = true;
    [SerializeField] private bool isVisible = true;

    [SerializeField] private GameObject massPoint;

    [SerializeField] private float mass = 1f;
    [SerializeField] private int length;
    [SerializeField] private int width;
    private float spacing_x;
    private float spacing_z;

    [SerializeField] private float springCoefficient = 1f;
    [SerializeField] private float friction = 0.1f;

    private Vector3 startPoint;

    private List<GameObject> mpList;

    private void Awake()
    {
        mpList = new List<GameObject>();

        GameObject clothPhysics = new GameObject();
        clothPhysics.name = "ClothPhysics";
        clothPhysics.transform.SetParent(transform);

        float x = targetMesh.GetComponent<MeshRenderer>().bounds.size.x;
        float z = targetMesh.GetComponent<MeshRenderer>().bounds.size.z;

        startPoint = targetMesh.transform.position - Vector3.right * x / 2
            - Vector3.forward * z / 2;

        spacing_x = x / (length - 1);
        spacing_z = z / (width - 1);

        clothPhysics.transform.position = startPoint + Vector3.right * x / 2 + Vector3.forward * z / 2;

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                int index = i * width + j;

                Vector3 position = startPoint + Vector3.right * i * spacing_x + Vector3.forward * j * spacing_z;
                GameObject mp = Instantiate(massPoint, position, Quaternion.identity);
                mp.name = "MassPoint_" + index;
                mp.transform.SetParent(clothPhysics.transform);

                if (freezeEdges && (i == 0 || i == length - 1 || j == 0 || j == width - 1))
                    mp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

                mpList.Add(mp);

                var spring = mp.GetComponent<Spring>();
                if (j > 0)
                    spring.AddEnd(mpList[index - 1]);
                if (i > 0)
                {
                    spring.AddEnd(mpList[index - width]);
                    if (j > 0)
                        spring.AddEnd(mpList[index - width - 1]);
                    if (j < width - 1)
                        spring.AddEnd(mpList[index - width + 1]);
                }

            }
        }
    }

    private void FixedUpdate()
    {
        foreach (GameObject mp in mpList)
        {
            mp.GetComponent<Spring>().Perform(mass, springCoefficient, friction, isVisible);
        }

        targetMesh.GetComponent<MeshFilter>().mesh.vertices = GetCoords();
        targetMesh.GetComponent<MeshFilter>().mesh.RecalculateBounds();
    }

    private Vector3[] GetCoords()
    {
        Vector3[] coords = new Vector3[mpList.Count];
        for (int i = 0; i < mpList.Count; i++)
        {
            Vector3 coord = mpList[i].transform.localPosition;
            coord.x /= targetMesh.transform.localScale.x;
            coord.y /= targetMesh.transform.localScale.y;
            coord.z /= targetMesh.transform.localScale.z;
            coords[i] = coord;
        }
        return coords;
    }
}
