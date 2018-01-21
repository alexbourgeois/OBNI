//http://docs.unity3d.com/412/Documentation/ScriptReference/Camera.OnPreCull.html
using UnityEngine;
using System.Collections;

namespace Spout{

	[RequireComponent (typeof(Camera))]
	[ExecuteInEditMode]
	public class InvertCamera : MonoBehaviour {
		private Camera camera;
		void Start ()
		{
		    camera = GetComponent<Camera>();
		}
		
		void OnPreCull () {
		    if (camera.activeTexture != null)
		    {
		        GetComponent<Camera>().ResetWorldToCameraMatrix();
		        GetComponent<Camera>().ResetProjectionMatrix();
		        GetComponent<Camera>().projectionMatrix = GetComponent<Camera>().projectionMatrix *
		                                                  Matrix4x4.Scale(new Vector3(1, -1, 1));
		    }
		}
		
		void OnPreRender () {
		    if (camera.activeTexture != null)
		        GL.invertCulling = true;
		}
		
		void OnPostRender () {
		    if (camera.activeTexture != null)
                GL.invertCulling = false;
		}
		
	}

}