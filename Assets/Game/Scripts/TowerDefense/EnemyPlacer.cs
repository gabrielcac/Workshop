using System.Collections;
using UnityEngine;

/// <summary>
/// Controla as waves do jogo, e insere os inimigos no mapa um a um.
/// </summary>
public class EnemyPlacer : MonoBehaviour
{
	public Wave[] waves;
	public float waveInterval;
	public Gate gate;

	private float _waveTimer;
	private int _currentWave;
	private bool _finished;

	private void Start()
	{
		StartCoroutine(PlaceWave(waves[_currentWave++]));
		if (_currentWave == waves.Length)
		{
			_finished = true;
		}
		else
		{
			_waveTimer = waveInterval;
		}
	}

	private void Update()
	{
		if(_finished)
		{
			return;
		}

		_waveTimer -= Time.deltaTime;
		if(_waveTimer <= 0)
		{
			StartCoroutine(PlaceWave(waves[_currentWave++]));
			if(_currentWave == waves.Length)
			{
				_finished = true;
			}
			else
			{
				_waveTimer = waveInterval;
			}
		}
	}

	/// <summary>
	/// Desenha um círculo vermelho semi transparente no mapa, para que possamos visualizar este objeto.
	/// Gizmos são desenhados apenas no editor, e não ficarão visíveis no produto final.
	/// </summary>
	private void OnDrawGizmos()
	{
		Gizmos.color = new Color(1, 0, 0, .5f);
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.DrawSphere(Vector3.zero, 1);
	}

	/// <summary>
	/// Corotina que coloca os inimigos de uma wave em jogo.
	/// </summary>
	/// <param name="wave"></param>
	/// <returns></returns>
	private IEnumerator PlaceWave(Wave wave)
	{
		for(int enemyType = 0, j = wave.enemies.Length; enemyType < j; enemyType++)
		{
			for(int enemyCount = 0, l = wave.enemies[enemyType].quantity; enemyCount < l; enemyCount++)
			{
				Enemy enemy = Instantiate(wave.enemies[enemyType].enemyPrefab, transform.position, Quaternion.identity);
				enemy.SetTarget(gate);
				yield return new WaitForSeconds(wave.enemyPlacementInterval);
			}
		}
	}
}