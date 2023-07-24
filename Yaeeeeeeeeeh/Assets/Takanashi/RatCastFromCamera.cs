using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatCastFromCamera : MonoBehaviour
{
    [Header("ÉLÉÖÅ[Éu")]
    [SerializeField] private GameObject cube;

    private MeshFilter mesh_filter;
    private Mesh mesh;

    // Start is called before the first frame update
    void Start()
    {
        mesh_filter = cube.GetComponent<MeshFilter>();
        mesh = mesh_filter.mesh;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 dir = cube.gameObject.transform.position - gameObject.transform.position;
            dir += vertices[i];
            Ray ray = new Ray(gameObject.transform.position, dir);
            Debug.DrawRay(ray.origin, ray.direction * 30, Color.red, 0.1f); // í∑Ç≥ÇRÇOÅAê‘êFÇ≈ÇTïbä‘â¬éãâª
        }
    }
}
