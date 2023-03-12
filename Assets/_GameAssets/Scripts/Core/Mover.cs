using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using DG.Tweening;
using Game.Managers;

namespace Game.Core
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _moveTime;

        private Vector3 _movePos;
        private bool _canMove;
        private Tween moveTween;

        #region UnityBuildinFunctions
        private void Update()
        {
            if (_canMove)
                MoveTo(_movePos);
        }

        private void OnDestroy()
        {
            moveTween.Kill();
        }
        #endregion

        #region CustomMethods
        public void MoveToUiAction(GameObject target)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.useGravity = false;

            _movePos = target.GetComponent<RectTransform>().TransformPoint(target.GetComponent<RectTransform>().rect.center);
            _canMove = true;
        }
        public void MoveTween(GameObject target)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.useGravity = false;

            _movePos = target.GetComponent<RectTransform>().TransformPoint(target.GetComponent<RectTransform>().rect.center);

            moveTween = DOTween.Sequence()
                .Append(transform.DOScale(Vector3.one * 0.5f, _moveTime))
                .Join(transform.DOMove(_movePos, _moveTime).SetEase(Ease.Linear).OnComplete(() =>
                {
                    this.transform.position = _movePos;
                    if (GetComponent<Item3D>() != null)
                        GetComponent<Item3D>().ReachedTarget(target);
                }));
        }
        private void MoveTo(Vector3 pos)
        {
            Vector3 movePos = new Vector3(pos.x, pos.y + (this.transform.localScale.y / 2), pos.z);
            float distance = Vector3.Distance(this.transform.position, movePos);

            if (distance < 0.1f)
            {
                this.transform.position = movePos;
                _canMove = false;
                this.enabled = false;
            }
            else
                this.transform.position = Vector3.Lerp(this.transform.position, movePos, Time.deltaTime * _moveSpeed);
        }
        #endregion
    }
}
