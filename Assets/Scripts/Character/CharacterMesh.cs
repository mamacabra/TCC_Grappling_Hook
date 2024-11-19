using Character.States;
using Character.Utils;
using Const;
using UnityEngine;
using LocalMultiplayer;
using LocalMultiplayer.Data;

namespace Character
{
    public class CharacterMesh : ACharacterMonoBehaviour
    {
        [SerializeField] private Transform meshParent;
        [SerializeField] private Transform deathMeshParent;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
        private CharacterDeathAvatarHandler characterDeathAvatarHandler;
        public CharacterItemsHandle characterItemsHandle;
        public Animator animator ;

        [Header("Color Layer Materials")]
        [SerializeField] private Material colorLayer01Material;
        [SerializeField] private Material colorLayer02Material;
        [SerializeField] private Material colorLayer03Material;
        [SerializeField] private Material colorLayer04Material;
        [SerializeField] private Material colorLayer05Material;
        [SerializeField] private Material colorLayer06Material;

        [Header("Color Layer Materials")]
        [SerializeField] private Material colorLayerArrow01Material;
        [SerializeField] private Material colorLayerArrow02Material;
        [SerializeField] private Material colorLayerArrow03Material;
        [SerializeField] private Material colorLayerArrow04Material;
        [SerializeField] private Material colorLayerArrow05Material;
        [SerializeField] private Material colorLayerArrow06Material;

        public void OnEnable()
        {
            CameraManager.Instance.CallWinnerDance += OnCallWinnerDance;
        }

        private void OnDisable()
        {
            if (CameraManager.Instance)
            {
                CameraManager.Instance.CallWinnerDance -= OnCallWinnerDance;
            }
        }

        private void OnCallWinnerDance()
        {
            if (CharacterEntity.CharacterState.State is not DeathState)
            {
                int id = CharacterEntity.Character.Id;
                if (PlayersManager.Instance.winnerSupreme == id)
                    CharacterEntity.CharacterState.SetWinnerState();
                else if (!PlayersManager.Instance.CheckIfGameOver())
                    CharacterEntity.CharacterState.SetWinnerState();
            }
        }

        public void SetColor(CharacterColor characterColor) {
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
            characterItemsHandle = instance.GetComponentInChildren<CharacterItemsHandle>();
            if (characterItemsHandle) CharacterEntity.Character.crown = characterItemsHandle.crown;
            CharacterEntity.Character.EnableCrown();

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

        public GameObject GetMeshParent => meshParent.gameObject;

        private void ChangeColorLayer() {
            var characterId = CharacterEntity.Character.Id;
            var colorLayer = PlayerColorLayerManager.DefineCharacterColorLayer(characterId);

            CharacterEntity.Character.gameObject.layer = (int) colorLayer;
            ChangeChildColorLayer(CharacterEntity.Character.transform, (int) colorLayer);
        }

        private void ChangeChildColorLayer(Transform parent, int layer = 0) {
            foreach (Transform child in parent) {
                child.gameObject.layer = layer;
                ChangeChildColorLayer(child, layer);

                if (child.name == "CharDirection") {
                    var meshRenderer = child.GetComponent<MeshRenderer>();
                    meshRenderer.material = GetCircleColorLayerMaterial((ControlColorsLayer) layer);
                }
                if (child.name == "CharDirectionArrow") {
                    var meshRenderer = child.GetComponent<MeshRenderer>();
                    meshRenderer.material = GetArrowColorLayerMaterial((ControlColorsLayer) layer);
                }
            }
        }

        private Material GetCircleColorLayerMaterial(ControlColorsLayer layer)
        {
            return layer switch
            {
                ControlColorsLayer.Yellow => colorLayer01Material,
                ControlColorsLayer.Green => colorLayer02Material,
                ControlColorsLayer.Blue => colorLayer03Material,
                ControlColorsLayer.Purple => colorLayer04Material,
                ControlColorsLayer.Red => colorLayer05Material,
                ControlColorsLayer.White => colorLayer06Material,
                _ => colorLayer01Material
            };
        }

        private Material GetArrowColorLayerMaterial(ControlColorsLayer layer)
        {
            return layer switch
            {
                ControlColorsLayer.Yellow => colorLayerArrow01Material,
                ControlColorsLayer.Green => colorLayerArrow02Material,
                ControlColorsLayer.Blue => colorLayerArrow03Material,
                ControlColorsLayer.Purple => colorLayerArrow04Material,
                ControlColorsLayer.Red => colorLayerArrow05Material,
                ControlColorsLayer.White => colorLayerArrow06Material,
                _ => colorLayerArrow01Material
            };
        }
    }
}
