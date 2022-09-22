using UnityEngine;

namespace Card
{
    public class CardAnimationState : StateMachineBehaviour
    {
        private Card _card;
        private bool _isInitialized;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            if (!_isInitialized)
            {
                _card = animator.gameObject.GetComponent<Card>();
                _isInitialized = true;
            }

            _card.cardEventChannel.CardsInteractionActive(false);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            _card.cardEventChannel.CardsInteractionActive(true);
            _card.OnAnimationEnds(stateInfo.shortNameHash);
        }

        public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
        }

        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
        }
    }
}