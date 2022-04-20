

/** BlueBack.DrwaInstance
*/
namespace BlueBack.DrwaInstance
{
	/** DrawInstance
	*/
	public class DrawInstance : System.IDisposable
	{
		/** mesh
		*/
		private UnityEngine.Mesh mesh;

		/** execute
		*/
		private DrawInstance_Execute_Base execute;

		/** graphicbuffer_list
		*/
		private GraphicsBuffer_Base[] graphicbuffer_list;

		/** count
		*/
		private int count;

		/** drawinstanceparam
		*/
		private UnityEngine.ComputeBuffer drawinstanceparam_buffer;
		private uint[] drawinstanceparam_raw;

		/** boudn
		*/
		private UnityEngine.Bounds bound;

		/** shadowcastingmode
		*/
		private UnityEngine.Rendering.ShadowCastingMode shadowcastingmode;

		/** receiveshadows
		*/
		private bool receiveshadows;

		/** lightprobeusage
		*/
		private UnityEngine.Rendering.LightProbeUsage lightprobeusage;

		/** constructor
		*/
		public DrawInstance(UnityEngine.Mesh a_mesh,DrawInstance_Execute_Base a_execute,GraphicsBuffer_Base[] a_graphicbuffer_list)
		{
			//mesh
			this.mesh = a_mesh;

			//execute
			this.execute = a_execute;

			//graphicbuffer
			this.graphicbuffer_list = a_graphicbuffer_list;
			for(int ii=0;ii<this.graphicbuffer_list.Length;ii++){
				this.graphicbuffer_list[ii].Create();
			}

			//count
			this.count = 0;

			//drawinstanceparam
			this.drawinstanceparam_raw = new uint[5]{0,0,0,0,0};
			this.drawinstanceparam_raw[0] = (uint)this.mesh.GetIndexCount(0);
			this.drawinstanceparam_raw[1] = (uint)this.count;
			this.drawinstanceparam_raw[2] = (uint)this.mesh.GetIndexStart(0);
			this.drawinstanceparam_raw[3] = (uint)this.mesh.GetBaseVertex(0);
			this.drawinstanceparam_buffer = new UnityEngine.ComputeBuffer(1,this.drawinstanceparam_raw.Length * sizeof(uint),UnityEngine.ComputeBufferType.IndirectArguments);
			this.drawinstanceparam_buffer.SetData(this.drawinstanceparam_raw);

			//bound
			this.bound = new UnityEngine.Bounds(UnityEngine.Vector3.zero,new UnityEngine.Vector3(1000.0f,1000.0f,1000.0f));

			//shadowcastingmode
			this.shadowcastingmode = UnityEngine.Rendering.ShadowCastingMode.Off;

			//receiveshadows
			this.receiveshadows = false;

			//lightprobeusage
			this.lightprobeusage = UnityEngine.Rendering.LightProbeUsage.Off;
		}

		/** [System.IDisposable]Dispose
		*/
		public void Dispose()
		{
			//mesh
			this.mesh = null;

			//execute
			this.execute = null;

			//graphicbuffer
			this.graphicbuffer_list = null;

			//drawinstanceparam
			if(this.drawinstanceparam_buffer != null){
				this.drawinstanceparam_buffer.Dispose();
				this.drawinstanceparam_buffer = null;
			}
		}

		/** Draw
		*/
		public void Draw(UnityEngine.Material a_material,UnityEngine.Camera a_camera,int a_layer)
		{
			//PreDraw
			this.count = this.execute.PreDraw(a_material,this.graphicbuffer_list);

			//drawinstanceparam
			{
				this.drawinstanceparam_raw[1] = (uint)this.count;
				this.drawinstanceparam_buffer.SetData(this.drawinstanceparam_raw);
			}

			//DrawMeshInstancedIndirect
			UnityEngine.Graphics.DrawMeshInstancedIndirect(
				this.mesh,
				0,
				a_material,
				this.bound,
				this.drawinstanceparam_buffer,
				0,
				null,
				this.shadowcastingmode,
				this.receiveshadows,
				a_layer,
				a_camera,
				this.lightprobeusage
			);
		}
	}
}
