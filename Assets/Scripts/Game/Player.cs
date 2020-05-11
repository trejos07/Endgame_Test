using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;
using System;

public class Player : Character
{
    [SerializeField]private float movSpeed = 10F;
    [SerializeField]private float shotingSpeed = 6;

    private float hVal = 0F;
    private float vVal = 0F;
    private float pitchVal = 0F;
    private Coroutine shooting;
    private bool haveKey=false;
    private bool aiming=false;

    public bool HaveKey { get => haveKey; set => haveKey = value; }

    protected override void Start()
    {
        base.Start();
        try{
            VariableJoystick[] joysticks = FindObjectsOfType<VariableJoystick>();
            joysticks.First(j => j.name == "Aim_Joystick").OnJoysticInput += AimTo;
            joysticks.First(j => j.name == "Walk_Joystick").OnJoysticInput += MoveTo;
        }
        catch (Exception e) { Debug.LogError(e); }
        mRenderer.material.SetColor("_Color1", new Color(.12f,.53f,.95f));
        mAnimator.SetFloat("moving", 0);
        ShootFlashVFX.Stop();
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        mAnimator.SetBool("shooting", true);
    //        mAnimator.SetLayerWeight(1, 1);
    //        ShootFlashVFX.Play();

    //    }
    //    if (Input.GetKeyUp(KeyCode.Space))
    //    {
    //        mAnimator.SetBool("shooting", false);
    //        mAnimator.SetLayerWeight(1, 0);
    //        ShootFlashVFX.Stop();
    //    }
    //}

    public void AimTo(Vector2 _aimDir){
        if (_aimDir.magnitude != 0){
            aiming = true;
            float rotY = Mathf.Atan2(_aimDir.x, _aimDir.y) * Mathf.Rad2Deg - 180;
            Quaternion targetRot = Quaternion.Euler(transform.rotation.x, rotY, transform.rotation.z);
            mRigidbody.MoveRotation(Quaternion.Slerp(mRigidbody.rotation, targetRot, RotSpeed * Time.deltaTime));
            mAnimator.SetBool("shooting", true);
            mAnimator.SetLayerWeight(1, 1);
            ShootFlashVFX.Play();
        }else{
            aiming = false;
            mAnimator.SetBool("shooting", false);
            mAnimator.SetLayerWeight(1, 0);
            ShootFlashVFX.Stop();
        }
    }
    public void MoveTo(Vector2 _movDir){
        Vector3 input = new Vector3(_movDir.x, 0, _movDir.y);
        _movDir = _movDir.magnitude > 1 ? _movDir.normalized : _movDir;
        mAnimator.SetFloat("moving", _movDir.magnitude);

        if (_movDir.magnitude != 0){
            Vector3 newPos = transform.position + input * movSpeed * Time.deltaTime;
            mRigidbody.MovePosition(newPos);
            if (!aiming)
            {
                float rotY = Mathf.Atan2(_movDir.x, _movDir.y) * Mathf.Rad2Deg - 180;
                Quaternion targetRot = Quaternion.Euler(transform.rotation.x, rotY, transform.rotation.z);
                mRigidbody.MoveRotation(Quaternion.Slerp(mRigidbody.rotation, targetRot, RotSpeed * Time.deltaTime));
            }
        }
    }

    protected override void OnDeath()
    {
        try{
            VariableJoystick[] joysticks = FindObjectsOfType<VariableJoystick>();
            joysticks.First(j => j.name == "Aim_Joystick").OnJoysticInput -= AimTo;
            joysticks.First(j => j.name == "Walk_Joystick").OnJoysticInput -= MoveTo;
        }
        catch (Exception e) { Debug.LogError(e); }
        base.OnDeath();
        SceneManager.LoadScene(0);
    }

}