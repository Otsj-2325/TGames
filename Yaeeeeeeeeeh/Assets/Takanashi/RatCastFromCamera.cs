using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class RatCastFromCamera : MonoBehaviour
{
    [Header("�L���[�u")]
    [SerializeField] private GameObject cube;

    private MeshFilter mesh_filter;
    private Mesh mesh;

    private bool create = true;

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
        List<Vector3> vertices3 = new List<Vector3>();
        Vector3[] vertices2 = mesh.vertices;        // �L���[�u�̏ꏊ�Ɗe���_�̍��W�����Z��������
        List<float> point_y = new List<float>();    // Y���W���擾

        // ���_��Y���W���擾
        for(int i = 0; i < vertices.Length; i++)
        {
            vertices3.Add(vertices[i]);

            vertices2[i] = cube.gameObject.transform.position + vertices[i];
            if (!point_y.Contains(vertices2[i].y))
            {
                point_y.Add(vertices2[i].y);
            }
        }

        // ����Ȃ����W���폜(���ʓ�)
        for(int i = 0; i < point_y.Count; i++)
        {
            float right_inner = 0.0f;   // �ł��E���̃x�N�g���̓��ς̒l
            float left_inner = 0.0f;    // �ł������̃x�N�g���̓��ς̒l
            int right_index = -1;       // �ł��E���̃x�N�g���̃C���f�b�N�X�ԍ�
            int left_index = -1;        // �ł������̃x�N�g���̃C���f�b�N�X�ԍ�
            for (int j = 0; j < vertices2.Length; j++)
            {
                // �ł��E���������̍��W�����߂�
                if(vertices2[j].y == point_y[i])
                {
                    Vector3 temp_straight = cube.gameObject.transform.position - gameObject.transform.position;
                    Vector3 temp_this = vertices2[j] - gameObject.transform.position;
                    float temp_inner = Vector3.Dot(temp_straight, temp_this);
                    if(temp_inner > 0 && right_inner < temp_inner)
                    {
                        right_inner = temp_inner;
                        right_index = j;
                    }
                    else if(temp_inner < 0 && left_inner > temp_inner)
                    {
                        left_inner = temp_inner;
                        left_index = j;
                    }
                }
            }

            // ����Y���W�ōł��E�����łȂ���Δz�񂩂�폜����(���̒��_�͏������Ȃ�)
            for (int j = 0; j < vertices2.Length; j++)
            {
                if (vertices2[j].y == point_y[i])
                {
                    if(j != right_index && j != left_index)
                    {
                        vertices3.Remove(vertices[i]);
                    }
                }
            }
        }

        var new_mesh = new Mesh();
        var vertex = new List<Vector3>();

        for (int i = 0; i < vertices3.Count; i++)
        {
            // ���C�̐���
            Vector3 dir = cube.gameObject.transform.position - gameObject.transform.position;
            dir += vertices3[i];
            Ray ray = new Ray(gameObject.transform.position, dir);
            Debug.DrawRay(ray.origin, ray.direction * 30, Color.red, 0.1f); // �����R�O�A�ԐF�łT�b�ԉ���

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                // �Փː悪�ǃ^�O�ł����
                if (hit.collider.CompareTag("Wall"))
                {
                    Vector3 hit_point = hit.point;
                                       
                    vertex.Add(new Vector3(hit_point.x, hit_point.y));
                }
            }
        }

        var indices = new List<int>();
        for (int i = 0; i < vertex.Count; i++)
        {
            indices.Add(i);

            if (i == vertex.Count - 1)
            {
                indices.Add(0);
            }
            else
            {
                indices.Add(i + 1);
                //Debug.Log("index �F" + (i + 1));

            }
            //Debug.Log("index �F" + i);
        }

        if (create)
        {
            new_mesh.SetVertices(vertex);
            new_mesh.SetIndices(indices, MeshTopology.Lines, 0);

            //MeshFilter��shredMesh��MeshRenderer�Ɉ����n���B
            var mfilter = this.GetComponent<MeshFilter>();
            mfilter.sharedMesh = new_mesh;

            create = false;
        }
    }
}
