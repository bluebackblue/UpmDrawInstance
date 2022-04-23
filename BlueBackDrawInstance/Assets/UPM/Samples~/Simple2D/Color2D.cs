

/** BlueBack.DrawInstance.Samples.Simple2D
*/
namespace BlueBack.DrawInstance.Samples.Simple2D
{
	/** Color2D
	*/
	public sealed class Color2D : System.IDisposable
	{
		/** MAX
		*/
		public const int MAX_X = 16 * 16;
		public const int MAX_Y = 9 * 16;

		/** mesh
		*/
		public UnityEngine.Mesh mesh;

		/** マテリアル。
		*/
		public UnityEngine.Material material;
		public int material_id_status;
		public int material_id_custom_matrix;

		/** camera
		*/
		public UnityEngine.Camera camera;

		/** drawinstance
		*/
		public BlueBack.DrwaInstance.DrawInstance drawinstance;
		public BlueBack.DrwaInstance.Buffer<Color2D_Status> drawinstance_buffer;

		/** index
		*/
		private int index;

		/** constructor
		*/
		public Color2D(UnityEngine.Material a_material,UnityEngine.Mesh a_mesh,UnityEngine.Camera a_camera)
		{
			//mesh
			this.mesh = a_mesh;

			//material
			this.material = a_material;
			this.material_id_status = UnityEngine.Shader.PropertyToID("status");
			this.material_id_custom_matrix = UnityEngine.Shader.PropertyToID("custom_matrix");

			//camera
			this.camera = a_camera;

			//custom_matrix
			{
				float t_scale = 1.0f / (MAX_Y);
				float t_aspect = (float)UnityEngine.Screen.height / UnityEngine.Screen.width;
			
				UnityEngine.Matrix4x4 t_custom_matrix = new UnityEngine.Matrix4x4(
					new UnityEngine.Vector4(t_aspect * t_scale * 2,0.0f,0.0f,0.0f),
					new UnityEngine.Vector4(0.0f,t_scale * 2,0.0f,0.0f),
					new UnityEngine.Vector4(0.0f,0.0f,t_scale * 2,0.0f),
					new UnityEngine.Vector4(-1.0f,-1.0f,0.0f,1.0f)
				);

				this.material.SetMatrix(this.material_id_custom_matrix,t_custom_matrix);
			}

			//drawinstance
			this.drawinstance = new BlueBack.DrwaInstance.DrawInstance(this.mesh);
			this.drawinstance_buffer = new DrwaInstance.Buffer<Color2D_Status>(MAX_X * MAX_Y,24);

			//index
			this.index = 0;
		}

		/** Draw
		*/
		public void Draw()
		{
			int t_drawcount = MAX_X * MAX_Y;
			
			{
				this.index++;
				for(int yy=0;yy<MAX_Y;yy++){
					for(int xx=0;xx<MAX_X;xx++){
						UnityEngine.Vector2 t_local = new UnityEngine.Vector2((MAX_X / 2 - xx),(MAX_Y / 2 - yy));
						this.drawinstance_buffer.raw[yy * MAX_X + xx] = new Color2D_Status(
							new UnityEngine.Vector3(xx,yy,0.0f),
							new UnityEngine.Vector4(
								UnityEngine.Mathf.Abs(UnityEngine.Mathf.Sin(UnityEngine.Time.realtimeSinceStartup - t_local.magnitude * 0.01f + 1)),
								UnityEngine.Mathf.Abs(UnityEngine.Mathf.Sin(UnityEngine.Time.realtimeSinceStartup - t_local.magnitude * 0.01f + 2)),
								UnityEngine.Mathf.Abs(UnityEngine.Mathf.Sin(UnityEngine.Time.realtimeSinceStartup - t_local.magnitude * 0.01f + 3)),
								((xx + yy * MAX_X) == (this.index % (MAX_Y * MAX_Y))) ? 0.0f : 1.0f
							)
						);
					}
				}
				this.drawinstance_buffer.Apply(this.material,this.material_id_status);
			}

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

