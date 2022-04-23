

/** BlueBack.DrawInstance.Samples.Simple3D
*/
namespace BlueBack.DrawInstance.Samples.Simple3D
{
	/** Color3D
	*/
	public sealed class Color3D : System.IDisposable
	{
		/** MAX
		*/
		public const int MAX = 10000;

		/** mesh
		*/
		public UnityEngine.Mesh mesh;

		/** マテリアル。
		*/
		public UnityEngine.Material material;
		public int material_id_status;

		/** camera
		*/
		public UnityEngine.Camera camera;

		/** drawinstance
		*/
		public BlueBack.DrwaInstance.DrawInstance drawinstance;
		public BlueBack.DrwaInstance.Buffer<Color3D_Status> drawinstance_buffer;

		/** 計算用。
		*/
		public Color3D_Item[] list;

		/** constructor
		*/
		public Color3D(UnityEngine.Material a_material,UnityEngine.Mesh a_mesh,UnityEngine.Camera a_camera)
		{
			//mesh
			this.mesh = a_mesh;

			//material
			this.material = a_material;
			this.material_id_status = UnityEngine.Shader.PropertyToID("status");

			//camera
			this.camera = a_camera;

			//drawinstance
			this.drawinstance = new BlueBack.DrwaInstance.DrawInstance(this.mesh);
			this.drawinstance_buffer = new DrwaInstance.Buffer<Color3D_Status>(MAX,80);

			//this
			this.list = new Color3D_Item[MAX];
			for(int ii=0;ii<this.list.Length;ii++){
				this.list[ii] = new Color3D_Item(){
					position = new UnityEngine.Vector3(UnityEngine.Random.value * 100 - 50,UnityEngine.Random.value * 100 - 50,UnityEngine.Random.value * 100 - 50),
					position_delta = UnityEngine.Quaternion.AngleAxis(UnityEngine.Random.value * 1.0f,new UnityEngine.Vector3(UnityEngine.Random.value * 0.1f,1.0f,UnityEngine.Random.value * 0.1f).normalized),
					quaternion = UnityEngine.Quaternion.identity,
					scale = new UnityEngine.Vector3(1,1,1),
					quaternion_delta = UnityEngine.Quaternion.AngleAxis(UnityEngine.Random.value * 3.0f,new UnityEngine.Vector3(1,1,1).normalized)
				};
			}
		}

		/** Draw
		*/
		public void Draw()
		{
			int t_drawcount = MAX;

			for(int ii=0;ii<t_drawcount;ii++){
				this.list[ii].quaternion = this.list[ii].quaternion_delta * this.list[ii].quaternion;
				this.list[ii].quaternion.Normalize();

				this.list[ii].position = this.list[ii].position_delta * this.list[ii].position;

				UnityEngine.Matrix4x4 t_matrix = UnityEngine.Matrix4x4.TRS(this.list[ii].position,this.list[ii].quaternion,this.list[ii].scale);

				UnityEngine.Vector4 t_color = new UnityEngine.Vector4(
					UnityEngine.Mathf.Abs(UnityEngine.Mathf.Sin(UnityEngine.Time.realtimeSinceStartup * 15 - ii + 1)),
					UnityEngine.Mathf.Abs(UnityEngine.Mathf.Sin(UnityEngine.Time.realtimeSinceStartup * 15 - ii + 2)),
					UnityEngine.Mathf.Abs(UnityEngine.Mathf.Sin(UnityEngine.Time.realtimeSinceStartup * 15 - ii + 3)),
					1.0f
				);

				this.drawinstance_buffer.raw[ii] = new Color3D_Status(t_matrix,t_color);
			}

			this.drawinstance_buffer.Apply(this.material,this.material_id_status);

			this.drawinstance.Draw(this.material,this.camera,0,t_drawcount,0);
		}

		/** [System.IDisposable]破棄。
		*/
		public void Dispose()
		{
			//drawinstance
			if(this.drawinstance != null){
				this.drawinstance.Dispose();
				this.drawinstance = null;
			}

			//drawinstance_buffer
			if(this.drawinstance_buffer != null){
				this.drawinstance_buffer.Dispose();
				this.drawinstance_buffer = null;
			}
		}
	}
}

