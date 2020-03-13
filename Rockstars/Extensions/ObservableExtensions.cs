using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;

namespace System
{
    /// <summary>
    ///     THIRD PARTY CODE: Contains extension methods for working with <see cref="IObservable{T}"/> types.
    /// </summary>
    public static class ObservableExtensions
    {
        /// <summary>
        ///     Creates an <see cref="IObservable{T}"/> for a property on an <see cref="INotifyPropertyChanged"/> type.
        /// </summary>
        /// <typeparam name="TSource">Type of the source object (which must implement <see cref="INotifyPropertyChanged"/>).</typeparam>
        /// <typeparam name="TProperty">Type of the property being pointed to by <paramref name="property"/>.</typeparam>
        /// <param name="source">A source object that implements <see cref="INotifyPropertyChanged"/>.</param>
        /// <param name="property">An expression pointing to a property on the source object for which an <see cref="IObservable{T}"/> must be created.</param>
        /// <returns>Returns an <see cref="IObservable{T}"/> that can be subscribed to in order to get property changes.</returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Should be disposed by the caller.")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Necessary to get type safety.")]
        public static IObservable<TProperty> PropertyChanges<TSource, TProperty>(this TSource source, Expression<Func<TSource, TProperty>> property)
            where TSource : INotifyPropertyChanged
        {
            // Validate arguments
            if (object.Equals(source, default(TSource)))
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            // Get the property being pointed to by the expression
            var propertyInfo = property.ToPropertyInfo();
            var propertyChangedObservable = Observable.FromEventPattern<PropertyChangedEventArgs>(source, nameof(INotifyPropertyChanged.PropertyChanged));
            var observable = propertyChangedObservable.Where(@event => @event.EventArgs.PropertyName == propertyInfo.Name)
                                                      .Select(_ => (TProperty)propertyInfo.GetValue(source, null));

            var subject = new BehaviorSubject<TProperty>((TProperty)propertyInfo.GetValue(source, null));
            observable.Subscribe(subject);

            return subject;
        }

        /// <summary>
        ///     Disposes this <see cref="IDisposable"/> with the provided <see cref="CompositeDisposable"/>.
        /// </summary>
        /// <param name="disposable">An <see cref="IDisposable"/> to add to the <paramref name="composite"/>.</param>
        /// <param name="composite">A <see cref="CompositeDisposable"/> instance to add the <paramref name="disposable"/> to.</param>
        public static void DisposeWith(this IDisposable disposable, CompositeDisposable composite)
        {
            // Validate arguments
            if (disposable == null)
            {
                throw new ArgumentNullException(nameof(disposable));
            }
            if (composite == null)
            {
                throw new ArgumentNullException(nameof(composite));
            }

            // Add the disposable to the composite
            composite.Add(disposable);
        }

        /// <summary>
        ///     Converts an <see cref="Expression"/> into a <see cref="PropertyInfo"/> for the property it points to.
        /// </summary>
        /// <typeparam name="TTarget">Type of the target object.</typeparam>
        /// <typeparam name="TValue">Type of the property.</typeparam>
        /// <param name="expression">An expression pointing to a property on the target object.</param>
        /// <returns></returns>
        private static PropertyInfo ToPropertyInfo<TTarget, TValue>(this Expression<Func<TTarget, TValue>> expression)
        {
            // Get the body of the expression
            Expression body = expression.Body;
            if (body.NodeType != ExpressionType.MemberAccess)
            {
                throw new ArgumentException("Property expression must be of the form 'x => x.SomeProperty'", nameof(expression));
            }

            // Cast the expression to the appropriate type
            MemberExpression memberExpression = (MemberExpression)body;

            return memberExpression.Member as PropertyInfo;
        }
    }
}
