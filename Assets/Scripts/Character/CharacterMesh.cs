using System.Collections;
using System.Collections.Generic;
using Character.Utils;
using UnityEditor.Animations;
using UnityEngine;

namespace Character
{
    public class CharacterMesh : ACharacterMonoBehaviour
    {
        [SerializeField] private Transform meshParent;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
        [SerializeField] private Animator animator;

        public new void Setup(CharacterEntity entity)
        {
            CharacterEntity = entity;

            var meshParent = transform.Find("MeshParent");
        }

        public void SetColor(PlayersManager.CharacterColor characterColor) {
            if (meshRenderer)
                meshRenderer.material.color = PlayersManager.GetColor(characterColor);
            if (skinnedMeshRenderer)
                skinnedMeshRenderer.material.color = PlayersManager.GetColor(characterColor);
        }

        public void SetMesh(ECharacterType charaterType) {
            if (meshRenderer) meshRenderer.gameObject.SetActive(false);
            if (skinnedMeshRenderer) skinnedMeshRenderer.gameObject.SetActive(false);
            GameObject _modelPrefab = Resources.Load<ResourcesCharacters>("ResourcesCharacters").GetCharacterData(charaterType).characterPrefab;
            GameObject instance = Instantiate(_modelPrefab, meshParent);
            MeshRenderer _meshRenderer = instance.GetComponentInChildren<MeshRenderer>();
            if (_meshRenderer) {
                meshRenderer = _meshRenderer;
            }
            SkinnedMeshRenderer _skinnedMeshRenderer = instance.GetComponentInChildren<SkinnedMeshRenderer>();
            if (_skinnedMeshRenderer) {
                skinnedMeshRenderer = _skinnedMeshRenderer;
            }
            animator = instance.GetComponentInChildren<Animator>();
        }
    }
}