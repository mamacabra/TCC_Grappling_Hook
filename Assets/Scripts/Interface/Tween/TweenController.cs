using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
public class TweenController : MonoBehaviour
{
     [Header("Boleans")]
        public bool doScale;
        public bool doMove;
        public bool doScaleLoop;
        public bool doMoveloop;
        public bool doRotate;
        public bool waitCommand;

        [Header("Animation Ease")]
        public Ease scaleEase = Ease.InOutSine;
        public Ease moveEase = Ease.InOutSine;
        
        [Header("Initial and Final Scale Value")]
        public Vector3 initialScale;
        public Vector3 finalScale;
        
        [Header("Initial and Final Position Value")]
        public Vector3 initialPos;
         Vector3 finalPos;
        
        [Header("Rotate Value")]
        public Vector3 finalRotate;
        
        [Header("Duration Values")]
        public float scaleDuration = 1f;
        public float moveDuration = 1f;
        public float rotateDuration = 1f;
        
        [Header("Others")]
        [SerializeField]public float waitBeforeDo;
        [SerializeField]public bool pingPongLoopping;
        private TweenerCore<Vector3, Vector3, VectorOptions> move;
        
        [SerializeField]public float waitBeforeDoReverse;

        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = transform.GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            finalPos = transform.localPosition;
            
            Stop();
            Reset();
            
            if(waitCommand) return;
            StartAll();
        }

        public void StartAll()
        {
            if(doMove)
                DoMove();
            if(doScale)
                DoScale();
            if(doRotate)
                DoRotate();
        }
        private void OnDisable()
        {
            Stop();
        }
        private void OnDestroy()
        {
            Stop();
        }
        public void Reset()
        {
            if(doScale) transform.localScale = initialScale;
            if(doMove) transform.localPosition = initialPos;
        }
        public void Stop()
        {
            DOTween.Kill(transform);
            DOTween.Kill(move);
            
            if(routine != null)
                StopCoroutine(routine);
        }
        public void DoScale()
        {
            if (!doScale) return;

            if (!doScaleLoop)
                move = transform.DOScale(finalScale, scaleDuration).SetEase(scaleEase).SetDelay(waitBeforeDo).SetUpdate(true);
            else
                move =  transform.DOScale(finalScale, scaleDuration).SetEase(scaleEase).SetLoops(-1, LoopType.Yoyo)
                    .SetDelay(waitBeforeDo).SetUpdate(true);
        }

        public void DoScaleReverse()
        {
            move =  transform.DOScale(0, scaleDuration).SetEase(scaleEase).SetDelay(waitBeforeDoReverse);
        }
        public void DoMove()
        {
            if (!doMove) return;

            if (!doMoveloop)
                move = rectTransform.DOLocalMove(finalPos, moveDuration).SetEase(moveEase).SetDelay(waitBeforeDo);
            else
                move = rectTransform.DOLocalMove(finalPos, moveDuration).SetEase(moveEase).SetLoops(-1, LoopType.Yoyo).SetDelay(waitBeforeDo);
        }
        
        public void DoRotate()
        {
            if (!doRotate) return;

            routine = StartCoroutine(Wait());

            IEnumerator Wait()
            {
                yield return new WaitForSeconds(waitBeforeDo);
                
                t = transform.DOLocalRotate(finalRotate, rotateDuration).SetLoops(-1, pingPongLoopping ? LoopType.Yoyo : LoopType.Restart).SetEase(Ease.Linear);
            }
        }
        
        private Coroutine routine;
        private TweenerCore<Quaternion, Vector3, QuaternionOptions> t;
        public void Resete()
        {
            transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
            if(t!= null) {t.Kill();}
        }
        private void RotateRight()
        {
            t = transform.DOLocalRotate(finalRotate, rotateDuration).SetEase(Ease.Linear).OnComplete(() => { RotateLeft(); });
        }
        private void RotateLeft()
        {
            var newPos = finalRotate;
            newPos.y = 0;
           t = transform.DOLocalRotate(newPos, rotateDuration).SetEase(Ease.Linear).OnComplete(() => { RotateRight(); });
            
        }
        
    }