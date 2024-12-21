using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/Ability/Move")]
    public class Move : StateData
    {
        [SerializeField] private float BlockDistance;
        [SerializeField] private AnimationCurve SpeedGraph;
        [SerializeField] private float Speed;
        [SerializeField] private bool IsConstantMove;
        [SerializeField] private bool LockTurn180Deg;
        [SerializeField] private bool AllowEarlyTurn;
        public bool LockDirectionNextState;

        [Header("Momentum")]
        public bool UseMomentum;
        public float StartingMomentum;
        public float MaxMomentum;
        public bool ClearMomentumOnExit;

        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (AllowEarlyTurn && !characterControl.AnimProgress.disAllowEarlyTurn)
            {
                if (!characterControl.AnimProgress.LockDirectionNextState)
                {
                    if (characterControl.MoveLeft)
                    {
                        characterControl.SetFaceForward(false);
                    }

                    if (characterControl.MoveRight)
                    {
                        characterControl.SetFaceForward(true);
                    }
                }
                else
                {
                    characterControl.AnimProgress.LockDirectionNextState = false;
                }

            }
            characterControl.AnimProgress.disAllowEarlyTurn = false;

            if (StartingMomentum > 0.001f)
            {
                if (characterControl.IsFacingForward())
                {
                    characterControl.AnimProgress.AirMomentum = StartingMomentum;
                }
                else
                {
                    characterControl.AnimProgress.AirMomentum = -StartingMomentum;
                }
            }
        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterControl.AnimProgress.LockDirectionNextState = LockDirectionNextState;

            if (characterControl.AnimProgress.FrameUpdated)
            {
                return;
            }

            characterControl.AnimProgress.FrameUpdated = true;

            if (characterControl.Jump)
            {
                animator.SetBool(TransitionParameters.Jump.ToString(), true);
            }

            if (UseMomentum)
            {
                UpdateMomentum(characterControl, stateInfo);
            }
            else
            {
                if (IsConstantMove)
                {
                    ConstantMove(characterControl, stateInfo);
                }
                else
                {
                    ControlledMove(characterControl, animator, stateInfo);
                }
            }
        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (ClearMomentumOnExit)
            {
                characterControl.AnimProgress.AirMomentum = 0f;
            }
        }

        private void UpdateMomentum(CharacterControl characterControl, AnimatorStateInfo animatorStateInfo)
        {
            if (characterControl.MoveRight)
            {
                characterControl.AnimProgress.AirMomentum += SpeedGraph.Evaluate(animatorStateInfo.normalizedTime) * Speed * Time.deltaTime;
            }

            if (characterControl.MoveLeft)
            {
                characterControl.AnimProgress.AirMomentum -= SpeedGraph.Evaluate(animatorStateInfo.normalizedTime) * Speed * Time.deltaTime;
            }

            if (Mathf.Abs(characterControl.AnimProgress.AirMomentum) >= MaxMomentum)
            {
                if (characterControl.AnimProgress.AirMomentum > 0f)
                {
                    characterControl.AnimProgress.AirMomentum = MaxMomentum;
                }

                if (characterControl.AnimProgress.AirMomentum < 0f)
                {
                    characterControl.AnimProgress.AirMomentum = -MaxMomentum;
                }
            }

            if (characterControl.AnimProgress.AirMomentum > 0f)
            {
                characterControl.SetFaceForward(true);
            }
            else if (characterControl.AnimProgress.AirMomentum < 0f)
            {
                characterControl.SetFaceForward(false);
            }

            if (!CheckFront(characterControl))
            {
                characterControl.MoveAbleCharacter(Speed, Mathf.Abs(characterControl.AnimProgress.AirMomentum));
            }
        }

        private void ConstantMove(CharacterControl characterControl, AnimatorStateInfo stateInfo)
        {
            if (!CheckFront(characterControl))
            {
                characterControl.MoveAbleCharacter(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
            }
        }

        private void ControlledMove(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterControl.MoveRight && characterControl.MoveLeft)
            {
                animator.SetBool(TransitionParameters.Move.ToString(), false);
                return;
            }

            if (!characterControl.MoveRight && !characterControl.MoveLeft)
            {
                animator.SetBool(TransitionParameters.Move.ToString(), false);
                return;
            }

            if (characterControl.MoveRight)
            {
                if (!CheckFront(characterControl))
                {
                    characterControl.MoveAbleCharacter(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
                }
            }

            if (characterControl.MoveLeft)
            {
                if (!CheckFront(characterControl))
                {
                    characterControl.MoveAbleCharacter(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
                }
            }
            CheckTurn(characterControl);
        }

        private void CheckTurn(CharacterControl characterControl)
        {
            if (!LockTurn180Deg)
            {
                if (characterControl.MoveRight)
                {
                    characterControl.SetFaceForward(true);
                }

                if (characterControl.MoveLeft)
                {
                    characterControl.SetFaceForward(false);
                }
            }
        }

        private bool CheckFront(CharacterControl control)
        {
            foreach (GameObject o in control.FrontSpheres)
            {
                RaycastHit hit;
                Debug.DrawRay(o.transform.position, o.transform.forward * BlockDistance, Color.red);
                if (Physics.Raycast(o.transform.position, o.transform.forward, out hit, BlockDistance))
                {
                    if (!control.RagdollParts.Contains(hit.collider))
                    {
                        if (!IsBodyPart(hit.collider) && !Ledge.IsLedge(hit.collider.gameObject))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        bool IsBodyPart(Collider col)
        {
            CharacterControl control = col.transform.root.GetComponent<CharacterControl>();

            if (null == control)
            {
                return false;
            }

            if (control.gameObject == col.gameObject)
            {
                return false;
            }

            if (control.RagdollParts.Contains(col))
            {
                return true;
            }

            return false;
        }
    }
}

