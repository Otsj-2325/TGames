using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SCR_StateControllerBase : MonoBehaviour
{
    protected Dictionary<int, SCR_StateBase> m_StateDic = new Dictionary<int, SCR_StateBase>();

    public int m_CurrentState { protected set; get; }

    public abstract void Initialize(int initializeStateType);

    public void UpdateSequence()
    {
        int nextState = (int)m_StateDic[m_CurrentState].StateUpdate();
        AutoStateTransitionSequence(nextState);
    }

    // �X�e�[�g�̎����J��
    protected void AutoStateTransitionSequence(int nextState)
    {
        if (m_CurrentState == nextState)
        {
            return;
        }

        m_StateDic[m_CurrentState].OnExit();
        m_CurrentState = nextState;
        m_StateDic[m_CurrentState].OnEnter();
    }
}
