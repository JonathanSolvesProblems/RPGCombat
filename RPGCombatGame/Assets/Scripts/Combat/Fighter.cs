using UnityEngine;
using RPG.Movement;
using RPG.Core;
using UnityEngine.UIElements;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float timeBtwAttacks = 1f;
        [SerializeField] private float weaponDamage = 5f;

        private float timeSinceLastAttack = 0;
        private Transform target;
        private Mover mover;
        private ActionScheduler actionScheduler;
        private Animator animator;
        private Health health;


        void Start()
        {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
            target = GameObject.Find("Enemy").transform;
            health = target.GetComponent<Health>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            
            if (target == null) return;

            if (!GetIsInRange()) // bug to fix, always moves to target.
            {
                mover.MoveTo(target.position);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            // throttling attacks
            if (timeSinceLastAttack > timeBtwAttacks)
            {
                // will trigger Hit() event
                animator.SetTrigger("attack");
                timeSinceLastAttack = 0;
                
            }
        }
        
        // hit animation event
        void Hit()
        {
            health.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            actionScheduler.StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }
    }
}