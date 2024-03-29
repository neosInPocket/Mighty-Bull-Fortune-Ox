using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class ChainGun : MonoBehaviour
{
	[Header("Scripts Ref:")]
	public ChainGrapple grappleRope;

	[Header("Layers Settings:")]
	[SerializeField] private bool grappleToAll = false;
	[SerializeField] private int grappableLayerNumber = 9;
	public Camera mainCamera => Camera.main;

	[Header("Transform Ref:")]
	public Transform gunHolder;
	public Transform gunPivot;
	public Transform firePoint;

	[Header("Physics Ref:")]
	public SpringJoint2D m_springJoint2D;
	public Rigidbody2D m_rigidbody;

	[Header("Rotation:")]
	[SerializeField] private bool rotateOverTime = true;
	[Range(0, 60)] [SerializeField] private float rotationSpeed = 4;
	
	private bool hasMaxDistance = true;
	private float maxDistnace = 20;
	public float MaxGrappleDistance
	{
		get => maxDistnace;
		set => maxDistnace = value;
	}
	private float minDistance = 0.1f;

	private enum LaunchType
	{
		Transform_Launch,
		Physics_Launch
	}

	[Header("Launching:")]
	[SerializeField] private bool launchToPoint = true;
	[SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
	[SerializeField] private float launchSpeed = 1;

	[Header("No Launch To Point")]
	[SerializeField] private bool autoConfigureDistance = false;
	[SerializeField] private float targetDistance = 3;
	[SerializeField] private float targetFrequncy = 1;

	[HideInInspector] public Vector2 grapplePoint;
	[HideInInspector] public Vector2 grappleDistanceVector;
	public bool isLaunched;
	private bool isSetGrapple;
	private Vector2 lastFingerPosition;
	
	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		Initialize();
		grappleRope.enabled = false;
		m_springJoint2D.enabled = false;
	}
	
	public void Initialize()
	{
		EnableHook();
	}
	
	public void EnableHook()
	{
		Touch.onFingerDown += SetEnabled;
		Touch.onFingerMove += SetFingerPosition;
		Touch.onFingerUp += SetDisabled;
	}
	public void DisableHook()
	{
		Touch.onFingerDown -= SetEnabled;
		Touch.onFingerMove -= SetFingerPosition;
		Touch.onFingerUp -= SetDisabled;
		Unhook();
	}
	
	public void Unhook()
	{
		isLaunched = false;
	}
	
	private void SetEnabled(Finger finger)
	{
		isLaunched = true;
		isSetGrapple = true;
		lastFingerPosition = finger.screenPosition;
	} 
	private void SetDisabled(Finger finger)
	{
		isLaunched = false;
	} 
	private void SetFingerPosition(Finger finger) => lastFingerPosition = finger.screenPosition;

	public void SimulateGrappleHook()
	{
		isSimulating = true;
		StartCoroutine(Simulate());
	}
	
	public void DisableSimulate()
	{
		isSimulating = false;
		StopAllCoroutines();
		isLaunched = false;
		m_rigidbody.velocity = Vector2.zero;
		m_rigidbody.angularVelocity = 0;
		transform.parent.parent.position = new Vector2(0, -4.16f);
	}
	
	private IEnumerator Simulate()
	{
		while (isSimulating)
		{
			lastFingerPosition = new Vector2(813f, 907f);
			isSetGrapple = true;
			isLaunched = true;
			
			yield return new WaitForSeconds(2);
			isLaunched = false;
			transform.parent.parent.position = new Vector2(0, -4.16f);
			m_rigidbody.velocity = Vector2.zero;
			m_rigidbody.angularVelocity = 0;
			yield return new WaitForSeconds(0.5f);
		}
	}
	
	public bool isSimulating;

	private void Update()
	{
		if (isSetGrapple)
		{
			SetGrapplePoint();
			isSetGrapple = false;
		}
		else if (isLaunched)
		{
			if (grappleRope.enabled)
			{
				RotateGun(grapplePoint, false);
			}
			else
			{
				Vector2 fingerPos = mainCamera.ScreenToWorldPoint(lastFingerPosition);
				RotateGun(fingerPos, true);
			}

			if (launchToPoint && grappleRope.isGrappling)
			{
				if (launchType == LaunchType.Transform_Launch)
				{
					Vector2 firePointDistnace = firePoint.position - gunHolder.localPosition;
					Vector2 targetPos = grapplePoint - firePointDistnace;
					gunHolder.position = Vector2.Lerp(gunHolder.position, targetPos, Time.deltaTime * launchSpeed);
				}
			}
		}
		else if (!isLaunched)
		{
			grappleRope.enabled = false;
			m_springJoint2D.enabled = false;
			m_rigidbody.gravityScale = 1;
		}
		else
		{
			Vector2 fingerPos = mainCamera.ScreenToWorldPoint(lastFingerPosition);
			RotateGun(fingerPos, true);
		}
	}

	void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
	{
		Vector3 distanceVector = lookPoint - gunPivot.position;

		float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
		if (rotateOverTime && allowRotationOverTime)
		{
			gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
		}
		else
		{
			gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
	}

	void SetGrapplePoint()
	{
		Vector2 distanceVector = mainCamera.ScreenToWorldPoint(lastFingerPosition) - gunPivot.position;
		if (Physics2D.Raycast(firePoint.position, distanceVector.normalized))
		{
			RaycastHit2D[] _hit = Physics2D.RaycastAll(firePoint.position, distanceVector.normalized);
			if (_hit[1].transform.gameObject.layer == grappableLayerNumber || grappleToAll)
			{
				var distance = Vector2.Distance(_hit[1].point, firePoint.position);
				if (distance <= minDistance) return;
				
				if (distance <= maxDistnace || !hasMaxDistance)
				{
					grapplePoint = _hit[1].point;
					grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
					grappleRope.enabled = true;
				}
			}
		}
	}

	public void Grapple()
	{
		m_springJoint2D.autoConfigureDistance = false;
		if (!launchToPoint && !autoConfigureDistance)
		{
			m_springJoint2D.distance = targetDistance;
			m_springJoint2D.frequency = targetFrequncy;
		}
		if (!launchToPoint)
		{
			if (autoConfigureDistance)
			{
				m_springJoint2D.autoConfigureDistance = true;
				m_springJoint2D.frequency = 0;
			}

			m_springJoint2D.connectedAnchor = grapplePoint;
			m_springJoint2D.enabled = true;
		}
		else
		{
			switch (launchType)
			{
				case LaunchType.Physics_Launch:
					m_springJoint2D.connectedAnchor = grapplePoint;

					Vector2 distanceVector = firePoint.position - gunHolder.position;

					m_springJoint2D.distance = distanceVector.magnitude;
					m_springJoint2D.frequency = launchSpeed;
					m_springJoint2D.enabled = true;
					break;
				case LaunchType.Transform_Launch:
					m_rigidbody.gravityScale = 0;
					m_rigidbody.velocity = Vector2.zero;
					break;
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (firePoint != null && hasMaxDistance)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(firePoint.position, maxDistnace);
		}
	}

}