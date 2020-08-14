using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnderTheMoonlight.Characters;
using UnityEngine;
using UnityEngine.TestTools;

namespace UnderTheMoonlight.Tests.PlayModeTests
{
    public class CharacterMovementTests
    {
        private CharacterMovement characterMovement;

        [OneTimeSetUp]
        public void TestSetup()
        {
            Time.timeScale = 20f;
        }
        
        [SetUp]
        public void SetUp()
        {
            GameObject characterGameObject = new GameObject();
            characterMovement = characterGameObject.AddComponent<CharacterMovement>();
        }
        
        [OneTimeTearDown]
        public void TestTearDown()
        {
            foreach (var gameObject in Object.FindObjectsOfType<GameObject>())
            {
                gameObject.SetActive(false);
            }
        }
        
        [UnityTest]
        public IEnumerator movementDir_is_vector3_up_when_set_direction_to_vector2_up()
        {
            characterMovement.SetDirection(Vector2.up);

            yield return null;
            
            Assert.AreEqual(Vector3.up, characterMovement.movementDir);
        }
        
        [UnityTest]
        public IEnumerator movementDir_is_vector3_down_when_set_direction_to_vector2_down()
        {
            characterMovement.SetDirection(Vector2.down);

            yield return null;
            
            Assert.AreEqual(Vector3.down, characterMovement.movementDir);
        }
        
        [UnityTest]
        public IEnumerator movementDir_is_vector3_left_when_set_direction_to_vector2_left()
        {
            characterMovement.SetDirection(Vector2.left);

            yield return null;
            
            Assert.AreEqual(Vector3.left, characterMovement.movementDir);
        }
        
        [UnityTest]
        public IEnumerator movementDir_is_vector3_right_when_set_direction_to_vector2_right()
        {
            characterMovement.SetDirection(Vector2.right);

            yield return null;
            
            Assert.AreEqual(Vector3.right, characterMovement.movementDir);
        }
        
        [UnityTest]
        public IEnumerator movementDir_is_vector3_zero_when_set_direction_to_vector2_one()
        {
            characterMovement.SetDirection(Vector2.one);

            yield return null;
            
            Assert.AreEqual(Vector3.zero, characterMovement.movementDir);
        }
        
        [UnityTest]
        public IEnumerator movementDir_is_vector3_zero_when_set_direction_to_vector2_zero()
        {
            characterMovement.SetDirection(Vector2.zero);

            yield return null;
            
            Assert.AreEqual(Vector3.zero, characterMovement.movementDir);
        }
        
        [UnityTest]
        public IEnumerator character_position_is_vector3_up_when_set_direction_to_vector2_up()
        {
            characterMovement.SetDirection(Vector2.up);

            yield return new WaitUntil(() => characterMovement.movementDir == Vector3.zero);
            
            Assert.AreEqual(Vector3.up, characterMovement.transform.position);
        }
        
        [UnityTest]
        public IEnumerator character_position_is_vector3_down_when_set_direction_to_vector2_down()
        {
            characterMovement.SetDirection(Vector2.down);

            yield return new WaitUntil(() => characterMovement.movementDir == Vector3.zero);
            
            Assert.AreEqual(Vector3.down, characterMovement.transform.position);
        }
        
        [UnityTest]
        public IEnumerator character_position_is_vector3_left_when_set_direction_to_vector2_left()
        {
            characterMovement.SetDirection(Vector2.left);

            yield return new WaitUntil(() => characterMovement.movementDir == Vector3.zero);
            
            Assert.AreEqual(Vector3.left, characterMovement.transform.position);
        }
        
        [UnityTest]
        public IEnumerator character_position_is_vector3_right_when_set_direction_to_vector2_right()
        {
            characterMovement.SetDirection(Vector2.right);

            yield return new WaitUntil(() => characterMovement.movementDir == Vector3.zero);
            
            Assert.AreEqual(Vector3.right, characterMovement.transform.position);
        }
        
        [UnityTest]
        public IEnumerator character_position_is_vector3_zero_when_set_direction_to_vector2_one()
        {
            characterMovement.SetDirection(Vector2.one);

            yield return new WaitUntil(() => characterMovement.movementDir == Vector3.zero);
            
            Assert.AreEqual(Vector3.zero, characterMovement.transform.position);
        }
        
        [UnityTest]
        public IEnumerator character_position_is_vector3_zero_when_set_direction_to_vector2_zero()
        {
            characterMovement.SetDirection(Vector2.zero);

            yield return new WaitUntil(() => characterMovement.movementDir == Vector3.zero);
            
            Assert.AreEqual(Vector3.zero, characterMovement.transform.position);
        }
        
        [UnityTest]
        public IEnumerator character_position_is_vector3_zero_when_set_direction_to_vector2_up_and_wall_position_is_vector3_up()
        {
            GameObject wall = new GameObject {layer = LayerMask.NameToLayer("Wall")};
            wall.transform.position = Vector3.up;
            wall.AddComponent<BoxCollider2D>();

            yield return null;
            
            characterMovement.impassableLayers = LayerMask.GetMask("Wall");
            characterMovement.SetDirection(Vector2.up);

            yield return new WaitForSeconds(0.25f);
            
            Assert.AreEqual(Vector3.zero, characterMovement.transform.position);
        }
        
        [UnityTest]
        public IEnumerator character_position_is_vector3_zero_when_set_direction_to_vector2_down_and_wall_position_is_vector3_down()
        {
            GameObject wall = new GameObject {layer = LayerMask.NameToLayer("Wall")};
            wall.transform.position = Vector3.down;
            wall.AddComponent<BoxCollider2D>();

            yield return null;
            
            characterMovement.impassableLayers = LayerMask.GetMask("Wall");
            characterMovement.SetDirection(Vector2.down);

            yield return new WaitForSeconds(0.25f);
            
            Assert.AreEqual(Vector3.zero, characterMovement.transform.position);
        }
        
        [UnityTest]
        public IEnumerator character_position_is_vector3_zero_when_set_direction_to_vector2_right_and_wall_position_is_vector3_right()
        {
            GameObject wall = new GameObject {layer = LayerMask.NameToLayer("Wall")};
            wall.transform.position = Vector3.right;
            wall.AddComponent<BoxCollider2D>();

            yield return null;
            
            characterMovement.impassableLayers = LayerMask.GetMask("Wall");
            characterMovement.SetDirection(Vector2.right);

            yield return new WaitForSeconds(0.25f);
            
            Assert.AreEqual(Vector3.zero, characterMovement.transform.position);
        }
        
        [UnityTest]
        public IEnumerator character_position_is_vector3_zero_when_set_direction_to_vector2_left_and_wall_position_is_vector3_left()
        {
            GameObject wall = new GameObject {layer = LayerMask.NameToLayer("Wall")};
            wall.transform.position = Vector3.left;
            wall.AddComponent<BoxCollider2D>();

            yield return null;
            
            characterMovement.impassableLayers = LayerMask.GetMask("Wall");
            characterMovement.SetDirection(Vector2.left);

            yield return new WaitForSeconds(0.25f);
            
            Assert.AreEqual(Vector3.zero, characterMovement.transform.position);
        }
    }
}
