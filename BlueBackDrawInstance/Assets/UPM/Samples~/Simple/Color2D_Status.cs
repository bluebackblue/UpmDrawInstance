

/** BlueBack.DrawInstance.Samples.Simple
*/
namespace BlueBack.DrawInstance.Samples.Simple
{
	/** Color2D_Status
	*/
	public readonly struct Color2D_Status
	{
		/** position
		*/
		public readonly UnityEngine.Vector2 position;

		/** color
		*/
		public readonly UnityEngine.Vector4 color;

		/** constructor
		*/
		public Color2D_Status(UnityEngine.Vector2 a_position,UnityEngine.Vector4 a_color)
		{
			//position
			this.position = a_position;

			//color
			this.color = a_color;
		}
	}
}

