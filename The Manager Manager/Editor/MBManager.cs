    using Sirenix.OdinInspector.Editor;
    using System;
    using System.Linq;
    using UnityEditor;

    public class MBManager : OdinEditorWindow
    {
        private static Type[] typesToDisplay = TypeCache.GetTypesWithAttribute<ManageableAttribute>()
            .OrderBy(m => m.Name)
            .ToArray();

        private Type selectedType;

        [MenuItem("Tools/MB Manager")]
        private static void OpenEditor() => GetWindow<MBManager>();

        protected override void OnGUI()
        {
            GUIUtils.SelectButtonList(ref selectedType, typesToDisplay);
            base.OnGUI();
        }

        protected override object GetTarget()
        {
            if (selectedType == null && typesToDisplay.Length > 0)
                selectedType = typesToDisplay[0];

           if (selectedType == null)
                return null;
            else
                return FindObjectOfType(selectedType);
        }
    }


