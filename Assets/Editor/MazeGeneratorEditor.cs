using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MazeGenerator))]
public class MazeGeneratorEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        MazeGenerator myTarget = (MazeGenerator)target;
        if(GUILayout.Button("CreateBlocks")){
            if(!myTarget.generatedBlocks){
                myTarget.GenerateBlocks();
            }
        }
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Step")){
            myTarget.Step();
        }
        if(GUILayout.Button("Instant")){
            if(myTarget.completed || myTarget.generatedBlocks){
                Clean(myTarget);
                myTarget.GenerateBlocks();
            }
            while(!myTarget.completed) myTarget.Step();
        }
        GUILayout.EndHorizontal();

        if(GUILayout.Button("Clean")){
            Clean(myTarget);
        }
        if(GUILayout.Button("Bake")){
            myTarget.BakeNavMesh();
        }
    }

    void Clean(MazeGenerator myTarget){
        while(myTarget.transform.childCount > 0)
        foreach (Transform g in myTarget.transform){
            DestroyImmediate(g.gameObject);
        }
        myTarget.generatedBlocks = false;
        myTarget.completed = false;

    }
}
