using UnityEngine;

namespace Lightbug.GrabIt
{

[System.Serializable]
public class GrabObjectProperties{
	
	public bool m_useGravity = false;
	public float m_drag = 10;
	public float m_angularDrag = 10;
	public RigidbodyConstraints m_constraints = RigidbodyConstraints.FreezeRotation;		

}

	public class GrabIt : MonoBehaviour {

		[Header("Input")]
		[SerializeField] KeyCode m_rotatePitchPosKey = KeyCode.I;
		[SerializeField] KeyCode m_rotatePitchNegKey = KeyCode.K;
		[SerializeField] KeyCode m_rotateYawPosKey = KeyCode.L;
		[SerializeField] KeyCode m_rotateYawNegKey = KeyCode.J;

		[Header("Grab properties")]

		[SerializeField]
		[Range(4, 50)]
		float m_grabSpeed = 7;

		[SerializeField]
		[Range(0.1f, 5)]
		float m_grabMinDistance = 1;

		[SerializeField]
		[Range(4, 25)]
		float m_grabMaxDistance = 10;

		[SerializeField]
		float m_scrollWheelSpeed = 5;

		[SerializeField]
		[Range(50, 500)]
		float m_angularSpeed = 300;

		[SerializeField]
		[Range(10, 50)]
		float m_impulseMagnitude = 25;




		[Header("Affected Rigidbody Properties")]
		[SerializeField] GrabObjectProperties m_grabProperties = new GrabObjectProperties();

		GrabObjectProperties m_defaultProperties = new GrabObjectProperties();

		[Header("Layers")]
		[SerializeField]
		LayerMask m_collisionMask;



		Rigidbody m_targetRB = null;
		Transform m_transform;

		Vector3 m_targetPos;
		GameObject m_hitPointObject;
		float m_targetDistance;

		public bool m_grabbing = false;
		public bool m_hoveringGrab = false;
		bool m_applyImpulse = false;
		bool m_isHingeJoint = false;

		bool mouseButtonUp = true;

		//Debug
		LineRenderer m_lineRenderer;

		//MY CODE
		public MousePos3D mouseScript;
		public QuestBoardCheck questBoardScript;
		public movecam moveCamScript;
		public TutorialScript ts;

		private float scroll;

		public static GrabIt instance;

		void Awake()
		{
			instance = this;

			//m_transform = transform;
			m_transform = mouseScript.mousePos;
			m_hitPointObject = new GameObject("Point");

			m_lineRenderer = GetComponent<LineRenderer>();
		}


		void Update()
		{
			canGrabCheck();

			if (m_grabbing)//if already holding an object
			{
                //m_targetDistance += Input.GetAxisRaw("Mouse ScrollWheel") * m_scrollWheelSpeed;
                //m_targetDistance = Mathf.Clamp(m_targetDistance, m_grabMinDistance, m_grabMaxDistance);

                scroll = Input.GetAxis("Mouse ScrollWheel") * m_scrollWheelSpeed;

                m_targetPos = m_transform.position + new Vector3(0,0,1) * m_targetDistance;
				//Debug.Log(m_transform.forward);
				//m_targetPos = m_transform.position + m_transform.forward;

				if (!m_isHingeJoint) { // no need for this 
					if (Input.GetKey(m_rotatePitchPosKey) || Input.GetKey(m_rotatePitchNegKey)) {
						m_targetRB.constraints = RigidbodyConstraints.None;
					} else {
						m_targetRB.constraints = m_grabProperties.m_constraints;
					}
					//Debug.Log("no hinge");
				}
				else
				{
                    //Debug.Log("yes hinge");
                }


				if (Input.GetMouseButtonUp(0)) {
					//if the cam is moving
					if (!moveCamScript.canMoveCam)
					{
                        mouseButtonUp = true;//my code
                    }
					else
					{
                        Reset();
                        m_grabbing = false;
                    }

				} else if (Input.GetMouseButtonDown(1)) {
					m_applyImpulse = true;
				}
			}
			else //if not holding an object
			{

				if (Input.GetMouseButtonDown(0))
				{
					mouseButtonUp = false; //my code

					//Debug.Log("Pressed Button Not grabbing");
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // send out ray at position of mouse
					RaycastHit hitInfo;
					if (Physics.Raycast(ray, out hitInfo, m_grabMaxDistance, m_collisionMask))
					{
						//Debug.Log(hitInfo.collider.name);

						if (hitInfo.collider.gameObject.CompareTag("Quest"))
						{
                            Rigidbody rb = hitInfo.collider.transform.parent.GetComponent<Rigidbody>();

							//TUTORIAL STUFF
							if (ts.tutP1)
							{
								ts.holdingQuest = true;
                                moveCamScript.dontFlash = false;
								if (ts.questsOnBoard && !moveCamScript.flashOn)
								{
                                    moveCamScript.ButtonFlashUp(moveCamScript.leftButton);
                                }

                            }


                            if (rb != null)
                            {
                                Set(rb, hitInfo.distance);
                                //Debug.Log(hitInfo.distance);
                                m_grabbing = true;
                            }
                        }
						else
						{
                            Rigidbody rb = hitInfo.collider.GetComponent<Rigidbody>();

                            if (rb != null)
                            {
                                Set(rb, hitInfo.distance);
                                //Debug.Log(hitInfo.distance);
                                m_grabbing = true;
                            }
                        }
                        //Rigidbody rb = hitInfo.collider.GetComponent<Rigidbody>();

					}
				}
			}

		}

		void Set(Rigidbody target, float distance)
		{
			//back to norm
            m_targetRB = target;

			Debug.Log(m_targetRB.gameObject.name);

			m_targetRB.gameObject.GetComponent<Billboard>().isHeld = true;

			if (m_targetRB.CompareTag("Quest"))
			{
				m_targetRB.useGravity = true;

                m_targetRB.constraints = RigidbodyConstraints.None;
				m_targetRB.constraints = RigidbodyConstraints.FreezeRotation;

                // Get all colliders attached to this GameObject
                Collider[] colliderz = m_targetRB.gameObject.GetComponents<Collider>();

                // Disable each collider
                foreach (Collider col in colliderz)
                {
                    //col.isTrigger = false;
                }

                Collider colliderchild = m_targetRB.transform.GetChild(1).GetComponent<Collider>();

                colliderchild.isTrigger = true;

				m_targetRB.transform.GetChild(1).gameObject.layer = LayerMask.NameToLayer("PickedUp");

            }

			else
			{
                m_targetRB.gameObject.layer = LayerMask.NameToLayer("PickedUp");
            }

			//m_targetRB.gameObject.GetComponent<BoxCollider>().enabled = false; //THIS WAS THE FIRST COLLIDER CODE


            // Get all colliders attached to this GameObject
            Collider[] colliders = m_targetRB.gameObject.GetComponents<Collider>();

            // Disable each collider
            foreach (Collider col in colliders)
            {
				//col.enabled = false;
				col.isTrigger = true;
            }


            //m_targetRB.gameObject.layer = LayerMask.NameToLayer("PickedUp");//OLD
            //m_targetRB.gameObject.tag = "PickedUp";
            //m_targetRB.gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("PickedUp");
            //m_targetRB.gameObject.layer = LayerMask.NameToLayer("PickedUp");


            m_isHingeJoint = target.GetComponent<HingeJoint>() != null; // no need

			//Rigidbody default properties
			m_defaultProperties.m_useGravity = m_targetRB.useGravity;
			m_defaultProperties.m_drag = m_targetRB.drag;
			m_defaultProperties.m_angularDrag = m_targetRB.angularDrag;
			m_defaultProperties.m_constraints = m_targetRB.constraints;

			//Grab Properties	
			m_targetRB.useGravity = m_grabProperties.m_useGravity;
			m_targetRB.drag = m_grabProperties.m_drag;
			//m_targetRB.drag = m_targetRB.drag * 100;
			m_targetRB.angularDrag = m_grabProperties.m_angularDrag;
			m_targetRB.constraints = m_isHingeJoint ? RigidbodyConstraints.None : m_grabProperties.m_constraints;


			m_hitPointObject.transform.SetParent(target.transform);

			m_targetDistance = distance;
			m_targetPos = m_transform.position + m_transform.forward * m_targetDistance;

			m_hitPointObject.transform.position = m_targetPos;
			m_hitPointObject.transform.LookAt(m_transform.position);
		}

		void Reset()
		{
            m_targetRB.gameObject.GetComponent<Billboard>().isHeld = false;
			//m_targetRB.gameObject.layer = LayerMask.NameToLayer("GrabIt");

			if (m_targetRB.CompareTag("Quest"))
			{
                m_targetRB.transform.GetChild(1).gameObject.layer = LayerMask.NameToLayer("GrabIt");

                //TUTORIAL STUFF
                if (ts.tutP1)
				{
					ts.holdingQuest = false;
                    moveCamScript.dontFlash = true;
					moveCamScript.TurnFlashOff();
                }
            }
			else
			{
                m_targetRB.gameObject.layer = LayerMask.NameToLayer("GrabIt");
            }

            //m_targetRB.gameObject.GetComponent<BoxCollider>().enabled = true; //FIRST COLLIDER CODE

            // Get all colliders attached to this GameObject
            Collider[] colliders = m_targetRB.gameObject.GetComponents<Collider>();

            // Disable each collider
            foreach (Collider col in colliders)
            {
				//col.enabled = true;
				col.isTrigger = false;
            }

            //m_targetRB.gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Default");

			//Grab Properties	
			m_targetRB.useGravity = m_defaultProperties.m_useGravity;
			m_targetRB.drag = m_defaultProperties.m_drag;
			m_targetRB.angularDrag = m_defaultProperties.m_angularDrag;
			m_targetRB.constraints = m_defaultProperties.m_constraints;

            if (m_targetRB.CompareTag("Quest") && m_targetRB.transform.GetChild(0).GetComponent<QuestBoardCheck>().onBoard)
            {

				Debug.Log("Onboard");
				//m_targetRB.isKinematic = true;
				m_targetRB.useGravity = false;
				m_targetRB.velocity = Vector3.zero;

                m_targetRB.constraints = RigidbodyConstraints.FreezeAll;

                // Get all colliders attached to this GameObject
                Collider[] colliderz = m_targetRB.gameObject.GetComponents<Collider>();

                // Disable each collider
                foreach (Collider col in colliderz)
                {
					//col.isTrigger = true;
                }


            }

			else if (m_targetRB.CompareTag("Quest") && !m_targetRB.transform.GetChild(0).GetComponent<QuestBoardCheck>().onBoard)
			{
                m_targetRB.gameObject.GetComponent<Collider>().isTrigger = true;

                Collider colliderchild = m_targetRB.transform.GetChild(1).GetComponent<Collider>();

                colliderchild.isTrigger = false;
            }

            m_targetRB = null;

			m_hitPointObject.transform.SetParent(null);

			if (m_lineRenderer != null)
				m_lineRenderer.enabled = false;


        }

		void Grab()
		{
			Vector3 hitPointPos = m_hitPointObject.transform.position;
            //Vector3 dif = m_targetPos - hitPointPos;
            Vector3 dif = m_transform.position - m_targetRB.position; //YYYEEEESSSS

			if (m_isHingeJoint)
				m_targetRB.AddForceAtPosition(m_grabSpeed * dif * 100, hitPointPos, ForceMode.Force);
			else
				m_targetRB.velocity = m_grabSpeed * dif;


			if (m_lineRenderer != null) {
				m_lineRenderer.enabled = true;
				m_lineRenderer.SetPositions(new Vector3[] { m_targetPos, hitPointPos });
			}
		}

		void Rotate()
		{
            if (Input.GetKey(m_rotatePitchPosKey)) {
            	m_targetRB.AddTorque(m_transform.right * m_angularSpeed);
            } else if (Input.GetKey(m_rotatePitchNegKey)) {
            	m_targetRB.AddTorque(-m_transform.right * m_angularSpeed);
            }

            //if (Input.GetKey(m_rotateYawPosKey)) {
            //	m_targetRB.AddTorque(-m_transform.up * m_angularSpeed);
            //} else if (Input.GetKey(m_rotateYawNegKey)) {
            //	m_targetRB.AddTorque(m_transform.up * m_angularSpeed);
            //}

        }

		void FixedUpdate()
		{
			if (!m_grabbing)
				return;

			if (!m_isHingeJoint)
				Rotate();

			Grab();

			if (m_applyImpulse) {
				m_targetRB.velocity = m_transform.forward * m_impulseMagnitude;
				Reset();
				m_grabbing = false;
				m_applyImpulse = false;
			}

		}


		public void CamMoveCheck()
		{
			if (mouseButtonUp)
			{
                Reset();
                m_grabbing = false;
            }
        }

		private void canGrabCheck()
		{
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // send out ray at position of mouse
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, m_grabMaxDistance, m_collisionMask))
            {
				//hovering grabable object!
				if (!m_grabbing)
				{
                    m_hoveringGrab = true;
                }

            }
            else
            {
                m_hoveringGrab = false;
            }
        }
	}

}
