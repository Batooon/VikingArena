using Code.Logic.EnemySpawners;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(SpawnMarker))]
    public class SpawnMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(SpawnMarker marker, GizmoType dizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(marker.transform.position, .5f);
        }
    }
}