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
            if(meshRenderer) meshRenderer.gameObject.SetActive(false);
            //animator.SetFloat("Blend", speed);
            GameObject modelPrefab = Resources.Load<ResourcesCharacters>("ResourcesCharacters").GetCharacterData(charaterType).characterPrefab;
            MeshRenderer _meshRenderer = modelPrefab.GetComponentInChildren<MeshRenderer>();
            if(_meshRenderer) {
                meshRenderer = Instantiate(_meshRenderer, meshParent);
                animator = meshRenderer.GetComponent<Animator>();
            }
            SkinnedMeshRenderer _skinnedMeshRenderer = modelPrefab.GetComponentInChildren<SkinnedMeshRenderer>();
            if(_skinnedMeshRenderer) {
                skinnedMeshRenderer = Instantiate(_skinnedMeshRenderer, meshParent);
                animator = skinnedMeshRenderer.GetComponent<Animator>();
            }
        }
    }
}