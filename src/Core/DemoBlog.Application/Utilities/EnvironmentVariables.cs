using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace DemoBlog.Application.Utilities
{
    public static class EnvironmentVariables
    {
        public static void Populate<TInstance>(this TInstance instance, IConfiguration configuration, bool allRequired = true) where TInstance : class
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            instance.PopulateInstance(configuration, allRequired);
        }

        private static void PopulateInstance<TInstance>(this TInstance instance, IConfiguration configuration = null, bool allRequired = true) where TInstance : class
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            IEnumerable<PropertyInfo> members = instance
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => IsAllowedType(p.PropertyType) && p.CanWrite);

            GetAndSetVariables(instance, members, configuration, allRequired);
        }

        private static void GetAndSetVariables(object target, IEnumerable<MemberInfo> members, IConfiguration configuration = null, bool allRequired = true)
        {
            foreach (var member in members)
            {
                if (member.MemberType != MemberTypes.Property && member.MemberType != MemberTypes.Field)
                    continue;

                var attr = member.GetCustomAttribute(typeof(DataMemberAttribute), true) as DataMemberAttribute;
                var name = member.Name;
                var required = allRequired;

                if (attr != null && attr.IsNameSetExplicitly)
                {
                    name = attr.Name;
                    required = attr.IsRequired;
                }
                string value = GetValueFromConfigOrEnvironment(name, configuration);

                if (value is null && required)
                    throw new ArgumentException($"Could not find Environment Variable with name {name}");

                // skip null values to not overwrite any correct defaults
                if (value is null)
                    continue;

                if (member is PropertyInfo property)
                {
                    var convertedValue = Convert.ChangeType(value, property.PropertyType);
                    property.SetValue(target, convertedValue);
                }
                else if (member is FieldInfo field)
                {
                    var convertedValue = Convert.ChangeType(value, field.FieldType);
                    field.SetValue(target, convertedValue);
                }
            }
        }

        private static string GetValueFromConfigOrEnvironment(string name, IConfiguration configuration)
        {
            if (configuration != null)
                return configuration.GetValue<string>(name);

            var processValue = Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
            var userValue = Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.User);
            var machineValue = Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Machine);

            if (!string.IsNullOrWhiteSpace(processValue))
                return processValue;
            if (!string.IsNullOrWhiteSpace(userValue))
                return userValue;
            if (!string.IsNullOrWhiteSpace(machineValue))
                return machineValue;

            return null;
        }

        private static bool IsAllowedType(Type t)
        {
            if (t == typeof(string)) return true;
            if (t == typeof(long)) return true;
            if (t == typeof(int)) return true;
            if (t == typeof(double)) return true;
            if (t == typeof(decimal)) return true;
            if (t == typeof(bool)) return true;

            return false;
        }
    }
}
