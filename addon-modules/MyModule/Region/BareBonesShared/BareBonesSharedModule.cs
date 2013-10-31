/*
 * Copyright (c) Contributors, http://opensimulator.org/
 * See CONTRIBUTORS.TXT for a full list of copyright holders.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the OpenSimulator Project nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE DEVELOPERS ``AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE CONTRIBUTORS BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Reflection;
using log4net;
using Mono.Addins;
using Nini.Config;
using OpenSim.Region.Framework.Interfaces;
using OpenSim.Region.Framework.Scenes;

//required for newer versions of Mono
//[assembly: Addin("BareBonesSharedModule", "0.1")] 
//[assembly: AddinDependency("OpenSim", "0.5")] 

namespace MyModule.Region.BareBonesShared
{
    /// <summary>
    /// Simplest possible example of a shared region module.
    /// </summary>
    /// <remarks>
    /// This module is the simplest possible example of a shared region module (a module which is shared by every
    /// scene/region running on the simulator).  If anybody wants to create a more complex example in the future then 
    /// please create a separate class.
    /// 
    /// This module is not active by default.  If you want to see it in action, 
    /// then just uncomment the line below starting with [Extension(Path...
    /// 
    /// When the module is enabled it will print messages when it receives certain events to the screen and the log
    /// file.
    /// </remarks>
    /// //makes the module visible to OpenSim's module mechanism (Mono.Addins)
    [Extension(Path = "/OpenSim/RegionModules", NodeName = "RegionModule", Id = "BareBonesSharedModule")]
    public class BareBonesSharedModule : ISharedRegionModule
    {
        //can be used to output messages both to the console & the OpenSim.log file
        private static readonly ILog m_log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /*
       * ======= ======= ======= ======= ======= ======= ======= ======= ======= ======= ======= ======= ======= =======
       * Methods required by IRegionModuleBase (which ISharedRegionModule & INonSharedRegionModule extend)
       */

        /*
         * This name is shown when the console command "show modules" is run.
         */
        public string Name { get { return "Bare Bones Shared Module"; } }

        /*
        * If this is not null, then the module is not loaded if any other module implements the given interface.
        * One use for this is to provide 'stub' functionality implementations that are only active if no other
        * module is present.
        */
        public Type ReplaceableInterface { get { return null; } }

        /*
        * This method is called immediately after the region module has been loaded into the runtime, before it
        * has been added to a scene or scenes. IConfigSource is a Nini class that contains the concatentation of
        * config parameters from OpenSim.ini, OpenSimDefaults.ini and the appropriate ini files in bin/config-include
        */
        public void Initialise(IConfigSource source)
        {
            m_log.DebugFormat("[BARE BONES SHARED]: INITIALIZED MODULE");
        }
        
        public void PostInitialise()
        {
            m_log.DebugFormat("[BARE BONES SHARED]: POST INITIALIZED MODULE");
        }

        /*
         * This method will be invoked when the sim is closing down.
         */
        public void Close()
        {
            m_log.DebugFormat("[BARE BONES SHARED]: CLOSED MODULE");
        }

        /*
         * This method is called when a region is added to the module. For shared modules this will happen multiple
         * times (one for each module). For non-shared modules this will happen only once. The module can store the
         * scene reference and use it later to reach and invoke OpenSim internals and interfaces.
         */
        public void AddRegion(Scene scene)
        {
            m_log.DebugFormat("[BARE BONES SHARED]: REGION {0} ADDED", scene.RegionInfo.RegionName);
        }

        /*
        * Called when a region is removed from a module. For shared modules this can happen multiple times. For
        * non-shared region modules this will happen only once and should shortly be followed by a Close(). On
        * simulator shutdown, this method will be called before Close(). RemoveRegion() can also be called if a
        * region/scene is manually removed while the simulator is running.
        */
        public void RemoveRegion(Scene scene)
        {
            m_log.DebugFormat("[BARE BONES SHARED]: REGION {0} REMOVED", scene.RegionInfo.RegionName);
        }

        /*
         * Called when all modules have been added for a particular scene/region. Since all other modules are now
         * loaded, this gives the module an opportunity to obtain interfaces or subscribe to events on other modules.
         * Called once for a non-shared region module and multiple times for shared region modules.
         */
        public void RegionLoaded(Scene scene)
        {
            m_log.DebugFormat("[BARE BONES SHARED]: REGION {0} LOADED", scene.RegionInfo.RegionName);
        }

        /*
         * End of methods required by IRegionModuleBase
         * ======= ======= ======= ======= ======= ======= ======= ======= ======= ======= ======= ======= ======= =======
         */
  
    }
}