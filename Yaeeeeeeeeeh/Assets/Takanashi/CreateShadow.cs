using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class CreateShadow : MonoBehaviour
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
        // E�L�[����������e�̌`����I�u�W�F�N�g�𐶐�
        if (!Input.GetKeyDown(KeyCode.E))
        {
            return;
        }

        Vector3[] vertices = mesh.vertices;
        List<Vector3> vertices3 = new List<Vector3>();
        Vector3[] vertices2 = mesh.vertices;        // �L���[�u�̏ꏊ�Ɗe���_�̍��W�����Z��������
        List<float> point_y = new List<float>();    // Y���W���擾

        // ���_��Y���W���擾
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices3.Add(vertices[i]);

            vertices2[i] = cube.gameObject.transform.position + vertices[i];
            if (!point_y.Contains(vertices2[i].y))
            {
                point_y.Add(vertices2[i].y);
            }
        }

        // ����Ȃ����W���폜(���ʓ�)
        for (int i = 0; i < point_y.Count; i++)
        {
            List<int> right_max_index = new List<int>();  // �ł��E���̃x�N�g���̃C���f�b�N�X�ԍ�
            List<int> left_max_index = new List<int>();   // �ł������̃x�N�g���̃C���f�b�N�X�ԍ�

            float[] angle = new float[vertices2.Length];
            float right_angle = -1.0f;
            float left_angle = -1.0f;
            for (int j = 0; j < vertices2.Length; j++)
            {
                angle[j] = -1.0f;

                // �ł��E���������̍��W�����߂�(Y���W�͓���)
                if (vertices2[j].y == point_y[i])
                {
                    Vector3 temp_straight = cube.gameObject.transform.position - gameObject.transform.position;
                    Vector3 temp_this = vertices2[j] - gameObject.transform.position;
                    float temp_inner = Vector3.Dot(temp_straight, temp_this);
                    float angleRad = Mathf.Acos(temp_inner / (temp_straight.magnitude * temp_this.magnitude));
                    float angleDeg = angleRad * Mathf.Rad2Deg;
                    angle[j] = angleDeg;

                    temp_straight.y = 0.0f;
                    temp_this.y = 0.0f;
                    Vector3 temp_outer = Vector3.Cross(temp_straight, temp_this);

                    // �_���E���ɂ���
                    if (temp_outer.y > 0)
                    {
                        if (right_max_index.Count == 0)
                        {
                            right_angle = angle[j];
                            right_max_index.Add(j);
                        }

                        if (right_angle < angle[j])
                        {
                            right_angle = angle[j];
                            right_max_index.Clear();
                            right_max_index.Add(j);
                        }
                        else if (right_angle == angle[j])
                        {
                            right_max_index.Add(j);
                        }
                    }
                    // �_�������ɂ���
                    else
                    {
                        if (left_max_index.Count == 0)
                        {
                            left_angle = angle[j];
                            left_max_index.Add(j);
                        }

                        if (left_angle < angle[j])
                        {
                            left_angle = angle[j];
                            left_max_index.Clear();
                            left_max_index.Add(j);
                        }
                        else if (left_angle == angle[j])
                        {
                            left_max_index.Add(j);
                        }
                    }
                }
            }

            // ����Y���W�ōł��E�����łȂ���Δz�񂩂�폜����(���̒��_�͏������Ȃ�)
            for (int j = 0; j < vertices2.Length; j++)
            {
                if (vertices2[j].y == point_y[i])
                {
                    if (!right_max_index.Contains(j) && !left_max_index.Contains(j))
                    {
                        vertices3.Remove(vertices[j]);
                    }
                }
            }
        }

        Mesh new_mesh = new Mesh();
        var vertex = new List<Vector3>();

        // ���C�𐶐����ǂɓ��āA1�e�̍��W���擾
        for (int i = 0; i < vertices3.Count; i++)
        {
            // ���C�̐���
            Vector3 dir = cube.gameObject.transform.position - gameObject.transform.position;
            dir += vertices3[i];
            Ray ray = new Ray(gameObject.transform.position, dir);
            Debug.DrawRay(ray.origin, ray.direction * 30, Color.red, 0.1f); // �����R�O�A�ԐF�łT�b�ԉ���

            RaycastHit[] hits = Physics.RaycastAll(ray);
            foreach (RaycastHit hit in hits)
            {
                // �Փː悪�ǃ^�O�ł���΍��W���擾
                if (hit.collider.CompareTag("Wall"))
                {
                    vertex.Add(new Vector3(hit.point.x, hit.point.y, hit.point.z));
                }
            }
        }

        Vector3 center_point = new Vector3();    // �e�̒������W
        Vector3 dir_center = cube.gameObject.transform.position - gameObject.transform.position;
        Ray ray_center = new Ray(gameObject.transform.position, dir_center);
        RaycastHit[] hits_center = Physics.RaycastAll(ray_center);
        foreach (RaycastHit hit in hits_center)
        {
            // �Փː悪�ǃ^�O�ł���΍��W���擾
            if (hit.collider.CompareTag("Wall"))
            {
                center_point = hit.point;
            }
        }

        List<int> right_index = new List<int>();
        List<int> left_index = new List<int>();
        Vector3[] point = new Vector3[8];
        for (int i = 0; i < vertex.Count; i++)
        {
            // ��
            if (vertex[i].x < center_point.x)
            {
                // ��
                if (vertex[i].y < center_point.y)
                {
                    point[0] = vertex[i];
                }
                // ��
                else
                {
                    point[2] = vertex[i];
                }
            }
            // �E
            else
            {
                // ��
                if (vertex[i].y < center_point.y)
                {
                    point[1] = vertex[i];
                }
                // ��
                else
                {
                    point[3] = vertex[i];
                }
            }
        }

        // ���̂ɂ���
        for (int i = 0; i < 4; i++)
        {
            point[i + 4] = new Vector3(point[i].x, point[i].y, -0.1f);
        }
        new_mesh.vertices = point;

        //// �C���f�b�N�X��ݒ�
        //var indices = new List<int>();
        //for (int i = 0; i < vertex.Count; i++)
        //{
        //    indices.Add(i);

        //    if (i == vertex.Count - 1)
        //    {
        //        indices.Add(0);
        //    }
        //    else
        //    {
        //        indices.Add(i + 1);
        //        //Debug.Log("index �F" + (i + 1));

        //    }
        //    //Debug.Log("index �F" + i);
        //}

        // ���_�C���f�b�N�X��ݒ�i�����̂̏ꍇ�j
        int[] triangles = new int[]
        {
            // �O��
            0, 1, 2, // 1�ڂ̎O�p�`
            1, 3, 2, // 2�ڂ̎O�p�`

            // �w��
            5, 4, 6, // 1�ڂ̎O�p�`
            5, 6, 7, // 2�ڂ̎O�p�`

            // ���
            2, 3, 6, // 1�ڂ̎O�p�`
            3, 7, 6, // 2�ڂ̎O�p�`

            // ���
            0, 4, 1, // 1�ڂ̎O�p�`
            1, 4, 5, // 2�ڂ̎O�p�`

            // ����
            4, 0, 6, // 1�ڂ̎O�p�`
            6, 0, 2, // 2�ڂ̎O�p�`

            // �E��
            1, 5, 3, // 1�ڂ̎O�p�`
            5, 7, 3, // 2�ڂ̎O�p�`
        };
        new_mesh.triangles = triangles;

        // �@�����v�Z���Đݒ�
        new_mesh.RecalculateNormals();

        GameObject newObject = new GameObject("NewMeshObject");

        //MeshFilter��shredMesh��MeshRenderer�Ɉ����n���B
        MeshFilter mfilter = newObject.AddComponent<MeshFilter>();
        mfilter.mesh = new_mesh;

        // MeshRenderer��ǉ����ă}�e���A�����A�^�b�`
        MeshRenderer meshRenderer = newObject.AddComponent<MeshRenderer>();

        newObject.transform.position = Vector3.zero; // �K�v�Ȉʒu�ɕύX����

        BoxCollider boxCol = newObject.AddComponent<BoxCollider>();    // �����蔻��ǉ�

        Rigidbody rb = newObject.AddComponent<Rigidbody>();     // �d�͂�������
        rb.useGravity = true;

        newObject.transform.localScale = new Vector3(1.0f, 1.0f, 0.5f);

        create = false;
    }
}
