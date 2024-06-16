
using Character.Utils;
using UnityEngine;

namespace Character
{
    public class CharacterMesh : ACharacterMonoBehaviour
    {
        [SerializeField] private Transform meshParent;
        [SerializeField] private Transform deathMeshParent;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
        private CharacterDeathAvatarHandler characterDeathAvatarHandler;
        public Animator animator ;

        public void SetColor(PlayersManager.CharacterColor characterColor) {
            if (meshRenderer)
                meshRenderer.material.color = PlayersManager.GetColor(characterColor);
            if (skinnedMeshRenderer)
                skinnedMeshRenderer.material.color = PlayersManager.GetColor(characterColor);
        }

        public void SetMesh(ECharacterType charaterType) {
            if (meshRenderer) meshRenderer.gameObject.SetActive(false);
            if (skinnedMeshRenderer) skinnedMeshRenderer.gameObject.SetActive(false);
            var charData = Resources.Load<ResourcesCharacters>("ResourcesCharacters").GetCharacterData(charaterType);
            if (charData.Equals(default(SCharacterData))) return;
            GameObject _modelPrefab = charData.characterPrefab;
            GameObject _deathModelPrefab = charData.deathCharacterPrefab;
            GameObject instance = Instantiate(_modelPrefab, meshParent);
            GameObject deathInstance = Instantiate(_deathModelPrefab, deathMeshParent);
            deathInstance.TryGetComponent(out characterDeathAvatarHandler);
            MeshRenderer _meshRenderer = instance.GetComponentInChildren<MeshRenderer>();
            if (_meshRenderer) {
                meshRenderer = _meshRenderer;
            }
            SkinnedMeshRenderer _skinnedMeshRenderer = instance.GetComponentInChildren<SkinnedMeshRenderer>();
            if (_skinnedMeshRenderer) {
                skinnedMeshRenderer = _skinnedMeshRenderer;
            }
            animator = instance.GetComponentInChildren<Animator>();

            ChangeColorLayer();
        }

        public void ActiveDeath(Transform killedBy) {
            meshParent.gameObject.SetActive(false);
            deathMeshParent.gameObject.SetActive(true);

            characterDeathAvatarHandler.AddForceToBodies(CharacterEntity.Character.characterBody.position - killedBy.position);
        }

        public void ResetMesh() {
            deathMeshParent.gameObject.SetActive(false);
            meshParent.gameObject.SetActive(true);
        }

        private void ChangeColorLayer() {
            var characterId = CharacterEntity.Character.Id;
            var colorLayer = PlayerColorLayerManager.DefineCharacterColorLayer(characterId);

            CharacterEntity.Character.gameObject.layer = colorLayer;
            ChangeChildColorLayer(CharacterEntity.Character.transform, colorLayer);
        }

        private static void ChangeChildColorLayer(Transform parent, int layer) {
            foreach (Transform child in parent) {
                child.gameObject.layer = layer;
                ChangeChildColorLayer(child, layer);
            }
        }
    }
}
