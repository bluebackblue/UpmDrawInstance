

/** BlueBack.DrawInstance.Samples.Simple3D
*/
namespace BlueBack.DrawInstance.Samples.Simple3D
{
	/** Main_MonoBehaviour
	*/
	public class Main_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** color3d
		*/
		public Color3D color3d;

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
			this.color3d = new Color3D(this.material,this.mesh,UnityEngine.GameObject.Find("Camera").GetComponent<UnityEngine.Camera>());
		}

		/** Update
		*/
		private void Update()
		{
			this.color3d.Draw();
		}

		/** OnDisable
		*/
		private void OnDisable()
		{
			this.color3d.Dispose();
		}
	}
}

