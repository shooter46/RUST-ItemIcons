using System.Collections.Generic;
using System.Linq;
using Oxide.Core.Configuration;
using UnityEngine;
using Newtonsoft.Json;

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

			webrequest.Enqueue("https://raw.githubusercontent.com/shooter46/RUST-ItemIcons/main/config/ItemIcons.json", null, (code, response) =>
			{
				if (response != null && code == 200)
				{
					try
					{
						Dictionary<string, string> items = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
						foreach(var item in items) {
							cfg[item.Key] = item.Value;
						}
					}
					catch {}
					Config.WriteObject(cfg, true);
				}
			}, this);

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
