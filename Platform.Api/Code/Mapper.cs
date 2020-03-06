using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Platform.Api.Code
{
    public static class Mapper
    {
        public static P FromTo<D, P>(D model)
        {
            P entity = (P)Activator.CreateInstance(typeof(P));
            foreach (PropertyInfo propModelo in model.GetType().GetProperties())
            {
                PropertyInfo propertyEntity = entity.GetType().GetProperty(propModelo.Name);

                if (propertyEntity != null)
                {
                    object objectProperty = propModelo.GetValue(model, null);
                    if (objectProperty != null)
                    {
                        Type propertyTypeIn = propertyEntity.PropertyType;
                        if (propertyEntity.PropertyType.IsGenericType && propertyTypeIn.GetGenericTypeDefinition() == typeof(Nullable<>))
                            propertyTypeIn = propertyTypeIn.GetGenericArguments()[0];

                        if (propertyTypeIn.IsEnum)
                            propertyEntity.SetValue(entity, Enum.ToObject(propertyTypeIn, Convert.ToInt32(objectProperty)), null);
                        else
                            propertyEntity.SetValue(entity, objectProperty, null);
                    }
                }
            }
            return entity;
        }

    }
}
