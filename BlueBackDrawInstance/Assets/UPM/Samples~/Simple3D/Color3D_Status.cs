

/** BlueBack.DrawInstance.Samples.Simple3D
*/
namespace BlueBack.DrawInstance.Samples.Simple3D
{
	/** Color3D_Status
	*/
	public readonly struct Color3D_Status
	{
		/** localmatrix
		*/
		public readonly UnityEngine.Matrix4x4 localmatrix;

		/** color
		*/
		public readonly UnityEngine.Vector4 color;

		/** constructor
		*/
		public Color3D_Status(UnityEngine.Matrix4x4 a_localmatrix,UnityEngine.Vector4 a_color)
		{
			//localmatrix
			this.localmatrix = a_localmatrix;

			//color
			this.color = a_color;
		}
	}
}

