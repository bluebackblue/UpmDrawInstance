

/** BlueBack.DrawInstance.Samples.Simple2D
*/
namespace BlueBack.DrawInstance.Samples.Simple2D
{
	/** Main_MonoBehaviour
	*/
	public sealed class Main_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** color2d
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
		private void Start()
		{
			this.color2d = new Color2D(this.material,this.mesh,UnityEngine.GameObject.Find("Camera").GetComponent<UnityEngine.Camera>());
		}

		/** Update
		*/
		private void Update()
		{
			this.color2d.Draw();
		}

		/** OnDisable
		*/
		private void OnDisable()
		{
			this.color2d.Dispose();
		}
	}
}

