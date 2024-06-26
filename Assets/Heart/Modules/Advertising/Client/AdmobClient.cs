﻿using Pancake.Common;
#if PANCAKE_ADVERTISING && PANCAKE_ADMOB
using GoogleMobileAds.Api;
using Pancake.Tracking;
#endif

namespace Pancake.Monetization
{
    public sealed class AdmobClient : AdClient
    {
        public override void Init()
        {
#if PANCAKE_ADVERTISING && PANCAKE_ADMOB
            MobileAds.Initialize(_ =>
            {
                App.RunOnMainThread(() =>
                {
                    // Indicates if the Unity app should be paused when a full-screen ad is displayed.
                    // On Android, Unity is paused when displaying full-screen ads. Calling this method with true duplicates this behavior on iOS.
                    MobileAds.SetiOSAppPauseOnBackground(true);
                    if (!adSettings.AdmobEnableTestMode) return;
                    var configuration = new RequestConfiguration {TestDeviceIds = adSettings.AdmobDevicesTest};
                    MobileAds.SetRequestConfiguration(configuration);
                });
            });

            adSettings.AdmobBanner.paidedCallback = AppTracking.TrackRevenue;
            adSettings.AdmobInter.paidedCallback = AppTracking.TrackRevenue;
            adSettings.AdmobReward.paidedCallback = AppTracking.TrackRevenue;
            adSettings.AdmobRewardInter.paidedCallback = AppTracking.TrackRevenue;
            adSettings.AdmobAppOpen.paidedCallback = AppTracking.TrackRevenue;
            RegisterAppStateChange();
            LoadInterstitial();
            LoadRewarded();
            LoadRewardedInterstitial();
            LoadAppOpen();
            LoadBanner();
            (adSettings.AdmobBanner as IBannerHide)?.Hide(); // hide banner first time when banner auto show when loaded
#endif
        }

        public override void LoadBanner()
        {
#if PANCAKE_ADVERTISING && PANCAKE_ADMOB
            adSettings.AdmobBanner.Load();
#endif
        }

        public override void LoadInterstitial()
        {
#if PANCAKE_ADVERTISING && PANCAKE_ADMOB
            if (!IsInterstitialReady()) adSettings.AdmobInter.Load();
#endif
        }

        public override bool IsInterstitialReady()
        {
#if PANCAKE_ADVERTISING && PANCAKE_ADMOB
            return adSettings.AdmobInter.IsReady();
#else
            return false;
#endif
        }

        public override void LoadRewarded()
        {
#if PANCAKE_ADVERTISING && PANCAKE_ADMOB
            if (!IsRewardedReady()) adSettings.AdmobReward.Load();
#endif
        }

        public override bool IsRewardedReady()
        {
#if PANCAKE_ADVERTISING && PANCAKE_ADMOB
            return adSettings.AdmobReward.IsReady();
#else
            return false;
#endif
        }

        public override void LoadRewardedInterstitial()
        {
#if PANCAKE_ADVERTISING && PANCAKE_ADMOB
            if (!IsRewardedInterstitialReady()) adSettings.AdmobRewardInter.Load();
#endif
        }

        public override bool IsRewardedInterstitialReady()
        {
#if PANCAKE_ADVERTISING && PANCAKE_ADMOB
            return adSettings.AdmobRewardInter.IsReady();
#else
            return false;
#endif
        }

        private void ShowAppOpen()
        {
#if PANCAKE_ADVERTISING && PANCAKE_ADMOB
            if (statusAppOpenFirstIgnore) adSettings.AdmobAppOpen.Show();
            statusAppOpenFirstIgnore = true;
#endif
        }

        public override void LoadAppOpen()
        {
#if PANCAKE_ADVERTISING && PANCAKE_ADMOB
            if (!IsAppOpenReady()) adSettings.AdmobAppOpen.Load();
#endif
        }

        public override bool IsAppOpenReady()
        {
#if PANCAKE_ADVERTISING && PANCAKE_ADMOB
            return adSettings.AdmobAppOpen.IsReady();
#else
            return false;
#endif
        }

#if PANCAKE_ADVERTISING && PANCAKE_ADMOB
        private void RegisterAppStateChange() { GoogleMobileAds.Api.AppStateEventNotifier.AppStateChanged += OnAppStateChanged; }

        private void OnAppStateChanged(GoogleMobileAds.Common.AppState state)
        {
            if (state == GoogleMobileAds.Common.AppState.Foreground)
            {
                if (adSettings.CurrentNetwork == EAdNetwork.Admob) ShowAppOpen();
            }
        }
#endif
    }
}