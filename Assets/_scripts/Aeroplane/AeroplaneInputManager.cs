using UnityEngine;
using UnityEngine.UIElements;

public class AeroplaneInputManager : MonoBehaviour
{
	[SerializeField]
	private UIDocument uiDocument;

	[SerializeField]
	private AeroplaneController aeroplaneController;


	private void Start()
	{
		//uiDocument.rootVisualElement.Q<Slider>("HeadingSlider").RegisterValueChangedCallback += () => SetHeading;

		//uiDocument.rootVisualElement.Q<Slider>("HeadingSlider").RegisterValueChangedCallback(v =>
		//{
		//	aeroplaneController.TargetHeadingInDegrees = v.newValue;
		//});	

		Toggle toggle = uiDocument.rootVisualElement.Q<Toggle>("Atoggle");

		toggle.RegisterValueChangedCallback(OnTestToggleChanged);
	}

	private void OnTestToggleChanged(ChangeEvent<bool> evt)
	{
		if (evt.newValue)
			aeroplaneController.TargetHeadingInDegrees = 090f;
		else
			aeroplaneController.TargetHeadingInDegrees = 270;
	}
}
