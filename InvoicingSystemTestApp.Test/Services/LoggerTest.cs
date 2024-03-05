using Moq;
using NUnit.Framework.Internal;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoicingSystemTestApp.Services;

namespace InvoicingSystemTestApp.Test.Services
{
    [TestFixture]
    public class LoggerTests
    {
        [Test]
        public void Log_PrefixesInputStringWithDateTime()
        {
            // Arrange
            var expectedDateTime = new DateTime(2020, 1, 1, 12, 34, 56);
            var mockTimeProvider = new Mock<ITimeProvider>();
            mockTimeProvider.Setup(m => m.Now).Returns(expectedDateTime);

            var mockFileWriter = new Mock<IFileWriter>();
            var logger = new InvoicingSystemTestApp.Services.Logger(mockFileWriter.Object, mockTimeProvider.Object);

            var testMessage = "Test message";
            var expectedMessage = $"[{expectedDateTime:dd.MM.yy HH:mm:ss}] {testMessage}";

            // Act
            logger.Log(testMessage);

            // Assert
            mockFileWriter.Verify(fw => fw.WriteLine(It.Is<string>(s => s == expectedMessage)), Times.Once());
        }
    }

}
