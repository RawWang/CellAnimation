using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CellAnimation : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {


	// Use this for initialization
	void Start () {
		Tweener tweener = transform.DOScale(new Vector3(1.2f,1.2f,1.0f),0.25f);
		tweener.SetAutoKill (false);
		tweener.SetEase (Ease.InOutQuad);
		tweener.Pause ();
	}

	public void OnPointerEnter (PointerEventData eventData)
	{
		transform.DOPlayForward ();
	}

	public void OnPointerExit (PointerEventData eventData)
	{
		transform.DOPlayBackwards ();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
