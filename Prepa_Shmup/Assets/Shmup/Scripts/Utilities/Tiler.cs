using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiler : MonoBehaviour
{
	[SerializeField]
	private List<Tile> _tiles = null;

	[SerializeField]
	private Tile _defaultTile = null;

	public void StartTiler()
	{
		if (_defaultTile == null)
		{
			StartTile(null, true, transform.position);
		}
		else
		{
			StartTile(_defaultTile, false, transform.position);
		}
	}

	private void OnEnable()
	{
		for (int i = 0, length = _tiles.Count; i < length; i++)
		{
			Tile tile = _tiles[i];
			tile.LengthExceeded += OnTileLengthExceeded;
			tile.Activate(false, Vector3.zero);
		}

		if (_defaultTile != null)
		{
			_defaultTile.LengthExceeded += OnTileLengthExceeded;
			_defaultTile.Activate(false, Vector3.zero);
		}
	}

	private void OnDisable()
	{
		for (int i = 0, length = _tiles.Count; i < length; i++)
		{
			_tiles[i].LengthExceeded -= OnTileLengthExceeded;
		}
		if (_defaultTile != null)
		{
			_defaultTile.LengthExceeded -= OnTileLengthExceeded;
		}
	}

	private void OnTileLengthExceeded(Tile sender, Vector3 endPosition)
	{
		StartTile(sender, true, endPosition);
	}

	private void StartTile(Tile specificTile, bool ignoreSpecificTile, Vector3 startPosition)
	{
		Tile newSelection = null;
		if (ignoreSpecificTile == true)
		{
			List<Tile> buffer = new List<Tile>(_tiles);
			buffer.Remove(specificTile);
			newSelection = buffer[Random.Range(0, buffer.Count)];
		}
		else
		{
			newSelection = specificTile;
		}
		newSelection.Activate(true, startPosition);
	}
}
