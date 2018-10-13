using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class VirtualJoyTuto : MonoBehaviour,IDragHandler,IPointerUpHandler,IPointerDownHandler {

	public PlayerScriptTuto player1;

	private Image bgImg;
	private Image PadImg;
	private Vector3 inputVector;

	void Start () {
		bgImg = GetComponent<Image> ();
		PadImg = transform.GetChild (0).GetComponent<Image> ();
	}
	
	public virtual void OnDrag(PointerEventData ped)
	{
		Vector2 pos;
		if(RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform,ped.position,ped.pressEventCamera,out pos) ){
			pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
			pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);
			inputVector = new Vector3 (pos.x * 2 - 1, 0, pos.y * 2 - 1);
			inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;
			PadImg.rectTransform.anchoredPosition = new Vector3 (inputVector.x * (bgImg.rectTransform.sizeDelta.x / 2), inputVector.z * (bgImg.rectTransform.sizeDelta.y / 2));
		}
		if((Mathf.Abs(inputVector.x) > Mathf.Abs(inputVector.z))){
			if (inputVector.x > 0) {
				player1.RightPress ();
				player1.UpRelease ();
				player1.DownRelease ();
				player1.LeftRelease ();
			} else {
				player1.LeftPress ();
				player1.UpRelease ();
				player1.DownRelease ();
				player1.RightRelease ();
			}
		}
		if((Mathf.Abs(inputVector.z) > Mathf.Abs(inputVector.x))){
			if (inputVector.z > 0) {
				player1.UpPress ();
				player1.RightRelease ();
				player1.DownRelease ();
				player1.LeftRelease ();
			} else {
				player1.DownPress ();
				player1.UpRelease ();
				player1.LeftRelease ();
				player1.RightRelease ();
			}
		}
	}

	public virtual void OnPointerDown(PointerEventData ped)
	{
		OnDrag (ped);
	}

	public virtual void OnPointerUp(PointerEventData ped)
	{
		inputVector = Vector3.zero;
		PadImg.rectTransform.anchoredPosition = Vector3.zero;
		player1.UpRelease ();
		player1.DownRelease ();
		player1.RightRelease ();
		player1.LeftRelease ();
	}

}
