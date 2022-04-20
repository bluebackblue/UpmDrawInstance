

/** BlueBack.DrawInstance.Samples.Simple
*/
namespace BlueBack.DrawInstance.Samples.Simple
{
	/** Main_MonoBehaviour
	*/
	public class Main_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** execute
		*/
		public Color2D color2d;

		/** material
		*/
		public UnityEngine.Material material;

		/** mesh
		*/
		public UnityEngine.Mesh mesh;

		/** Start
		*/
		void Start()
		{
			this.color2d = new Color2D(this.material,this.mesh);
		}

		/** Update
		*/
		void Update()
		{
			this.color2d.Draw();
		}

		/** OnDisable
		*/
		void OnDisable()
		{
			this.color2d.Dispose();
		}
	}
}

