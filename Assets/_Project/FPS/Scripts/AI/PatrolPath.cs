using System;
using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.AI
{
    public class PatrolPath : MonoBehaviour
    {
        public EnemyController enemyControllerPrefab;
        public List<Transform> transformCreation;
        [Tooltip("Enemies that will be assigned to this path on Start")]
        public List<EnemyController> EnemiesToAssign = new List<EnemyController>();

        [Tooltip("The Nodes making up the path")]
        public List<Transform> PathNodes = new List<Transform>();
        public int index;
        public int enemyCount;
        IEnumerator Start()
        {
            yield return new WaitForSeconds(0.3f);
            for (int i = 0; i < enemyCount; i++)
            {
                CreateEnemy();
            }
        }
        public void CreateEnemy()
        {
            if (enemyControllerPrefab == null) return;
            var pos = transformCreation[UnityEngine.Random.Range(0, transformCreation.Count)];
            var enemy = Instantiate(enemyControllerPrefab,pos.position,Quaternion.identity,this.transform);
            EnemiesToAssign.Add(enemy);
            enemy.PatrolPath = this; 
            enemy.Init();
           var health =  enemy.GetComponent<Health>();
            health.OnDie += OnDead;
        }

        private void OnDead()
        {
            CreateEnemy();
        }

        public float GetDistanceToNode(Vector3 origin, int destinationNodeIndex)
        {
            if (destinationNodeIndex < 0 ||
                PathNodes[destinationNodeIndex] == null)
            {
                return -1f; 
            }
            if (destinationNodeIndex >= PathNodes.Count)
            {
                return (PathNodes[0].position - origin).magnitude;
            }
            return (PathNodes[destinationNodeIndex].position - origin).magnitude;
        }
         
        public Vector3 GetPositionOfPathNode(int nodeIndex)
        {
            index = nodeIndex;
            if (nodeIndex < 0 || PathNodes[nodeIndex] == null)
            {
                return Vector3.zero;
            }
            if(nodeIndex >= PathNodes.Count-1)
            {
               
                return PathNodes[nodeIndex].position;
            }
           // Debug.Log(nodeIndex);
            return PathNodes[nodeIndex].position;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            for (int i = 0; i < PathNodes.Count; i++)
            {
                int nextIndex = i + 1;
                if (nextIndex >= PathNodes.Count)
                {
                    nextIndex -= PathNodes.Count;
                }

                Gizmos.DrawLine(PathNodes[i].position, PathNodes[nextIndex].position);
                Gizmos.DrawSphere(PathNodes[i].position, 0.1f);
            }
        }
    }
}