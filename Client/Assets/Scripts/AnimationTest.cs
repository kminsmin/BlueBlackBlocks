using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(Test());
    }

    IEnumerator Test()
    {

        _anim.SetBool("Run", true);
        yield return new WaitForSeconds(2f);
        _anim.SetBool("Run", false);

        _anim.SetBool("Jump", true);
        yield return new WaitForSeconds(2f);
        _anim.SetBool("Jump", false);

        _anim.SetBool("WallJump", true);
        yield return new WaitForSeconds(2f);
        _anim.SetBool("WallJump", false);

        _anim.SetTrigger("Hit");
        yield return new WaitForSeconds(2f);

        _anim.SetTrigger("Desappearing");
        yield return new WaitForSeconds(2f);
        
    }
}
