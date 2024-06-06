using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ScriptableObjectJsonConverter<T> : JsonConverter<T>
    where T : ScriptableObject
{
    Dictionary<string, PropertyInfo> propertyDict;
    public ScriptableObjectJsonConverter()
    {
        GetJsonProperty();
    }

    private void GetJsonProperty()
    {
        // Create a dictionary that maps property names to property info objects
        var properties = typeof(T).GetProperties();
        propertyDict = new Dictionary<string, PropertyInfo>();
        foreach (var property in properties)
        {
            var attribute = property.GetCustomAttribute<JsonPropertyAttribute>();
            if (attribute != null)
            {
                propertyDict[attribute.PropertyName] = property;
            }
        }
    }

    public override T ReadJson(JsonReader reader, System.Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        try
        {
            T data = ScriptableObject.CreateInstance<T>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string propertyName = (string)reader.Value;
                    reader.Read();

                    // Use the dictionary to get the property info object
                    if (propertyDict.TryGetValue(propertyName, out var property))
                    {
                        // Set the property value
                        property.SetValue(data, reader.Value);
                    }
                }
            }

            return data;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        return null;
    }

    public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
    {
        writer.WriteStartObject();

        foreach (var property in propertyDict)
        {
            writer.WritePropertyName(property.Key);
            writer.WriteValue(property.Value.GetValue(value));
        };

        writer.WriteEndObject();
    }
}