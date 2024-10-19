using UnityEngine;

namespace NeinUtility
{

	public class PersistentBlank : MonoBehaviour
	{
		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
		}
	}
}