/*using System;
using UnityEditor;
using UnityEditor.UIElements;
using DG.Tweening;
using UnityEngine;

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(TweenController)), CanEditMultipleObjects]
public class TweenEditor : UnityEditor.Editor
{
        private int fontSize = 13;
        private FontStyle fontStyle = FontStyle.Bold;
        public override void OnInspectorGUI()
        {
            TweenController tween = (TweenController)target;

            EditorGUILayout.LabelField("Boleans", EditorStyles.boldLabel);
      
            tween.waitCommand = EditorGUILayout.Toggle("Wait Command", tween.waitCommand);
            
            EditorGUILayout.Space(5);
            
            tween.doScale = EditorGUILayout.Toggle("Do Scale", tween.doScale);
            tween.doMove = EditorGUILayout.Toggle("Do Move", tween.doMove);
            tween.doRotate = EditorGUILayout.Toggle("Do Rotate", tween.doRotate);
            EditorGUILayout.Space(10);
            
            GUIStyle TextFieldStyles  = new GUIStyle(EditorStyles.boldLabel);
            TextFieldStyles.fontSize = fontSize;
            TextFieldStyles.fontStyle = fontStyle;
            
           
            if (tween.doScale)
            {
                TextFieldStyles.normal.textColor = new Color32(170, 179, 171,255);
                GUI.backgroundColor = new Color32(203, 214, 205,255);
                EditorGUILayout.LabelField("Scale Parameters", TextFieldStyles);
                EditorGUILayout.Space(5);
                tween.doScaleLoop = EditorGUILayout.Toggle("Do Scale Loop", tween.doScaleLoop);
                tween.initialScale = EditorGUILayout.Vector3Field("Initial Scale", tween.initialScale);
                tween.finalScale = EditorGUILayout.Vector3Field("Final Scale", tween.finalScale);
                tween.scaleEase = (Ease)EditorGUILayout.EnumPopup("Scale Ease", tween.scaleEase);
                tween.scaleDuration = EditorGUILayout.FloatField("Scale Duration", tween.scaleDuration);
                EditorGUILayout.Space(10);
            }

            if (tween.doMove)
            {
                TextFieldStyles.normal.textColor = new Color32(235, 239, 201, 255);
                GUI.backgroundColor = new Color32(234, 237, 211,255);
                EditorGUILayout.LabelField("Move Parameters", TextFieldStyles);
                EditorGUILayout.Space(5);
                tween.doMoveloop = EditorGUILayout.Toggle("Do Move Loop", tween.doMoveloop);
                tween.initialPos = EditorGUILayout.Vector3Field("Initial Position", tween.initialPos);
               // tween.finalPos = EditorGUILayout.Vector3Field("Final Position", tween.finalPos);
                tween.moveEase = (Ease)EditorGUILayout.EnumPopup("Move Ease", tween.moveEase);
                tween.moveDuration = EditorGUILayout.FloatField("Move Duration", tween.moveDuration);
                EditorGUILayout.Space(10);

            }
            if (tween.doRotate)
            {
                TextFieldStyles.normal.textColor = new Color32(232, 202, 175,255);
                GUI.backgroundColor = new Color32(230, 211, 195,255);
                EditorGUILayout.LabelField("Rotate Parameters", TextFieldStyles);
                EditorGUILayout.Space(5);
                tween.finalRotate = EditorGUILayout.Vector3Field("Final Rotate", tween.finalRotate);
                tween.rotateDuration = EditorGUILayout.FloatField("Rotate Duration", tween.rotateDuration);
                tween.pingPongLoopping = EditorGUILayout.Toggle("Ping Pong Loopping", tween.pingPongLoopping);
                EditorGUILayout.Space(10);

            }
            
            TextFieldStyles.normal.textColor = new Color32(196, 203, 183,255);
            GUI.backgroundColor = new Color32(220, 227, 207,255);
            tween.waitBeforeDo = EditorGUILayout.FloatField("Wait Before Do", tween.waitBeforeDo);
            
            serializedObject.ApplyModifiedProperties();

        }
}        
#endif*/