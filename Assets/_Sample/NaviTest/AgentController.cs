using UnityEngine;
using UnityEngine.AI;

namespace MySample
{
    //마우스로 그라운드를 클릭하면 클릭한 지점으로 Agent를 이동
    public class AgentController : MonoBehaviour
    {
        #region Variables
        private NavMeshAgent agent;

        [SerializeField] private Vector3 worldPosition;     //이동 목표지점
        #endregion

        private void Start()
        {
            //참조
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                SetDestinationToMousePosition();

                //worldPosition = RayToWorldPosition();
                //agent.SetDestination(worldPosition);
            }
        }

        void SetDestinationToMousePosition()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        //Vector3 RayToWorldPosition()
        //{
        //    Vector3 hitposition = Vector3.zero;
        //    RaycastHit hit;
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        hitposition = hit.point;
        //    }

        //    return hitposition;
        //}

    }
}