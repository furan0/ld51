using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CanShake))]
class CanShakeEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        CanShake shaker = (CanShake) target;

        if(GUILayout.Button("Shake"))
            shaker.shakeMoiCa();

    }
}