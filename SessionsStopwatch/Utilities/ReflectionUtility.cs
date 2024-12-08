using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SessionsStopwatch.Utilities;

public static class ReflectionUtility {
    public static IEnumerable<Type> GetDerivedTypes(this Type type) {
        return type.Assembly.GetTypes().Where(x => x != type && x.IsAssignableTo(type));
    }
}