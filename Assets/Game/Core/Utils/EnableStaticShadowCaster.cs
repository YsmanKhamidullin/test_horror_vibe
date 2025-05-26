using UnityEngine;

namespace Game.Core.Utils
{
    public class EnableStaticShadowCaster : MonoBehaviour
    {
        // Этот метод можно вызвать через контекстное меню в инспекторе.
        [ContextMenu("Включить Static Shadow Caster для всех статичных объектов")]
        private void SetupStaticShadowCaster()
        {
            // Находим все MeshRenderer в сцене
            MeshRenderer[] renderers = GameObject.FindObjectsOfType<MeshRenderer>();

            foreach (MeshRenderer renderer in renderers)
            {
                // Проверяем, является ли родительский объект статичным
                if (renderer.gameObject.isStatic)
                {
                    // Устанавливаем режим отбрасывания теней в включенное состояние
                    renderer.staticShadowCaster = true;
                }
            }

            Debug.Log("Static Shadow Caster включены для всех статичных объектов.");
        }
    }
}
