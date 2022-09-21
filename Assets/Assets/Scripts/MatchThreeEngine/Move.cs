namespace MatchThreeEngine
{
	public sealed class Move
	{
		public readonly int X1;
		public readonly int Y1;
		
		public readonly int X2;
		public readonly int Y2;

		public Move(int x1 = 0, int y1 = 0, int x2 = 0, int y2 = 0)
		{
			X1 = x1;
			Y1 = y1;
			
			X2 = x2;
			Y2 = y2;
		}
	}
}
