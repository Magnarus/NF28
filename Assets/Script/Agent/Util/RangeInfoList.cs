using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RangeInfoList  {

	private List<RangeInfo> list = new List<RangeInfo>();

	public RangeInfoList() { }

	public RangeInfo GetRangeInfo(int posX) {
		foreach (RangeInfo r in list) {
			if (r.PosX == posX)
				return r;
		}
		return null;
	}

	public void AddRangeInfo(int posX, int yMin,int yMax) {
		RangeInfo r = new RangeInfo ();
		r.PosX = posX;
		r.MaxY = yMax;
		r.MinY = yMin;
		list.Add (r);
	}
}
