using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CCCornerCell : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler{

	[SerializeField,Range(0,1),Header("切角大小")]
	public float width;            //设置切角的长度，百分比

	[SerializeField,Range(0,1)]
	public float height;           //设置切角的宽度，百分比


//	[HideInInspector]
	public GameObject titlePanel;

//	[HideInInspector]
	public GameObject timePanel;

//	[HideInInspector]
	public Text time;          //时间标签

//	[HideInInspector]
	public Text title;         //名称标签

//	[HideInInspector]
	public Image image;       

	private Color orginColor;

	private Color titleColor;

	public delegate void ClickEventHandler ();

	public static event ClickEventHandler onClicked;


	// Use this for initialization
	void Start () 
	{
		//时间panel显示动画
		Tweener timeTweener = timePanel.transform.DOScale (new Vector3(1.0f,1.0f,1.0f), 0.01f);
		timeTweener.SetAutoKill (false);
		timeTweener.SetEase (Ease.InOutSine);
		timeTweener.Pause ();

		//整个cell缩放动画
		Tweener tweener = this.transform.DOScale(new Vector3(1.1f, 1.1f, 1.0f),0.25f);
		tweener.SetAutoKill (false);
		tweener.SetEase (Ease.InOutQuad);
		tweener.Pause ();

		orginColor = titlePanel.GetComponent<Image> ().color;
		titleColor = title.color;

		//添加材质球
		Material mat = new Material(Shader.Find("Custom/CCCornerCell"));
		mat.SetFloat ("_Width", width);
		mat.SetFloat ("_Height", height);
		this.GetComponent<Image>().material = mat;
	}

	public void OnPointerEnter (PointerEventData eventData)
	{	
		this.transform.DOPlayForward ();
		this.transform.SetAsLastSibling ();

		timePanel.transform.DOPlayForward ();

		ChangeTitleColor ();
		ShowGradientColor ();
	}

	public void OnPointerExit (PointerEventData eventData)
	{
		this.transform.DOPlayBackwards ();
		timePanel.transform.DOPlayBackwards ();

		title.color = titleColor;
		titlePanel.GetComponent<Image> ().color = orginColor;
	}

	public void  OnPointerClick (PointerEventData eventData)
	{
		if (onClicked != null) 
		{
			onClicked();
		}
	}

	//改变title文本的颜色
	private void ChangeTitleColor()
	{
		title.color = CCColorConventor.HexToRGB("ffb69c");
	}

	//显示渐变色
	private void ShowGradientColor()
	{
		Color foreColor = CCColorConventor.HexToRGB("fd7035");
		Color backColor = CCColorConventor.HexToRGB("ff8d45");
		titlePanel.GetComponent<Image> ().color = Color.Lerp(foreColor, backColor, Mathf.PingPong(Time.time, 1));
	}

	// Update is called once per frame
	void Update () 
	{
		
	}
}
