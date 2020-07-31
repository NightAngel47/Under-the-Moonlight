using System.Collections;
using NUnit.Framework;
using UnderTheMoonlight.Level;
using UnityEngine;
using UnityEngine.TestTools;

namespace UnderTheMoonlight.Tests.PlayModeTests
{
    public class MoonlightTests
    {
        private readonly GameObject moonlightGameObject = new GameObject();
        private MoonlightBehaviour moonlight;

        [SetUp]
        public void BeforeEveryTest()
        {
            Time.timeScale = 20f;
            moonlightGameObject.AddComponent<Animator>();
        }

        [UnityTest]
        public IEnumerator isMoonlightActive_is_true_when_useTimer_is_false()
        {
            moonlight = moonlightGameObject.AddComponent<MoonlightBehaviour>();
            moonlight.useTimer = false;
            
            yield return null;
            
            Assert.AreEqual(true, moonlight.isMoonlightActive);
        }

        [UnityTest] 
        public IEnumerator isMoonlightActive_is_false_when_useTimer_is_true_and_moonlightActiveTime_is_1_sec()
        {
            moonlight = moonlightGameObject.AddComponent<MoonlightBehaviour>();
            moonlight.useTimer = true;
            moonlight.moonlightActiveTime = 1f;
            
            yield return new WaitForSeconds(moonlight.moonlightActiveTime);
            yield return null;

            Assert.AreEqual(false, moonlight.isMoonlightActive);
        }

        [UnityTest] 
        public IEnumerator isMoonlightActive_is_true_when_isMoonlightActive_is_false_useTimer_is_true_and_moonlightInactiveTime_is_1_sec()
        {
            moonlight = moonlightGameObject.AddComponent<MoonlightBehaviour>();
            moonlight.isMoonlightActive = false;
            moonlight.useTimer = true;
            moonlight.moonlightInactiveTime = 1f;
            
            yield return new WaitForSeconds(moonlight.moonlightInactiveTime);
            yield return null;

            Assert.AreEqual(true, moonlight.isMoonlightActive);
        }

        [UnityTest] 
        public IEnumerator isMoonlightActive_is_true_when_useTimer_is_true_and_moonlightActiveTime_and_moonlightInActiveTime_are_1_sec()
        {
            moonlight = moonlightGameObject.AddComponent<MoonlightBehaviour>();
            moonlight.useTimer = true;
            moonlight.moonlightActiveTime = 1f;
            moonlight.moonlightInactiveTime = 1f;
            
            yield return new WaitForSeconds(moonlight.moonlightActiveTime);
            yield return null;
            yield return new WaitForSeconds(moonlight.moonlightInactiveTime);
            yield return null;

            Assert.AreEqual(true, moonlight.isMoonlightActive);
        }
    }
}
