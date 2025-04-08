using UnityEngine;
using UnityEditor;
using System;


public class EditorUtils : EditorWindow
{
    [MenuItem("Tools/Open Refresh and Play Button Window", priority = 1)]
    public static void Refresh()
    {
        var window = EditorWindow.GetWindow<EditorUtils>();
        window.titleContent.text = "Refresh and Play";
    }
    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        EditorApplication.playModeStateChanged += OnEnterPlayMode;
    }
    private static void OnEnterPlayMode(PlayModeStateChange obj)
    {
        if (obj == PlayModeStateChange.ExitingEditMode) {
            Debug.Log("Exiting Edit Mode");
            AssetDatabase.Refresh(ImportAssetOptions.Default);
        }
    }
    private void OnGUI()
    {
        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Refresh and Play"))
            {
                //AssetDatabase.Refresh(ImportAssetOptions.Default);
                EditorApplication.EnterPlaymode();
            }
        }
    }
    // https://docs.unity3d.com/6000.0/Documentation/ScriptReference/MenuItem.html
    [MenuItem("Tools/Reset Position &g")]  // %g means Ctrl+G (Cmd+G on macOS)
    private static void ResetPosition()
    {
        if (Selection.activeGameObject != null)
        {
            // Change the position of the selected GameObject
            Selection.activeGameObject.transform.position = new Vector3(0, 0, 0);
            Debug.Log("Position changed to (0, 0, 0)");
        }
        else
        {
            Debug.LogWarning("No GameObject selected.");
        }
    }
    [MenuItem("Tools/Get Bound Size")]  // %g means Ctrl+G (Cmd+G on macOS)
    private static void GetBoundSize()
    {
        if (Selection.activeGameObject != null)
        {
            Debug.Log("Bounds size: " + Selection.activeGameObject.GetComponent<MeshRenderer>().bounds.size);
        }
        else
        {
            Debug.LogWarning("No GameObject selected.");
        }
    }
    [MenuItem("Tools/Reset Rotation &r")]  // %g means Ctrl+G (Cmd+G on macOS)
    private static void ResetRotation()
    {
        if (Selection.activeGameObject != null)
        {

            Selection.activeGameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            Debug.Log("Position changed to (0, 0, 0)");
        }
        else
        {
            Debug.LogWarning("No GameObject selected.");
        }
    }
    [MenuItem("Tools/Rigidbody")]
    private static void CreateRigidbody()
    {
        if (Selection.gameObjects != null)
        {
            foreach (GameObject go in Selection.gameObjects)
            {
                Rigidbody rb = go.GetComponent<Rigidbody>();
                if (rb == null)
                    rb = go.AddComponent(typeof(Rigidbody)) as Rigidbody;
                rb.isKinematic = true;
            }
        }
        else
        {
            Debug.LogWarning("No GameObject selected.");
        }
    }
    [MenuItem("Tools/EmptyObject")]
    private static void CreateEmptyObject()
    {
        var name = "GameManager"; //EditorGUIUtility.systemCopyBuffer;
        // if (string.IsNullOrEmpty(name))
        // {
        //     name = "GameManager";
        // }
        /*if (!File.Exists("Assets/Scripts/" + name + ".cs"))
        {
            File.WriteAllText("Assets/Scripts/" + name + ".cs", @"using UnityEngine;

public class " + name + @": MonoBehaviour
{
    void Awake()
    {
        var boundsSize = GetComponent<Renderer>().bounds.size;
        
    }
    void Start()
    {

    }
    void Update()
    {

    }
    void FixedUpdate()
    {

    }
}");
            
        }*/


        if (Selection.activeGameObject != null)
        {

            var emptyObject = new GameObject(name);
            emptyObject.transform.SetParent(Selection.activeGameObject.transform);
            emptyObject.AddComponent(Type.GetType(name + ",Assembly-Csharp"));
        }
        else
        {
            var emptyObject = new GameObject(name);
            emptyObject.AddComponent(Type.GetType(name + ",Assembly-Csharp"));

        }
    }
    [MenuItem("Tools/Plane")]
    private static void CreatePlane()
    {
        var obj = GameObject.CreatePrimitive(PrimitiveType.Plane);
        obj.transform.position = new Vector3(0, 0, 0);


    }
    [MenuItem("Tools/Cube")]
    private static void CreateCube()
    {
        var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.transform.position = new Vector3(0, 0, 0);
        obj.AddComponent(typeof(Rigidbody));



    }
    [MenuItem("Tools/Capsule")]
    private static void Capsule()
    {
        var obj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        obj.transform.position = new Vector3(0, 0, 0);
        obj.AddComponent(typeof(Rigidbody));

    }
    [MenuItem("Tools/Sphere")]
    private static void CreateSphere()
    {
        var obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        obj.transform.position = new Vector3(0, 0, 0);
        obj.AddComponent(typeof(Rigidbody));

    }
    [MenuItem("Tools/ParticleSystem")]
    private static void ParticleSystem()
    {
        var obj = new GameObject("ParticleSystem");
        ParticleSystem ps = obj.AddComponent<ParticleSystem>();
        ParticleSystemRenderer psr = obj.AddComponent<ParticleSystemRenderer>();
    }

    public static void AddTag(string tag)
    {
        UnityEngine.Object[] asset = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset");
        if ((asset != null) && (asset.Length > 0))
        {
            SerializedObject so = new SerializedObject(asset[0]);
            SerializedProperty tags = so.FindProperty("tags");

            for (int i = 0; i < tags.arraySize; ++i)
            {
                if (tags.GetArrayElementAtIndex(i).stringValue == tag)
                {
                    return;     // Tag already present, nothing to do.
                }
            }

            tags.InsertArrayElementAtIndex(0);
            tags.GetArrayElementAtIndex(0).stringValue = tag;
            so.ApplyModifiedProperties();
            so.Update();
        }
    }
    [MenuItem("Tools/Tag")]
    private static void Tag()
    {
        AddTag(EditorGUIUtility.systemCopyBuffer);
    }
}