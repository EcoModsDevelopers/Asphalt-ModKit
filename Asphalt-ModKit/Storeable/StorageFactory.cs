using Asphalt.Service;
using Asphalt.Service.Permissions;
using Asphalt.Storeable.CommonFileStorage;
using Asphalt.Storeable.Json;
using Asphalt.Storeable.Yaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asphalt.Storeable
{
    public static class StorageFactory
    {
        public static IFileStorageSerializer GetSerializer(Type pServerPlugin, Type pProperty)
        {
            return new JsonFileStorageSerializer();
        }

        public static Func<PropertyFieldInfo, Dictionary<string, object>, object> GetStorageFactory(Type pServerPlugin, Type pProperty)
        {
            IFileStorageSerializer serializer = GetSerializer(pServerPlugin, pProperty);

            if (pProperty == typeof(IStorage))
                return (l_pfi, defaultValues) => new FileStorage(serializer, Path.Combine(ServiceHelper.GetServerPluginFolder(pServerPlugin), l_pfi.GetStorageLocationAttribute().Location), defaultValues, true);

            if (pProperty == typeof(IStorageCollection))
                return (l_pfi, defaultValues) => new FileStorageCollection(serializer, Path.Combine(ServiceHelper.GetServerPluginFolder(pServerPlugin), l_pfi.GetStorageLocationAttribute().Location), defaultValues);

            if (pProperty == typeof(IUserStorageCollection))
                return (l_pfi, defaultValues) => new FileUserStorageCollection(serializer, Path.Combine(ServiceHelper.GetServerPluginFolder(pServerPlugin), l_pfi.GetStorageLocationAttribute().Location), defaultValues);

            if (pProperty == typeof(IPermissionService))
                return (l_pfi, defaultValues) => new FilePermissionStorage(serializer, Path.Combine(ServiceHelper.GetServerPluginFolder(pServerPlugin), "Permissions"), defaultValues);


            throw new NotImplementedException(pProperty?.ToString());
        }
    }
}
