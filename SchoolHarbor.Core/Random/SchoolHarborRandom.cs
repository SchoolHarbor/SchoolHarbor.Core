using System.Reflection;
using SchoolHarbor.Core.Identifier;

namespace SchoolHarbor.Core.Random;

    public class SchoolHarborRandom
    {
        private static readonly System.Random Seed = new System.Random();

        private static readonly Dictionary<Type, Type> InterfaceMappings = new Dictionary<Type, Type>
        {
        };

        private static readonly Dictionary<Type, Func<object>> TypeImplementations = new Dictionary<Type, Func<object>>
        {
            { typeof(int), () => SchoolHarborRandom.Int() },
            { typeof(string), () => SchoolHarborRandom.String() },
            { typeof(DateTime), () => SchoolHarborRandom.Date() },
            { typeof(bool), () => SchoolHarborRandom.Bool() },
            { typeof(byte), () => SchoolHarborRandom.Byte() },
            { typeof(decimal), () => SchoolHarborRandom.Decimal() },
            { typeof(ReferenceId), () => SchoolHarborRandom.ReferenceId() }
        };

        public static int Int() => Seed.Next(10000, 200000);

        public static string String() => Guid.NewGuid().ToString();

        public static bool Bool() => Seed.Next(0, 2) == 1;

        public static byte Byte() => (byte)Seed.Next(byte.MinValue, byte.MaxValue + 1);

        public static decimal Decimal() => Seed.Next(2) == 0 ? 0.5M : 1.0M;

        public static int GradeLevel() => Seed.Next(0, 8);

        public static ReferenceId ReferenceId() => new ReferenceId(
            ReferenceIdKind.Database,
            ReferenceIdSourceKind.SchoolHarborSql, 
            SchoolHarborRandom.Int().ToString());

        public static DateTime Date()
        {
            var startYear = 1990;
            var endYear = 2030;
            var year = Seed.Next(startYear, endYear + 1);
            var month = Seed.Next(1, 13);
            var day = DateTime.DaysInMonth(year, month);
            var randomDay = Seed.Next(1, day + 1);

            return new DateTime(year, month, randomDay);
        }

        public static T GenerateRandomPoco<T>()
            where T : new()
        {
            var obj = new T();
            PopulateObjectWithRandomValues(obj);
            return obj;
        }

        public static T GenerateRandomPoco<T>(Dictionary<string, object> propertyOverrides)
            where T : new()
        {
            var obj = new T();
            PopulateObjectWithRandomValues(obj, propertyOverrides);
            return obj;
        }

        public static T GenerateRandomDomain<T>()
        {
            return (T)GenerateRandomDomainInternal(typeof(T));
        }

        public static T GenerateRandomDomain<T>(Dictionary<string, object> parameterValues)
        {
            return (T)GenerateRandomDomainInternal(typeof(T), parameterValues);
        }

        private static object GenerateRandomDomainInternal(Type type, Dictionary<string, object> parameterValues = null)
        {
            type = ResolveType(type);

            var constructor = type.GetConstructors().MaxBy(x => x.GetParameters().Length)
                              ?? throw new InvalidOperationException($"No suitable constructor found for type {type.FullName}");

            var resolvedParameterValues = constructor.GetParameters()
                .Select(x => parameterValues != null && parameterValues.TryGetValue(x.Name.ToPascalCase(), out var specifiedValue)
                    ? specifiedValue
                    : (x.ParameterType.IsPrimitive || IsSupported(x.ParameterType)
                        ? ImplementType(x.ParameterType)
                        : GenerateRandomDomainInternal(x.ParameterType)))
                .ToArray();

            return constructor.Invoke(resolvedParameterValues);
        }

        private static void PopulateObjectWithRandomValues(object obj, Dictionary<string, object> propertyOverrides = null)
        {
            foreach (var property in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanWrite))
            {
                var value = propertyOverrides != null && propertyOverrides.TryGetValue(property.Name, out var specifiedValue)
                    ? specifiedValue
                    : ImplementType(property.PropertyType);

                property.SetValue(obj, value);
            }
        }
        

        private static object ImplementType(Type type)
        {
            if (TypeImplementations.TryGetValue(type, out var implementation))
            {
                return implementation();
            }

            return Activator.CreateInstance(type);
        }

        private static Type ResolveType(Type type) => type.IsInterface && InterfaceMappings.TryGetValue(type, out var concreteType) ? concreteType : type;

        private static bool IsSupported(Type type) => TypeImplementations.Keys.Contains(type);
    }