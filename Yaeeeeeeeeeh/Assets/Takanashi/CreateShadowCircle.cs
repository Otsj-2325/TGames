using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class CreateShadowCircle : MonoBehaviour
{
    [Header("���̉����镨")]
    [SerializeField] private GameObject circle;

    private MeshFilter mesh_filter;
    private Mesh mesh;
    //private List<MeshFilter> mesh_filter = new List<MeshFilter>();
    //private List<Mesh> mesh = new List<Mesh>();

    private bool create = true;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("LightingObject");

        //// ���b�V�����擾
        //foreach (GameObject obj in objectsWithTag)
        //{
        //    mesh_filter.Add(obj.GetComponent<MeshFilter>());
        //    mesh.Add(obj.GetComponent<MeshFilter>().mesh);
        //}

        mesh_filter = circle.GetComponent<MeshFilter>();
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

        //GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("LightingObject");
        //List<Vector3[]> vertices = new List<Vector3[]>();
        //List<Vector3[]> vertices2 = new List<Vector3[]>();
        //List<Vector3[]> vertices3 = new List<Vector3[]>();

        //foreach (Mesh temp_mesh in mesh)
        //{
        //    vertices.Add(temp_mesh.vertices);
        //    vertices3.Add(temp_mesh.vertices);

        //    Vector3[] temp_vector3 = new Vector3[temp_mesh.vertices.Length];
        //    for(int i = 0; i < temp_mesh.vertices.Length; i++)
        //    {
        //        foreach (GameObject obj in objectsWithTag)
        //        {
        //            if(obj.GetComponent<MeshFilter>().mesh == temp_mesh)
        //            {
        //                temp_vector3[i] = temp_mesh.vertices[i] + obj.gameObject.transform.position;
        //            }
        //        }
        //    }
        //    vertices2.Add(temp_vector3);
        //}

        Vector3[] vertices = mesh.vertices;
        List<Vector3> vertices3 = new List<Vector3>();
        Vector3[] vertices2 = mesh.vertices;        // �L���[�u�̏ꏊ�Ɗe���_�̍��W�����Z��������
        List<float> point_y = new List<float>();    // Y���W���擾

        // ���_��Y���W���擾
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices3.Add(vertices[i]);

            vertices2[i] = circle.gameObject.transform.position + vertices[i];
            if (!point_y.Contains(vertices2[i].y))
            {
                point_y.Add(vertices2[i].y);
            }
        }

        //===========================================
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
                    Vector3 temp_straight = circle.gameObject.transform.position - gameObject.transform.position;
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
        //===========================================


        HashSet<Vector3> vertex = new HashSet<Vector3>();
        List<float> vertex_x = new List<float>();

        //===========================================
        // ���C�𐶐����ǂɓ��āA�e�̍��W���擾
        for (int i = 0; i < vertices3.Count; i++)
        {
            // ���C�̐���
            Vector3 dir = circle.gameObject.transform.position - gameObject.transform.position;
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
                    vertex_x.Add(hit.point.x);
                }
            }
        }

        // �e�̒������W���擾
        Vector3 center_point = new Vector3();    
        Vector3 dir_center = circle.gameObject.transform.position - gameObject.transform.position;
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
        //===========================================


        Mesh new_mesh = new Mesh();
        Vector3[] point = new Vector3[(vertex.Count + 1) * 2];

        //===========================================
        // X���W���E���猩�āA���בւ���(���W�����v���ɂȂ�悤��)
        int point_up_index = 1;
        int point_down_index = 0;
        bool point_center = false;
        vertex_x.Sort();
        for (int i = vertex_x.Count - 1; i > -1; i--)
        {
            // �O��X���W�Ɠ����l�ł���Ώ��������Ȃ�
            if(i != vertex_x.Count - 1)
            {
                if (vertex_x[i] == vertex_x[i + 1])
                {
                    continue;
                }
            }

            foreach(Vector3 temp in vertex)
            {
                // �����̓_�͂͂���
                if (temp == center_point)
                {
                    point_center = true;
                    continue;
                }

                // �E�����X���W�Ɠ����ł���΍��W��V�����ϐ��ɓ����
                if (vertex_x[i] == temp.x)
                {
                    // �㔼��
                    if (center_point.y <= temp.y)
                    {
                        point[point_up_index++] = temp;
                    }
                    // ������
                    else
                    {
                        point[vertex.Count - point_down_index] = temp;
                        point_down_index++;
                    }
                }
            }
        }
        //===========================================

        point[0] = center_point;
        int vertex_num = 0;

        // �����̍��W�����ɓo�^����Ă���΃J�E���g�͂��̂܂�
        if (point_center)
        {
            vertex_num = vertex.Count;
        }
        // �����̍��W���̃J�E���g������
        else
        {
            vertex_num = vertex.Count + 1;
        }

        // ���̂ɂ���
        for (int i = 0; i < vertex_num; i++)
        {
            point[i + vertex_num] = new Vector3(point[i].x, point[i].y, -0.1f);
        }
        new_mesh.vertices = point;


        //=======================================
        // ���_�C���f�b�N�X�̐���
        int[] triangles = new int[(vertex_num - 1) * 2 * 3 + (vertex_num - 1) * 2 * 3];

        // �\��
        for (int i = 0; i < (vertex_num - 2); i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 2;
            triangles[i * 3 + 2] = i + 1;
        }
        triangles[(vertex_num - 2) * 3] = 0;
        triangles[(vertex_num - 2) * 3 + 1] = 1;
        triangles[(vertex_num - 2) * 3 + 2] = vertex_num - 1;

        // ����
        for (int i = 0; i < (vertex_num - 2); i++)
        {
            triangles[(i + (vertex_num - 1)) * 3] = vertex_num;
            triangles[(i + (vertex_num - 1)) * 3 + 1] = vertex_num + i + 1;
            triangles[(i + (vertex_num - 1)) * 3 + 2] = vertex_num + i + 2;
        }
        triangles[((vertex_num - 1) * 2 - 1) * 3] = vertex_num;
        triangles[((vertex_num - 1) * 2 - 1) * 3 + 1] = vertex_num - 1 + vertex_num;
        triangles[((vertex_num - 1) * 2 - 1) * 3 + 2] = vertex_num + 1;

        // ����
        for (int i = 1; i < (vertex_num - 1); i++)
        {
            int temp_num = ((vertex_num - 1) * 2 - 2) + i * 2;
            triangles[temp_num * 3] = i;
            triangles[temp_num * 3 + 1] = i + 1;
            triangles[temp_num * 3 + 2] = i + vertex_num + 1;

            triangles[(temp_num + 1) * 3] = i;
            triangles[(temp_num + 1) * 3 + 1] = i + vertex_num + 1;
            triangles[(temp_num + 1) * 3 + 2] = i + vertex_num;
        }

        triangles[(((vertex_num - 1) * 2 - 2) + (vertex_num - 1) * 2) * 3] = vertex_num - 1;
        triangles[(((vertex_num - 1) * 2 - 2) + (vertex_num - 1) * 2) * 3 + 1] = 1;
        triangles[(((vertex_num - 1) * 2 - 2) + (vertex_num - 1) * 2) * 3 + 2] = vertex_num + 1;

        triangles[(((vertex_num - 1) * 2 - 2) + (vertex_num - 1) * 2 + 1) * 3] = vertex_num - 1;
        triangles[(((vertex_num - 1) * 2 - 2) + (vertex_num - 1) * 2 + 1) * 3 + 1] = vertex_num + 1;
        triangles[(((vertex_num - 1) * 2 - 2) + (vertex_num - 1) * 2 + 1) * 3 + 2] = vertex_num + vertex_num - 1;

        new_mesh.triangles = triangles;
        //=======================================


        //===========================================
        // ���Mesh��񂩂�̗��̂̐݌v
        // �@�����v�Z���Đݒ�
        new_mesh.RecalculateNormals();

        GameObject newObject = new GameObject("NewMeshObject");

        //MeshFilter��shredMesh��MeshRenderer�Ɉ����n���B
        MeshFilter mfilter = newObject.AddComponent<MeshFilter>();
        mfilter.mesh = new_mesh;

        // MeshRenderer��ǉ����ă}�e���A�����A�^�b�`
        MeshRenderer meshRenderer = newObject.AddComponent<MeshRenderer>();
        MeshCollider meshCollider = newObject.AddComponent<MeshCollider>();
        meshCollider.convex = true;

        newObject.transform.position = Vector3.zero; // �K�v�Ȉʒu�ɕύX����

        newObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
}
