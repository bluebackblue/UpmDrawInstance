

/** BlueBack.DrwaInstance
*/
namespace BlueBack.DrwaInstance
{
	/** GraphicsBuffer_Base
	*/
	public interface GraphicsBuffer_Base : System.IDisposable
	{
		/** Create
		*/
		void Create();

		/** PreDraw
		*/
		void PreDraw(UnityEngine.Material a_material);
	}
}

