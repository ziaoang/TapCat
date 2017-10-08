using System;
using UnityEngine;

public class BigInt {
	static string[] symbol = new string[]{"","A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"};

	int[] data;
	int maxIndex;

	public BigInt() {
		data = new int[symbol.Length];
		maxIndex = 0;
	}

	public BigInt(string s) {
		data = new int[symbol.Length];
		maxIndex = 0;

		char lastChar = s [s.Length - 1];
		if (lastChar >= 'A' && lastChar <= 'Z') {
			maxIndex = lastChar - 'A' + 1;
			string[] t = s.Substring(0, s.Length - 1).Split('.');
			data [maxIndex] = int.Parse(t[0]);
			data [maxIndex-1] = int.Parse(t[1]);
		} else {
			data [0] = int.Parse (s);
		}
	}

	public static BigInt operator + (BigInt left, BigInt right) {
		BigInt sum = new BigInt ();
		sum.maxIndex = Math.Max (left.maxIndex, right.maxIndex);

		int op = 0;
		for (int i = 0; i <= sum.maxIndex; i++) {
			op += left.data [i] + right.data [i];
			sum.data [i] = op % 1000;
			op /= 1000;
		}

		if (op > 0) {
			if (sum.maxIndex + 1 < symbol.Length) {
				sum.maxIndex ++;
				sum.data [sum.maxIndex] = op;
			} else {
				for (int i = 0; i <= sum.maxIndex; i++) {
					sum.data [i] = 999;
				}
			}
		}
		
		return sum;
	}

	public override string ToString() {
		if (maxIndex == 0) {
			return data [0].ToString ();
		}
		return string.Format("{0}.{1:D3}{2}", data[maxIndex], data[maxIndex-1], symbol[maxIndex]);
	}
}


