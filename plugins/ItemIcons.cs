using System.Collections.Generic;
using System.Linq;
using Oxide.Core.Configuration;
using UnityEngine;

namespace Oxide.Plugins
{
    [Info("ItemIcons", "SHooTeR", "1.0.0")]
    public class ItemIcons : RustPlugin
    {
		private Dictionary<string, string> cfg;

		protected override void LoadDefaultConfig()
        {

			var config = new Dictionary<string, string>();

			var gameObjectArray = FileSystem.LoadAll<GameObject>("Assets/", ".item");
			var itemList = gameObjectArray.Select(x => x.GetComponent<ItemDefinition>()).Where(x => x != null).ToList();

			foreach (var item in itemList)
				config[item.shortname] = "assets/icons/isloading.png";
				
            Config.WriteObject(config, true);
		}

		private void OnServerInitialized()
        {
			cfg = Config.ReadObject<Dictionary<string, string>>();
			
			foreach (var item in ItemManager.itemList)
			{
				if(!cfg.ContainsKey(item.shortname))
					cfg[item.shortname] = "assets/icons/isloading.png";
			}
			
			Config.WriteObject(cfg, true);
		}
		
		public string ItemIcon(string shortname, bool placeholder = true)
		{
			if(cfg.ContainsKey(shortname))
				return cfg[shortname];
			if(placeholder)
				return "assets/content/textures/missing_icon.png";
			return null;
		}
    }
}
