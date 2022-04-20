

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief デバッグツール。自動生成。
*/


/** BlueBack.DrawInstance
*/
namespace BlueBack.DrawInstance
{
	/** DebugTool
	*/
	public static class DebugTool
	{
		/** s_AssertProc
		*/
		#if(DEF_BLUEBACK_DRAWINSTANCE_ASSERT)
		public static void DefaultAssertProc(System.Exception a_exception,string a_message)
		{
			if(a_message != null){
				UnityEngine.Debug.LogError(a_message);
			}

			if(a_exception != null){
				UnityEngine.Debug.LogError(a_exception.ToString());
			}

			UnityEngine.Debug.Assert(false);
		}
		public delegate void AssertProcType(System.Exception a_exception,string a_message);
		public static AssertProcType s_AssertProc = DefaultAssertProc;
		#endif

		/** s_LogProc
		*/
		#if(DEF_BLUEBACK_DRAWINSTANCE_LOG)
		public static void DefaultLogProc(string a_message)
		{
			UnityEngine.Debug.Log(a_message);
		}
		public delegate void LogProcType(string a_message);
		public static LogProcType s_LogProc = DebugTool.DefaultLogProc;
		#endif

		/** Assert
		*/
		#if(DEF_BLUEBACK_DRAWINSTANCE_ASSERT)
		public static void Assert(bool a_flag,System.Exception a_exception = null)
		{
			if(a_flag != true){
				s_AssertProc(a_exception,null);
			}
		}
		#endif

		/** Assert
		*/
		#if(DEF_BLUEBACK_DRAWINSTANCE_ASSERT)
		public static void Assert(bool a_flag,string a_message)
		{
			if(a_flag != true){
				s_AssertProc(null,a_message);
			}
		}
		#endif

		#if(DEF_BLUEBACK_DRAWINSTANCE_LOG)
		public static void Log(string a_message)
		{
			s_LogProc(a_message);
		}
		#endif

		/** EditorLog
		*/
		#if(UNITY_EDITOR)
		public static void EditorLog(string a_text)
		{
			UnityEngine.Debug.Log(a_text);
		}
		#endif

		/** EditorLogError
		*/
		#if(UNITY_EDITOR)
		public static void EditorLogError(string a_text)
		{
			UnityEngine.Debug.LogError(a_text);
		}
		#endif
	}
}

