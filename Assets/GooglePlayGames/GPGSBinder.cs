using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi.Events;
using Firebase.Auth;
using Firebase.Extensions;
 
public class GPGSBinder : MonoBehaviour
{
    private static GPGSBinder instance = null;
    
    private FirebaseAuth auth;
    private FirebaseUser user;

    public ISavedGameClient SavedGame => 
        PlayGamesPlatform.Instance.SavedGame;
    public IEventsClient Events => 
        PlayGamesPlatform.Instance.Events;


    public Action<bool> loginState; // 로그인 여부를 체크하는 델리게이트
    
    //public string UserId =>
    //    user.UserId;

    public string UserId
    {
        get
        {
            if (user == null)
            {
                return null;
            }

            return user.UserId;
        }
    }

    public string DisplayName =>
        user.DisplayName;

    public static GPGSBinder Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        Debug.Log("Called GPGSBinder Start");
        
        PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder()
            .RequestIdToken()
            .RequestEmail()
            .Build());
        
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        // 구글 플레이 게임 활성화
 
        // Firebase Unity SDK에는 Google Play  서비스가 필요하고 SDK를 사용하려면 최신버전이여야 함.
        // 구글 플레이 버전이 재대로 되어있는지, 지원하고 있는 버전인지 체크해보자.
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(dependencyTask =>
        {
            // 결과가 나오게 되면 어떻게 처리할 것인지 지정한다. 결과를 task의 Result 안에 있다.
            var dependencyStatus = dependencyTask.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // 사용할 수 있다는 결과가 나오면 그냥 사용하면 된다.
                FirebaseInit();
            }
            else
            {
                // Firebase Unity SDK is not safe to use here.
                UnityEngine.Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
            }
        });
        
    }
    
    private void FirebaseInit()
    {
        Debug.Log("Called Firebase Init");

        auth = FirebaseAuth.DefaultInstance;

        // 초기화시 자동 로그인 방지 임시처리
        if (auth.CurrentUser != null)
        {
            auth.SignOut();
        }

        // auth의 상태가 변경될 때마다 특정 이벤트를 하도록 등록 (이벤트 핸들러 등록)         
        auth.StateChanged += AuthStateChanged;

    }
    
    private void AuthStateChanged(object sender, EventArgs e)
    {
        if (auth.CurrentUser == user) return;

        // 지금 유저와 이전 유저가 다르다는 것은 게정 상태가 변한것.
        bool signed = (auth.CurrentUser != user && auth.CurrentUser != null);
        if (!signed && user != null)
        {
            Debug.Log("파이버베이스 로그아웃");
            loginState?.Invoke(false);
        }

        user = auth.CurrentUser;
        if (signed)
        {
            Debug.Log("파이어베이스 로그인");
            loginState?.Invoke(true);
        }
    }
    
    public void TryGoogleLogin()
    {
        if (!Social.localUser.authenticated) // 로그인 되어 있지 않다면
        {
            Social.localUser.Authenticate(success => // 로그인 시도
            {
                if (success) // 성공하면
                {
                    Debug.Log("Google Login Success");
                    StartCoroutine(TryFirebaseLogin()); // Firebase Login 시도
                }
                else // 실패하면
                {
                    Debug.Log("Google Login Fail");
                }
            });
        }
    }
 
    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator TryFirebaseLogin()
    {
        // 파이어베이스 로그인에 쓸 IDToken을 요청하여 가져온다.
        while (string.IsNullOrEmpty(((PlayGamesLocalUser)Social.localUser).GetIdToken()))
            yield return null;
        string idToken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();
 
 
        Credential credential = GoogleAuthProvider.GetCredential(idToken, null);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }
 
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }
 
    public void TryLogout()
    {
        if (Social.localUser.authenticated) // 로그인 되어 있다면
        {
            PlayGamesPlatform.Instance.SignOut(); // Google 로그아웃
            auth.SignOut(); // Firebase 로그아웃
            
            Debug.Log("로그아웃");
        }
        else
        {
            Debug.Log("로그인이 되어있지 않습니다.");
        }
    }
 
    
    public void SaveCloud(string fileName, string saveData, Action<bool> onCloudSaved = null)
    {
        SavedGame.OpenWithAutomaticConflictResolution(fileName, DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLastKnownGood, (status, game) =>
            {
                if (status == SavedGameRequestStatus.Success)
                {
                    var update = new SavedGameMetadataUpdate.Builder().Build();
                    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(saveData);
                    // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                    SavedGame.CommitUpdate(game, update, bytes, (status2, game2) =>
                    {
                        onCloudSaved?.Invoke(status2 == SavedGameRequestStatus.Success);
                    });
                }
            });
    }

    public void LoadCloud(string fileName, Action<bool, string> onCloudLoaded = null)
    {
        SavedGame.OpenWithAutomaticConflictResolution(fileName, DataSource.ReadCacheOrNetwork, 
            ConflictResolutionStrategy.UseLastKnownGood, (status, game) => 
            {
                if (status == SavedGameRequestStatus.Success)
                {
                    // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                    SavedGame.ReadBinaryData(game, (status2, loadedData) =>
                    {
                        if (status2 == SavedGameRequestStatus.Success)
                        {
                            var data = System.Text.Encoding.UTF8.GetString(loadedData);
                            onCloudLoaded?.Invoke(true, data);
                        }
                        else
                            onCloudLoaded?.Invoke(false, null);
                    });
                }
            });
    }

    public void DeleteCloud(string fileName, Action<bool> onCloudDeleted = null)
    {
        SavedGame.OpenWithAutomaticConflictResolution(fileName,
            DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, (status, game) =>
            {
                if (status == SavedGameRequestStatus.Success)
                {
                    SavedGame.Delete(game);
                    onCloudDeleted?.Invoke(true);
                }
                else 
                    onCloudDeleted?.Invoke(false);
            });
    }
    
    public void ShowAchievementUI() => 
        Social.ShowAchievementsUI();

	public void UnlockAchievement(string gpgsId, Action<bool> onUnlocked = null) => 
        Social.ReportProgress(gpgsId, 100, success => onUnlocked?.Invoke(success));

    public void IncrementAchievement(string gpgsId, int steps, Action<bool> onUnlocked = null) =>
        PlayGamesPlatform.Instance.IncrementAchievement(gpgsId, steps, success => onUnlocked?.Invoke(success));


    public void ShowAllLeaderboardUI() =>
        Social.ShowLeaderboardUI();

    public void ShowTargetLeaderboardUI(string gpgsId) => 
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(gpgsId);

    public void ReportLeaderboard(string gpgsId, long score, Action<bool> onReported = null) =>
        Social.ReportScore(score, gpgsId, success => onReported?.Invoke(success));

	public void LoadAllLeaderboardArray(string gpgsId, Action<UnityEngine.SocialPlatforms.IScore[]> onloaded = null) => 
        Social.LoadScores(gpgsId, onloaded);

    public void LoadCustomLeaderboardArray(string gpgsId, int rowCount, LeaderboardStart leaderboardStart, 
        LeaderboardTimeSpan leaderboardTimeSpan, Action<bool, LeaderboardScoreData> onloaded = null)
    {
        PlayGamesPlatform.Instance.LoadScores(gpgsId, leaderboardStart, rowCount, LeaderboardCollection.Public, leaderboardTimeSpan, data =>
        {
            onloaded?.Invoke(data.Status == ResponseStatus.Success, data);
        });
    }
    
    public void IncrementEvent(string gpgsId, uint steps) 
    {
        Events.IncrementEvent(gpgsId, steps);
    }

    public void LoadEvent(string gpgsId, Action<bool, IEvent> onEventLoaded = null)
    {
        Events.FetchEvent(DataSource.ReadCacheOrNetwork, gpgsId, (status, iEvent) =>
        {
            onEventLoaded?.Invoke(status == ResponseStatus.Success, iEvent);
        });
    }

    public void LoadAllEvent(Action<bool, List<IEvent>> onEventsLoaded = null)
    {
        Events.FetchAllEvents(DataSource.ReadCacheOrNetwork, (status, events) =>
        {
            onEventsLoaded?.Invoke(status == ResponseStatus.Success, events);
        });
    }
    
    // 계정 정보 수정
    public void OnChangeDisplayName(string newDisplayName)
    {
        FirebaseUser user = auth.CurrentUser;
        UserProfile profile = new UserProfile();
        profile.DisplayName = newDisplayName;
        user.UpdateUserProfileAsync(profile).ContinueWith(updateTask => 
        {
            if (updateTask.IsCanceled) {
                Debug.LogError("UpdateUserProfileAsync was canceled.");
                return;
            }
            if (updateTask.IsFaulted) {
                Debug.LogError("UpdateUserProfileAsync encountered an error: " + updateTask.Exception);
                return;
            }

            Debug.Log("User profile updated successfully.");
            Debug.Log(profile.DisplayName);
        });
    }
}