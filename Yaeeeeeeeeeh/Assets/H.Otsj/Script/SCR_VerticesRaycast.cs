using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_VerticesRaycast : MonoBehaviour
{
    MeshFilter m_mFilter;
    Mesh m_mesh;

    // Start is called before the first frame update
    void Start()
    {
        m_mFilter = GetComponent<MeshFilter>();//MeshFilter���擾���Ă�����
        m_mesh = m_mFilter.sharedMesh;//Mesh���擾����

    }

    // Update is called once per frame
    void Update()
    {
        var vertices = m_mesh.vertices.Clone() as Vector3[];//�擾����Mesh�̒��_��������Ă���
        List<Vector3> calcPoint = new List<Vector3>();

        var rot = transform.rotation;

        foreach (var point in vertices)
        {
            var p = point;

            /*�����ŁA�@vertices�@�Ɂ@transform.rotation�@��K�p��������*/
            /*�Ƃ��́A���_���W��transform.rotation���|���邾��*/
            p = rot * p;

            //transform.Scale�̓K�p
            p.x *= transform.localScale.x;
            p.y *= transform.localScale.y;
            p.z *= transform.localScale.z;

            //transform.position�̓K�p
            p += transform.position;

            calcPoint.Add(p);
        }

        int i = 0;
        foreach (var point in calcPoint) {
            Debug.Log( i++ + " : " + point);

            Debug.DrawRay(point, transform.forward, Color.blue);
        }
    }
}
