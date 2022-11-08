using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace MediatorTest.WebHost.Configurations
{
    public static class MvcOptionsExtensions
    {
        public static MvcOptions UseDateOnlyStringConverter(this MvcOptions options)
        {
            TypeDescriptor.AddAttributes(typeof(DateOnly), new TypeConverterAttribute(typeof(DateOnlyTypeConverter)));

            return options;
        }
    }
}
