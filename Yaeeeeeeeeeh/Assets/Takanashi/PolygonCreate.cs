using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonCreate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // ���GameObject���쐬
        GameObject newObject = new GameObject("new_mesh");

        // �V����Mesh���쐬
        Mesh mesh = new Mesh();

        // ���_���W��ݒ�
        Vector3[] vertices = new Vector3[]
        {
            // �O��
            new Vector3(-1f, -1f, 1f),
            new Vector3(1f, -1f, 1f),
            new Vector3(-1f, 1f, 1f),
            new Vector3(1f, 1f, 1f),

            // �w��
            new Vector3(-1f, -1f, -1f),
            new Vector3(1f, -1f, -1f),
            new Vector3(-1f, 1f, -1f),
            new Vector3(1f, 1f, -1f),
        };
        mesh.vertices = vertices;

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
        mesh.triangles = triangles;

        // �@�����v�Z���Đݒ�
        mesh.RecalculateNormals();

        // Mesh��MeshFilter�ɃA�^�b�`
        MeshFilter meshFilter = newObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        // MeshRenderer��ǉ����ă}�e���A�����A�^�b�`
        MeshRenderer meshRenderer = newObject.AddComponent<MeshRenderer>();
        // �����œK�؂ȃ}�e���A�����A�^�b�`����

        // �V�����I�u�W�F�N�g���V�[���ɒǉ�
        newObject.transform.position = Vector3.zero; // �K�v�Ȉʒu�ɕύX����
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
