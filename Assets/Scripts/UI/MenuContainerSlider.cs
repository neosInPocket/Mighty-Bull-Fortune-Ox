using System.Collections;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class MenuContainerSlider : MonoBehaviour
{
	[SerializeField] private RectTransform container;
	[SerializeField] private RectTransform pointer;
	[SerializeField] private float fadeTime;
	private float SettingsDestination = -1242f;
	private float MenuDestination = 0;
	private float StoreDestination = 1242f;
	private float currentVelocity;
	private float destination;
	private int direction;
	
	private float pointerSettingsDectination = 414f;
	private float pointerMenuDectination = 0;
	private float pointerStoreDectination = -414f;
	private float pointerDestination;
	private float currentPointerVelocity;
	
	private void SetFade(float goal, float pointerGoal)
	{
		StopAllCoroutines();
		destination = goal - container.transform.localPosition.x;
		pointerDestination = pointerGoal - pointer.transform.localPosition.x;
		
		currentVelocity = Mathf.Abs(destination) / fadeTime;
		currentPointerVelocity = Mathf.Abs(pointerDestination) / fadeTime;
		direction = (int)(destination / Mathf.Abs(destination));
		
		StartCoroutine(Fade(goal, pointerGoal));
	}
	
	public void Store()
	{
		SetFade(StoreDestination, pointerStoreDectination);
	}
	
	public void Menu()
	{
		SetFade(MenuDestination, pointerMenuDectination);
	}
	
	public void Settings()
	{
		SetFade(SettingsDestination, pointerSettingsDectination);
	}
	
	private IEnumerator Fade(float destination, float pointerDestination)
	{
		if (direction == -1)
		{
			while (container.transform.localPosition.x > destination)
			{
				float currentDistance = Mathf.Abs(destination - container.transform.localPosition.x);
				var newX = container.transform.localPosition.x - currentVelocity * (currentDistance + 40) / 1242;
				container.transform.localPosition = new Vector2(newX, container.transform.localPosition.y);
				
				float currentPointerDistance = Mathf.Abs(pointerDestination - pointer.transform.localPosition.x);
				var newPointerX = pointer.transform.localPosition.x + currentPointerVelocity * (currentPointerDistance + 13.333333f) / 414f;
				pointer.transform.localPosition = new Vector2(newPointerX, pointer.transform.localPosition.y);
				yield return new WaitForFixedUpdate();
			}
		}
		else
		{
			while (container.transform.localPosition.x < destination)
			{
				float currentDistance = destination - container.transform.localPosition.x;
				var newX = container.transform.localPosition.x + currentVelocity * (currentDistance + 40) / 1242;
				container.transform.localPosition = new Vector2(newX, container.transform.localPosition.y);
				
				float currentPointerDistance = Mathf.Abs(pointerDestination - pointer.transform.localPosition.x);
				var newPointerX = pointer.transform.localPosition.x - currentPointerVelocity * (currentPointerDistance + 13.333333f) / 414f;
				pointer.transform.localPosition = new Vector2(newPointerX, pointer.transform.localPosition.y);
				yield return new WaitForFixedUpdate();
			}
		}
		
		container.transform.localPosition = new Vector2(destination, container.transform.localPosition.y);
	}
}
