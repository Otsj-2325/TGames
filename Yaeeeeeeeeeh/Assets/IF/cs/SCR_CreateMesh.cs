using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SCR_CreateMesh���A�^�b�`����̂ɕK�{�ŁA�Ȃ���Ύ����ǉ����Ă����A�g���r���[�g�i�����j
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class SCR_CreateMesh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var mesh = new Mesh();
        var vertex = new Vector3[3];
        
        //���_���W�̌���A���̎��̌��_��transform.position
        vertex[0] = new Vector3(-0.5f, -0.5f);
        vertex[1] = new Vector3(0.0f, 0.5f);
        vertex[2] = new Vector3(0.5f, -0.5f);

        //Mesh�C���X�^���X�ɒ��_�ƃC���f�b�N�X��ݒ�
        mesh.SetVertices(vertex);
        mesh.SetTriangles(new int[] { 0, 1, 2 }, 0);

        //MeshFilter��shredMesh��MeshRenderer�Ɉ����n���B
        var mfilter = this.GetComponent<MeshFilter>();
        mfilter.sharedMesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
