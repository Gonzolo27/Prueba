using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRootAnimation : MonoBehaviour
{
    Animator m_Animator;
    string m_ClipName;
    AnimatorClipInfo[] m_CurrentClipInfo;

    float m_CurrentClipLength;
    private void Start()
    {
        //Get them_Animator, which you attach to the GameObject you intend to animate.
        m_Animator = gameObject.GetComponent<Animator>();
        
    }

    private void Update()
    {
        //Fetch the current Animation clip information for the base layer
        m_CurrentClipInfo = this.m_Animator.GetCurrentAnimatorClipInfo(0);
        //Access the current length of the clip
        m_CurrentClipLength = m_CurrentClipInfo[0].clip.length;
        //Access the Animation clip name
        m_ClipName = m_CurrentClipInfo[0].clip.name;
        if (m_ClipName.Equals("Bee_Attack"))
        {
            float distance = Vector3.Distance(this.transform.position, this.transform.parent.position);
            if (distance > 0.1f)
            {
                this.transform.parent.position = this.transform.position;
            }
        }
        
    }
}
