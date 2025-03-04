using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothChanger : MonoBehaviour
    {
        public SkinnedMeshRenderer mesh;
        public Texture2D texture;
        public string shaderIdName = "_EmissionMap";

        private Texture2D _defaultTexture;

        private void Awake()
        {
            _defaultTexture = (Texture2D)mesh.materials[0].GetTexture(shaderIdName);
            mesh.materials[0].SetTexture(shaderIdName, texture);
        }


        [NaughtyAttributes.Button]
        private void ChangeTexture()
        {
            mesh.materials[0].SetTexture(shaderIdName, texture);
        }


        public void ChangeTexture(ClothSetup setup)
        {
            mesh.materials[0].SetTexture(shaderIdName, setup.texture);
        }

        public void ResetTexture()
        {
            mesh.materials[0].SetTexture(shaderIdName, _defaultTexture);
        }
    }
}
