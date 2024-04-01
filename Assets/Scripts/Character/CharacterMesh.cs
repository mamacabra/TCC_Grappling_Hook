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

        public void SetMesh(CharaterModel charaterModel) {
            if(meshRenderer) meshRenderer.gameObject.SetActive(false);
            GameObject modelPrefab = Resources.Load<ResourcesPrefabs>("CharacterModels").prefabs[(int)charaterModel];
            MeshRenderer _meshRenderer = modelPrefab.GetComponent<MeshRenderer>();
            meshRenderer = Instantiate(_meshRenderer, meshParent);
        }
    }
}