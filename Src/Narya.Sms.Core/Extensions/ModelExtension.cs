﻿using System.Reflection;
using Narya.Sms.Core.Interfaces;
using Narya.Sms.Core.Models;

namespace Narya.Sms.Core.Extensions;

public static class ModelExtension
{
    public static Result<T> ConvertTo<T>(object model) where T : class, IProviderConfig, new()
    {
        var config = new T();
        var configType = typeof(T);

        foreach (var property in configType.GetProperties())
        {
            Result result;
            var modelProperty = model.GetType().GetProperty(property.Name);
            if (modelProperty is not null) result = SetValue(config, property, modelProperty.GetValue(model));
            else result = SetValue(config, property, null);
            if (result.IsFailure) return Result<T>.Failure(result.Errors);
        }

        return Result<T>.Success(config);
    }

    private static Result SetValue<T>(T config, PropertyInfo property, object? value)
        where T : class, IProviderConfig, new()
    {
        if (config.ValidateProperty(config, property.Name, value) is false)
            return Result.Failure($"Invalid '{property.Name}' in 'Sms Configurations'.");
        property.SetValue(config,
            value is not null ? value :
            property.PropertyType.IsValueType ? Activator.CreateInstance(property.PropertyType) : null);
        return Result.Success();
    }
}