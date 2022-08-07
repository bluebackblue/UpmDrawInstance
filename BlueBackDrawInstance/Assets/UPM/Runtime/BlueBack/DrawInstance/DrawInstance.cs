

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief ドローインスタンス。
*/


/** BlueBack.DrawInstance
*/
namespace BlueBack.DrawInstance
{
	/** DrawInstance
	*/
	public class DrawInstance : System.IDisposable
	{
		/** mesh
		*/
		private UnityEngine.Mesh mesh;

		/** drawinstanceparam
		*/
		private UnityEngine.ComputeBuffer drawinstanceparam_buffer;
		private uint[] drawinstanceparam_raw;

		/** boudn
		*/
		public UnityEngine.Bounds bound;

		/** shadowcastingmode
		*/
		public UnityEngine.Rendering.ShadowCastingMode shadowcastingmode;

		/** receiveshadows
		*/
		public bool receiveshadows;

		/** lightprobeusage
		*/
		public UnityEngine.Rendering.LightProbeUsage lightprobeusage;

		/** constructor
		*/
		public DrawInstance(UnityEngine.Mesh a_mesh)
		{
			//mesh
			this.mesh = a_mesh;

			//drawinstanceparam
			this.drawinstanceparam_raw = new uint[5]{0,0,0,0,0};
			this.drawinstanceparam_raw[0] = (uint)this.mesh.GetIndexCount(0);
			this.drawinstanceparam_raw[1] = (uint)0;
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

			//drawinstanceparam_buffer
			if(this.drawinstanceparam_buffer != null){
				this.drawinstanceparam_buffer.Dispose();
				this.drawinstanceparam_buffer = null;
			}

			//drawinstanceparam_raw
			this.drawinstanceparam_raw = null;
		}

		/** Draw
		*/
		public void Draw(UnityEngine.Material a_material,UnityEngine.MaterialPropertyBlock a_material_propertyblock,UnityEngine.Camera a_camera,int a_layer,int a_drawcount,int a_submesh)
		{
			//drawinstanceparam
			{
				this.drawinstanceparam_raw[1] = (uint)a_drawcount;
				this.drawinstanceparam_buffer.SetData(this.drawinstanceparam_raw);
			}

			//DrawMeshInstancedIndirect
			UnityEngine.Graphics.DrawMeshInstancedIndirect(
				this.mesh,
				a_submesh,
				a_material,
				this.bound,
				this.drawinstanceparam_buffer,
				0,
				a_material_propertyblock,
				this.shadowcastingmode,
				this.receiveshadows,
				a_layer,
				a_camera,
				this.lightprobeusage
			);
		}
	}
}

