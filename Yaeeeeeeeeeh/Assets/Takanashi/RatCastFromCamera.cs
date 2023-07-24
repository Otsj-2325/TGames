using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class RatCastFromCamera : MonoBehaviour
{
    [Header("キューブ")]
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
        Vector3[] vertices2 = mesh.vertices;        // キューブの場所と各頂点の座標を加算したもの
        List<float> point_y = new List<float>();    // Y座標を取得

        // 頂点のY座標を取得
        for(int i = 0; i < vertices.Length; i++)
        {
            vertices3.Add(vertices[i]);

            vertices2[i] = cube.gameObject.transform.position + vertices[i];
            if (!point_y.Contains(vertices2[i].y))
            {
                point_y.Add(vertices2[i].y);
            }
        }

        // いらない座標を削除(裏面等)
        for(int i = 0; i < point_y.Count; i++)
        {
            float right_inner = 0.0f;   // 最も右側のベクトルの内積の値
            float left_inner = 0.0f;    // 最も左側のベクトルの内積の値
            int right_index = -1;       // 最も右側のベクトルのインデックス番号
            int left_index = -1;        // 最も左側のベクトルのインデックス番号
            for (int j = 0; j < vertices2.Length; j++)
            {
                // 最も右側か左側の座標を求める
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

            // 同じY座標で最も右か左でなければ配列から削除する(その頂点は処理しない)
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
            // レイの生成
            Vector3 dir = cube.gameObject.transform.position - gameObject.transform.position;
            dir += vertices3[i];
            Ray ray = new Ray(gameObject.transform.position, dir);
            Debug.DrawRay(ray.origin, ray.direction * 30, Color.red, 0.1f); // 長さ３０、赤色で５秒間可視化

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                // 衝突先が壁タグであれば
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
                //Debug.Log("index ：" + (i + 1));

            }
            //Debug.Log("index ：" + i);
        }

        if (create)
        {
            new_mesh.SetVertices(vertex);
            new_mesh.SetIndices(indices, MeshTopology.Lines, 0);

            //MeshFilterはshredMeshをMeshRendererに引き渡す。
            var mfilter = this.GetComponent<MeshFilter>();
            mfilter.sharedMesh = new_mesh;

            create = false;
        }
    }
}
