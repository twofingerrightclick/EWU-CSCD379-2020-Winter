using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BlogEngine.Data.Tests
{
    [TestClass]
    public class ReadFileTests
    {
        [TestMethod]
        public void GetValue_WithFile_ReturnsInt()
        {
            // Arrange
            var readFile = new InMemoryProvideConfiguration("42");
            var config = new Configuration(readFile);

            // Act
            int result = config.GetValue();

            // Assert
            Assert.AreEqual(42, result);
        }
    }

    public interface IProvideConfiguration
    {
        string GetContents();
    }

    public class Configuration
    {
        private IProvideConfiguration ReadFile { get; }

        public Configuration(IProvideConfiguration readFile)
        {
            ReadFile = readFile ?? throw new ArgumentNullException(nameof(readFile));
        }

        public int GetValue()
        {
            string fileContents = ReadFile.GetContents();
            if (int.TryParse(fileContents, out int result))
            {
                return result;
            }
            return -1;
        }
    }

    internal class InMemoryProvideConfiguration : IProvideConfiguration
    {
        public InMemoryProvideConfiguration(string data)
        {
            Data = data;
        }

        public string Data { get; }

        public string GetContents(string _)
            => Data;

        public string GetContents()
            => Data;
    }
}
