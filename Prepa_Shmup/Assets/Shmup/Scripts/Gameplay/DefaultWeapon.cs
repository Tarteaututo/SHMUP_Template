using UnityEngine;

/// <summary>
/// Bind a user input and a ProjectileLauncher to launch a projectile when the input is used.
/// </summary>
[RequireComponent(typeof(ProjectileLauncher))]
[System.Serializable]
public class DefaultWeapon : MonoBehaviour
{
	#region Fields
	[SerializeField]
	private ProjectileLauncher _projectileLauncher = null;

	[SerializeField]
	private string _inputName = "Fire1";

	[SerializeField]
	private string _lockedInputName = "Fire2";
	#endregion Fields

	#region Methods
	private void Update()
	{
		UpdateInputs();
	}

	private void UpdateInputs()
	{
		bool inputReceived = Input.GetButton(_inputName);
		bool lockingInputReceived = false;
		if (_lockedInputName != string.Empty)
		{
			lockingInputReceived = Input.GetButton(_lockedInputName);
		}

		if (inputReceived == true && lockingInputReceived == false)
		{
			_projectileLauncher.LaunchProjectile();
		}
	}
	#endregion Methods

}
