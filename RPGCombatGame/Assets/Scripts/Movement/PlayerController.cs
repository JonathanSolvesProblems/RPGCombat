using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        Mover mover;

        Fighter fighter;

        void Start()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
        }

        void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                if (target == null)
                    continue;

                if (Input.GetMouseButtonDown(0))
                {
                    fighter.Attack(target);
                }

                return true;
            }
            
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit); // getting position info where the raycast hit. - Raycast method bool

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    mover.StartMoveAction(hit.point);
                }

                return true;
            }

            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}

// mono tab, extract method shortcuts not working. Fix.