
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xunit;

namespace WebAppSample.Acceptance.Tests
{
    /// <summary>
    /// Base class with common generic Then functionality
    /// </summary>
    public abstract class ThenBase
    {
        private readonly List<string> _failures = new List<string>();

        public void PerformAssertions()
        {
            if (_failures.Count == 0) return;

            Assert.True(false, string.Join('\n', _failures));
        }

        protected void BuildErrorMessage(string methodName, string errorMessage)
        {
            _failures.Add($"{methodName} - {errorMessage}");
        }

        protected void CheckCondition(bool logFailure, string message, [CallerMemberName]string methodName = null)
        {
            if (logFailure)
                BuildErrorMessage(methodName, message);
        }

        protected void CheckCondition(Action conditionCheckWhichThrowsExceptionOnFail, string message, [CallerMemberName]string methodName = null)
        {
            try
            {
                conditionCheckWhichThrowsExceptionOnFail();
            }
            catch (Exception)
            {
                BuildErrorMessage(methodName, message);
            }
        }
    }
}
