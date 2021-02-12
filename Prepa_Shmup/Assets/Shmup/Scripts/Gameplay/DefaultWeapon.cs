using UnityEngine;

[System.Serializable]
public class DefaultWeapon : MonoBehaviour
{
	#region Fields
	[SerializeField]
	private string _inputName = "Fire1";

	[SerializeField]
	private Projectile _projectile = null;

	[SerializeField]
	private int _projectileCount = 1;

	[SerializeField]
	private float _horizontalProjectilesSpacing = 1f;

	[SerializeField]
	private float _fireRate = 0.1f;

	[SerializeField]
	private Transform _muzzle = null;

	[System.NonSerialized]
	private float _currentFireTime = 0;
	#endregion Fields

	#region Methods
	private void Update()
	{
		UpdateInputs();
	}

	private void UpdateInputs()
	{
		bool fired = Input.GetButton(_inputName);

		if (fired == true && _currentFireTime <= 0)
		{
			_currentFireTime = _fireRate;
			DoFireMultiple();
		}

		if (_currentFireTime > 0)
		{
			_currentFireTime -= Time.deltaTime;
		}
	}

	private void DoFire(Vector3 position)
	{
		var instance = Object.Instantiate(_projectile);
		instance.transform.position = position;
		instance.transform.rotation = _muzzle.transform.rotation;
	}

	private void DoFireMultiple()
	{
		float distance = (_horizontalProjectilesSpacing * (_projectileCount - 1));
		Vector3 position = _muzzle.transform.localPosition;
		position.x -= distance * 0.5f;
		for (int i = 0; i < _projectileCount; i++)
		{
			Vector3 firePosition = new Vector3(position.x + _horizontalProjectilesSpacing * i, 0, 0);
			firePosition = _muzzle.transform.position + _muzzle.transform.TransformDirection(firePosition);
			DoFire(firePosition);
		}
	}
	#endregion Methods

}
