using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HoldData {
	public static float PlayerHealth=100;
	public static float PlayerInfect=75;
	public static void clearData()
	{
		PlayerHealth = 100;
		PlayerInfect = 75;
	}
}
