using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Net
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField, Range(0f, 30f)] private float m_moveSpeed = 3f;
        [SerializeField, Range(0f, 100f)] private float m_damage = 10f;
        [SerializeField, Range(0f, 30f)] private float m_lifeTime = 10f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
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

    }
}