using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI;

public class AntiOnOff : MonoBehaviour {

	public PostProcessingBehaviour pst;

	public Toggle tgl;

	public void OnToggleChange()
	{
		pst.enabled = tgl.isOn;
	} 
}
