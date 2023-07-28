using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SCR_StateBase : MonoBehaviour
{
   public bool m_StateTerminal = false;

    // �X�e�[�g�R���g���[���[
    protected SCR_StateControllerBase SCR_Controller;

    protected int m_StateType { set; get; }

    // ����������
    public virtual void Initialize(int stateType)
    {
        m_StateType = stateType;
        SCR_Controller = GetComponent<SCR_StateControllerBase>();
    }
    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract int StateUpdate();
}