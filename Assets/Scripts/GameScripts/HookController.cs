using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using FingerTouch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class HookController : MonoBehaviour
{
	[SerializeField] private PlayerUpgradesSO playerUpgrades;
	[SerializeField] private HookPointerController hookPointer;
	[SerializeField] private SpriteRenderer hook;
	[SerializeField] private PlayerController player;
	[SerializeField] private float hookSpeed; // remove serializefield
	[SerializeField] private float maxHookDistance; // remove serializefield
	private bool isShooted;
	private bool isReachedMax;
	private bool isHooked;
	private Vector2 currentContantPoint;
	private Rigidbody2D PlayerRigid => player.Rigidbody; 
	
	private void Start()
	{
		// add scriptable objects value
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		OnControls();
		player.OnPlatformTouched += PlayerPlatformTouchedHandler;
	}
	
	private void PlayerPlatformTouchedHandler()
	{
		isHooked = false;
		PlayerRigid.gravityScale = 1;
		PlayerRigid.velocity = Vector2.zero;
	}
	
	private void Update()
	{
		if (!isShooted) return;
		
		if (!isReachedMax)
		{
			hook.size = new Vector2(hook.size.x, hook.size.y + hookSpeed * Time.deltaTime);
			if (hook.size.y >= maxHookDistance)
			{
				hook.size = new Vector2(hook.size.x, maxHookDistance);
				isReachedMax = true;
			}
		}
		
		if (isReachedMax)
		{
			hook.size = new Vector2(hook.size.x, hook.size.y - hookSpeed * Time.deltaTime);
			if (hook.size.y <= 0)
			{
				hook.size = new Vector2(hook.size.x, 0);
				isShooted = false;
				isReachedMax = false;
				hookPointer.EnablePointerRotation();
			}
			
			if (isHooked)
			{
				var transformVector = new Vector2(transform.position.x, transform.position.y);
				player.Rigidbody.velocity = hookSpeed * (currentContantPoint - transformVector).normalized;
			}
		}
	}
	
	public void OnControls()
	{
		FingerTouch.onFingerDown += ShootHook;
	}
	
	public void OffControls()
	{
		FingerTouch.onFingerDown -= ShootHook;
	}
	
	private void ShootHook(Finger finger)
	{
		hookPointer.DisablePointerRotation();
		isShooted = true;
	}
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.TryGetComponent<PlatformController>(out PlatformController platform))
		{
			currentContantPoint = collision.GetContact(0).point;
			player.Rigidbody.gravityScale = 0;
			
			isHooked = true;
			isReachedMax = true;
		}
	}
}
