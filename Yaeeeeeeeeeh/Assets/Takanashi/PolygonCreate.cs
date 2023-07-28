using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonCreate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 空のGameObjectを作成
        GameObject newObject = new GameObject("new_mesh");

        // 新しいMeshを作成
        Mesh mesh = new Mesh();

        // 頂点座標を設定
        Vector3[] vertices = new Vector3[]
        {
            // 前面
            new Vector3(-1f, -1f, 1f),
            new Vector3(1f, -1f, 1f),
            new Vector3(-1f, 1f, 1f),
            new Vector3(1f, 1f, 1f),

            // 背面
            new Vector3(-1f, -1f, -1f),
            new Vector3(1f, -1f, -1f),
            new Vector3(-1f, 1f, -1f),
            new Vector3(1f, 1f, -1f),
        };
        mesh.vertices = vertices;

        // 頂点インデックスを設定（立方体の場合）
        int[] triangles = new int[]
        {
            // 前面
            0, 1, 2, // 1つ目の三角形
            1, 3, 2, // 2つ目の三角形

            // 背面
            5, 4, 6, // 1つ目の三角形
            5, 6, 7, // 2つ目の三角形

            // 上面
            2, 3, 6, // 1つ目の三角形
            3, 7, 6, // 2つ目の三角形

            // 底面
            0, 4, 1, // 1つ目の三角形
            1, 4, 5, // 2つ目の三角形

            // 左面
            4, 0, 6, // 1つ目の三角形
            6, 0, 2, // 2つ目の三角形

            // 右面
            1, 5, 3, // 1つ目の三角形
            5, 7, 3, // 2つ目の三角形
        };
        mesh.triangles = triangles;

        // 法線を計算して設定
        mesh.RecalculateNormals();

        // MeshをMeshFilterにアタッチ
        MeshFilter meshFilter = newObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        // MeshRendererを追加してマテリアルをアタッチ
        MeshRenderer meshRenderer = newObject.AddComponent<MeshRenderer>();
        // ここで適切なマテリアルをアタッチする

        // 新しいオブジェクトをシーンに追加
        newObject.transform.position = Vector3.zero; // 必要な位置に変更する
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
