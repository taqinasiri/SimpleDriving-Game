using System;

#if UNITY_ANDROID

using Unity.Notifications.Android;

#endif

using UnityEngine;

public class AndroidNotificationHandler : MonoBehaviour
{
#if UNITY_ANDROID
    private const string ChannlId = "notification_cahnnel";

    public void ScheduleNotification(DateTime dateTime)
    {
        AndroidNotificationChannel notificationChannel = new()
        {
            Id = ChannlId,
            Name = "Notification Channel",
            Description = "Description",
            Importance = Importance.Default,
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        AndroidNotification notification = new()
        {
            Title = "Energy Recharged!",
            Text = "Your energy has recharged, com back to play again!",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = dateTime
        };

        AndroidNotificationCenter.SendNotification(notification,ChannlId);
    }

#endif
}