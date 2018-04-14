// (c) Unity Technologies Japan/UCL
// http://unity-chan.com/contents/license_jp/
using UnityEngine;
using System.Collections;

// Auto Blink
// This script based https://gist.github.com/dskjal/17034ad3b97bc6596248881f02caaafe
// for youten-yume2 model
namespace YVTuber {
	public class AutoBlink : MonoBehaviour
	{
		public bool isActive = true;                // オート目パチ有効
		public SkinnedMeshRenderer refFace;         // Faceへの参照
		public float ratio_Close = 95.0f;           // 閉じ目ブレンドシェイプ比率
		public float ratio_HalfClose = 30.0f;       // 半閉じ目ブレンドシェイプ比率
		[HideInInspector]
		public float ratio_Open = 0.0f;
		public float timeBlink = 0.2f;              // 目パチの時間

		public float threshold = 0.3f;              // ランダム判定の閾値
		public float interval = 3.0f;               // ランダム判定のインターバル


		class EyelidAnimator {
			private float timeBlinkSec;
			private float timeRemaining = 0f;

			private SkinnedMeshRenderer refFace; // Faceへの参照

			enum Status
			{
				Open = 0,
				HalfClose,
				Close,
				HalfOpen,
				NotAnimating,

				STATUS_LENGTH
			}
			private Status eyeStatus;   // 現在の目パチステータス

			struct StateTransition {
				public Status NextState;
				public float StartWeight;
				public float EndWeight;
			}
			StateTransition[] stateTable = new StateTransition[(int)Status.STATUS_LENGTH];

			public EyelidAnimator(SkinnedMeshRenderer face) {
				refFace = face;

				stateTable[0].NextState = Status.HalfClose;
				stateTable[1].NextState = Status.Close;
				stateTable[2].NextState = Status.HalfOpen;
				stateTable[3].NextState = Status.NotAnimating;
			}

			public void Start(float timeBlinkSec, float ratioClose, float ratioHalfClose, float ratioOpen) {
				this.timeBlinkSec = timeBlinkSec / 4f;
				timeRemaining = this.timeBlinkSec;

				eyeStatus = Status.Open;
				stateTable[3].EndWeight = stateTable[0].StartWeight = ratioOpen;
				stateTable[0].EndWeight = stateTable[1].StartWeight = ratioHalfClose;
				stateTable[1].EndWeight = stateTable[2].StartWeight = ratioClose;
				stateTable[2].EndWeight = stateTable[3].StartWeight = ratioHalfClose;
			}

			private void setRatio(float ratio) {
				refFace.SetBlendShapeWeight(0, ratio); // 0:left blink morph index
				refFace.SetBlendShapeWeight(1, ratio); // 1:right blink morph index
			}

			// Must call every frame
			public void Update() {
				if (!IsAnimating()) {
					return;
				}

				timeRemaining -= Time.deltaTime;
				// 残り時間が少なくなるにつれて 1 に近づく
				var animWeight = 1f - Mathf.Clamp(timeRemaining/this.timeBlinkSec, 0, 1);

				var stateData = stateTable[(int)eyeStatus];
				if (timeRemaining < 0f) {
					eyeStatus = stateData.NextState;
					timeRemaining += timeBlinkSec;
				}

				var ratio = Mathf.Lerp(stateData.StartWeight, stateData.EndWeight, animWeight);
				setRatio(ratio);
			}

			public bool IsAnimating() {
				return eyeStatus != Status.NotAnimating;
			}
		}
		private EyelidAnimator eyelidAnimator;

		void Awake ()
		{
			if (refFace == null) {
				refFace = GameObject.Find("Body").GetComponent<SkinnedMeshRenderer>();
			}
			eyelidAnimator = new EyelidAnimator(refFace);
		}

		void Start ()
		{
			// ランダム判定用関数をスタートする
			StartCoroutine ("RandomChange");
		}

		void Update ()
		{
			if (isActive) {
				eyelidAnimator.Update();
			}
		}

		// ランダム判定用関数
		IEnumerator RandomChange ()
		{
			// 無限ループ開始
			while (true) {
				//ランダム判定用シード発生
				float _seed = Random.Range (0.0f, 1.0f);
				if (!eyelidAnimator.IsAnimating()) {
					if (_seed > threshold) {
						eyelidAnimator.Start(timeBlink, ratio_Close, ratio_HalfClose, ratio_Open);
					}
				}
				// 次の判定までインターバルを置く
				yield return new WaitForSeconds (interval);
			}
		}
	}
}