using AutoMapper;

using FluentValidation;
using FluentValidation.Results;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Moq;

using NUnit.Framework;

using Softtek.Cashflow.Api.Controllers;
using Softtek.Cashflow.Application.Entry.Command;
using Softtek.Cashflow.Application.Entry.Query;
using Softtek.Cashflow.Domain.Common.ViewModel;
using Softtek.Cashflow.Domain.Entities.Transactions.Model;
using Softtek.Cashflow.Domain.ViewModel.Cashflow;

namespace Softtek.Cashflow.Tests.Controllers
{
    [TestFixture]
    public class CashflowControllerTest
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<IValidator<CashflowCommand>> _validatorMock;
        private Mock<ILogger<CashflowController>> _loggerMock;
        private Mock<IMapper> _mapperMock;
        private CashflowController _controller;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _validatorMock = new Mock<IValidator<CashflowCommand>>();
            _loggerMock = new Mock<ILogger<CashflowController>>();
            _mapperMock = new Mock<IMapper>();
            _controller = new CashflowController(_loggerMock.Object, _mediatorMock.Object, _validatorMock.Object, _mapperMock.Object);
        }

        [Test]
        public void Should_entring_with_success()
        {
            #region Entring Success

            // arrange
            _loggerMock.Setup(x => x.Log(
                 It.IsAny<LogLevel>(),
                 It.IsAny<EventId>(),
                 It.IsAny<It.IsAnyType>(),
                 It.IsAny<Exception>(),
                 (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));

            _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<CashflowCommand>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(new ValidationResult()));

            _mediatorMock.Setup(x => x.Send(It.IsAny<CashflowCommand>(), It.IsAny<CancellationToken>()));

            // act
            var result = _controller.EntryAsync(It.IsAny<CashflowCommand>()).GetAwaiter().GetResult();

            var actionResult = (StatusCodeResult)result;

            // assert
            _loggerMock.Verify(x => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));

            _validatorMock.Verify(x => x.ValidateAsync(It.IsAny<CashflowCommand>(), It.IsAny<CancellationToken>()), Times.Once);

            _mediatorMock.Verify(x => x.Send(It.IsAny<CashflowCommand>(), It.IsAny<CancellationToken>()), Times.Once);

            Assert.That(actionResult.StatusCode, Is.EqualTo(200));

            #endregion
        }

        [Test]
        public void Should_entring_with_validdation_error()
        {
            #region Entring Validation Errors

            var exception = new ValidationException("", new List<ValidationFailure> {
                new ValidationFailure(){ ErrorMessage = "Campo Valor deve conter um valor monetário. ex: 10.00" },
                new ValidationFailure(){ ErrorMessage = "Campo Valor deve ser diferente de 0." },
            });

            // arrange
            _loggerMock.Setup(x => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));

            _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<CashflowCommand>(), It.IsAny<CancellationToken>()))
                .Throws(() => exception);

            // act
            var result = _controller.EntryAsync(It.IsAny<CashflowCommand>()).GetAwaiter().GetResult();

            var actionResult = (ObjectResult)result;

            // assert
            _loggerMock.Verify(x => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));

            _validatorMock.Verify(x => x.ValidateAsync(It.IsAny<CashflowCommand>(), It.IsAny<CancellationToken>()), Times.Once);

            Assert.That(actionResult.StatusCode, Is.EqualTo(400));

            var viewModelErrors = actionResult.Value as ValidateErrorViewModel;

            Assert.That(exception.Errors.ToArray()[0].ErrorMessage, Is.EqualTo(viewModelErrors?.Errors[0]));
            Assert.That(exception.Errors.ToArray()[1].ErrorMessage, Is.EqualTo(viewModelErrors?.Errors[1]));
            Assert.That(actionResult.StatusCode, Is.EqualTo(400));

            #endregion
        }

        [Test]
        public void Should_entring_with_unknown_error()
        {
            #region Entring Unknown Error

            // arrange
            _loggerMock.Setup(x => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));

            _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<CashflowCommand>(), It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            // act
            var result = _controller.EntryAsync(It.IsAny<CashflowCommand>()).GetAwaiter().GetResult();

            var actionResult = (ObjectResult)result;

            // assert
            _loggerMock.Verify(x => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));

            _validatorMock.Verify(x => x.ValidateAsync(It.IsAny<CashflowCommand>(), It.IsAny<CancellationToken>()), Times.Once);

            var viewModelError = actionResult.Value as UnknownErrorViewModel;

            Assert.That(viewModelError.Error, Is.EqualTo("Erro desconhecido"));
            Assert.That(actionResult.StatusCode, Is.EqualTo(500));

            #endregion
        }

        [Test]
        public void Should_get_consolidated_daily_balance_with_success()
        {
            #region Report Success

            var consolidatedDailyBalance = new List<ConsolidatedBalanceViewModel>
            {
                new ConsolidatedBalanceViewModel
                {
                    Date = DateTime.Now.Date,
                    Balance = 10.00m
                },
                new ConsolidatedBalanceViewModel
                {
                    Date = DateTime.Now.AddDays(10).Date,
                    Balance = -50.00m
                }
            };

            // arrange
            _loggerMock.Setup(x => x.Log(
                 It.IsAny<LogLevel>(),
                 It.IsAny<EventId>(),
                 It.IsAny<It.IsAnyType>(),
                 It.IsAny<Exception>(),
                 (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));

            _mediatorMock.Setup(x => x.Send(It.IsAny<ConsolidatedBalanceQuery>(), It.IsAny<CancellationToken>()));

            _mapperMock.Setup(x => x.Map<IList<ConsolidatedBalanceViewModel>>(It.IsAny<IList<CashflowModel>>())).Returns(() => consolidatedDailyBalance);

            // act
            var result = _controller.GetConsolidatedDailyBallance().GetAwaiter().GetResult();

            var actionResult = (ObjectResult)result;

            var viewModelSuccess = actionResult.Value as IList<ConsolidatedBalanceViewModel>;

            // assert
            _loggerMock.Verify(x => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));

            _mediatorMock.Verify(x => x.Send(It.IsAny<ConsolidatedBalanceQuery>(), It.IsAny<CancellationToken>()), Times.Once);

            _mapperMock.Verify(x => x.Map<IList<ConsolidatedBalanceViewModel>>(It.IsAny<IList<CashflowModel>>()), Times.Once);

            Assert.That(actionResult.StatusCode, Is.EqualTo(200));
            Assert.That(viewModelSuccess[0].Balance, Is.EqualTo(consolidatedDailyBalance[0].Balance));
            Assert.That(viewModelSuccess[0].Date.Date, Is.EqualTo(consolidatedDailyBalance[0].Date.Date));
            Assert.That(viewModelSuccess[1].Balance, Is.EqualTo(consolidatedDailyBalance[1].Balance));
            Assert.That(viewModelSuccess[1].Date.Date, Is.EqualTo(consolidatedDailyBalance[1].Date.Date));

            #endregion
        }

        [Test]
        public void Should_get_consolidated_daily_balance_with_unknown_error()
        {
            #region Report Success

            // arrange
            _loggerMock.Setup(x => x.Log(
                 It.IsAny<LogLevel>(),
                 It.IsAny<EventId>(),
                 It.IsAny<It.IsAnyType>(),
                 It.IsAny<Exception>(),
                 (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));

            _mediatorMock.Setup(x => x.Send(It.IsAny<ConsolidatedBalanceQuery>(), It.IsAny<CancellationToken>()));

            _mapperMock.Setup(x => x.Map<IList<ConsolidatedBalanceViewModel>>(It.IsAny<IList<CashflowModel>>())).Throws<Exception>();

            // act
            var result = _controller.GetConsolidatedDailyBallance().GetAwaiter().GetResult();

            var actionResult = (ObjectResult)result;

            var viewModelSuccess = actionResult.Value as IList<ConsolidatedBalanceViewModel>;

            // assert
            _loggerMock.Verify(x => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));

            _mediatorMock.Verify(x => x.Send(It.IsAny<ConsolidatedBalanceQuery>(), It.IsAny<CancellationToken>()), Times.Once);

            _mapperMock.Verify(x => x.Map<IList<ConsolidatedBalanceViewModel>>(It.IsAny<IList<CashflowModel>>()), Times.Once);

            var viewModelError = actionResult.Value as UnknownErrorViewModel;

            Assert.That(viewModelError.Error, Is.EqualTo("Erro desconhecido"));
            Assert.That(actionResult.StatusCode, Is.EqualTo(500));

            #endregion
        }
    }
}