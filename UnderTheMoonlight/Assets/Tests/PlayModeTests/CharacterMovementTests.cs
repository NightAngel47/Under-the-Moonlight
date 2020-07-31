using System.Collections;
using NUnit.Framework;
using UnderTheMoonlight.Characters;
using UnityEngine;
using UnityEngine.TestTools;

namespace UnderTheMoonlight.Tests.PlayModeTests
{
    public class CharacterMovementTests
    {
        private GameObject characterGameObject = new GameObject();
        private CharacterMovement characterMovement;
        
        [SetUp]
        public void BeforeEveryTest()
        {
            Time.timeScale = 20f;
            characterGameObject = new GameObject();
            characterMovement = characterGameObject.AddComponent<CharacterMovement>();
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
            
            Assert.AreEqual(Vector3.up, characterGameObject.transform.position);
        }
        
        [UnityTest]
        public IEnumerator character_position_is_vector3_down_when_set_direction_to_vector2_down()
        {
            characterMovement.SetDirection(Vector2.down);

            yield return new WaitUntil(() => characterMovement.movementDir == Vector3.zero);
            
            Assert.AreEqual(Vector3.down, characterGameObject.transform.position);
        }
        
        [UnityTest]
        public IEnumerator character_position_is_vector3_left_when_set_direction_to_vector2_left()
        {
            characterMovement.SetDirection(Vector2.left);

            yield return new WaitUntil(() => characterMovement.movementDir == Vector3.zero);
            
            Assert.AreEqual(Vector3.left, characterGameObject.transform.position);
        }
        
        [UnityTest]
        public IEnumerator character_position_is_vector3_right_when_set_direction_to_vector2_right()
        {
            characterMovement.SetDirection(Vector2.right);

            yield return new WaitUntil(() => characterMovement.movementDir == Vector3.zero);
            
            Assert.AreEqual(Vector3.right, characterGameObject.transform.position);
        }
        
        [UnityTest]
        public IEnumerator character_position_is_vector3_zero_when_set_direction_to_vector2_one()
        {
            characterMovement.SetDirection(Vector2.one);

            yield return new WaitUntil(() => characterMovement.movementDir == Vector3.zero);
            
            Assert.AreEqual(Vector3.zero, characterGameObject.transform.position);
        }
        
        [UnityTest]
        public IEnumerator character_position_is_vector3_zero_when_set_direction_to_vector2_zero()
        {
            characterMovement.SetDirection(Vector2.zero);

            yield return new WaitUntil(() => characterMovement.movementDir == Vector3.zero);
            
            Assert.AreEqual(Vector3.zero, characterGameObject.transform.position);
        }
    }
}
