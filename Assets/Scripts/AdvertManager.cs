using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Advertisements;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class AdvertManager : MonoBehaviour {

	public static AdvertManager adManager;

	private BannerView bannerView;
	private InterstitialAd interstitial;
	private static string outputMessage = "";
	private bool testingApp = false;

	void Awake(){
		if (adManager != null) {
			Destroy (gameObject);
		} else {
			adManager = this;
			GameObject.DontDestroyOnLoad (gameObject);
		}
	}

	void Start(){
	}

	void Update(){
		// Calculate simple moving average for time to render screen. 0.1 factor used as smoothing
		// value.
		//deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
	}



//	/**
//	 * Shows an ad only if one is not currently showing and there is one ready.
//	 **/
//	public bool showUnityAd(){
//		bool wasSucc = false;
//		if (!Advertisement.isShowing) {
//			if (Advertisement.IsReady ()){
//				Advertisement.Show ();
//				wasSucc = true;
//			} else {
//				Debug.Log ("Not Ready");
//			}
//		}
//		return wasSucc;
//	}
//
//	public void RequestBanner(){
//		#if UNITY_ANDROID
//		string adUnitId = "ca-app-pub-6333353846841342/6157690114";
//		#elif UNITY_IPHONE
//		string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
//		#else
//		string adUnitId = "unexpected_platform";
//		#endif
//
//		// Create a 320x50 banner at the top of the screen.
//		BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
//		// Create an empty ad request.
//		AdRequest request = new AdRequest.Builder().Build();
//		// Load the banner with the request.
//		bannerView.LoadAd(request);
//	}

	public void RequestBanner(){
		#if UNITY_EDITOR
			string adUnitId = "unused";
		#elif UNITY_ANDROID
			string adUnitId = "ca-app-pub-6333353846841342/6157690114";
		#elif UNITY_IPHONE
			string adUnitId = "ca-app-pub-6333353846841342/5385634111";
		#else
			string adUnitId = "unexpected_platform";
		#endif

		// Create a 320x50 banner at the top of the screen.
		bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
		// Register for ad events.
		bannerView.AdLoaded += HandleAdLoaded;
		bannerView.AdFailedToLoad += HandleAdFailedToLoad;
		bannerView.AdOpened += HandleAdOpened;
		bannerView.AdClosing += HandleAdClosing;
		bannerView.AdClosed += HandleAdClosed;
		bannerView.AdLeftApplication += HandleAdLeftApplication;
		// Load a banner ad.
		bannerView.LoadAd(createAdRequest());
		bannerView.Show ();
	}

	public void destoryBanner(){
		bannerView.Destroy ();
	}

	public void RequestInterstitial(){
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-6333353846841342/9815833719";
		#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-6333353846841342/8339100514";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		// Create an interstitial.
		interstitial = new InterstitialAd(adUnitId);
		// Register for ad events.
		interstitial.AdLoaded += HandleInterstitialLoaded;
		interstitial.AdFailedToLoad += HandleInterstitialFailedToLoad;
		interstitial.AdOpened += HandleInterstitialOpened;
		interstitial.AdClosing += HandleInterstitialClosing;
		interstitial.AdClosed += HandleInterstitialClosed;
		interstitial.AdLeftApplication += HandleInterstitialLeftApplication;
		// Load an interstitial ad.
		interstitial.LoadAd(createAdRequest());
		//interstitial.Show ();
	}

	public void showInterstitial(){
		interstitial.Show ();
	}

	private AdRequest createAdRequest(){
//		if (testingApp) {
//			Debug.Log ("TESTAD");
//			return new AdRequest.Builder()
//			.AddTestDevice(AdRequest.TestDeviceSimulator)
//			.AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
//			.AddKeyword("game")
//			.SetGender(Gender.Male)
//			.SetBirthday(new DateTime(1999, 1, 1))
//			.TagForChildDirectedTreatment(false)
//			.AddExtra("color_bg", "9B30FF")
//			.Build();
//		} else {
			return new AdRequest.Builder ().Build ();
//		}
	}

//	private AdRequest createTestAdRequest(){
//		return new AdRequest.Builder()
//		.AddTestDevice(AdRequest.TestDeviceSimulator)
//		.AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
//		.AddKeyword("game")
//		.SetGender(Gender.Male)
//		.SetBirthday(new DateTime(1999, 1, 1))
//		.TagForChildDirectedTreatment(false)
//		.AddExtra("color_bg", "9B30FF")
//		.Build();
//	}

	#region Banner callback handlers

	public void HandleAdLoaded(object sender, EventArgs args){
		print("HandleAdLoaded event received.");
	}

	public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args){
		print("HandleFailedToReceiveAd event received with message: " + args.Message);
	}

	public void HandleAdOpened(object sender, EventArgs args){
		print("HandleAdOpened event received");
	}

	void HandleAdClosing(object sender, EventArgs args){
		print("HandleAdClosing event received");
	}

	public void HandleAdClosed(object sender, EventArgs args){
		print("HandleAdClosed event received");
	}

	public void HandleAdLeftApplication(object sender, EventArgs args){
		print("HandleAdLeftApplication event received");
	}

	#endregion

	#region Interstitial callback handlers

	public void HandleInterstitialLoaded(object sender, EventArgs args){
		print("HandleInterstitialLoaded event received.");
	}

	public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args){
		print("HandleInterstitialFailedToLoad event received with message: " + args.Message);
	}

	public void HandleInterstitialOpened(object sender, EventArgs args){
		print("HandleInterstitialOpened event received");
	}

	void HandleInterstitialClosing(object sender, EventArgs args){
		print("HandleInterstitialClosing event received");
	}

	public void HandleInterstitialClosed(object sender, EventArgs args){
		print("HandleInterstitialClosed event received");
	}
	
	public void HandleInterstitialLeftApplication(object sender, EventArgs args){
		print("HandleInterstitialLeftApplication event received");
	}

	#endregion

}
