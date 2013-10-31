 // https://github.com/BlueWall/Example-Region-Module/blob/master/Src/ExampleModule.cs
using System;
using System.Collections.Generic;
using System.Reflection;
using log4net;
using Mono.Addins;
using Nini.Config;
using OpenSim.Region.Framework.Interfaces;
using OpenSim.Region.Framework.Scenes;

using OpenMetaverse; // Vector3
using OpenSim.Framework;

namespace MyModule.Region.HelloWorld
{
    [Extension(Path = "/OpenSim/RegionModules", NodeName = "RegionModule", Id = "HelloWorldModule")]
    public class HelloWorldModule : ISharedRegionModule
    {
        private static readonly ILog m_log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public string Name { get { return "KEZUNLIN'S Hello World Module"; } }
       
        public Type ReplaceableInterface { get { return null; } }


        #region Fields

        private bool m_enabled = false;
        private int m_InitCount = 0; // used for showing us the sequence number

        /*
         * A Scene is OpenSim's representation of the contents of a region. As this is a shared region module there may be
         * many regions & thus many Scenes so we need a List. However if this were a non-shared region module there would be
         * only one Scene so we wouldn't need a list.
         * 
         * A Dictionary is like a hashtable, in this instance used to associate Scenes with collections of prims.
         */
        List<Scene> m_scenes = new List<Scene>();
        Dictionary<Scene, List<SceneObjectGroup>> scene_prims = new Dictionary<Scene, List<SceneObjectGroup>>();
        int counter = 0;
        bool positive = true;

        #endregion


        /*
         * Initialise->PostInitialise->AddRegion->RegionLoaded
         * 
         * RemoveRegion
         */
         
        public void Initialise(IConfigSource source)
        {
            m_log.DebugFormat("[HelloWorldModule]: Running {0} Sequence {1} : Enabled {2}", "Initialise", (m_InitCount++).ToString(), m_enabled.ToString());
        }

        public void PostInitialise()
        {
            m_log.DebugFormat("[HelloWorldModule]: Running {0} Sequence {1} : Enabled {2}", "PostInitialise", (m_InitCount++).ToString(), m_enabled.ToString());
        }

        public void Close()
        {
            m_log.DebugFormat("[HelloWorldModule]: Running {0} Sequence {1} : Enabled {2}", "Close", (m_InitCount++).ToString(), m_enabled.ToString());
        }

        public void AddRegion(Scene scene)
        {
            m_log.DebugFormat("[HelloWorldModule]: Running {0} Sequence {1} : Enabled {2}", "AddRegion", (m_InitCount++).ToString(), m_enabled.ToString());
            m_log.DebugFormat("[HelloWorldModule]: Add region: {0}", scene.RegionInfo.RegionName);

            m_scenes.Add(scene);
            scene.EventManager.OnFrame += OnFrameUpdate; // onframe onnewclient

            //if (scene.RegionInfo.RegionName.Equals("huyu00"))
                CreateHelloWorldObject(scene);
        }

        public void RemoveRegion(Scene scene)
        {
            m_log.DebugFormat("[HelloWorldModule]: Running {0} Sequence {1} : Enabled {2}", "RemoveRegion", (m_InitCount++).ToString(), m_enabled.ToString());
            m_log.DebugFormat("[HelloWorldModule]: remove region: {0}", scene.RegionInfo.RegionName);

            m_scenes.Remove(scene);
            scene.EventManager.OnFrame -= OnFrameUpdate; // onframe onnewclient
        }

        public void RegionLoaded(Scene scene)
        {
            m_log.DebugFormat("[HelloWorldModule]: Running {0} Sequence {1} : Enabled {2}", "RegionLoaded", (m_InitCount++).ToString(), m_enabled.ToString());
            m_log.DebugFormat("[HelloWorldModule]: load region: {0}", scene.RegionInfo.RegionName);
        }

        /*
         * OnFrame is triggered every time there is a render frame in opensim, 
         * which is about 20 times per second.
         * If you are firing on the OnFrame event you need to do something small, 
         * or punt most of the time, 
         * as you'll negatively impact the performance of the system otherwise.
         * */

        private void OnFrameUpdate()
        {
            if (counter++ % 50 == 0)
            { // Notice that I have a counter there so that things only move every 50 heartbeats
                foreach (KeyValuePair<Scene, List<SceneObjectGroup>> kvp in scene_prims)
                {
                    //m_log.Debug("[HelloWorldModule] FrameUpdate!");
                    foreach (SceneObjectGroup sog in kvp.Value)
                    {
                        //  I also have a direction (positive) that can be true or false, 
                        //  so that the prims move back and forth
                        //  As you can see, the way to move prims is to set their AbsolutePosition property directly
                        if (positive)
                            sog.AbsolutePosition += new Vector3(5, 5, 0);
                        else
                            sog.AbsolutePosition += new Vector3(-5, -5, 0);

                        // After that, we tell opensim to schedule the object for an update, so that we can see it move
                        sog.ScheduleGroupForTerseUpdate();
                    }
                }
                positive = !positive;
            }
        }

        private void CreateHelloWorldObject(Scene scene)
        {
            // We're going to write HELLO with prims
            // SOG represents a linked group of objects, the link sets in SL, with root part and all
            List<SceneObjectGroup> prims = new List<SceneObjectGroup>();

            // First prim: |
            Vector3 pos = new Vector3(120, 128, 30);
            SceneObjectGroup sog = new SceneObjectGroup(UUID.Zero, pos, PrimitiveBaseShape.CreateBox());
            sog.RootPart.Scale = new Vector3(0.3f, 0.3f, 2f);
            prims.Add(sog);

            // Second prim: -
            pos = new Vector3(120.5f, 128f, 30f);
            sog = new SceneObjectGroup(UUID.Zero, pos, PrimitiveBaseShape.CreateBox());
            sog.RootPart.Scale = new Vector3(1, 0.3f, 0.3f);
            prims.Add(sog);

            // Third prim: |
            pos = new Vector3(121, 128, 30);
            sog = new SceneObjectGroup(UUID.Zero, pos, PrimitiveBaseShape.CreateBox());
            sog.RootPart.Scale = new Vector3(0.3f, 0.3f, 2);
            prims.Add(sog);

            // Fourth prim: |
            pos = new Vector3(122, 128, 30);
            sog = new SceneObjectGroup(UUID.Zero, pos, PrimitiveBaseShape.CreateBox());
            sog.RootPart.Scale = new Vector3(0.3f, 0.3f, 2);
            prims.Add(sog);

            // Fifth prim: - (up)
            pos = new Vector3(122.5f, 128, 31);
            sog = new SceneObjectGroup(UUID.Zero, pos, PrimitiveBaseShape.CreateBox());
            sog.RootPart.Scale = new Vector3(1, 0.3f, 0.3f);
            prims.Add(sog);

            // Sixth prim: - (middle)
            pos = new Vector3(122.5f, 128, 30);
            sog = new SceneObjectGroup(UUID.Zero, pos, PrimitiveBaseShape.CreateBox());
            sog.RootPart.Scale = new Vector3(1, 0.3f, 0.3f);
            prims.Add(sog);

            // Seventh prim: - (low)
            pos = new Vector3(122.5f, 128, 29);
            sog = new SceneObjectGroup(UUID.Zero, pos, PrimitiveBaseShape.CreateBox());
            sog.RootPart.Scale = new Vector3(1, 0.3f, 0.3f);
            prims.Add(sog);

            // Eighth prim: | 
            pos = new Vector3(124, 128, 30);
            sog = new SceneObjectGroup(UUID.Zero, pos, PrimitiveBaseShape.CreateBox());
            sog.RootPart.Scale = new Vector3(0.3f, 0.3f, 2);
            prims.Add(sog);

            // Ninth prim: _
            pos = new Vector3(124.5f, 128, 29);
            sog = new SceneObjectGroup(UUID.Zero, pos, PrimitiveBaseShape.CreateBox());
            sog.RootPart.Scale = new Vector3(1, 0.3f, 0.3f);
            prims.Add(sog);

            // Tenth prim: | 
            pos = new Vector3(126, 128, 30);
            sog = new SceneObjectGroup(UUID.Zero, pos, PrimitiveBaseShape.CreateBox());
            sog.RootPart.Scale = new Vector3(0.3f, 0.3f, 2);
            prims.Add(sog);

            // Eleventh prim: _
            pos = new Vector3(126.5f, 128, 29);
            sog = new SceneObjectGroup(UUID.Zero, pos, PrimitiveBaseShape.CreateBox());
            sog.RootPart.Scale = new Vector3(1, 0.3f, 0.3f);
            prims.Add(sog);

            // Twelveth prim: O
            pos = new Vector3(129, 128, 30);
            sog = new SceneObjectGroup(UUID.Zero, pos, PrimitiveBaseShape.CreateBox());
            sog.RootPart.Scale = new Vector3(2, 0.3f, 2);
            prims.Add(sog);

            // Add these to the managed objects
            scene_prims.Add(scene, prims);

            // Now place them visibly on the scene
            foreach (SceneObjectGroup sogr in prims)
            {
                scene.AddNewSceneObject(sogr, false);
            }

            //
            //ScenePresence avatar = scene.GetScenePresence("Test", "User");
            //Vector3 location = new Vector3(128, 128, 50);
            //avatar.MoveToTarget(location, true, true);
        }

    }
}
