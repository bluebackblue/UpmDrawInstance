

/** BlueBack.DrwaInstance
*/
namespace BlueBack.DrwaInstance
{
	/** GraphicsBuffer_Generic_Base
	*/
	public abstract class GraphicsBuffer_Generic_Base<STRUCTUREDTYPE> : GraphicsBuffer_Base
		where STRUCTUREDTYPE : struct
	{
		/** graphicsbuffer
		*/
		private UnityEngine.GraphicsBuffer graphicsbuffer;

		/** list
		*/
		private Unity.Collections.NativeArray<STRUCTUREDTYPE> list;

		/** Create
		*/
		public void Create()
		{
			this.Create(out this.graphicsbuffer,out this.list);
		}

		/** PreDraw
		*/
		public void PreDraw(UnityEngine.Material a_material)
		{
			this.PreDraw(a_material,this.graphicsbuffer,this.list);
		}

		/** Create
		*/
		public abstract void Create(out UnityEngine.GraphicsBuffer a_graphicsbuffer,out Unity.Collections.NativeArray<STRUCTUREDTYPE> a_list);

		/** PreDraw
		*/
		public abstract void PreDraw(UnityEngine.Material a_material,UnityEngine.GraphicsBuffer a_graphicsbuffer,Unity.Collections.NativeArray<STRUCTUREDTYPE> a_list);

		/** Delete
		*/
		public abstract void Delete(ref UnityEngine.GraphicsBuffer a_graphicsbuffer,ref Unity.Collections.NativeArray<STRUCTUREDTYPE> a_list);

		/** [BlueBack.DrwaInstance.GraphicsBuffer_Generic_Base]Dispose
		*/
		public void Dispose()
		{
			this.Delete(ref this.graphicsbuffer,ref this.list);
		}
	}
}

