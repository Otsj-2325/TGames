using UnityEngine;

public class SCR_CameraViewMesh : MonoBehaviour
{
    // ��������|���S���̃}�e���A��
    public Material polygonMaterial;

    // �J�����̎Q��
    private Camera mainCamera;

    void Start()
    {
        // �J�����ւ̎Q�Ƃ��擾
        mainCamera = Camera.main;
    }

    void Update()
    {
        // �J�����̃r���[�|�[�g�̒��S���擾
        Vector3 viewportCenter = new Vector3(0.5f, 0.5f, mainCamera.nearClipPlane);

        // �J�����̃r���[�|�[�g�̒��S��Ray���΂�
        Ray ray = mainCamera.ViewportPointToRay(viewportCenter);

        // Ray���Փ˂���ʒu�����߂�
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // �q�b�g�����ʒu���擾
            Vector3 hitPoint = hit.point;

            // �J��������q�b�g�ʒu�Ɍ������x�N�g�������߂�
            Vector3 toHitPoint = hitPoint - mainCamera.transform.position;

            // �q�b�g�ʒu���班�����Ɉړ������鋗���i�K�X�������Ă��������j
            float offsetDistance = 10.0f;

            // �q�b�g�ʒu���班�����Ɉړ�������
            Vector3 newPosition = hitPoint + toHitPoint.normalized * offsetDistance;

            // �V�����|���S���𐶐�
            GameObject newPolygon = new GameObject("GeneratedPolygon");
            newPolygon.transform.position = newPosition;
            newPolygon.transform.rotation = mainCamera.transform.rotation;

            // �V�����|���S����MeshFilter��MeshRenderer��ǉ�
            MeshFilter meshFilter = newPolygon.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = newPolygon.AddComponent<MeshRenderer>();

            // �V�����|���S���̃��b�V�����쐬
            Mesh mesh = new Mesh();

            // ���b�V���̒��_����ݒ�i�K�X���_����ݒ肵�Ă��������j
            Vector3[] vertices = new Vector3[]
            {
                new Vector3(-0.5f, -0.5f, 0f),
                new Vector3(0.5f, -0.5f, 0f),
                new Vector3(0.5f, 0.5f, 0f),
                new Vector3(-0.5f, 0.5f, 0f)
            };
            mesh.vertices = vertices;

            // ���b�V���̃|���S����ݒ�i�K�X�|���S������ݒ肵�Ă��������j
            int[] triangles = new int[]
            {
                0, 1, 2,
                2, 3, 0
            };
            mesh.triangles = triangles;

            // ���b�V����ݒ�
            meshFilter.mesh = mesh;

            // �}�e���A����ݒ�
            meshRenderer.material = polygonMaterial;
        }
    }
}
