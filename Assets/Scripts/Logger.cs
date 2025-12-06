using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

public static class Logger {
  [Conditional("DEBUG")]
  public static void Log(object message, Object obj = null) {
    Debug.Log(message, obj);
  }

  [Conditional("DEBUG")]
  public static void LogError(object message, Object obj = null) {
    Debug.LogError(message, obj);
  }

  [Conditional("DEBUG")]
  public static void LogWarning(object message, Object obj = null) {
    Debug.LogWarning(message, obj);
  }

  [Conditional("DEBUG")]
  public static void LogException(Exception exception) {
    Debug.LogException(exception);
  }
}