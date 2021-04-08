using System.IO;
using UnityEditor;
using UnityEngine;

namespace UPMG
{
    public class PlaneMeshGeneratorWindow : EditorWindow
    {
        private const string Title = "Mesh Transformer";

        private int _width;
        private int _height;
        private float _unitLength;
        private string _exportMeshName;
        private bool _isHorizontal;
        private DefaultAsset _exportDirectory;
        private Editor _gameObjectEditor;

        [MenuItem("Window/UniPlaneMeshGenerator")]
        private static void Open()
        {
            GetWindow<PlaneMeshGeneratorWindow>(Title).Show();
        }

        void OnGUI()
        {
            _width = EditorGUILayout.IntField("Width", _width);
            _height = EditorGUILayout.IntField("Height", _height);
            _unitLength = EditorGUILayout.FloatField("Unit Length", _unitLength);
            _isHorizontal = EditorGUILayout.Toggle("Horizontal", _isHorizontal);

            GUILayout.Space(10);

            _exportDirectory =
                (DefaultAsset) EditorGUILayout.ObjectField("Directory", _exportDirectory, typeof(DefaultAsset), true);
            _exportMeshName = EditorGUILayout.TextField("Export Mesh Name", _exportMeshName);

            if (_exportDirectory != null && !string.IsNullOrEmpty(_exportMeshName))
            {
                if (GUILayout.Button("Export"))
                {
                    var newMesh = PlaneMeshGenerator.GenerateHorizontal(_width, _height, _unitLength, _isHorizontal);
                    Export(newMesh);
                }
            }

            if (GUILayout.Button("Refresh Preview"))
            {
                var newMesh = PlaneMeshGenerator.GenerateHorizontal(_width, _height, _unitLength, _isHorizontal);
                _gameObjectEditor = Editor.CreateEditor(newMesh);
            }

            if (_gameObjectEditor != null)
            {
                _gameObjectEditor.OnInteractivePreviewGUI(GUILayoutUtility.GetRect(200, 200), new GUIStyle());
            }
        }

        void Export(Mesh mesh)
        {
            var exportDirectoryPath = AssetDatabase.GetAssetPath(_exportDirectory);
            if (Path.GetExtension(_exportMeshName) != ".asset")
            {
                _exportMeshName += ".asset";
            }

            var exportPath = Path.Combine(exportDirectoryPath, _exportMeshName);
            AssetDatabase.CreateAsset(mesh, exportPath);
        }
    }
}