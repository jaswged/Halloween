using UnityEngine;
using System.Collections;

[System.Serializable]
public struct IntVector2 {
	public int X;//{get;set;}
	public int Z;//{get;set;}

	public IntVector2(int x, int z){
		this.X = x;
		this.Z = z;
	}
	
	public static IntVector2 operator + (IntVector2 a, IntVector2 b){
		a.X += b.X;
		a.Z += b.Z;
		return a;
	}
}