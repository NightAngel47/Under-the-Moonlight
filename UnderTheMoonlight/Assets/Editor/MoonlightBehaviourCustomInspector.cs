using UnderTheMoonlight.Level;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnderTheMoonlight.Editor
{
    [CustomEditor(typeof(MoonlightBehaviour))]
    public class MoonlightBehaviourCustomInspector : UnityEditor.Editor
    {
        private VisualTreeAsset visualTree;
        private StyleSheet styles;

        private SerializedObject serializedMoonlight;

        private SerializedProperty EnterMoonlight;
        private SerializedProperty ExitMoonlight;

        private SerializedProperty moonlightActive;
        private SerializedProperty useTimerValue;
        private SerializedProperty moonlightActiveTime;
        private SerializedProperty moonlightInactiveTime;

        private VisualElement classElements;

        private Slider moonlightActiveTimeField;
        private Slider moonlightInactiveTimeField;

        private const float moonlightTimeMin = 1f;
        private const float moonlightTimeMax = 120f;

        private FloatField moonlightActiveTimeFloatField;
        private FloatField moonlightInactiveTimeFloatField;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        }

        /// <summary> Draws the Unity Events within an IMGUIContainers. </summary>
        private void DrawUnityEvents()
        {
            EditorGUILayout.PropertyField(EnterMoonlight);
            EditorGUILayout.PropertyField(ExitMoonlight);

            if (GUI.changed)
            {
                serializedMoonlight.ApplyModifiedProperties();
            }
        }

        public override VisualElement CreateInspectorGUI()
        {
            serializedMoonlight = new SerializedObject((MoonlightBehaviour)target);

            EnterMoonlight = serializedMoonlight.FindProperty("EnterMoonlight");
            ExitMoonlight = serializedMoonlight.FindProperty("ExitMoonlight");

            moonlightActive = serializedMoonlight.FindProperty("isMoonlightActive");
            useTimerValue = serializedMoonlight.FindProperty("useTimer");
            moonlightActiveTime = serializedMoonlight.FindProperty("moonlightActiveTime");
            moonlightInactiveTime = serializedMoonlight.FindProperty("moonlightInactiveTime");

            VisualElement inspector = new VisualElement();

            visualTree = Resources.Load("MoonlightBehaviourCustomInspectorHierarchy") as VisualTreeAsset;

            styles = Resources.Load("MoonlightBehaviourCustomInspectorStyles") as StyleSheet;
            inspector.styleSheets.Add(styles);

            VisualElement spacer = new VisualElement();
            spacer.AddToClassList("spacer");
            inspector.Add(spacer);

            classElements = new VisualElement();
            classElements.name = "Class Elements";
            inspector.Add(UpdateClassElements());

            return inspector;
        }

        /// <summary> Updates the way values are drawn in the inspector. </summary>
        /// <returns> The visual element containing the heirarchy and styles of the class. </returns>
        private VisualElement UpdateClassElements()
        {
            classElements.Clear();

            IMGUIContainer eventsContainer = new IMGUIContainer(DrawUnityEvents);
            classElements.Add(eventsContainer);

            visualTree.CloneTree(classElements);

            Toggle isMoonlightActiveToggle = classElements.Q<Toggle>("isMoonlightActive");
            isMoonlightActiveToggle.value = moonlightActive.boolValue;
            isMoonlightActiveToggle.RegisterValueChangedCallback(IsMoonlightActiveValueChanged);

            Toggle useTimerToggle = classElements.Q<Toggle>("useTimer");
            useTimerToggle.value = useTimerValue.boolValue;
            useTimerToggle.RegisterValueChangedCallback(UseTimersValueChanged);

            #region Active Field

            moonlightActiveTimeField = classElements.Q<Slider>("moonlightActiveTime");
            moonlightActiveTimeField.lowValue = moonlightTimeMin;
            moonlightActiveTimeField.highValue = moonlightTimeMax;
            moonlightActiveTimeField.value = moonlightActiveTime.floatValue;
            moonlightActiveTimeField.RegisterValueChangedCallback(MoonlightActiveTimeValueChanged);

            moonlightActiveTimeFloatField = classElements.Q<FloatField>("moonlightActiveValue");
            moonlightActiveTimeFloatField.value = moonlightActiveTime.floatValue;
            moonlightActiveTimeFloatField.RegisterValueChangedCallback(MoonlightActiveTimeValueChanged);

            #endregion

            #region Inactive Field

            moonlightInactiveTimeField = classElements.Q<Slider>("moonlightInactiveTime");
            moonlightInactiveTimeField.lowValue = moonlightTimeMin;
            moonlightInactiveTimeField.highValue = moonlightTimeMax;
            moonlightInactiveTimeField.value = moonlightInactiveTime.floatValue;
            moonlightInactiveTimeField.RegisterValueChangedCallback(MoonlightInactiveTimeValueChanged);

            moonlightInactiveTimeFloatField = classElements.Q<FloatField>("moonlightInactiveValue");
            moonlightInactiveTimeFloatField.value = moonlightInactiveTime.floatValue;
            moonlightInactiveTimeFloatField.RegisterValueChangedCallback(MoonlightInactiveTimeValueChanged);

            #endregion

            UpdateMoonlightTimerFields();

            return classElements;
        }

        /// <summary> Callback when a value is changed. </summary>
        /// <param name="callback"> Callback for the value change. </param>
        private void IsMoonlightActiveValueChanged(ChangeEvent<bool> callback)
        {
            moonlightActive.boolValue = callback.newValue;
            serializedMoonlight.ApplyModifiedProperties();
        }

        /// <summary> Callback when a value is changed. </summary>
        /// <param name="callback"> Callback for the value change. </param>
        private void UseTimersValueChanged(ChangeEvent<bool> callback)
        {
            useTimerValue.boolValue = callback.newValue;
            serializedMoonlight.ApplyModifiedProperties();

            UpdateMoonlightTimerFields();
        }

        /// <summary> Callback when a value is changed. </summary>
        /// <param name="callback"> Callback for the value change. </param>
        private void MoonlightActiveTimeValueChanged(ChangeEvent<float> callback)
        {
            moonlightActiveTime.floatValue = callback.newValue;
            moonlightActiveTimeField.value = callback.newValue;
            moonlightActiveTimeFloatField.value = callback.newValue;

            serializedMoonlight.ApplyModifiedProperties();
        }

        /// <summary> Callback when a value is changed. </summary>
        /// <param name="callback"> Callback for the value change. </param>
        private void MoonlightInactiveTimeValueChanged(ChangeEvent<float> callback)
        {
            moonlightInactiveTime.floatValue = callback.newValue;
            moonlightInactiveTimeField.value = callback.newValue;
            moonlightInactiveTimeFloatField.value = callback.newValue;

            serializedMoonlight.ApplyModifiedProperties();
        }

        /// <summary> Removes the Moonlight timer fields. </summary>
        private void UpdateMoonlightTimerFields()
        {
            if (useTimerValue.boolValue)
            {
                moonlightActiveTimeField.value = moonlightActiveTime.floatValue;
                moonlightInactiveTimeField.value = moonlightInactiveTime.floatValue;

                classElements.Add(moonlightActiveTimeField);
                classElements.Add(moonlightInactiveTimeField);
            }
            else
            {
                classElements.Remove(moonlightActiveTimeField);
                classElements.Remove(moonlightInactiveTimeField);
            }
        }
    }
}