using UnityEngine;

public class Following : MonoBehaviour
{
	public Transform target;

	private void LateUpdate()
	{
		if (target == null)
		{
			return;
		}

		if (target.position.y > transform.position.y)
		{
			transform.position = new Vector2(0, target.position.y);
		}
	}
}
