using System;
using System.Reflection;
using UnityEngine;

namespace Game
{
    public static class AutoMigration
    {
        public static T MigrateMissingFields<T>(T data, int targetVersion) where T : class, new()
        {
            if (data == null) data = new T();

            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fields)
            {
                // Skip static or readonly fields
                if (field.IsStatic || field.IsInitOnly) continue;

                var value = field.GetValue(data);

                // Handle Version field explicitly
                if (field.Name.Equals("Version", StringComparison.OrdinalIgnoreCase))
                {
                    int version = (int)(value ?? 0);
                    if (version < targetVersion)
                    {
                        field.SetValue(data, targetVersion);
                        Debug.LogWarning($"{typeof(T).Name} is outdated. Migrating from version {version} to {targetVersion}");
                    }
                    continue;
                }

                // Handle uninitialized fields
                if (IsDefault(value, field.FieldType))
                {
                    object defaultValue = Activator.CreateInstance(field.FieldType);
                    field.SetValue(data, defaultValue);
                }

                // Recursively migrate nested classes
                if (!field.FieldType.IsPrimitive && field.FieldType != typeof(string))
                {
                    value = field.GetValue(data);
                    if (value != null)
                    {
                        var migrateMethod = typeof(AutoMigration).GetMethod("MigrateMissingFields");
                        var genericMigrate = migrateMethod.MakeGenericMethod(field.FieldType);
                        var migratedValue = genericMigrate.Invoke(null, new[] { value, targetVersion });
                        field.SetValue(data, migratedValue);
                    }
                }
            }

            return data;
        }

        private static bool IsDefault(object value, Type type)
        {
            if (value == null) return true;
            if (type.IsValueType) return Activator.CreateInstance(type).Equals(value);
            return false;
        }
    }
}
