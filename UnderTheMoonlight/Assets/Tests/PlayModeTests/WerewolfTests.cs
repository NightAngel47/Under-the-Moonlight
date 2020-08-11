using System.Collections;
using NUnit.Framework;
using UnderTheMoonlight.Characters;
using UnderTheMoonlight.Level;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TestTools;

namespace UnderTheMoonlight.Tests.PlayModeTests
{
    public class WerewolfTests
    {
        [OneTimeSetUp]
        public void TestSetup()
        {
            Time.timeScale = 4f;
        }
        
        [TearDown]
        public void TearDown()
        {
            foreach (var gameObject in Object.FindObjectsOfType<GameObject>())
            {
                if (gameObject.TryGetComponent(out Collider2D collider))
                    collider.enabled = false;
            }
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
        public IEnumerator werewolf_InMoonlight_is_true_when_werewolf_enters_moonlight()
        {
            // Moonlight setup
            GameObject moonlightGameObject = new GameObject {tag = "Moonlight"};
            moonlightGameObject.transform.position = Vector3.up;
            moonlightGameObject.AddComponent<BoxCollider2D>().isTrigger = true;
            moonlightGameObject.AddComponent<Animator>();
            MoonlightBehaviour moonlight = moonlightGameObject.AddComponent<MoonlightBehaviour>();
            moonlight.EnterMoonlight = new WerewolfTransformation();
            moonlight.ExitMoonlight = new WerewolfTransformation();
            
            yield return null;

            // Werewolf setup
            GameObject werewolfGameObject = new GameObject {tag = "Werewolf", layer = LayerMask.NameToLayer("Character")};
            werewolfGameObject.AddComponent<CircleCollider2D>().radius = 0.25f;
            Rigidbody2D werewolfRigidbody2D = werewolfGameObject.AddComponent<Rigidbody2D>();
            werewolfRigidbody2D.gravityScale = 0;
            werewolfRigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            werewolfGameObject.AddComponent<Animator>();
            WerewolfMovement werewolf = werewolfGameObject.AddComponent<WerewolfMovement>();

            yield return null;

            werewolf.SetDirection(Vector2.up);
            
            yield return new WaitUntil((() => werewolf.transform.position.y >= 1f));

            Assert.AreEqual(true, werewolf.InMoonlight);
        }
        
        [UnityTest]
        public IEnumerator werewolf_InMoonlight_is_false_when_werewolf_exits_moonlight()
        {
            // Moonlight setup
            GameObject moonlightGameObject = new GameObject {tag = "Moonlight"};
            moonlightGameObject.transform.position = Vector3.up;
            moonlightGameObject.AddComponent<BoxCollider2D>().isTrigger = true;
            moonlightGameObject.AddComponent<Animator>();
            MoonlightBehaviour moonlight = moonlightGameObject.AddComponent<MoonlightBehaviour>();
            moonlight.EnterMoonlight = new WerewolfTransformation();
            moonlight.ExitMoonlight = new WerewolfTransformation();
            
            yield return null;

            // Werewolf setup
            GameObject werewolfGameObject = new GameObject {tag = "Werewolf", layer = LayerMask.NameToLayer("Character")};
            werewolfGameObject.AddComponent<CircleCollider2D>().radius = 0.25f;
            Rigidbody2D werewolfRigidbody2D = werewolfGameObject.AddComponent<Rigidbody2D>();
            werewolfRigidbody2D.gravityScale = 0;
            werewolfRigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            werewolfGameObject.AddComponent<Animator>();
            WerewolfMovement werewolf = werewolfGameObject.AddComponent<WerewolfMovement>();
            werewolf.impassableLayers = LayerMask.GetMask("Wall");

            yield return null;

            werewolf.SetDirection(Vector2.up);
            
            yield return new WaitUntil((() => werewolf.transform.position.y >= 2f));

            Assert.AreEqual(false, werewolf.InMoonlight);
        }

        [UnityTest]
        public IEnumerator werewolf_position_is_vector3_0_2_0_when_moving_through_moonlight_up()
        {
            // Moonlight setup
            GameObject moonlightGameObject = new GameObject {tag = "Moonlight"};
            moonlightGameObject.transform.position = Vector3.up;
            moonlightGameObject.AddComponent<BoxCollider2D>().isTrigger = true;
            moonlightGameObject.AddComponent<Animator>();
            MoonlightBehaviour moonlight = moonlightGameObject.AddComponent<MoonlightBehaviour>();
            moonlight.EnterMoonlight = new WerewolfTransformation();
            moonlight.ExitMoonlight = new WerewolfTransformation();
            
            yield return null;

            // Werewolf setup
            GameObject werewolfGameObject = new GameObject {tag = "Werewolf", layer = LayerMask.NameToLayer("Character")};
            werewolfGameObject.AddComponent<CircleCollider2D>().radius = 0.25f;
            Rigidbody2D werewolfRigidbody2D = werewolfGameObject.AddComponent<Rigidbody2D>();
            werewolfRigidbody2D.gravityScale = 0;
            werewolfRigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            werewolfGameObject.AddComponent<Animator>();
            WerewolfMovement werewolf = werewolfGameObject.AddComponent<WerewolfMovement>();
            werewolf.impassableLayers = LayerMask.GetMask("Wall");

            yield return null;

            werewolf.SetDirection(Vector2.up);
            
            yield return new WaitForSeconds(3f);
            yield return new WaitUntil((() => werewolf.movementDir == Vector3.zero));

            Assert.AreEqual(new Vector3(0, 2, 0), werewolf.transform.position);
        }
        
        [UnityTest]
        public IEnumerator werewolf_position_is_vector3_0_neg2_0_when_moving_through_moonlight_down()
        {
            // Moonlight setup
            GameObject moonlightGameObject = new GameObject {tag = "Moonlight"};
            moonlightGameObject.transform.position = Vector3.down;
            moonlightGameObject.AddComponent<BoxCollider2D>().isTrigger = true;
            moonlightGameObject.AddComponent<Animator>();
            MoonlightBehaviour moonlight = moonlightGameObject.AddComponent<MoonlightBehaviour>();
            moonlight.EnterMoonlight = new WerewolfTransformation();
            moonlight.ExitMoonlight = new WerewolfTransformation();
            
            yield return null;

            // Werewolf setup
            GameObject werewolfGameObject = new GameObject {tag = "Werewolf", layer = LayerMask.NameToLayer("Character")};
            werewolfGameObject.AddComponent<CircleCollider2D>().radius = 0.25f;
            Rigidbody2D werewolfRigidbody2D = werewolfGameObject.AddComponent<Rigidbody2D>();
            werewolfRigidbody2D.gravityScale = 0;
            werewolfRigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            werewolfGameObject.AddComponent<Animator>();
            WerewolfMovement werewolf = werewolfGameObject.AddComponent<WerewolfMovement>();
            werewolf.impassableLayers = LayerMask.GetMask("Wall");

            yield return null;

            werewolf.SetDirection(Vector2.down);
            
            yield return new WaitForSeconds(3f);
            yield return new WaitUntil((() => werewolf.movementDir == Vector3.zero));

            Assert.AreEqual(new Vector3(0, -2, 0), werewolf.transform.position);
        }
        
        [UnityTest]
        public IEnumerator werewolf_position_is_vector3_neg2_0_0_when_moving_through_moonlight_left()
        {
            // Moonlight setup
            GameObject moonlightGameObject = new GameObject {tag = "Moonlight"};
            moonlightGameObject.transform.position = Vector3.left;
            moonlightGameObject.AddComponent<BoxCollider2D>().isTrigger = true;
            moonlightGameObject.AddComponent<Animator>();
            MoonlightBehaviour moonlight = moonlightGameObject.AddComponent<MoonlightBehaviour>();
            moonlight.EnterMoonlight = new WerewolfTransformation();
            moonlight.ExitMoonlight = new WerewolfTransformation();
            
            yield return null;

            // Werewolf setup
            GameObject werewolfGameObject = new GameObject {tag = "Werewolf", layer = LayerMask.NameToLayer("Character")};
            werewolfGameObject.AddComponent<CircleCollider2D>().radius = 0.25f;
            Rigidbody2D werewolfRigidbody2D = werewolfGameObject.AddComponent<Rigidbody2D>();
            werewolfRigidbody2D.gravityScale = 0;
            werewolfRigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            werewolfGameObject.AddComponent<Animator>();
            WerewolfMovement werewolf = werewolfGameObject.AddComponent<WerewolfMovement>();
            werewolf.impassableLayers = LayerMask.GetMask("Wall");

            yield return null;

            werewolf.SetDirection(Vector2.left);
            
            yield return new WaitForSeconds(3f);
            yield return new WaitUntil((() => werewolf.movementDir == Vector3.zero));

            Assert.AreEqual(new Vector3(-2, 0, 0), werewolf.transform.position);
        }
        
        [UnityTest]
        public IEnumerator werewolf_position_is_vector3_2_0_0_when_moving_through_moonlight_left()
        {
            // Moonlight setup
            GameObject moonlightGameObject = new GameObject {tag = "Moonlight"};
            moonlightGameObject.transform.position = Vector3.right;
            moonlightGameObject.AddComponent<BoxCollider2D>().isTrigger = true;
            moonlightGameObject.AddComponent<Animator>();
            MoonlightBehaviour moonlight = moonlightGameObject.AddComponent<MoonlightBehaviour>();
            moonlight.EnterMoonlight = new WerewolfTransformation();
            moonlight.ExitMoonlight = new WerewolfTransformation();
            
            yield return null;

            // Werewolf setup
            GameObject werewolfGameObject = new GameObject {tag = "Werewolf", layer = LayerMask.NameToLayer("Character")};
            werewolfGameObject.AddComponent<CircleCollider2D>().radius = 0.25f;
            Rigidbody2D werewolfRigidbody2D = werewolfGameObject.AddComponent<Rigidbody2D>();
            werewolfRigidbody2D.gravityScale = 0;
            werewolfRigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            werewolfGameObject.AddComponent<Animator>();
            WerewolfMovement werewolf = werewolfGameObject.AddComponent<WerewolfMovement>();
            werewolf.impassableLayers = LayerMask.GetMask("Wall");

            yield return null;

            werewolf.SetDirection(Vector2.right);
            
            yield return new WaitForSeconds(3f);
            yield return new WaitUntil((() => werewolf.movementDir == Vector3.zero));

            Assert.AreEqual(new Vector3(2, 0, 0), werewolf.transform.position);
        }
        [UnityTest]
        public IEnumerator werewolf_movementDir_is_vector3_down_when_moving_through_moonlight_with_movementDir_vector3_up_and_hits_wall()
        {
            // Wall setup
            GameObject wall = new GameObject {layer = LayerMask.NameToLayer("Wall")};
            wall.transform.position = new Vector3(0.0f, 2.0f, 0.0f);
            wall.AddComponent<BoxCollider2D>();
            wall.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            
            yield return null;

            // Moonlight setup
            GameObject moonlightGameObject = new GameObject {tag = "Moonlight"};
            moonlightGameObject.transform.position = new Vector3(0.0f, 1.0f, 0.0f);
            moonlightGameObject.AddComponent<BoxCollider2D>().isTrigger = true;
            moonlightGameObject.AddComponent<Animator>();
            MoonlightBehaviour moonlight = moonlightGameObject.AddComponent<MoonlightBehaviour>();
            moonlight.EnterMoonlight = new WerewolfTransformation();
            moonlight.ExitMoonlight = new WerewolfTransformation();
            
            yield return null;

            // Werewolf setup
            GameObject werewolfGameObject = new GameObject {tag = "Werewolf", layer = LayerMask.NameToLayer("Character")};
            werewolfGameObject.AddComponent<CircleCollider2D>().radius = 0.25f;
            Rigidbody2D werewolfRigidbody2D = werewolfGameObject.AddComponent<Rigidbody2D>();
            werewolfRigidbody2D.gravityScale = 0;
            werewolfRigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            werewolfGameObject.AddComponent<Animator>();
            WerewolfMovement werewolf = werewolfGameObject.AddComponent<WerewolfMovement>();
            werewolf.impassableLayers = LayerMask.GetMask("Wall");

            yield return null;

            werewolf.SetDirection(Vector2.up);

            yield return new WaitForSeconds(2.5f);

            Assert.AreEqual(Vector3.down, werewolf.movementDir);
        }
        
        [UnityTest]
        public IEnumerator werewolf_movementDir_is_vector3_up_when_moving_through_moonlight_with_movementDir_vector3_down_and_hits_wall()
        {
            // Wall setup
            GameObject wall = new GameObject {layer = LayerMask.NameToLayer("Wall")};
            wall.transform.position = new Vector3(0.0f, -2.0f, 0.0f);
            wall.AddComponent<BoxCollider2D>();
            wall.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            
            yield return null;

            // Moonlight setup
            GameObject moonlightGameObject = new GameObject {tag = "Moonlight"};
            moonlightGameObject.transform.position = new Vector3(0.0f, -1.0f, 0.0f);
            moonlightGameObject.AddComponent<BoxCollider2D>().isTrigger = true;
            moonlightGameObject.AddComponent<Animator>();
            MoonlightBehaviour moonlight = moonlightGameObject.AddComponent<MoonlightBehaviour>();
            moonlight.EnterMoonlight = new WerewolfTransformation();
            moonlight.ExitMoonlight = new WerewolfTransformation();
            
            yield return null;

            // Werewolf setup
            GameObject werewolfGameObject = new GameObject {tag = "Werewolf", layer = LayerMask.NameToLayer("Character")};
            werewolfGameObject.AddComponent<CircleCollider2D>().radius = 0.25f;
            Rigidbody2D werewolfRigidbody2D = werewolfGameObject.AddComponent<Rigidbody2D>();
            werewolfRigidbody2D.gravityScale = 0;
            werewolfRigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            werewolfGameObject.AddComponent<Animator>();
            WerewolfMovement werewolf = werewolfGameObject.AddComponent<WerewolfMovement>();
            werewolf.impassableLayers = LayerMask.GetMask("Wall");

            yield return null;

            werewolf.SetDirection(Vector2.down);

            yield return new WaitForSeconds(2.5f);

            Assert.AreEqual(Vector3.up, werewolf.movementDir);
        }
        
        [UnityTest]
        public IEnumerator werewolf_movementDir_is_vector3_left_when_moving_through_moonlight_with_movementDir_vector3_right_and_hits_wall()
        {
            // Wall setup
            GameObject wall = new GameObject {layer = LayerMask.NameToLayer("Wall")};
            wall.transform.position = new Vector3(2.0f, 0.0f, 0.0f);
            wall.AddComponent<BoxCollider2D>();
            wall.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            
            yield return null;

            // Moonlight setup
            GameObject moonlightGameObject = new GameObject {tag = "Moonlight"};
            moonlightGameObject.transform.position = new Vector3(1.0f, 0.0f, 0.0f);
            moonlightGameObject.AddComponent<BoxCollider2D>().isTrigger = true;
            moonlightGameObject.AddComponent<Animator>();
            MoonlightBehaviour moonlight = moonlightGameObject.AddComponent<MoonlightBehaviour>();
            moonlight.EnterMoonlight = new WerewolfTransformation();
            moonlight.ExitMoonlight = new WerewolfTransformation();
            
            yield return null;

            // Werewolf setup
            GameObject werewolfGameObject = new GameObject {tag = "Werewolf", layer = LayerMask.NameToLayer("Character")};
            werewolfGameObject.AddComponent<CircleCollider2D>().radius = 0.25f;
            Rigidbody2D werewolfRigidbody2D = werewolfGameObject.AddComponent<Rigidbody2D>();
            werewolfRigidbody2D.gravityScale = 0;
            werewolfRigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            werewolfGameObject.AddComponent<Animator>();
            WerewolfMovement werewolf = werewolfGameObject.AddComponent<WerewolfMovement>();
            werewolf.impassableLayers = LayerMask.GetMask("Wall");

            yield return null;

            werewolf.SetDirection(Vector2.right);

            yield return new WaitForSeconds(2.5f);

            Assert.AreEqual(Vector3.left, werewolf.movementDir);
        }
        
        [UnityTest]
        public IEnumerator werewolf_movementDir_is_vector3_right_when_moving_through_moonlight_with_movementDir_vector3_left_and_hits_wall()
        {
            // Wall setup
            GameObject wall = new GameObject {layer = LayerMask.NameToLayer("Wall")};
            wall.transform.position = new Vector3(-2.0f, 0.0f, 0.0f);
            wall.AddComponent<BoxCollider2D>();
            wall.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            
            yield return null;

            // Moonlight setup
            GameObject moonlightGameObject = new GameObject {tag = "Moonlight"};
            moonlightGameObject.transform.position = new Vector3(-1.0f, 0.0f, 0.0f);
            moonlightGameObject.AddComponent<BoxCollider2D>().isTrigger = true;
            moonlightGameObject.AddComponent<Animator>();
            MoonlightBehaviour moonlight = moonlightGameObject.AddComponent<MoonlightBehaviour>();
            moonlight.EnterMoonlight = new WerewolfTransformation();
            moonlight.ExitMoonlight = new WerewolfTransformation();
            
            yield return null;

            // Werewolf setup
            GameObject werewolfGameObject = new GameObject {tag = "Werewolf", layer = LayerMask.NameToLayer("Character")};
            werewolfGameObject.AddComponent<CircleCollider2D>().radius = 0.25f;
            Rigidbody2D werewolfRigidbody2D = werewolfGameObject.AddComponent<Rigidbody2D>();
            werewolfRigidbody2D.gravityScale = 0;
            werewolfRigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            werewolfGameObject.AddComponent<Animator>();
            WerewolfMovement werewolf = werewolfGameObject.AddComponent<WerewolfMovement>();
            werewolf.impassableLayers = LayerMask.GetMask("Wall");

            yield return null;

            werewolf.SetDirection(Vector2.left);

            yield return new WaitForSeconds(2.5f);

            Assert.AreEqual(Vector3.right, werewolf.movementDir);
        }
    }
}
