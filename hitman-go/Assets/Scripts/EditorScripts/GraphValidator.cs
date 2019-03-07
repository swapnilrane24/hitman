﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using PathSystem;
namespace EditorScripts
{
    [CustomEditor(typeof(ScriptableGraph)), CanEditMultipleObjects]
    // Start is called before the first frame update
    public class GraphValidator : Editor
    {
        public override void OnInspectorGUI()
        {
            ScriptableGraph graph = (ScriptableGraph)target;
            int nodes;
            

            
            
            base.OnInspectorGUI();
           if(GUILayout.Button("Create Grid"))
            {
                for (int i = 0; i < graph.maxwidth; i++)
                {
                    GUILayout.BeginHorizontal();
                    for (int j = 0; j < graph.maxwidth; j++)
                    {
                        EditorGUILayout.IntField("", -1);
                    }
                    GUILayout.EndHorizontal();
                }
            }
            if (GUILayout.Button("Set NodeId"))
            {
                if (graph.set)
                {
                    for (int i = 0; i < graph.Graph.Count; i++)
                    {
                        graph.Graph[i].node.uniqueID = i;
                        graph.Graph[i].up = -1;
                        graph.Graph[i].down = -1;
                        graph.Graph[i].left = -1;
                        graph.Graph[i].right = -1;
                        graph.set = false;
                    }
                }
            }
            if (GUILayout.Button("Velidate"))
            {
                for(int i = 0; i < graph.stars.Length; i++)
                {
                    if (graph.stars[i].name == "")
                    {
                        Debug.LogError("Star Name Not Set");
                    }
                }
            }
        }
    }
}