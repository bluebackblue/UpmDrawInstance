

/** BlueBack.DrawInstance.Samples.Simple
*/
namespace BlueBack.DrawInstance.Samples.Simple
{
	/** Color2D_GraphicsBuffer
	*/
	public sealed class Color2D_GraphicsBuffer : BlueBack.DrwaInstance.GraphicsBuffer_Generic_Base<Color2D_Status>
	{
		/** index
		*/
		private int index;

		/** constructor
		*/
		public Color2D_GraphicsBuffer()
		{
			this.index = 0;
		}

		/** Create
		*/
		public override void Create(out UnityEngine.GraphicsBuffer a_graphicsbuffer,out Unity.Collections.NativeArray<Color2D_Status> a_list)
		{
			//graphicsbuffer
			a_graphicsbuffer = new UnityEngine.GraphicsBuffer(UnityEngine.GraphicsBuffer.Target.Structured,Color2D.X_MAX * Color2D.Y_MAX,24);

			//list
			a_list = new Unity.Collections.NativeArray<Color2D_Status>(Color2D.X_MAX * Color2D.Y_MAX,Unity.Collections.Allocator.Persistent,Unity.Collections.NativeArrayOptions.UninitializedMemory);
		}

		/** Delete
		*/
		public override void Delete(ref UnityEngine.GraphicsBuffer a_graphicsbuffer,ref Unity.Collections.NativeArray<Color2D_Status> a_list)
		{
			//graphicsbuffer
			if(a_graphicsbuffer != null){
				a_graphicsbuffer.Dispose();
				a_graphicsbuffer = null;
			}

			//list
			if(a_list.IsCreated == true){
				a_list.Dispose();
			}
		}

		/** PreDraw
		*/
		public override void PreDraw(UnityEngine.Material a_material,UnityEngine.GraphicsBuffer a_graphicsbuffer,Unity.Collections.NativeArray<Color2D_Status> a_list)
		{
			this.index++;

			for(int yy=0;yy<Color2D.Y_MAX;yy++){
				for(int xx=0;xx<Color2D.X_MAX;xx++){
					UnityEngine.Vector2 t_local = new UnityEngine.Vector2((Color2D.X_MAX / 2 - xx),(Color2D.Y_MAX / 2 - yy));
					a_list[yy * Color2D.X_MAX + xx] = new Color2D_Status(){
						position = new UnityEngine.Vector3(xx,yy,0.0f),
						color = new UnityEngine.Vector4(
							UnityEngine.Mathf.Abs(UnityEngine.Mathf.Sin(UnityEngine.Time.realtimeSinceStartup - t_local.magnitude * 0.01f + 1)),
							UnityEngine.Mathf.Abs(UnityEngine.Mathf.Sin(UnityEngine.Time.realtimeSinceStartup - t_local.magnitude * 0.01f + 2)),
							UnityEngine.Mathf.Abs(UnityEngine.Mathf.Sin(UnityEngine.Time.realtimeSinceStartup - t_local.magnitude * 0.01f + 3)),
							((xx + yy * Color2D.X_MAX) == (this.index % (Color2D.Y_MAX * Color2D.Y_MAX))) ? 0.0f : 1.0f
						),
					};
				}
			}

			a_graphicsbuffer.SetData(a_list);

			a_material.SetBuffer("status",a_graphicsbuffer);
		}
	}
}

