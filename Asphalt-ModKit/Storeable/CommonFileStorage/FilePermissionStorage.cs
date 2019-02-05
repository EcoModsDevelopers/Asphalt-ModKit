using Asphalt.Api.Util;
using Asphalt.Service.Permissions;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Asphalt.Storeable.CommonFileStorage
{
    public class FilePermissionStorage : FileStorage, IPermissionService
    {
        public FilePermissionStorage(IFileStorageSerializer pSerializer, string pFileName, IDictionary<string, object> pDefaultValues = null, bool pSaveDefaultValues = false) : base(pSerializer, pFileName, pDefaultValues, pSaveDefaultValues)
        {
        }

        public override void Reload()
        {
            this.Content.Clear();

            Dictionary<string, object> tmpContent = mSerializer.Deserialize(FileUtil.ReadFromFile(FileName)) ?? new Dictionary<string, object>();

            //cast string values from file to PermissionGroup enum
            foreach (KeyValuePair<string, object> pair in tmpContent)
            {
                PermissionGroup value;
                try
                {
                    value = (PermissionGroup)Enum.Parse(typeof(PermissionGroup), (string)pair.Value);
                }
                catch
                {
                    throw new Exception($"Value '{pair.Value}' of permission '{pair.Key}' is not valid");
                }
                this.Content.Add(pair.Key, value);
            }

            if (mSaveDefaultValues && DefaultValues != null)
            {
                Content = MergeWithDefaultValues(Content, DefaultValues);
                //save the file even if it's empty to show that there are no default values
                ForceSave();
            }
        }

        public override void ForceSave()
        {
            FileUtil.WriteToFile(FileName, mSerializer.Serialize(this.Content.ToDictionary(k => k.Key, v => (object)v.Value.ToString())));
        }

        public bool CheckPermission(User user, string permission)
        {
            if (HasPermission(user, permission))
                return true;

            user.Player.SendTemporaryError(new LocString($"You don't have the permission to do this! <color=#595959>Permission needed: {permission}</color>"));
            return false;
        }

        public bool HasPermission(User user, string permission)
        {
            object obj = Get(permission);

            PermissionGroup? permissionGroup = obj as PermissionGroup?;

            if (permissionGroup == null)
                return false;

            switch (permissionGroup)
            {
                case PermissionGroup.Nobody:
                    return false;
                case PermissionGroup.User:
                    return true;
                case PermissionGroup.Admin:
                    return user.IsAdmin;
            }

            return false;
        }
    }
}
