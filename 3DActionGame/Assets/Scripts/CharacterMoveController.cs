using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveController : MonoBehaviour
{
    public float Speed = 6f;

    private Animator m_animator = null;
    private Rigidbody m_rigidbody = null;
    private Vector3 m_direction = Vector3.zero;

    private CharacterController m_characterController = null;
    private float m_turnSmoothVerocity = 0f;
    private float m_turnSmoothTime = 0.1f;

    private float m_gravityValue = -9.81f;
    private Vector3 m_playerGravityVelocity = Vector3.zero;

    private bool m_jumpFlag = false;
    private bool m_punchzFlag = false;
    private bool m_punchxFlag = false;
    private bool m_punchcFlag = false;
    private bool m_canWalk = true;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody>();
        m_characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        m_jumpFlag = Input.GetKeyDown(KeyCode.V);
        m_punchzFlag = Input.GetKeyDown(KeyCode.Z);
        m_punchxFlag = Input.GetKeyDown(KeyCode.X);
        m_punchcFlag = Input.GetKeyDown(KeyCode.C);
        
        Move(h, v);
    }

    


    private void Move(float horizontal, float vertical)
    {
        if (m_punchzFlag)
        {
            m_animator.SetTrigger("Punchz");
            StartCoroutine(StopWalk("Punchz"));
        }

        if (m_punchxFlag)
        {
            m_animator.SetTrigger("Punchx");
            StartCoroutine(StopWalk("Punchx"));
        }

        if (m_punchcFlag)
        {
            m_animator.SetTrigger("Punchc");
            StartCoroutine(StopWalk("Punchc"));
        }


        if (m_jumpFlag && m_characterController.isGrounded)
        {
            m_playerGravityVelocity.y = 6;
            m_animator.SetTrigger("Jump");
            m_characterController.Move(m_playerGravityVelocity * Time.deltaTime);
            m_jumpFlag = false;
        }

        if (m_canWalk)
        {
            var cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0;
            cameraForward = cameraForward.normalized;
            if (cameraForward.sqrMagnitude < 0.01f)
                return;

            Quaternion inputFrame = Quaternion.LookRotation(cameraForward, Vector3.up);
            var input = new Vector3(horizontal, 0, vertical);
            var cameraFromPlayer = inputFrame * input;

            if (cameraFromPlayer.sqrMagnitude >= 0.1f)
            {
                var targetAngle = Mathf.Atan2(cameraFromPlayer.x, cameraFromPlayer.z) * Mathf.Rad2Deg;
                var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_turnSmoothVerocity, m_turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                m_characterController.Move(cameraFromPlayer * Speed * Time.deltaTime);
            }

            if (!m_jumpFlag && !m_characterController.isGrounded)
            {
                m_playerGravityVelocity.y += m_gravityValue * Time.deltaTime * 2;
                m_characterController.Move(m_playerGravityVelocity * Time.deltaTime);
            }

            m_playerGravityVelocity.y += m_gravityValue * Time.deltaTime;
            m_characterController.Move(m_playerGravityVelocity * Time.deltaTime);

            m_animator.SetFloat("FrontVelocity", cameraFromPlayer.magnitude);
        }
    }


    public void FootR()
    {

    }
    public void FootL()
    {

    }

    private IEnumerator StopWalk(string animationName)
    {
        m_canWalk = false;

        yield return new WaitWhile(() => !m_animator.GetCurrentAnimatorStateInfo(0).IsName(animationName));

        yield return new WaitWhile(() => m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f);

        m_canWalk = true;
    }
}
