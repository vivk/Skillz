using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Features.SelectableInterface.Attributes;
using UnityEditor;
using UnityEngine;

namespace Features.SelectableInterface.Editor
{
    [CustomPropertyDrawer(typeof(SelectableImplAttribute), true)]
    public class InterfaceWithSerializableContentDrawer : PropertyDrawer
    {
        private static object GetTargetObjectOfProperty(SerializedProperty prop)
        {
            if (prop == null)
            {
                return null;
            }

            var path = prop.propertyPath.Replace(".Array.data[", "[");
            object obj = prop.serializedObject.targetObject;
            var elements = path.Split('.');
            foreach (var element in elements)
            {
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    obj = GetValue_Imp(obj, elementName, index);
                }
                else
                {
                    obj = GetValue_Imp(obj, element);
                }
            }
            return obj;
        }

        private static object GetValue_Imp(object source, string name)
        {
            if (source == null)
                return null;
            var type = source.GetType();

            while (type != null)
            {
                var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (f != null)
                    return f.GetValue(source);

                var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (p != null)
                    return p.GetValue(source, null);

                type = type.BaseType;
            }
            return null;
        }
        
        private static object GetValue_Imp(object source, string name, int index)
        {
            var enumerable = GetValue_Imp(source, name) as System.Collections.IEnumerable;
            if (enumerable == null) return null;
            var enm = enumerable.GetEnumerator();

            for (var i = 0; i <= index; i++)
            {
                if (!enm.MoveNext()) return null;
            }
            return enm.Current;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, true);
        }
        
        private static Type[] GetTypes(Type baseType)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => 
                    !p.IsAbstract 
                    && !p.IsInterface
                    && !p.ContainsGenericParameters
                    && p.IsSerializable
                    && baseType.IsAssignableFrom(p) 
                ).ToArray();
        }

        
        private static readonly Dictionary<string, Type[]> TypeCache = new();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var rect = new Rect(position)
            {
                height = EditorGUIUtility.singleLineHeight,
                x = 20,
            };
            var parts = property.managedReferenceFieldTypename.Split(" ");
            var typeName = parts[1] + "," + parts[0];
            Type[] types;
            var ok = TypeCache.TryGetValue(typeName, out var type);
            if (ok)
            {
                types = type;
            }
            else
            {
                types = GetTypes(Type.GetType(typeName,true));
                TypeCache.Add(typeName, types);
            }
            var typeNames = types.Select(t => t.Name).ToArray();
            var obj = GetTargetObjectOfProperty(property) ?? CreateInstance(property,0,types);
            var index = Array.IndexOf(typeNames, obj.GetType().Name);
            var newIndex = EditorGUI.Popup(rect, index, typeNames);
            if (newIndex != index)
            {
                CreateInstance(property, newIndex, types);
            }
            EditorGUI.PropertyField(position, property, GUIContent.none, true);
        }
        
        private static object CreateInstance(SerializedProperty property, int typeIndex, Type[] types)
        {
            var target = Activator.CreateInstance(types[typeIndex]);
            property.managedReferenceValue = target;
            property.serializedObject.ApplyModifiedProperties();
            return target;
        }

    }
}