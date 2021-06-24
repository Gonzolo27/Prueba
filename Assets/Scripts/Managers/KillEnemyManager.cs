using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemyManager : MonoBehaviour
{
    /// <summary>
    /// Instancia y destruye el sistema de part�culas de la muerte del enemigo
    /// Si no lo hac�a aqu� al destruir el enemigo no sal�an las part�culas
    /// </summary>
    /// <param name="position">posici�n del enemigo donde muere</param>
    /// <param name="ufx">Sistema de particulas a instanciar</param>
    public void KillEnemy(Transform position, GameObject ufx)
    {
        Destroy(Instantiate(ufx, position.transform), 1.0f);
    }
}
