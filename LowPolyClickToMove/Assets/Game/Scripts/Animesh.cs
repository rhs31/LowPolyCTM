using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Animesh : MonoBehaviour {
    public string m_CurrentAnimesh;
    public AnimeshData[] m_AnimeshData;
    private MeshFilter meshFilter;
    private AnimeshData m_CurrentAnimeshData=null  ;
    private float m_AnimElapsed;
    private int i;

    [System.Serializable]
    public class AnimeshData
    {
        public string m_Name; //Name of the animation i.e. Walk, Idle
        public float m_FrameTime = 0.2f;
        public Mesh[] m_Meshes; //The meshes that make up the animation
    }

    private AnimeshData GetAnimeshData(string name)//Returns the animation data for the specified animation name
    {
        foreach(var m in m_AnimeshData)
        {
            if (m.m_Name == name) return m;
        }
        return null;
    }

	void Awake(){
        meshFilter = GetComponent<MeshFilter>();//Needed so we can swap out the meshes
    }

    void Update()
    {
        if (m_CurrentAnimesh == "") return;
        if (m_CurrentAnimeshData == null || m_CurrentAnimeshData.m_Name != m_CurrentAnimesh)
        {   //First time through or current animation has been changed so load the animation data and play first frame
            m_CurrentAnimeshData = GetAnimeshData(m_CurrentAnimesh);
            if (m_CurrentAnimeshData != null)
            {
                m_AnimElapsed = 0f;
                i = 0;
                PlayAnimFrame(0);
            }
        }
        else
        {
            PlayAnimFrame(Time.deltaTime);//Play current frame of animation
        }
    }

    void PlayAnimFrame(float delta)//Plays correct frame according to elapsed time
    {
        if (m_AnimElapsed > m_CurrentAnimeshData.m_FrameTime)
        {
            i++;
            m_AnimElapsed = 0.0f;
        }
        else
        {
            m_AnimElapsed += delta;
        }
        if (i >= m_CurrentAnimeshData.m_Meshes.Length) i = 0;
        meshFilter.mesh = m_CurrentAnimeshData.m_Meshes[i];
    }
}
