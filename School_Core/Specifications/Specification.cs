using School_Core.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using School_Core.Domain.Models.Students;

namespace Domain.Specifications
{

    public abstract class Specification<T> : ISpecification<T>, IEquatable<Specification<T>>
    {
        public IQueryable<T> SatisfyEntitiesFrom(IQueryable<T> query)
        {
            return query.Where(Predicate);
        }

        public IEnumerable<T> SatisfyEntitiesFrom(IEnumerable<T> collection)
        {
            return collection.AsQueryable().Where(Predicate);
        }

        public bool IsSatisfiedBy(T entity)
        {
            return Predicate.Compile().Invoke(entity);
        }

        internal abstract Expression<Func<T, bool>> Predicate { get; }

        // Use && for correctness - specifications use lazy (conditional) evaluation internally.
        public static Specification<T> operator &(Specification<T> spec1, Specification<T> spec2)
        {
            return new AndSpecification<T>(spec1, spec2);
        }

        // Use || for correctness - specifications use lazy (conditional) evaluation internally.
        public static Specification<T> operator |(Specification<T> spec1, Specification<T> spec2)
        {
            return new OrSpecification<T>(spec1, spec2);
        }

        public static Specification<T> operator !(Specification<T> spec1)
        {
            return new NotSpecification<T>(spec1);
        }

        public static bool operator true(Specification<T> spec)
        {
            return false; // No Operation - boilerplate for conditional operators.
        }

        public static bool operator false(Specification<T> spec)
        {
            return false; // No Operation - boilerplate for conditional operators.
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            if (obj.GetType() != GetType())
                return false;

            // Specifications are equal, if they are of same type and have all same field values (this counts also automatic properties, because all automatic properties have backing fields accessible through reflection).
            return GetInstanceFields(GetType()).All(f => ValuesAreEqual(f.GetValue(obj), f.GetValue(this)));
        }

        private bool ValuesAreEqual(object value1, object value2)
        {
            if (value1 == null && value2 == null)
                return true;
            if (value1 == null || value2 == null)
                return false;
            if (ReferenceEquals(value1, value2) || value1.Equals(value2))
                return true;

            // compare arrays and tuples
            if (value1 is IStructuralEquatable equatable1 && value2 is IStructuralEquatable equatable2)
            {
                return equatable1.Equals(equatable2, StructuralComparisons.StructuralEqualityComparer);
            }

            // compare collections by values
            if (value1 is IEnumerable enumerable && value2 is IEnumerable enumerable2)
            {
                var arr1 = enumerable.Cast<object>().ToArray();
                var arr2 = enumerable2.Cast<object>().ToArray();

                return ((IStructuralEquatable)arr1).Equals(arr2, StructuralComparisons.StructuralEqualityComparer);
            }

            return false;
        }

        private IEnumerable<FieldInfo> GetInstanceFields(Type type) => type?.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
            .Where(f => f.Name != $"<{nameof(Predicate)}>k__BackingField").Concat(GetInstanceFields(type.BaseType)) ?? Enumerable.Empty<FieldInfo>();

        public override int GetHashCode()
        {
            var hashes = GetInstanceFields(GetType()).Select(f => f.GetValue(this)?.GetHashCode() ?? 0).ToList();
            hashes.Add(this.GetType().GetHashCode());
            return CombineHashCodes(hashes);
        }

        public bool Equals(Specification<T> other)
        {
            return Equals((object)other);
        }

        public static bool operator ==(Specification<T> left, Specification<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Specification<T> left, Specification<T> right)
        {
            return !Equals(left, right);
        }

        public static int CombineHashCodes(IEnumerable<int> hashCodes)
        {
            // This algorithm is stolen (and modified for our needs) from StackOverflows best answer http://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode/263416#263416

            unchecked // Overflow is fine, just wrap
            {
                int hashResult = 17;

                foreach (var hash in hashCodes)
                {
                    hashResult = (hashResult * 486187739) + hash; // It's better to pick a large prime to multiply, they say (apparently 486187739 is good?)
                }
                return hashResult;
            }
        }

        public static MatchAllSpecification<T> MatchAll => new MatchAllSpecification<T>();
    }

    /// <summary>
    /// Do not use this interface directly for implementing specifications, use abstract Specification<T> class for that.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISpecification<T>
{
    IQueryable<T> SatisfyEntitiesFrom(IQueryable<T> query);
    IEnumerable<T> SatisfyEntitiesFrom(IEnumerable<T> collection);
    bool IsSatisfiedBy(T entity);
}
}
