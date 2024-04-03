using System.Collections;
using System.Collections.Generic;
using Character.Utils;
using UnityEngine;

namespace Character
{
    public class CharacterMesh : ACharacterMonoBehaviour
    {
        [SerializeField] private Transform meshParent;
        [SerializeField] private MeshRenderer meshRenderer;

        public new void Setup(CharacterEntity entity)
        {
            CharacterEntity = entity;

            var meshParent = transform.Find("MeshParent");
        }

        public void SetColor(PlayersManager.CharacterColor characterColor) {
            meshRenderer.material.color = PlayersManager.GetColor(characterColor);
        }

        public void SetMesh(ECharacterType charaterType) {
            if(meshRenderer) meshRenderer.gameObject.SetActive(false);
            GameObject modelPrefab = Resources.Load<ResourcesCharacters>("ResourcesCharacters").GetCharacterData(charaterType).characterPrefab;
            MeshRenderer _meshRenderer = modelPrefab.GetComponent<MeshRenderer>();
            meshRenderer = Instantiate(_meshRenderer, meshParent);
        }
    }
}