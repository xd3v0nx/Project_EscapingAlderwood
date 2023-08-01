using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DuRound.MiniGame
{
    public class PlayerWalk : GuardWalk
    {
        private Button m_moveBtn, m_daggerBtn;
        private GuardWalk m_guardWalk;
        protected override void Awake()
        {
            base.Awake();
        }

        // Start is called before the first frame update
        void Start()
        {
            m_rigidBody2D = GetComponent<Rigidbody2D>();
            m_moveBtn = transform.parent.GetChild(4).transform.GetChild(0).GetComponent<Button>();
            m_daggerBtn = transform.parent.GetChild(4).transform.GetChild(1).GetComponent<Button>();
            m_moveBtn.onClick.AddListener(MoveForward);
            m_daggerBtn.onClick.AddListener(AttackingGuard);
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void MoveForward()
        {
            m_rigidBody2D.transform.position = new Vector2(m_rigidBody2D.transform.position.x + moveSpeed, m_rigidBody2D.transform.position.y);

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("UIGuard"))
            {
                m_daggerBtn.gameObject.SetActive(true);
                if (m_guardWalk == null) m_guardWalk = collision.GetComponent<GuardWalk>();
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("UIGuard"))
            {
                m_daggerBtn.gameObject.SetActive(false);
                m_guardWalk = null;
            }
        }
        private void AttackingGuard()
        {
            ResetPosition();
            m_guardWalk.ResetPosition();
           var cg =  transform.parent.parent.GetComponent<CanvasGroup>();
            cg.alpha = 0;
            cg.interactable = false;
            cg.blocksRaycasts = false;
            Mabel.instance.AddThomas();
            GuardController.instance.GuardAction(true);
        }
        private void OnDestroy()
        {
            m_moveBtn.onClick.RemoveAllListeners();
            m_daggerBtn.onClick.RemoveAllListeners();
            m_guardWalk = null;
        }
    }
}
