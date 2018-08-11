using System;
using System.ComponentModel;

namespace FancyControls
{
    /// <summary>
    /// Provides supplemental metadata to the FancyControls.AbstractControlDescriptor.
    /// </summary>
    /// <typeparam name="TAbstract">
    /// The type of abstract control or form.
    /// </typeparam>
    /// <typeparam name="TBase">
    /// The type of base control for control with type of TAbstract.
    /// </typeparam>
    public abstract class AbstractControlDescriptionProvider<TAbstract, TBase> : 
        TypeDescriptionProvider where TAbstract: TBase
    {
        /// <summary>
        /// Initializes a new instance of the FancyControls.AbstractControlDescriptionProvider class.
        /// </summary>
        public AbstractControlDescriptionProvider()
            : base(TypeDescriptor.GetProvider(typeof(TAbstract)))
        {
        }
        /// <summary>
        /// Performs normal reflection against the given object with the given type.
        /// </summary>
        /// <param name="objectType">
        /// The type of object for which to retrieve the System.Reflection.IReflect.
        /// </param>
        /// <param name="instance">
        /// An instance of the type. Can be null.
        /// </param>
        /// <returns>
        /// The type of reflection for this objectType.
        /// </returns>
        public override Type GetReflectionType(Type objectType, object instance)
        {
            if (objectType == typeof(TAbstract))
                return typeof(TBase);

            return base.GetReflectionType(objectType, instance);
        }
        /// <summary>
        /// Creates an object that can substitute for another data type.
        /// </summary>
        /// <param name="provider">
        /// An optional service provider.
        /// </param>
        /// <param name="objectType">
        /// The type of object to create. This parameter is never null.
        /// </param>
        /// <param name="argTypes">
        /// An optional array of types that represent the parameter types to be passed to
        /// the object's constructor. This array can be null or of zero length.
        /// </param>
        /// <param name="args">
        /// An optional array of parameter values to pass to the object's constructor.
        /// </param>
        /// <returns>
        /// The substitute System.Object.
        /// </returns>
        public override object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
        {
            if (objectType == typeof(TAbstract))
                objectType = typeof(TBase);

            return base.CreateInstance(provider, objectType, argTypes, args);
        }
    }
}
