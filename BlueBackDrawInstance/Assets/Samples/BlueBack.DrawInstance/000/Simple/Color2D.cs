

/** BlueBack.DrawInstance.Samples.Simple
*/
namespace BlueBack.DrawInstance.Samples.Simple
{
	/** Color2D
	*/
	public sealed class Color2D : BlueBack.DrwaInstance.Execute_Base , System.IDisposable
	{
		/** MAX
		*/
		public const int X_MAX = 16 * 16;
		public const int Y_MAX = 9 * 16;

		/** mesh
		*/
		public UnityEngine.Mesh mesh;

		/** material
		*/
		public UnityEngine.Material material;

		/** camera
		*/
		public UnityEngine.Camera camera;

		/** graphicbuffer_list
		*/
		public BlueBack.DrwaInstance.Buffer<Color2D_Status> drawinstance_buffer;

		/** drawinstance
		*/
		public BlueBack.DrwaInstance.DrawInstance drawinstance;

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

			//camera
			this.camera = a_camera;

			//custom_matrix
			{
				float t_scale = 1.0f / (Y_MAX);
				float t_aspect = (float)UnityEngine.Screen.height / UnityEngine.Screen.width;
			
				UnityEngine.Matrix4x4 t_custom_matrix = new UnityEngine.Matrix4x4(
					new UnityEngine.Vector4(t_aspect * t_scale * 2,0.0f,0.0f,0.0f),
					new UnityEngine.Vector4(0.0f,t_scale * 2,0.0f,0.0f),
					new UnityEngine.Vector4(0.0f,0.0f,t_scale * 2,0.0f),
					new UnityEngine.Vector4(-1.0f,-1.0f,0.0f,1.0f)
				);

				this.material.SetMatrix("custom_matrix",t_custom_matrix);
			}

			this.drawinstance_buffer = new DrwaInstance.Buffer<Color2D_Status>(X_MAX * Y_MAX,24);

			//drawinstance
			this.drawinstance = new BlueBack.DrwaInstance.DrawInstance(this.mesh,this);

			//index
			this.index = 0;
		}

		/** Draw
		*/
		public void Draw()
		{
			this.drawinstance.Draw(this.material,this.camera,0,0);
		}

		/** [BlueBack.DrwaInstance.Execute_Base]PreDraw
		*/
		public int PreDraw(UnityEngine.Material a_material)
		{
			this.index++;

			for(int yy=0;yy<Color2D.Y_MAX;yy++){
				for(int xx=0;xx<Color2D.X_MAX;xx++){
					UnityEngine.Vector2 t_local = new UnityEngine.Vector2((Color2D.X_MAX / 2 - xx),(Color2D.Y_MAX / 2 - yy));
					this.drawinstance_buffer.raw[yy * Color2D.X_MAX + xx] = new Color2D_Status(
						new UnityEngine.Vector3(xx,yy,0.0f),
						new UnityEngine.Vector4(
							UnityEngine.Mathf.Abs(UnityEngine.Mathf.Sin(UnityEngine.Time.realtimeSinceStartup - t_local.magnitude * 0.01f + 1)),
							UnityEngine.Mathf.Abs(UnityEngine.Mathf.Sin(UnityEngine.Time.realtimeSinceStartup - t_local.magnitude * 0.01f + 2)),
							UnityEngine.Mathf.Abs(UnityEngine.Mathf.Sin(UnityEngine.Time.realtimeSinceStartup - t_local.magnitude * 0.01f + 3)),
							((xx + yy * Color2D.X_MAX) == (this.index % (Color2D.Y_MAX * Color2D.Y_MAX))) ? 0.0f : 1.0f
						)
					);
				}
			}

			this.drawinstance_buffer.Apply(a_material,"status");

			return X_MAX * Y_MAX;
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

