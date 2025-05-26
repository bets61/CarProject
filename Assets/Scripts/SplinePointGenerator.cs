#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

#endif
using UnityEngine;

[ExecuteInEditMode]
public class SplinePointGenerator : MonoBehaviour
{
    public GameObject splineParent;
    public Transform trackStart;
    public float spacing = 5f;
    public int pointCount = 150;

    private void OnEnable()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            GenerateSplinePoints();
        }
#endif
    }

    public void GenerateSplinePoints()
    {
        // Eski noktaları sil (tekrar oluşturmak için)
        for (int i = splineParent.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(splineParent.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < pointCount; i++)
        {
            Vector3 forward = trackStart.forward;
            Vector3 spawnPos = trackStart.position + forward * spacing * i;

            if (Physics.Raycast(spawnPos + Vector3.up * 50f, Vector3.down, out RaycastHit hit, 100f))
            {
                GameObject point = new GameObject($"SplinePoint ({i})");
                point.transform.position = hit.point;
                point.transform.SetParent(splineParent.transform);
            }
            else
            {
                Debug.LogWarning($"[SplineGen] Nokta atlanıyor: {i} → Pos: {spawnPos}");
            }
        }

#if UNITY_EDITOR
        EditorUtility.SetDirty(splineParent); // Kaydetmeye zorla
        EditorSceneManager.MarkSceneDirty(gameObject.scene); // Sahneyi kaydedilebilir yap
#endif

        Debug.Log("Spline noktaları sahneye kalıcı olarak yerleştirildi.");
    }
}
