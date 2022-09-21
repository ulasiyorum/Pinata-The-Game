using UnityEngine;

namespace MatchThreeEngine
{
	[CreateAssetMenu(menuName = "Match 3 Engine/Tile Type Asset")]
	public sealed class TileTypeAsset : ScriptableObject
	{
		public int id;

		public int value;

		public Sprite sprite;
	}
}
