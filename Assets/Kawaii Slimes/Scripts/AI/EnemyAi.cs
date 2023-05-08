using UnityEngine;
using UnityEngine.AI;

public enum SlimeAnimationState { Idle, Walk, Jump, Attack, Damage }

public class EnemyAi : MonoBehaviour
{
    public Face faces;
    public GameObject SlimeBody;
    public SlimeAnimationState currentState;

    public Animator animator;
    public NavMeshAgent agent;
    public Transform[] waypoints;
    public int damType;

    private int currentWaypointIndex;
    private bool move;
    private Material faceMaterial;
    private Vector3 originalPos;

    public enum WalkType { Patrol, ToOrigin }
    private WalkType walkType;

    // Add jump interval and timer variables
    public float minJumpInterval = 5f;
    public float maxJumpInterval = 10f;
    private float jumpTimer;

    void Start()
    {
        originalPos = transform.position;
        faceMaterial = SlimeBody.GetComponent<Renderer>().materials[1];
        walkType = WalkType.Patrol;

        // Initialize jumpTimer
        jumpTimer = Random.Range(minJumpInterval, maxJumpInterval);
    }

    public void WalkToNextDestination()
    {
        currentState = SlimeAnimationState.Walk;
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
        SetFace(faces.WalkFace);
    }

    public void CancelGoNextDestination() => CancelInvoke(nameof(WalkToNextDestination));

    void SetFace(Texture tex)
    {
        faceMaterial.SetTexture("_MainTex", tex);
    }

    void Update()
    {
        // Update jump timer and check if it's time to jump
        jumpTimer -= Time.deltaTime;
        if (jumpTimer <= 0f)
        {
            RandomJump();
        }

        switch (currentState)
        {
            case SlimeAnimationState.Idle:
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) return;
                StopAgent();
                SetFace(faces.Idleface);
                break;

            case SlimeAnimationState.Walk:
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")) return;

                agent.isStopped = false;
                agent.updateRotation = true;

                if (walkType == WalkType.ToOrigin)
                {
                    agent.SetDestination(originalPos);
                    SetFace(faces.WalkFace);

                    if (agent.remainingDistance < agent.stoppingDistance)
                    {
                        walkType = WalkType.Patrol;
                        transform.rotation = Quaternion.identity;
                        currentState = SlimeAnimationState.Idle;
                    }
                }
                else
                {
                    if (waypoints[0] == null) return;

                    agent.SetDestination(waypoints[currentWaypointIndex].position);

                    if (agent.remainingDistance < agent.stoppingDistance)
                    {
                        currentState = SlimeAnimationState.Idle;
                        Invoke(nameof(WalkToNextDestination), 2f);
                    }
                }

                animator.SetFloat("Speed", agent.velocity.magnitude);
                break;

            case SlimeAnimationState.Jump:
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) return;

                StopAgent();
                SetFace(faces.jumpFace);
                animator.SetTrigger("Jump");
                break;

            case SlimeAnimationState.Attack:
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) return;

                StopAgent();
                SetFace(faces.attackFace);
                animator.SetTrigger("Attack");
                break;

            case SlimeAnimationState.Damage:
                                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Damage0")
                     || animator.GetCurrentAnimatorStateInfo(0).IsName("Damage1")
                     || animator.GetCurrentAnimatorStateInfo(0).IsName("Damage2")) return;

                StopAgent();
                animator.SetTrigger("Damage");
                animator.SetInteger("DamageType", damType);
                SetFace(faces.damageFace);
                break;
        }
    }

    private void RandomJump()
    {
        if (currentState == SlimeAnimationState.Idle || currentState == SlimeAnimationState.Walk)
        {
            currentState = SlimeAnimationState.Jump;
            jumpTimer = Random.Range(minJumpInterval, maxJumpInterval);
        }
    }

    private void StopAgent()
    {
        agent.isStopped = true;
        animator.SetFloat("Speed", 0);
        agent.updateRotation = false;
    }

    public void AlertObservers(string message)
    {
        if (message.Equals("AnimationDamageEnded"))
        {
            float distanceOrg = Vector3.Distance(transform.position, originalPos);
            if (distanceOrg > 1f)
            {
                walkType = WalkType.ToOrigin;
                currentState = SlimeAnimationState.Walk;
            }
            else currentState = SlimeAnimationState.Idle;
        }

        if (message.Equals("AnimationAttackEnded"))
        {
            currentState = SlimeAnimationState.Idle;
        }

        if (message.Equals("AnimationJumpEnded"))
        {
            currentState = SlimeAnimationState.Idle;
        }
    }

    void OnAnimatorMove()
    {
        Vector3 position = animator.rootPosition;
        position.y = agent.nextPosition.y;
        transform.position = position;
        agent.nextPosition = transform.position;
    }
}
