using Photon.Pun;
using UnityEngine;

public class PlayerAnimator : MonoBehaviourPun
{
    private PlayerController _controller;
    private Animator anim;
    private SpriteRenderer spriteRend;

    [Header("Particle FX")]
    [SerializeField] private GameObject jumpFX;
    [SerializeField] private GameObject landFX;

    public bool startedJumping {  private get; set; }
    public bool justLanded { private get; set; }
    public bool isOnWall { private get; set; }

    public float currentVelY;

    private void Start()
    {
        _controller = GetComponent<PlayerController>();
        spriteRend = GetComponentInChildren<SpriteRenderer>();
        anim = spriteRend.GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        if (!photonView.IsMine) return;
        photonView.RPC("CheckAnimationState", RpcTarget.All);
    }
    
    [PunRPC]
    private void CheckAnimationState()
    {
        anim.SetBool("Wall", isOnWall);
        
        if (startedJumping)
        {
            anim.SetTrigger("Jump");
            GameObject obj = Instantiate(jumpFX, transform.position - (Vector3.up * transform.localScale.y / 2), Quaternion.Euler(-90, 0, 0));
            Destroy(obj, 1);
            startedJumping = false;
            return;
        }

        if (justLanded)
        {
            anim.SetTrigger("Land");
            GameObject obj = Instantiate(landFX, transform.position - (Vector3.up * transform.localScale.y / 1.5f), Quaternion.Euler(-90, 0, 0));
            Destroy(obj, 1);
            justLanded = false;
            return;
        }

        anim.SetFloat("Vel Y", _controller.Rigidbody.velocity.y);
        anim.SetFloat("Vel X", Mathf.Abs(_controller.Rigidbody.velocity.x));
    }
}