using UnityEngine;

namespace BlazeAIDemo
{
    public class Health : MonoBehaviour
    {
        BlazeAI RobotAi;
        public float maxHealth = 100;
        public float currentHealth { get; set; }


        void Start()
        {
            RobotAi = GetComponent<BlazeAI>();
            currentHealth = maxHealth;

        }

        private void Update()
        {
            if (maxHealth <= 0)
            {
                RobotAi.Death();
            }
        }
    }
}
