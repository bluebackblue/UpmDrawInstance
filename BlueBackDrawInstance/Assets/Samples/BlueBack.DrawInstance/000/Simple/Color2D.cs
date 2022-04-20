

/** BlueBack.DrawInstance.Samples.Simple
*/
namespace BlueBack.DrawInstance.Samples.Simple
{
	/** Color2D
	*/
	public sealed class Color2D : BlueBack.DrwaInstance.DrawInstance_Execute_Base , System.IDisposable
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

		/** drawcamera
		*/
		public UnityEngine.Camera drawcamera;

		/** graphicbuffer_list
		*/
		public BlueBack.DrwaInstance.GraphicsBuffer_Base[] graphicbuffer_list;

		/** drawinstance
		*/
		public BlueBack.DrwaInstance.DrawInstance drawinstance;

		/** constructor
		*/
		public Color2D(UnityEngine.Material a_material,UnityEngine.Mesh a_mesh)
		{
			//material
			this.material = a_material;

			//mesh
			this.mesh = a_mesh;

			//drawcamera
			this.drawcamera = UnityEngine.GameObject.Find("Camera").GetComponent<UnityEngine.Camera>();

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

			//graphicbuffer_list
			this.graphicbuffer_list = new BlueBack.DrwaInstance.GraphicsBuffer_Base[]{
				new Color2D_GraphicsBuffer(),
			};

			//drawinstance
			this.drawinstance = new BlueBack.DrwaInstance.DrawInstance(this.mesh,this,this.graphicbuffer_list);
		}

		/** Draw
		*/
		public void Draw()
		{
			this.drawinstance.Draw(this.material,this.drawcamera,0);
		}

		/** [BlueBack.DrwaInstance.DrawInstance_Execute_Base]PreDraw
		*/
		public int PreDraw(UnityEngine.Material a_material,BlueBack.DrwaInstance.GraphicsBuffer_Base[] a_graphicbuffer_list)
		{
			for(int ii=0;ii<a_graphicbuffer_list.Length;ii++){
				a_graphicbuffer_list[ii].PreDraw(a_material);
			}

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

			//graphicbuffer
			if(this.graphicbuffer_list != null){
				for(int ii=0;ii<this.graphicbuffer_list.Length;ii++){
					this.graphicbuffer_list[ii].Dispose();
				}
				this.graphicbuffer_list = null;
			}
		}
	}
}

