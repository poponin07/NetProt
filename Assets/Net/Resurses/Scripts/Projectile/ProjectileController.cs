using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Net
{
    public class ProjectileController : MonoBehaviourPunCallbacks//, IPunObservable
    {
        [SerializeField, Range(0f, 30f)] private float m_moveSpeed = 3f;
        [SerializeField, Range(0f, 100f)] private float m_damage = 10f;
        [SerializeField, Range(0f, 30f)] private float m_lifeTime = 10f;
        
        void Start()
        {
            StartCoroutine(OnDie());
        }
        
        
        void FixedUpdate()
        {
            transform.position += transform.forward * m_moveSpeed * Time.deltaTime;
        }

        public float GetDamage()
        {
            Destroy(gameObject);
            return m_damage;
        }

        private IEnumerator OnDie()
        {
            yield return new WaitForSeconds(m_lifeTime);
            Destroy(gameObject);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                
                stream.SendNext(transform.position);
            }
            else
            {
                transform.position = (Vector3)stream.ReceiveNext();
            }
        }
    }
}