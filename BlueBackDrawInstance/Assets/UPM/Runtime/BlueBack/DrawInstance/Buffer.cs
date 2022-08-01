

/** BlueBack.DrawInstance
*/
namespace BlueBack.DrawInstance
{
	/** Buffer
	*/
	public sealed class Buffer<STRUCTUREDTYPE> : System.IDisposable
		where STRUCTUREDTYPE : struct
	{
		/** graphicsbuffer
		*/
		public UnityEngine.GraphicsBuffer graphicsbuffer;

		/** raw
		*/
		public Unity.Collections.NativeArray<STRUCTUREDTYPE> raw;

		/** constructor
		*/
		public Buffer(int a_max,int a_stride)
		{
			//graphicsbuffer
			this.graphicsbuffer = new UnityEngine.GraphicsBuffer(UnityEngine.GraphicsBuffer.Target.Structured,a_max,a_stride);

			//raw
			this.raw = new Unity.Collections.NativeArray<STRUCTUREDTYPE>(a_max,Unity.Collections.Allocator.Persistent,Unity.Collections.NativeArrayOptions.UninitializedMemory);
		}

		/** [System.IDisposable]破棄。
		*/
		public void Dispose()
		{
			//graphicsbuffer
			if(this.graphicsbuffer != null){
				this.graphicsbuffer.Dispose();
				this.graphicsbuffer = null;
			}

			//raw
			if(this.raw.IsCreated == true){
				this.raw.Dispose();
			}
		}

		/** Apply
		*/
		public void Apply(UnityEngine.Material a_material,string a_name)
		{
			this.graphicsbuffer.SetData(this.raw);
			a_material.SetBuffer(a_name,this.graphicsbuffer);
		}

		/** Apply
		*/
		public void Apply(UnityEngine.Material a_material,int a_shader_property_id)
		{
			this.graphicsbuffer.SetData(this.raw);
			a_material.SetBuffer(a_shader_property_id,this.graphicsbuffer);
		}

		/** Apply
		*/
		public void Apply(UnityEngine.MaterialPropertyBlock a_materialpropertyblock,string a_name)
		{
			this.graphicsbuffer.SetData(this.raw);
			a_materialpropertyblock.SetBuffer(a_name,this.graphicsbuffer);
		}

		/** Apply
		*/
		public void Apply(UnityEngine.MaterialPropertyBlock a_materialpropertyblock,int a_shader_property_id)
		{
			this.graphicsbuffer.SetData(this.raw);
			a_materialpropertyblock.SetBuffer(a_shader_property_id,this.graphicsbuffer);
		}
	}
}

