using UnityEditor;
using Library.UIManagement;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(UIManager))]
public class UIManagerEditor : Editor
{
    public VisualTreeAsset TreeAsset;
    private UIManager _uIManager;

    public override VisualElement CreateInspectorGUI()
    {
        if (!TreeAsset)
            return base.CreateInspectorGUI();

        _uIManager = (UIManager)target;

        VisualElement root = new VisualElement();
        TreeAsset.CloneTree(root);

        // Add your UI content here

        // root.Q<Label>("title").text = "Custom Property Drawer";
        root.Q<Button>("btn_generateUi").clickable.clicked += () => UIManager.Instance.GenerateUiList();
        root.Q<Button>("btn_generateEnum").clickable.clicked += () => UIManager.Instance.GenerateUiElementsEnumFile();
        
        return root;
    }
}