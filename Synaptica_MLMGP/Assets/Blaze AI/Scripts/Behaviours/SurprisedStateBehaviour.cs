using UnityEngine;
using UnityEngine.Events;

namespace BlazeAISpace
{
    [AddComponentMenu("Surprised State Behaviour/Surprised State Behaviour")]
    public class SurprisedStateBehaviour : MonoBehaviour
    {
        #region PROPERTIES

        [Tooltip("The surprised animation to play.")]
        public string anim;
        [Tooltip("The animation transition.")]
        public float animT = 0.25f;
        [Min(0), Tooltip("The duration to stay in this state and playing the animation.")]
        public float duration;

        [Tooltip("The speed of turning to face the target's caught position.")]
        public float turnSpeed = 10;

        [Tooltip("Set your audios in the audio scriptable in the General Tab in Blaze AI.")]
        public bool playAudio;

        public UnityEvent onStateEnter;
        public UnityEvent onStateExit;

        #endregion

        #region BEHAVIOUR VARS

        BlazeAI blaze;
        float _duration = 0f;

        bool playedAudio;
        bool turningDone;

        #endregion

        #region UNITY METHODS
        
        public virtual void Start()
        {
            blaze = GetComponent<BlazeAI>();

            // force shut if not the same state
            if (blaze.state != BlazeAI.State.surprised) {
                enabled = false;
            }
        }

        public virtual void OnDisable()
        {
            Reset();
            onStateExit.Invoke();
        }

        public virtual void OnEnable()
        {
            onStateEnter.Invoke();
            
            if (blaze == null) {
                blaze = GetComponent<BlazeAI>();

                if (blaze == null) {
                    Debug.LogWarning($"No Blaze AI component found in the gameobject: {gameObject.name}. AI behaviour will have issues.");
                    return;
                }
            }
        }

        void Reset()
        {
            turningDone = false;
            _duration = 0f;
            playedAudio = false;
        }
        
        public virtual void Update()
        {
            // only turn if turning hasn't finished
            if (!turningDone) 
            {
                // turn to face enemy -> this function returns true when done
                if (blaze.TurnTo(blaze.enemyPosOnSurprised, blaze.waypoints.leftTurnAnimAlert, blaze.waypoints.rightTurnAnimAlert, blaze.waypoints.turningAnimT, turnSpeed)) {
                    turningDone = true;
                }

                return;
            }
            
            
            // play animation
            blaze.animManager.Play(anim, animT);
            
            
            // play audio
            if (playAudio && !playedAudio) 
            {
                if (!blaze.IsAudioScriptableEmpty()) {
                    if (blaze.PlayAudio(blaze.audioScriptable.GetAudio(AudioScriptable.AudioType.SurprisedState))) {
                        playedAudio = true;
                    }
                }
            }
            

            // timer to quit surprised state
            _duration += Time.deltaTime;
            
            if (_duration >= duration) 
            {
                Reset();
                blaze.SetState(BlazeAI.State.attack);
            }
        }

        #endregion
    }
}