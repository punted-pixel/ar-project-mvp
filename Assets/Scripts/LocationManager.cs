using UnityEngine;
using System.Collections;

public class LocationManager : MonoBehaviour
{
    IEnumerator Start()
    {
        Debug.LogError("STARTING LOCATION SERVICES");
        // Check if the user has location service enabled.

        //NOTE FOR LATER:  Unity's permission request code doesn't halt code.  Add co routine later.
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.CoarseLocation)) {
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.CoarseLocation);
        }

        if (!Input.location.isEnabledByUser) {
            Debug.Log("Location is not enabled.");
            yield break;

          }

        // Starts the location service.
        Debug.Log("LOCATION IS ENABLED AHHHHHHH");
        Input.location.Start();

        // Waits until the location service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            yield break;
        }

        // If the connection failed this cancels location service use.
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }
        else
        {
            // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
            Debug.Log("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.deviceOrientation);
        }

        // Stops the location service if there is no need to query location updates continuously.
        Input.location.Stop();
    }
    void Update() {

        Debug.LogError("AHHHHHHHHH AHAHH HAHAHAHAH AHHHHHHH");
    
    }
}