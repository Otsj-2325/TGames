using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class PlayerCreateShadowMulti : MonoBehaviour
{
    [Header("���̉����鍶�g")]
    [SerializeField] private GameObject playerFlameLeft;
    [Header("���̉�����E�g")]
    [SerializeField] private GameObject playerFlameRight;
    [Header("���̉������g")]
    [SerializeField] private GameObject playerFlameUp;
    [Header("���̉����鉺�g")]
    [SerializeField] private GameObject playerFlameDown;

    // �e�Ɠ��������g�̎��
    private enum CollitedPlayerFlame
    {
        Left,
        Down,
        Right,
        Up,
        None
    }

    // �Q�[���I�u�W�F�N�g�̏���ۑ�����N���X
    private class LightingGameObject
    {
        private GameObject m_gameObj;
        private Mesh m_mesh;
        private List<Vector3> m_gameObjPoint;
        private HashSet<Vector3> m_vertex;
        private List<float> m_vertexX;
        private Vector3 m_centerPos;
        private Vector3[] m_point;
        private List<int> m_inFlameIndex;
        private int m_vertexNum;

        public LightingGameObject(GameObject gameObj, List<Vector3> gameObjPoint)
        {
            m_gameObj = gameObj;
            m_mesh = m_gameObj.GetComponent<MeshFilter>().mesh;
            m_gameObjPoint = gameObjPoint;
        }

        public void Reset()
        {
            m_vertex.Clear();
            m_vertexX.Clear();
            m_point.Initialize();
            m_inFlameIndex.Clear();
        }

        public GameObject GetGameObject()
        {
            return m_gameObj;
        }

        public Mesh GetMesh()
        {
            return m_mesh;
        }

        public List<Vector3> GetGameObjPoint()
        {
            return m_gameObjPoint;
        }

        public HashSet<Vector3> GetVertex()
        {
            return m_vertex;
        }

        public List<float> GetVertexX()
        {
            return m_vertexX;
        }

        public Vector3 GetCenterPos()
        {
            return m_centerPos;
        }

        public Vector3[] GetPoint()
        {
            return m_point;
        }

        public List<int> GetInFlameIndex()
        {
            return m_inFlameIndex;
        }

        public int GetVertexNum()
        {
            return m_vertexNum;
        }

        public void SetVertex(HashSet<Vector3> vertex)
        {
            m_vertex = vertex;
        }

        public void SetVertexX(List<float> vertexX)
        {
            m_vertexX = vertexX;
        }

        public void SetCenterPos(Vector3 centerPos)
        {
            m_centerPos = centerPos;
        }

        public void SetPoint(Vector3[] point)
        {
            m_point = point;
        }

        public void SetInFlameIndex(List<int> inFlameIndex)
        {
            m_inFlameIndex = inFlameIndex;
        }

        public void SetVertexNum(int vertexNum)
        {
            m_vertexNum = vertexNum;
        }
    }
    private List<LightingGameObject> lightingGameObj = new List<LightingGameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] gameObjAll = GameObject.FindGameObjectsWithTag("LightingObject");

        // ����Ȃ��_���폜
        foreach(GameObject temp in gameObjAll)
        {
            List<Vector3> tempGameObjPoint = DeletePoint(temp.gameObject.GetComponent<MeshFilter>().mesh, temp);
            lightingGameObj.Add(new LightingGameObject(temp, tempGameObjPoint));            
        }
    }

    // Update is called once per frame
    void Update()
    {
        // E�L�[����������e�̌`����I�u�W�F�N�g�𐶐�
        //if (!Input.GetKeyDown(KeyCode.E)) return;

        List<int> index = new List<int>();      // �g���̃Q�[���I�u�W�F�N�g�̃C���f�b�N�X�ԍ�

        // ���C�𐶐����g���̉e�̂ݓ����
        for(int i = 0; i < lightingGameObj.Count; i++)
        {
            List<Vector3> tempGameObjPoint = lightingGameObj[i].GetGameObjPoint();
            bool InFlame = false;
            HashSet<Vector3> tempVertex = new HashSet<Vector3>();
            List<float> tempVertexX = new List<float>();

            Vector3 dir = lightingGameObj[i].GetGameObject().gameObject.transform.position - gameObject.transform.position;

            for (int j = 0; j < tempGameObjPoint.Count; j++)
            {
                // ���C�̐���
                dir += tempGameObjPoint[j];
                Ray ray = new Ray(gameObject.transform.position, dir);
                Debug.DrawRay(ray.origin, ray.direction * 30, Color.red, 0.1f); // �����R�O�A�ԐF�łT�b�ԉ���

                RaycastHit[] hits = Physics.RaycastAll(ray);
                foreach (RaycastHit hit in hits)
                {
                    // �Փː悪�ǃ^�O�łȂ���Ώ������Ȃ�
                    if (!hit.collider.CompareTag("Wall")) continue;

                    // �g���ɂ��邩
                    if (!InFlame)
                    {
                        float flameUp = playerFlameLeft.transform.position.y + playerFlameLeft.transform.localScale.y / 2;
                        float flameDown = playerFlameLeft.transform.position.y - playerFlameLeft.transform.localScale.y / 2;
                        if (playerFlameLeft.transform.position.x < hit.point.x &&
                            playerFlameRight.transform.position.x > hit.point.x &&
                            flameUp > hit.point.y && flameDown < hit.point.y)
                            InFlame = true;
                    }

                    tempVertex.Add(new Vector3(hit.point.x, hit.point.y, hit.point.z));
                    tempVertexX.Add(hit.point.x);
                }

                dir -= tempGameObjPoint[j];
            }

            // ��x�ł��g���ɓ_�������Ă�����
            if (InFlame)
            {
                lightingGameObj[i].SetVertex(tempVertex);
                lightingGameObj[i].SetVertexX(tempVertexX);
                index.Add(i);
            }
        }

        // E�L�[����������e�̌`����I�u�W�F�N�g�𐶐�
        if (!Input.GetKeyDown(KeyCode.E)) return;

        // �e�̒������W���擾
        foreach(int tempIndex in index)
        {
            Vector3 tempCenterPos = GetShadowCenterPosition(lightingGameObj[tempIndex].GetGameObject());
            lightingGameObj[tempIndex].SetCenterPos(tempCenterPos);

            HashSet<Vector3> tempVertex = lightingGameObj[tempIndex].GetVertex();
            Vector3[] tempPoint = new Vector3[tempVertex.Count + 1];
            lightingGameObj[tempIndex].SetPoint(SortPointX(tempPoint, lightingGameObj[tempIndex].GetVertexX(),
                ref tempVertex, ref tempCenterPos));

            lightingGameObj[tempIndex].SetInFlameIndex(GetInFlameIndex(ref tempPoint));
        }

        //===========================================

        Mesh new_mesh = new Mesh();

        // �g���ɂ��邩�ǂ����𔻒f���A�V�����ϐ���ۑ�
        List<Vector3> tempPointInFlameAll = new List<Vector3>();

        foreach(int tempIndex in index)
        {
            Vector3 tempCenterPoint = lightingGameObj[tempIndex].GetCenterPos();
            Vector3[] pointInFlame = CreateInFlameShadow(lightingGameObj[tempIndex].GetInFlameIndex(), 
                lightingGameObj[tempIndex].GetPoint(), ref tempCenterPoint);
            foreach (Vector3 vec in pointInFlame)
            {
                tempPointInFlameAll.Add(vec);
            }
            lightingGameObj[tempIndex].SetVertexNum(pointInFlame.Length / 2);
        }

        Vector3[] pointInFlameAll = tempPointInFlameAll.ToArray();

        new_mesh.vertices = pointInFlameAll;


        //=======================================
        // ���_�C���f�b�N�X�̐���
        List<int> tempTrianglesAll = new List<int>();

        int firstIndex = 0;

        foreach(int tempIndex in index)
        {
            int tempVertexNum = lightingGameObj[tempIndex].GetVertexNum();
            int[] triangles = GetTriangles(tempVertexNum);
            foreach (int temp in triangles)
            {
                tempTrianglesAll.Add(temp + firstIndex);
            }
            firstIndex += tempVertexNum;
        }

        int[] trianglesAll = tempTrianglesAll.ToArray();
        new_mesh.triangles = trianglesAll;
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

        // ����g�����f�[�^�̃��Z�b�g
        foreach(int tempIndex in index)
        {
            lightingGameObj[tempIndex].Reset();
        }
    }

    // ����Ȃ��_���폜
    private List<Vector3> DeletePoint(Mesh objMesh, GameObject gameObj)
    {
        Vector3[] vertices = objMesh.vertices;
        List<Vector3> vertices3 = new List<Vector3>();
        Vector3[] vertices2 = objMesh.vertices;        // �L���[�u�̏ꏊ�Ɗe���_�̍��W�����Z��������
        List<float> point_y = new List<float>();    // Y���W���擾

        //���_��Y���W���擾
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices3.Add(vertices[i]);

            vertices2[i] = gameObj.gameObject.transform.position + vertices[i];
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

            float angle = 0.0f;
            float right_angle = -1.0f;
            float left_angle = -1.0f;
            for (int j = 0; j < vertices2.Length; j++)
            {
                angle = -1.0f;

                // �ł��E���������̍��W�����߂�(Y���W�͓���)
                if (vertices2[j].y == point_y[i])
                {
                    Vector3 temp_straight = gameObj.gameObject.transform.position - gameObject.transform.position;
                    Vector3 temp_this = vertices2[j] - gameObject.transform.position;
                    float temp_inner = Vector3.Dot(temp_straight, temp_this);
                    float angleRad = Mathf.Acos(temp_inner / (temp_straight.magnitude * temp_this.magnitude));
                    float angleDeg = angleRad * Mathf.Rad2Deg;
                    angle = angleDeg;

                    temp_straight.y = 0.0f;
                    temp_this.y = 0.0f;
                    Vector3 temp_outer = Vector3.Cross(temp_straight, temp_this);

                    // �_���E���ɂ���
                    if (temp_outer.y > 0)
                    {
                        if (right_max_index.Count == 0)
                        {
                            right_angle = angle;
                            right_max_index.Add(j);
                        }

                        if (right_angle < angle)
                        {
                            right_angle = angle;
                            right_max_index.Clear();
                            right_max_index.Add(j);
                        }
                        else if (right_angle == angle)
                        {
                            right_max_index.Add(j);
                        }
                    }
                    // �_�������ɂ���
                    else
                    {
                        if (left_max_index.Count == 0)
                        {
                            left_angle = angle;
                            left_max_index.Add(j);
                        }

                        if (left_angle < angle)
                        {
                            left_angle = angle;
                            left_max_index.Clear();
                            left_max_index.Add(j);
                        }
                        else if (left_angle == angle)
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

        return vertices3;
    }

    // �e�̒������W���擾
    private Vector3 GetShadowCenterPosition(GameObject gameObj)
    {
        Vector3 dir_center = gameObj.gameObject.transform.position - gameObject.transform.position;
        Ray ray_center = new Ray(gameObject.transform.position, dir_center);
        RaycastHit[] hits_center = Physics.RaycastAll(ray_center);
        foreach (RaycastHit hit in hits_center)
        {
            // �Փː悪�ǃ^�O�ł���΍��W���擾
            if (!hit.collider.CompareTag("Wall")) continue;

            return hit.point;
        }

        return Vector3.zero;
    }

    // ���v���ɕ��ёւ���
    private Vector3[] SortPointX(Vector3[] point, List<float> vertex_x, ref HashSet<Vector3> vertex, ref Vector3 centerPos)
    {
        for (int i = 0; i < point.Length; i++)
        {
            point[i] = Vector3.zero;
        }

        //===========================================
        // X���W���E���猩�āA���בւ���(���W�����v���ɂȂ�悤��)
        int point_up_index = 1;
        int point_down_index = 0;
        vertex_x.Sort();
        for (int i = vertex_x.Count - 1; i > -1; i--)
        {
            // �O��X���W�Ɠ����l�ł���Ώ��������Ȃ�
            if (i != vertex_x.Count - 1)
            {
                if (vertex_x[i] == vertex_x[i + 1])
                {
                    continue;
                }
            }

            foreach (Vector3 temp in vertex)
            {
                // �����̓_�͂͂���
                if (temp == centerPos) continue;

                // �E�����X���W�Ɠ����ł���΍��W��V�����ϐ��ɓ����
                if (vertex_x[i] == temp.x)
                {
                    // �㔼��
                    if (centerPos.y <= temp.y)
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

        return point;
    }

    // �g���ɓ����Ă���_�̔ԍ���Ԃ�
    private List<int> GetInFlameIndex(ref Vector3[] point)
    {
        // �v���C���[�̘g���ɓ����Ă邩�ǂ���
        List<int> inFlameIndex = new List<int>();   // �g���ɓ����Ă�_�̃C���f�b�N�X�ԍ��L�^
        for (int i = 1; i < point.Length; i++)
        {
            // ������Ԃł����āA�g�̃Q�[���I�u�W�F�N�g���Ȃ���Ώ������Ȃ�
            if (point[i] == Vector3.zero ||
                playerFlameLeft == null || playerFlameRight == null) continue;

            // �g���ɂ��邩
            float flameUp = playerFlameLeft.transform.position.y + playerFlameLeft.transform.localScale.y / 2;
            float flameDown = playerFlameLeft.transform.position.y - playerFlameLeft.transform.localScale.y / 2;
            if (playerFlameLeft.transform.position.x > point[i].x ||
                playerFlameRight.transform.position.x < point[i].x ||
                flameUp < point[i].y || flameDown > point[i].y)
                continue;

            inFlameIndex.Add(i);
        }

        return inFlameIndex;
    }

    // �g���ɂ���e�̍��W���擾
    private Vector3[] CreateInFlameShadow(List<int>inFlameIndex, Vector3[] point, ref Vector3 centerPos)
    {
        int pointNum = 0;           // �V�����_��ۑ�����ϐ��̐�
        bool inFlameAll = false;    // �g���ɑS�Ă��邩�ǂ���

        int upPointIndex = -1;          // ���v�������đO�̓_���g�O�Ŏ��̓_���g���̂���
        int upPointBeforeIndex = -1;    // ���v�������đO�̓_���g�O�Ŏ��̓_���g���̑O�̓_�̔ԍ�
        int downPointIndex = -1;        // ���v�������đO�̓_���g���Ŏ��̓_���g�O�̂���
        int downPointNextIndex = -1;    // ���v�������đO�̓_���g���Ŏ��̓_���g�O�̎��̓_�̔ԍ�

        // ��̕ϐ��̂��ꂼ��̏Փ˂����g�̎��
        CollitedPlayerFlame upPointCollitedPlayerFlame = CollitedPlayerFlame.None;
        CollitedPlayerFlame downPointCollitedPlayerFlame = CollitedPlayerFlame.None;

        // �S�Ęg���ɂ���Ȃ�
        if (inFlameIndex.Count == point.Length - 1)
        {
            pointNum = point.Length;
            inFlameAll = true;
        }
        else
        {
            for (int i = 1; i < point.Length; i++)
            {
                if (inFlameIndex.IndexOf(i) == -1) continue;

                // ���v�������đO�̓_���g�O�Ŏ��̓_���g���̂���
                if (i == 1 && upPointIndex == -1)
                {
                    if (inFlameIndex.IndexOf(point.Length - 1) != -1) continue;

                    upPointIndex = i;
                    upPointBeforeIndex = point.Length - 1;
                    break;
                }
                else if (upPointIndex == -1)
                {
                    if (inFlameIndex.IndexOf(i - 1) != -1) continue;

                    upPointIndex = i;
                    upPointBeforeIndex = i - 1;
                    break;
                }
            }

            for (int i = 1; i < point.Length; i++)
            {
                if (inFlameIndex.IndexOf(i) == -1) continue;

                // ���v�������đO�̓_���g���Ŏ��̓_���g�O�̂���
                if (i == point.Length - 1 && downPointIndex == -1)
                {
                    if (inFlameIndex.IndexOf(1) != -1) continue;

                    downPointIndex = i;
                    downPointNextIndex = 1;
                    break;
                }
                else if (downPointIndex == -1)
                {
                    if (inFlameIndex.IndexOf(i + 1) != -1) continue;

                    downPointIndex = i;
                    downPointNextIndex = i + 1;
                    break;
                }

            }

            // ���C��ł�
            // ���v�������đO�̓_���g�O�Ŏ��̓_���g���̂���
            Vector3 dir = point[upPointBeforeIndex] - point[upPointIndex];
            Ray ray = new Ray(point[upPointIndex], dir);
            Debug.DrawRay(ray.origin, ray.direction * 30, Color.red, 0.1f); // �����R�O�A�ԐF�łT�b�ԉ���
            RaycastHit[] hits = Physics.RaycastAll(ray);
            foreach (RaycastHit hit in hits)
            {
                // �Փ˂����g�̎�ނ�ۑ�
                if (hit.transform == playerFlameLeft)
                    upPointCollitedPlayerFlame = CollitedPlayerFlame.Left;
                else if (hit.transform == playerFlameRight)
                    upPointCollitedPlayerFlame = CollitedPlayerFlame.Right;
                else if (hit.transform == playerFlameUp)
                    upPointCollitedPlayerFlame = CollitedPlayerFlame.Up;
                else if (hit.transform == playerFlameDown)
                    upPointCollitedPlayerFlame = CollitedPlayerFlame.Down;
                else
                    continue;    // �Փː悪�v���C���[�̘g�^�O�łȂ���Ώ������Ȃ�

                point[upPointBeforeIndex] = hit.point;
                inFlameIndex.Add(upPointBeforeIndex);
                break;
            }

            // ���v�������đO�̓_���g���Ŏ��̓_���g�O�̂���
            dir = point[downPointNextIndex] - point[downPointIndex];
            ray = new Ray(point[downPointIndex], dir);
            Debug.DrawRay(ray.origin, ray.direction * 30, Color.red, 0.1f); // �����R�O�A�ԐF�łT�b�ԉ���
            hits = Physics.RaycastAll(ray);
            foreach (RaycastHit hit in hits)
            {
                // �Փ˂����g�̎�ނ�ۑ�
                if (hit.transform == playerFlameLeft)
                    downPointCollitedPlayerFlame = CollitedPlayerFlame.Left;
                else if (hit.transform == playerFlameRight)
                    downPointCollitedPlayerFlame = CollitedPlayerFlame.Right;
                else if (hit.transform == playerFlameUp)
                    downPointCollitedPlayerFlame = CollitedPlayerFlame.Up;
                else if (hit.transform == playerFlameDown)
                    downPointCollitedPlayerFlame = CollitedPlayerFlame.Down;
                else
                    continue;    // �Փː悪�v���C���[�̘g�^�O�łȂ���Ώ������Ȃ�

                point[downPointNextIndex] = hit.point;
                inFlameIndex.Add(downPointNextIndex);
                break;
            }

            //========================================================
            // �g�̊p���V�����}�`�ɓ����Ă���Ίp��ϐ��ɓ����
            Vector3 cornerPos = Vector3.zero;   // �g�̊p
            // ����
            if (upPointCollitedPlayerFlame == CollitedPlayerFlame.Down &&
                downPointCollitedPlayerFlame == CollitedPlayerFlame.Left)
            {
                cornerPos = playerFlameLeft.transform.position;
                cornerPos.y = playerFlameLeft.transform.position.y - playerFlameLeft.transform.localScale.y / 2;
            }
            // �E��
            else if (upPointCollitedPlayerFlame == CollitedPlayerFlame.Right &&
                     downPointCollitedPlayerFlame == CollitedPlayerFlame.Down)
            {
                cornerPos = playerFlameRight.transform.position;
                cornerPos.y = playerFlameRight.transform.position.y - playerFlameRight.transform.localScale.y / 2;
            }
            // �E��
            else if (upPointCollitedPlayerFlame == CollitedPlayerFlame.Up &&
                     downPointCollitedPlayerFlame == CollitedPlayerFlame.Right)
            {
                cornerPos = playerFlameRight.transform.position;
                cornerPos.y = playerFlameRight.transform.position.y + playerFlameRight.transform.localScale.y / 2;
            }
            // ����
            else if (upPointCollitedPlayerFlame == CollitedPlayerFlame.Left &&
                     downPointCollitedPlayerFlame == CollitedPlayerFlame.Up)
            {
                cornerPos = playerFlameLeft.transform.position;
                cornerPos.y = playerFlameLeft.transform.position.y + playerFlameLeft.transform.localScale.y / 2;
            }

            // �ϐ��ɓ����
            if (cornerPos != Vector3.zero)
            {
                // �O�̓_���O���̓_�����́A�O�̃C���f�b�N�X�ԍ����p�̍��W�ɂ���
                if (inFlameIndex.IndexOf(upPointBeforeIndex - 1) == -1)
                {
                    point[upPointBeforeIndex - 1] = cornerPos;
                    inFlameIndex.Add(upPointBeforeIndex - 1);
                }
            }
            //========================================================

            pointNum = inFlameIndex.Count + 1;
        }

        Vector3[] pointInFlame = new Vector3[pointNum * 2];
        int inFlameCount = 1;   // �V�����ϐ��̃C���f�b�N�X�ԍ��̃J�E���g

        // �V�����ϐ��ɐ؂�ւ�
        if (inFlameAll)
        {
            for (int i = 1; i < point.Length; i++)
            {
                pointInFlame[i] = point[i];
            }
            pointInFlame[0] = centerPos;     // ���S���W
        }
        else
        {
            for (int i = 1; i < point.Length; i++)
            {
                if (inFlameIndex.IndexOf(i) == -1) continue;

                pointInFlame[inFlameCount] = point[i];
                inFlameCount++;
            }

            // �V�����}�`�̒��S
            // �V�����}�`�̊e�[�����߁A���̒��S��}�`�̒��S�Ƃ���
            float mostLeftPos = pointInFlame[1].x;
            float mostRightPos = pointInFlame[1].x;
            float mostUpPos = pointInFlame[1].y;
            float mostDownPos = pointInFlame[1].y;
            for (int i = 2; i < point.Length; i++)
            {
                if (pointInFlame[i].x < mostLeftPos)
                    mostLeftPos = pointInFlame[i].x;
                else if (pointInFlame[i].x > mostRightPos)
                    mostRightPos = pointInFlame[i].x;

                if (pointInFlame[i].y < mostDownPos)
                    mostDownPos = pointInFlame[i].y;
                else if (pointInFlame[i].y > mostUpPos)
                    mostUpPos = pointInFlame[i].y;
            }
            Vector3 tempCenterPos = centerPos;
            tempCenterPos.x = mostLeftPos + (mostRightPos - mostLeftPos) / 2;
            tempCenterPos.y = mostDownPos + (mostUpPos - mostDownPos) / 2;
            pointInFlame[0] = tempCenterPos;
        }

        // ���̂ɂ���
        for (int i = 0; i < pointNum; i++)
        {
            pointInFlame[i + pointNum] = new Vector3(pointInFlame[i].x, pointInFlame[i].y, -0.1f);
        }

        return pointInFlame;
    }


    private int[] GetTriangles(int vertexNum)
    {
        //=======================================
        // ���_�C���f�b�N�X�̐���
        int[] triangles = new int[(vertexNum - 1) * 2 * 3 + (vertexNum - 1) * 2 * 3];

        // �\��
        for (int i = 0; i < (vertexNum - 2); i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 2;
            triangles[i * 3 + 2] = i + 1;
        }
        triangles[(vertexNum - 2) * 3] = 0;
        triangles[(vertexNum - 2) * 3 + 1] = 1;
        triangles[(vertexNum - 2) * 3 + 2] = vertexNum - 1;

        // ����
        for (int i = 0; i < (vertexNum - 2); i++)
        {
            triangles[(i + (vertexNum - 1)) * 3] = vertexNum;
            triangles[(i + (vertexNum - 1)) * 3 + 1] = vertexNum + i + 1;
            triangles[(i + (vertexNum - 1)) * 3 + 2] = vertexNum + i + 2;
        }
        triangles[((vertexNum - 1) * 2 - 1) * 3] = vertexNum;
        triangles[((vertexNum - 1) * 2 - 1) * 3 + 1] = vertexNum - 1 + vertexNum;
        triangles[((vertexNum - 1) * 2 - 1) * 3 + 2] = vertexNum + 1;

        // ����
        for (int i = 1; i < (vertexNum - 1); i++)
        {
            int temp_num = ((vertexNum - 1) * 2 - 2) + i * 2;
            triangles[temp_num * 3] = i;
            triangles[temp_num * 3 + 1] = i + 1;
            triangles[temp_num * 3 + 2] = i + vertexNum + 1;

            triangles[(temp_num + 1) * 3] = i;
            triangles[(temp_num + 1) * 3 + 1] = i + vertexNum + 1;
            triangles[(temp_num + 1) * 3 + 2] = i + vertexNum;
        }

        triangles[(((vertexNum - 1) * 2 - 2) + (vertexNum - 1) * 2) * 3] = vertexNum - 1;
        triangles[(((vertexNum - 1) * 2 - 2) + (vertexNum - 1) * 2) * 3 + 1] = 1;
        triangles[(((vertexNum - 1) * 2 - 2) + (vertexNum - 1) * 2) * 3 + 2] = vertexNum + 1;

        triangles[(((vertexNum - 1) * 2 - 2) + (vertexNum - 1) * 2 + 1) * 3] = vertexNum - 1;
        triangles[(((vertexNum - 1) * 2 - 2) + (vertexNum - 1) * 2 + 1) * 3 + 1] = vertexNum + 1;
        triangles[(((vertexNum - 1) * 2 - 2) + (vertexNum - 1) * 2 + 1) * 3 + 2] = vertexNum + vertexNum - 1;

        return triangles;
    }
}
