using System;
using System.Collections;
using NUnit.Framework.Constraints;

namespace Serilog.Tests.Support
{
    /// <summary>
    /// Helper class with properties and methods that supply
    ///             a number of constraints used in Asserts.
    /// </summary>
    public static class ContainsEx
    {
        public static DictionaryContainsKeyConstraint Key(object key )
        {
            return new DictionaryContainsKeyConstraint(key);
        }
    }

    /// <remarks>Backport from NUnit 3.0</remarks>
    public class DictionaryContainsKeyConstraint : CollectionContainsConstraint
    {
        object expected;

        /// <summary>
        ///     Construct a DictionaryContainsKeyConstraint
        /// </summary>
        /// <param name="expected"></param>
        public DictionaryContainsKeyConstraint(object expected)
            : base(expected)
        {
            this.expected = expected;
            DisplayName = "ContainsKey";
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate("dictionary containing key ");
            writer.WriteExpectedValue(expected);
        }

        /// <summary>
        ///     Test whether the expected key is contained in the dictionary
        /// </summary>
        // ReSharper disable once ParameterHidesMember
        protected override bool doMatch(IEnumerable actual)
        {
            IDictionary dictionary = actual as IDictionary;

            if (dictionary == null)
                throw new ArgumentException("The actual value must be an IDictionary", "actual");

            return base.doMatch(dictionary.Keys);
        }
    }
}