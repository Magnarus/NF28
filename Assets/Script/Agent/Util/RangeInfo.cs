using UnityEngine;
using System.Collections;

public class RangeInfo {
	
	private int posX;
	public int PosX {
		get {
			return posX;
		}
		set {
			posX = value;
		}
	}

	private int minY;

	public int MinY {
		get {
			return minY;
		}
		set {
			minY = value;
		}
	}

	private int maxY;
	public int MaxY {
		get {
			return maxY;
		}
		set {
			maxY = value;
		}
	}
}
