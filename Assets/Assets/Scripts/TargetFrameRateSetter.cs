using UnityEngine;

public static class TargetFrameRateSetter
{
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] private static void Initialize() => Application.targetFrameRate = 61;
}
