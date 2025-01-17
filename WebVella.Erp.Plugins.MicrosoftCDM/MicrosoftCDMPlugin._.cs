﻿using System;
using Newtonsoft.Json;
using WebVella.Erp;
using WebVella.Erp.Api;
using WebVella.Erp.Api.Models;
using WebVella.Erp.Database;
using WebVella.Erp.Exceptions;
using WebVella.Erp.Plugins.MicrosoftCDM.Model;

namespace WebVella.Erp.Plugins.MicrosoftCDM
{
	public partial class MicrosoftCDMPlugin : ErpPlugin
	{

		private const int WEBVELLA_CRM_INIT_VERSION = 20200824;

		public void ProcessPatches()
		{
			using (SecurityContext.OpenSystemScope())
			{

				
				var recMan = new RecordManager();
				var storeSystemSettings = DbContext.Current.SettingsRepository.Read();
				var systemSettings = new SystemSettings(storeSystemSettings);

				//Create transaction
				using (var connection = DbContext.Current.CreateConnection())
				{
					try
					{
						connection.BeginTransaction();

						//Here we need to initialize or update the environment based on the plugin requirements.
						//The default place for the plugin data is the "plugin_data" entity -> the "data" text field, which is used to store stringified JSON
						//containing the plugin settings or version

						//TODO: Develop a way to check for installed plugins
						#region << 1.Get the current ERP database version and checks for other plugin dependencies >>

						if (systemSettings.Version > 0)
						{
							//Do something if database version is not what you expect
						}

						#endregion

						#region << 2.Get the current plugin settings from the database >>

						var currentPluginSettings = new PluginSettings() { Version = WEBVELLA_CRM_INIT_VERSION };
						string jsonData = GetPluginData();
						if (!string.IsNullOrWhiteSpace(jsonData))
							currentPluginSettings = JsonConvert.DeserializeObject<PluginSettings>(jsonData);

						#endregion

						#region << 3. Run methods based on the current installed version of the plugin >>

						//Patch 20190123
						//{
						//	var patchVersion = 20190123;
						//	if (currentPluginSettings.Version < patchVersion)
						//	{
						//		try
						//		{
						//			currentPluginSettings.Version = patchVersion;
						//			Patch20190123(entMan, relMan, recMan);
						//		}
						//		catch (ValidationException ex)
						//		{
						//			var exception = ex;
						//			throw ex;
						//		}
						//		catch (Exception ex)
						//		{
						//			var exception = ex;
						//			throw ex;
						//		}
						//	}
						//}

						#endregion


						SavePluginData(JsonConvert.SerializeObject(currentPluginSettings));

						connection.CommitTransaction();
						
					}
					catch (ValidationException ex)
					{
						connection.RollbackTransaction();
						Console.WriteLine(ex);
						throw;
					}

					catch (Exception)
					{
						connection.RollbackTransaction();
						throw;
					}
				}
			}
		}

	}
}
