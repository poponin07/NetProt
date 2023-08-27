using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;

namespace Net
{
    public class PlayerController : MonoBehaviour, IPunObservable
    {
        private Rigidbody m_rb;
        private PlayerInput m_input;
        [SerializeField] private PhotonView m_photonView;
        [SerializeField] private GameObject m_bulletPrefab;
        [SerializeField] private Transform m_target;
        [SerializeField, Range(0f, 30f)] private float m_moveSpeed = 5f;
        [SerializeField, Range(0f, 30f)] private float m_maxMoveSpeed = 5f;
        [SerializeField, Range(0f, 100f)] private float m_health = 50f;
        [SerializeField, Range(0.1f, 10f)] private float m_rotatinDelay = 1f;

        [Space, SerializeField, Range(0.1f, 10f)]
        private float m_attackDelay = 0.5f;

        [SerializeField] private Transform m_firePoint;

        public float Health
        {
            get { return m_health;}
            set { m_health = value; }
        }

        private void Awake()
        {
            m_rb = gameObject.GetComponent<Rigidbody>();
            m_input = new PlayerInput();
            m_input.Player1.Enable();
            Health = m_health;
        }

        void FixedUpdate()
        {
            if (!m_photonView.IsMine) return;
                Vector2 direction = m_input.Player1.Movement.ReadValue<Vector2>();
            
            if (direction.x == 0 && direction.y == 0) return;

            var velocity = m_rb.velocity;

            velocity += new Vector3(direction.y, 0f, direction.x) * m_moveSpeed * Time.deltaTime;
            velocity.y = 0f;
            velocity = Vector3.ClampMagnitude(velocity, m_maxMoveSpeed);
            m_rb.velocity = velocity;
        }
        private void Start()
        {
            StartCoroutine(Focus());
            StartCoroutine(Fire());
        }
        
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(Debugger.PlayerData.Create(this));
            }
            else
            {
                ((Debugger.PlayerData)stream.ReceiveNext()).Set(this);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            ProjectileController bullet = other.gameObject.GetComponent<ProjectileController>();

            if (bullet == null) return;

            m_health -= bullet.GetDamage();

            if (m_health <= 0) Debug.Log($"Player with name {name} is dead!");
            
        }

        private  IEnumerator Focus()
        {
            while (true)
            {
                transform.LookAt(m_target);
                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
                yield return new WaitForSeconds(m_rotatinDelay);
            }
        }
        private  IEnumerator Fire()
        {
            while (true)
            {
                var bullet = Instantiate(m_bulletPrefab, m_firePoint.position, transform.rotation);
                yield return new WaitForSeconds(m_attackDelay); 
            }
        }
        
        private void OnDestroy()
        {
            m_input.Player1.Disable();
        }
    }
}