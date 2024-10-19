using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeinUtility
{
	public static class Utility
	{

		public static bool IsMouseOverUI => UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();

		public static void PrintArray<T>(T[] _array, bool _concatanate = false, string _seperator = ", ")
		{
			if (!_concatanate)
			{
				for (int i = 0; i < _array.Length; i++)
					Debug.Log(_array[i]);
			}
			else
			{
				string toPrint = "";

				for (int i = 0; i < _array.Length; i++)
				{
					toPrint += System.Convert.ToString(_array[i]);
					if (i < _array.Length - 1)
						toPrint += _seperator;
				}

				Debug.Log(toPrint);
			}
		}

	}
}
